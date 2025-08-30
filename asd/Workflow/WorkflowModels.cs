using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PyClickerRecorder.Workflow
{
    public enum WorkflowBlockType
    {
        MacroBlock,
        ConditionalBlock,
        LoopBlock,
        DelayBlock,
        VariableBlock
    }

    public enum ConditionType
    {
        // Essential operators only
        Equals,
        NotEquals,
        Contains,
        NotContains,
        GreaterThan,
        LessThan,
        IsEmpty,
        IsNotEmpty,
        Always,
        Never
    }

    public enum VariableSource
    {
        Clipboard,
        WindowTitle,
        Variable,
        Value
    }

    public enum LoopType
    {
        Count,
        Duration,
        UntilCondition
    }

    [JsonDerivedType(typeof(MacroBlock), typeDiscriminator: "macro")]
    [JsonDerivedType(typeof(ConditionalBlock), typeDiscriminator: "conditional")]
    [JsonDerivedType(typeof(LoopBlock), typeDiscriminator: "loop")]
    [JsonDerivedType(typeof(DelayBlock), typeDiscriminator: "delay")]
    [JsonDerivedType(typeof(VariableBlock), typeDiscriminator: "variable")]
    public abstract class WorkflowBlock
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = "";
        public WorkflowBlockType Type { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public List<string> NextBlocks { get; set; } = new List<string>();
        public bool IsEnabled { get; set; } = true;

        protected WorkflowBlock(WorkflowBlockType type)
        {
            Type = type;
        }
    }

    public class MacroBlock : WorkflowBlock
    {
        public string MacroName { get; set; } = "";
        public List<RecordedAction> Actions { get; set; } = new List<RecordedAction>();
        public int LoopCount { get; set; } = 1;
        public bool InfiniteLoop { get; set; } = false;
        public double SpeedFactor { get; set; } = 1.0;

        public MacroBlock() : base(WorkflowBlockType.MacroBlock) { }
    }

    public class ConditionalBlock : WorkflowBlock
    {
        public VariableSource LeftSource { get; set; } = VariableSource.Variable;
        public string LeftValue { get; set; } = "";
        public ConditionType ConditionType { get; set; } = ConditionType.Equals;
        public VariableSource RightSource { get; set; } = VariableSource.Value;
        public string RightValue { get; set; } = "";
        public bool CaseSensitive { get; set; } = false;
        public List<string> TrueBlocks { get; set; } = new List<string>();
        public List<string> FalseBlocks { get; set; } = new List<string>();
        
        // Direct storage for static values when using Value source
        public string? LeftStaticValue { get; set; } = null; 
        public string? RightStaticValue { get; set; } = null;
        
        // Legacy properties for backward compatibility
        public string? ConditionValue { get; set; } = "";
        public string TrueBlock { get; set; } = "";
        public string FalseBlock { get; set; } = "";

        public ConditionalBlock() : base(WorkflowBlockType.ConditionalBlock) { }
    }

    public class LoopBlock : WorkflowBlock
    {
        public LoopType LoopType { get; set; }
        public int Count { get; set; } = 1;
        public TimeSpan Duration { get; set; }
        public ConditionType ExitCondition { get; set; }
        public string ExitConditionValue { get; set; } = "";
        public List<string> LoopBlocks { get; set; } = new List<string>();
        
        // Legacy property for backward compatibility
        public string LoopBody { get; set; } = "";

        public LoopBlock() : base(WorkflowBlockType.LoopBlock) { }
    }

    public class DelayBlock : WorkflowBlock
    {
        public double DelaySeconds { get; set; } = 1.0;
        public bool RandomDelay { get; set; } = false;
        public double MinDelaySeconds { get; set; } = 0.5;
        public double MaxDelaySeconds { get; set; } = 2.0;

        public DelayBlock() : base(WorkflowBlockType.DelayBlock) { }
    }

    public class VariableBlock : WorkflowBlock
    {
        public string VariableName { get; set; } = "";
        public VariableSource Source { get; set; } = VariableSource.Clipboard;
        public string SourceValue { get; set; } = "";
        public string DirectValue { get; set; } = ""; // Direct value storage for Value source

        public VariableBlock() : base(WorkflowBlockType.VariableBlock) { }
    }

    public class WorkflowVariable
    {
        public string Name { get; set; } = "";
        public string Value { get; set; } = "";
        public DateTime LastUpdated { get; set; } = DateTime.Now;
    }

    public class Workflow
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = "New Workflow";
        public string Description { get; set; } = "";
        public List<WorkflowBlock> Blocks { get; set; } = new List<WorkflowBlock>();
        public string StartBlockId { get; set; } = "";
        public bool IsEnabled { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
        public Dictionary<string, WorkflowVariable> Variables { get; set; } = new Dictionary<string, WorkflowVariable>();
        public MacroScheduleMode ScheduleMode { get; set; }
        public DateTime ScheduledTime { get; set; }
        public DateTime ScheduledEndTime { get; set; }
        public bool RepeatDaily { get; set; } = false;
    }

    public class WorkflowExecutionContext
    {
        public Workflow Workflow { get; set; }
        public Dictionary<string, object> Variables { get; set; } = new Dictionary<string, object>();
        public WorkflowBlock? CurrentBlock { get; set; }
        public bool IsRunning { get; set; } = false;
        public DateTime StartTime { get; set; }
        public Exception? LastError { get; set; }
        public int ExecutionCount { get; set; } = 0;

        public WorkflowExecutionContext(Workflow workflow)
        {
            Workflow = workflow;
        }
    }
}