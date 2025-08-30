using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PyClickerRecorder.Workflow
{
    public class WorkflowEngine
    {
        private readonly Player _player;
        private readonly Dictionary<string, CancellationTokenSource> _runningWorkflows;
        private readonly Dictionary<string, WorkflowExecutionContext> _executionContexts;
        private readonly Random _random;

        public event EventHandler<WorkflowStatusEventArgs> WorkflowStatusChanged;

        public WorkflowEngine(Player player)
        {
            _player = player;
            _runningWorkflows = new Dictionary<string, CancellationTokenSource>();
            _executionContexts = new Dictionary<string, WorkflowExecutionContext>();
            _random = new Random();
        }

        public async Task ExecuteWorkflowAsync(Workflow workflow, CancellationToken cancellationToken = default)
        {
            if (_runningWorkflows.ContainsKey(workflow.Id))
            {
                throw new InvalidOperationException($"Workflow '{workflow.Name}' is already running.");
            }

            var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            _runningWorkflows[workflow.Id] = cts;

            var context = new WorkflowExecutionContext(workflow)
            {
                StartTime = DateTime.Now,
                IsRunning = true
            };
            
            // Initialize workflow variables
            if (context.Workflow.Variables != null)
            {
                foreach (var workflowVar in context.Workflow.Variables)
                {
                    context.Variables[workflowVar.Key] = workflowVar.Value.Value;
                }
            }
            
            _executionContexts[workflow.Id] = context;

            OnWorkflowStatusChanged(new WorkflowStatusEventArgs(workflow.Id, WorkflowStatus.Started, null));

            try
            {
                // Filter out blocks that are contained in loops or conditionals for main flow execution
                var mainFlowBlocks = workflow.Blocks
                    .Where(block => !block.NextBlocks.Any(nb => 
                        nb == "__LOOP_CONTAINED__" || 
                        nb == "__CONDITIONAL_TRUE_CONTAINED__" || 
                        nb == "__CONDITIONAL_FALSE_CONTAINED__"))
                    .ToList();

                // Check if main flow blocks have proper sequential connections
                bool hasSequentialConnections = mainFlowBlocks.Count > 1 && 
                    mainFlowBlocks.Take(mainFlowBlocks.Count - 1)
                        .All(block => block.NextBlocks.Any(nb => 
                            mainFlowBlocks.Any(b => b.Id == nb)));

                if (!hasSequentialConnections && mainFlowBlocks.Count > 0)
                {
                    // Set up sequential connections for main flow blocks
                    SetupSequentialConnections(mainFlowBlocks);
                }

                // Execute starting from the first main flow block or specified start block
                var startBlock = workflow.Blocks.FirstOrDefault(b => b.Id == workflow.StartBlockId);
                if (startBlock == null && mainFlowBlocks.Count > 0)
                {
                    startBlock = mainFlowBlocks.First();
                }

                if (startBlock != null)
                {
                    await ExecuteBlockAsync(context, startBlock, cts.Token);
                }

                OnWorkflowStatusChanged(new WorkflowStatusEventArgs(workflow.Id, WorkflowStatus.Completed, null));
            }
            catch (OperationCanceledException)
            {
                OnWorkflowStatusChanged(new WorkflowStatusEventArgs(workflow.Id, WorkflowStatus.Cancelled, null));
            }
            catch (Exception ex)
            {
                context.LastError = ex;
                OnWorkflowStatusChanged(new WorkflowStatusEventArgs(workflow.Id, WorkflowStatus.Failed, ex));
            }
            finally
            {
                context.IsRunning = false;
                _runningWorkflows.Remove(workflow.Id);
                _executionContexts.Remove(workflow.Id);
            }
        }

        private async Task ExecuteBlockAsync(WorkflowExecutionContext context, WorkflowBlock block, CancellationToken cancellationToken)
        {
            if (!block.IsEnabled)
            {
                await ExecuteNextBlocks(context, block, cancellationToken);
                return;
            }

            context.CurrentBlock = block;
            context.ExecutionCount++;

            switch (block)
            {
                case MacroBlock macroBlock:
                    await ExecuteMacroBlock(context, macroBlock, cancellationToken);
                    break;
                case VariableBlock variableBlock:
                    await ExecuteVariableBlock(context, variableBlock, cancellationToken);
                    break;
                case ConditionalBlock conditionalBlock:
                    await ExecuteConditionalBlock(context, conditionalBlock, cancellationToken);
                    break;
                case LoopBlock loopBlock:
                    await ExecuteLoopBlock(context, loopBlock, cancellationToken);
                    break;
                case DelayBlock delayBlock:
                    await ExecuteDelayBlock(context, delayBlock, cancellationToken);
                    break;
            }
        }

        private async Task ExecuteMacroBlock(WorkflowExecutionContext context, MacroBlock macroBlock, CancellationToken cancellationToken)
        {
            if (macroBlock.Actions?.Any() == true)
            {
                int loopCount = macroBlock.InfiniteLoop ? 0 : macroBlock.LoopCount;
                await _player.Play(macroBlock.Actions, loopCount, macroBlock.SpeedFactor, cancellationToken);
            }

            await ExecuteNextBlocks(context, macroBlock, cancellationToken);
        }

        private async Task ExecuteVariableBlock(WorkflowExecutionContext context, VariableBlock variableBlock, CancellationToken cancellationToken)
        {
            System.Diagnostics.Debug.WriteLine($"=== EXECUTING VARIABLE BLOCK ===");
            System.Diagnostics.Debug.WriteLine($"Block ID: {variableBlock.Id}");
            System.Diagnostics.Debug.WriteLine($"Variable Name: '{variableBlock.VariableName}'");
            System.Diagnostics.Debug.WriteLine($"Source: {variableBlock.Source}");
            System.Diagnostics.Debug.WriteLine($"SourceValue: '{variableBlock.SourceValue}'");
            
            if (string.IsNullOrEmpty(variableBlock.VariableName))
            {
                System.Diagnostics.Debug.WriteLine("Variable block: Empty variable name, skipping");
                await ExecuteNextBlocks(context, variableBlock, cancellationToken);
                return;
            }

            string value = "";
            
            try
            {
                System.Diagnostics.Debug.WriteLine($"Processing source type: {variableBlock.Source}");
                
                switch (variableBlock.Source)
                {
                    case VariableSource.Clipboard:
                        System.Diagnostics.Debug.WriteLine("Getting clipboard text...");
                        value = await GetClipboardTextAsync();
                        System.Diagnostics.Debug.WriteLine($"Clipboard value retrieved: '{value}'");
                        break;
                    case VariableSource.WindowTitle:
                        System.Diagnostics.Debug.WriteLine("Getting window title...");
                        value = GetActiveWindowTitle() ?? "";
                        System.Diagnostics.Debug.WriteLine($"Window title retrieved: '{value}'");
                        break;
                    case VariableSource.Variable:
                        System.Diagnostics.Debug.WriteLine($"Looking up variable: '{variableBlock.SourceValue}'");
                        if (context.Variables.TryGetValue(variableBlock.SourceValue ?? "", out var existingValue))
                        {
                            value = existingValue?.ToString() ?? "";
                            System.Diagnostics.Debug.WriteLine($"Found existing variable value: '{value}'");
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine($"Variable block: Source variable '{variableBlock.SourceValue}' not found");
                            value = "";
                        }
                        break;
                    case VariableSource.Value:
                        System.Diagnostics.Debug.WriteLine($"Processing direct value: '{variableBlock.DirectValue}'");
                        value = variableBlock.DirectValue ?? "";
                        System.Diagnostics.Debug.WriteLine($"Variable block: Using direct value '{value}' exactly as stored");
                        break;
                }

                // Additional debugging for clipboard changes
                if (variableBlock.Source == VariableSource.Clipboard)
                {
                    var currentClipboard = await GetClipboardTextAsync();
                    System.Diagnostics.Debug.WriteLine($"Current clipboard after variable processing: '{currentClipboard}'");
                }

                // Store the value in the execution context
                System.Diagnostics.Debug.WriteLine($"Before assignment - Variables count: {context.Variables.Count}");
                context.Variables[variableBlock.VariableName] = value;
                System.Diagnostics.Debug.WriteLine($"After assignment - Variable '{variableBlock.VariableName}' = '{value}' (from {variableBlock.Source})");
                System.Diagnostics.Debug.WriteLine($"After assignment - Variables count: {context.Variables.Count}");
                
                // Verify the assignment worked
                if (context.Variables.TryGetValue(variableBlock.VariableName, out var storedValue))
                {
                    System.Diagnostics.Debug.WriteLine($"VERIFICATION: Variable '{variableBlock.VariableName}' stored as: '{storedValue}'");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"ERROR: Variable '{variableBlock.VariableName}' not found after assignment!");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Variable block error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                context.Variables[variableBlock.VariableName] = "";
            }

            System.Diagnostics.Debug.WriteLine($"=== VARIABLE BLOCK COMPLETE ===");
            await ExecuteNextBlocks(context, variableBlock, cancellationToken);
        }

        private async Task ExecuteConditionalBlock(WorkflowExecutionContext context, ConditionalBlock conditionalBlock, CancellationToken cancellationToken)
        {
            bool conditionResult = await EvaluateCondition(context, conditionalBlock);

            // Execute blocks based on condition result
            List<string> targetBlockIds = new List<string>();
            
            if (conditionResult)
            {
                // True branch
                targetBlockIds.AddRange(conditionalBlock.TrueBlocks);
                if (!string.IsNullOrEmpty(conditionalBlock.TrueBlock) && !targetBlockIds.Contains(conditionalBlock.TrueBlock))
                {
                    targetBlockIds.Add(conditionalBlock.TrueBlock);
                }
            }
            else
            {
                // False branch
                targetBlockIds.AddRange(conditionalBlock.FalseBlocks);
                if (!string.IsNullOrEmpty(conditionalBlock.FalseBlock) && !targetBlockIds.Contains(conditionalBlock.FalseBlock))
                {
                    targetBlockIds.Add(conditionalBlock.FalseBlock);
                }
            }

            // Execute each block in the selected branch sequentially
            foreach (var blockId in targetBlockIds)
            {
                var nextBlock = context.Workflow.Blocks.FirstOrDefault(b => b.Id == blockId);
                if (nextBlock != null && nextBlock.IsEnabled)
                {
                    await ExecuteBlockAsync(context, nextBlock, cancellationToken);
                }
            }

            // Continue to next blocks after conditional (main flow continuation)
            await ExecuteNextBlocks(context, conditionalBlock, cancellationToken);
        }

        private async Task ExecuteLoopBlock(WorkflowExecutionContext context, LoopBlock loopBlock, CancellationToken cancellationToken)
        {
            // Get blocks to execute in the loop (either new LoopBlocks list or legacy LoopBody)
            var loopBlockIds = new List<string>();
            
            if (loopBlock.LoopBlocks.Count > 0)
            {
                loopBlockIds.AddRange(loopBlock.LoopBlocks);
            }
            else if (!string.IsNullOrEmpty(loopBlock.LoopBody))
            {
                loopBlockIds.Add(loopBlock.LoopBody);
            }

            if (loopBlockIds.Count == 0)
            {
                await ExecuteNextBlocks(context, loopBlock, cancellationToken);
                return;
            }

            switch (loopBlock.LoopType)
            {
                case LoopType.Count:
                    for (int i = 0; i < loopBlock.Count && !cancellationToken.IsCancellationRequested; i++)
                    {
                        await ExecuteLoopBlocks(context, loopBlockIds, cancellationToken);
                    }
                    break;
                case LoopType.Duration:
                    var endTime = DateTime.Now.Add(loopBlock.Duration);
                    while (DateTime.Now < endTime && !cancellationToken.IsCancellationRequested)
                    {
                        await ExecuteLoopBlocks(context, loopBlockIds, cancellationToken);
                    }
                    break;
                case LoopType.UntilCondition:
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        var conditionBlock = new ConditionalBlock
                        {
                            ConditionType = loopBlock.ExitCondition,
                            ConditionValue = loopBlock.ExitConditionValue
                        };
                        
                        if (await EvaluateCondition(context, conditionBlock))
                            break;
                            
                        await ExecuteLoopBlocks(context, loopBlockIds, cancellationToken);
                    }
                    break;
            }

            await ExecuteNextBlocks(context, loopBlock, cancellationToken);
        }

        private async Task ExecuteLoopBlocks(WorkflowExecutionContext context, List<string> blockIds, CancellationToken cancellationToken)
        {
            foreach (var blockId in blockIds)
            {
                var block = context.Workflow.Blocks.FirstOrDefault(b => b.Id == blockId);
                if (block != null && block.IsEnabled)
                {
                    await ExecuteBlockAsync(context, block, cancellationToken);
                }
            }
        }

        private async Task ExecuteDelayBlock(WorkflowExecutionContext context, DelayBlock delayBlock, CancellationToken cancellationToken)
        {
            double delaySeconds = delayBlock.DelaySeconds;
            
            if (delayBlock.RandomDelay)
            {
                delaySeconds = _random.NextDouble() * (delayBlock.MaxDelaySeconds - delayBlock.MinDelaySeconds) + delayBlock.MinDelaySeconds;
            }

            await Task.Delay(TimeSpan.FromSeconds(delaySeconds), cancellationToken);
            await ExecuteNextBlocks(context, delayBlock, cancellationToken);
        }

        private async Task ExecuteNextBlocks(WorkflowExecutionContext context, WorkflowBlock currentBlock, CancellationToken cancellationToken)
        {
            foreach (var nextBlockId in currentBlock.NextBlocks)
            {
                var nextBlock = context.Workflow.Blocks.FirstOrDefault(b => b.Id == nextBlockId);
                if (nextBlock != null)
                {
                    await ExecuteBlockAsync(context, nextBlock, cancellationToken);
                }
            }
        }

        private async Task<bool> EvaluateCondition(WorkflowExecutionContext context, ConditionalBlock conditionalBlock)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"=== EVALUATING CONDITION ===");
                System.Diagnostics.Debug.WriteLine($"Left Source: {conditionalBlock.LeftSource}, Left Value: '{conditionalBlock.LeftValue}'");
                System.Diagnostics.Debug.WriteLine($"Right Source: {conditionalBlock.RightSource}, Right Value: '{conditionalBlock.RightValue}'");
                System.Diagnostics.Debug.WriteLine($"Condition Type: {conditionalBlock.ConditionType}");
                System.Diagnostics.Debug.WriteLine($"Variables in context: {context.Variables.Count}");
                foreach (var kvp in context.Variables)
                {
                    System.Diagnostics.Debug.WriteLine($"  {kvp.Key} = '{kvp.Value}'");
                }
                
                // Get left value using unified method
                string leftValue = await GetConditionalValue(context, conditionalBlock.LeftSource, conditionalBlock.LeftValue, conditionalBlock.LeftStaticValue);
                System.Diagnostics.Debug.WriteLine($"Left value resolved: '{leftValue}' from {conditionalBlock.LeftSource}");
                
                // Get right value using unified method
                string rightValue = await GetConditionalValue(context, conditionalBlock.RightSource, conditionalBlock.RightValue, conditionalBlock.RightStaticValue);
                System.Diagnostics.Debug.WriteLine($"Right value resolved: '{rightValue}' from {conditionalBlock.RightSource}");

                // Handle special cases
                if (conditionalBlock.ConditionType == ConditionType.Always) 
                {
                    System.Diagnostics.Debug.WriteLine("Condition type is Always, returning true");
                    return true;
                }
                if (conditionalBlock.ConditionType == ConditionType.Never) 
                {
                    System.Diagnostics.Debug.WriteLine("Condition type is Never, returning false");
                    return false;
                }

                // Debug info
                System.Diagnostics.Debug.WriteLine($"Conditional evaluation: '{leftValue}' {conditionalBlock.ConditionType} '{rightValue}' (CaseSensitive: {conditionalBlock.CaseSensitive})");

                // Apply case sensitivity
                string leftCompare = leftValue;
                string rightCompare = rightValue;
                if (!conditionalBlock.CaseSensitive)
                {
                    leftCompare = leftValue.ToLowerInvariant();
                    rightCompare = rightValue.ToLowerInvariant();
                }
                
                // Extra debugging for equals comparison
                if (conditionalBlock.ConditionType == ConditionType.Equals)
                {
                    System.Diagnostics.Debug.WriteLine($"Comparing for equality:");
                    System.Diagnostics.Debug.WriteLine($"Left:  '{leftCompare}' (bytes: {string.Join(",", System.Text.Encoding.UTF8.GetBytes(leftCompare))})");
                    System.Diagnostics.Debug.WriteLine($"Right: '{rightCompare}' (bytes: {string.Join(",", System.Text.Encoding.UTF8.GetBytes(rightCompare))})");
                    System.Diagnostics.Debug.WriteLine($"String.Equals result: {leftCompare.Equals(rightCompare)}");
                }

                // Evaluate condition - simplified and consistent
                bool result = conditionalBlock.ConditionType switch
                {
                    ConditionType.Equals => leftCompare.Equals(rightCompare),
                    ConditionType.NotEquals => !leftCompare.Equals(rightCompare),
                    ConditionType.Contains => leftCompare.Contains(rightCompare),
                    ConditionType.NotContains => !leftCompare.Contains(rightCompare),
                    ConditionType.GreaterThan => CompareNumeric(leftValue, rightValue) > 0,
                    ConditionType.LessThan => CompareNumeric(leftValue, rightValue) < 0,
                    ConditionType.IsEmpty => string.IsNullOrEmpty(leftValue),
                    ConditionType.IsNotEmpty => !string.IsNullOrEmpty(leftValue),
                    _ => false
                };

                System.Diagnostics.Debug.WriteLine($"Conditional result: {result}");
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Conditional evaluation error: {ex.Message}");
                return false;
            }
        }

        // Helper method to check if a value is actually a placeholder from the UI
        private bool IsUIPlaceholder(string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;
                
            // Only these exact strings are considered placeholders (legacy support)
            return value == "Enter static value" ||
                   value == "Enter prompt text";
        }
        
        // Unified method to get conditional values consistently
        private async Task<string> GetConditionalValue(WorkflowExecutionContext context, VariableSource source, string value, string staticValue)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"GetConditionalValue: source={source}, value='{value}', staticValue='{staticValue}'");
                
                switch (source)
                {
                    case VariableSource.Value:
                        // For Value source, use the static value directly
                        var result = staticValue ?? "";
                        System.Diagnostics.Debug.WriteLine($"Using static value: '{result}'");
                        return result;
                        
                    case VariableSource.Variable:
                        // For Variable source, look up in context
                        if (context.Variables.TryGetValue(value ?? "", out var varValue))
                        {
                            var variableResult = varValue?.ToString() ?? "";
                            System.Diagnostics.Debug.WriteLine($"Found variable '{value}': '{variableResult}'");
                            return variableResult;
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine($"Variable '{value}' not found, returning empty");
                            return "";
                        }
                        
                    case VariableSource.Clipboard:
                        var clipboardResult = await GetClipboardTextAsync() ?? "";
                        System.Diagnostics.Debug.WriteLine($"Clipboard content: '{clipboardResult}'");
                        return clipboardResult;
                        
                    case VariableSource.WindowTitle:
                        var windowResult = GetActiveWindowTitle() ?? "";
                        System.Diagnostics.Debug.WriteLine($"Window title: '{windowResult}'");
                        return windowResult;
                        
                    default:
                        System.Diagnostics.Debug.WriteLine($"Unknown source type: {source}");
                        return "";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetConditionalValue: {ex.Message}");
                return "";
            }
        }
        
        private async Task<string> GetVariableValue(WorkflowExecutionContext context, VariableSource source, string value)
        {
            try
            {
                string result = "";
                switch (source)
                {
                    case VariableSource.Clipboard:
                        result = await GetClipboardTextAsync() ?? "";
                        break;
                    case VariableSource.WindowTitle:
                        result = GetActiveWindowTitle() ?? "";
                        break;
                    case VariableSource.Variable:
                        if (context.Variables.TryGetValue(value ?? "", out var varValue))
                        {
                            result = varValue?.ToString() ?? "";
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine($"Variable '{value}' not found in context");
                            result = "";
                        }
                        break;
                    case VariableSource.Value:
                        // For Value source, use the value directly
                        result = value ?? "";
                        System.Diagnostics.Debug.WriteLine($"GetVariableValue: Using static value '{result}'");
                        break;
                    default:
                        result = "";
                        break;
                }
                
                System.Diagnostics.Debug.WriteLine($"GetVariableValue: {source}('{value}') = '{result}'");
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetVariableValue error: {ex.Message}");
                return "";
            }
        }

        private int CompareNumeric(string left, string right)
        {
            if (double.TryParse(left, out var leftNum) && double.TryParse(right, out var rightNum))
            {
                return leftNum.CompareTo(rightNum);
            }
            return string.Compare(left, right, StringComparison.Ordinal);
        }


        private async Task<string> GetClipboardTextAsync()
        {
            string clipboardText = "";
            await Task.Run(() =>
            {
                try
                {
                    if (Clipboard.ContainsText())
                    {
                        clipboardText = Clipboard.GetText();
                    }
                }
                catch
                {
                    // Clipboard access can fail, return empty string
                }
            });
            return clipboardText;
        }

        private string? GetActiveWindowTitle()
        {
            try
            {
                var activeWindow = NativeMethods.GetForegroundWindow();
                if (activeWindow != IntPtr.Zero)
                {
                    return NativeMethods.GetWindowText(activeWindow);
                }
            }
            catch
            {
                // Window title access can fail
            }
            return null;
        }

        public void StopWorkflow(string workflowId)
        {
            if (_runningWorkflows.TryGetValue(workflowId, out var cts))
            {
                cts.Cancel();
            }
        }

        public bool IsWorkflowRunning(string workflowId)
        {
            return _runningWorkflows.ContainsKey(workflowId);
        }

        public WorkflowExecutionContext GetExecutionContext(string workflowId)
        {
            _executionContexts.TryGetValue(workflowId, out var context);
            return context;
        }

        private void SetupSequentialConnections(List<WorkflowBlock> mainFlowBlocks)
        {
            // Clear existing connections between main flow blocks only
            foreach (var block in mainFlowBlocks)
            {
                block.NextBlocks.RemoveAll(nb => mainFlowBlocks.Any(b => b.Id == nb));
            }

            // Set up sequential connections
            for (int i = 0; i < mainFlowBlocks.Count - 1; i++)
            {
                var currentBlock = mainFlowBlocks[i];
                var nextBlock = mainFlowBlocks[i + 1];
                
                // Only add if not already present and not a special marker
                if (!currentBlock.NextBlocks.Contains(nextBlock.Id))
                {
                    currentBlock.NextBlocks.Add(nextBlock.Id);
                }
            }
        }


        protected virtual void OnWorkflowStatusChanged(WorkflowStatusEventArgs e)
        {
            WorkflowStatusChanged?.Invoke(this, e);
        }
    }

    public enum WorkflowStatus
    {
        Started,
        Completed,
        Failed,
        Cancelled
    }

    public class WorkflowStatusEventArgs : EventArgs
    {
        public string WorkflowId { get; }
        public WorkflowStatus Status { get; }
        public Exception Exception { get; }

        public WorkflowStatusEventArgs(string workflowId, WorkflowStatus status, Exception exception)
        {
            WorkflowId = workflowId;
            Status = status;
            Exception = exception;
        }
    }
}