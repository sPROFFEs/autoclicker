using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PyClickerRecorder.Workflow
{
    public partial class WorkflowDesigner : Form
    {
        private Workflow _currentWorkflow;
        private WorkflowBlock? _selectedBlock;
        private WorkflowBlock? _draggedBlock;
        private Point _dragOffset;
        private bool _isDragging;
        private bool _isConnecting;
        private WorkflowBlock? _connectionSource;
        private Point _mousePosition;
        private readonly List<SavedMacro> _availableMacros;

        // UI Constants
        private const int BlockWidth = 120;
        private const int BlockHeight = 60;
        private const int GridSize = 20;
        private const int CanvasMargin = 50;

        public Workflow CurrentWorkflow => _currentWorkflow;

        public WorkflowDesigner(List<SavedMacro> availableMacros)
        {
            InitializeComponent();
            _availableMacros = availableMacros ?? new List<SavedMacro>();
            _currentWorkflow = new Workflow();
            SetupDesigner();
        }

        public WorkflowDesigner(Workflow? workflow, List<SavedMacro> availableMacros)
        {
            InitializeComponent();
            _availableMacros = availableMacros ?? new List<SavedMacro>();
            _currentWorkflow = workflow ?? new Workflow();
            SetupDesigner();
        }

        private void SetupDesigner()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.BackColor = Color.WhiteSmoke;
            this.Size = new Size(1000, 700);
            this.Text = $"Workflow Designer - {_currentWorkflow.Name}";
            
            CreateToolPanel();
            CreatePropertiesPanel();
            SetupCanvasEvents();
        }

        private void CreateToolPanel()
        {
            var toolPanel = new Panel
            {
                Dock = DockStyle.Left,
                Width = 200,
                BackColor = Color.LightGray
            };

            var lblTools = new Label
            {
                Text = "Toolbox",
                Location = new Point(10, 10),
                Size = new Size(180, 20),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            toolPanel.Controls.Add(lblTools);

            // Add block type buttons
            var btnMacro = CreateToolButton("Macro Block", WorkflowBlockType.MacroBlock, 40);
            var btnCondition = CreateToolButton("Conditional", WorkflowBlockType.ConditionalBlock, 70);
            var btnLoop = CreateToolButton("Loop Block", WorkflowBlockType.LoopBlock, 100);
            var btnDelay = CreateToolButton("Delay Block", WorkflowBlockType.DelayBlock, 130);

            toolPanel.Controls.Add(btnMacro);
            toolPanel.Controls.Add(btnCondition);
            toolPanel.Controls.Add(btnLoop);
            toolPanel.Controls.Add(btnDelay);

            // Add workflow controls
            var btnSave = new Button
            {
                Text = "Save Workflow",
                Location = new Point(10, 180),
                Size = new Size(180, 30)
            };
            btnSave.Click += SaveWorkflow_Click;
            toolPanel.Controls.Add(btnSave);

            var btnLoad = new Button
            {
                Text = "Load Workflow",
                Location = new Point(10, 220),
                Size = new Size(180, 30)
            };
            btnLoad.Click += LoadWorkflow_Click;
            toolPanel.Controls.Add(btnLoad);

            var btnRun = new Button
            {
                Text = "Run Workflow",
                Location = new Point(10, 260),
                Size = new Size(180, 30),
                BackColor = Color.LightGreen
            };
            btnRun.Click += RunWorkflow_Click;
            toolPanel.Controls.Add(btnRun);

            this.Controls.Add(toolPanel);
        }

        private Button CreateToolButton(string text, WorkflowBlockType blockType, int yPosition)
        {
            var button = new Button
            {
                Text = text,
                Location = new Point(10, yPosition),
                Size = new Size(180, 25),
                Tag = blockType
            };
            button.Click += ToolButton_Click;
            return button;
        }

        private void CreatePropertiesPanel()
        {
            var propertiesPanel = new Panel
            {
                Dock = DockStyle.Right,
                Width = 250,
                BackColor = Color.LightGray
            };

            var lblProperties = new Label
            {
                Text = "Properties",
                Location = new Point(10, 10),
                Size = new Size(230, 20),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            propertiesPanel.Controls.Add(lblProperties);

            this.Controls.Add(propertiesPanel);
        }

        private void SetupCanvasEvents()
        {
            this.MouseDown += Canvas_MouseDown;
            this.MouseMove += Canvas_MouseMove;
            this.MouseUp += Canvas_MouseUp;
            this.Paint += Canvas_Paint;
            this.KeyDown += Canvas_KeyDown;
            this.KeyPreview = true;
        }

        private void ToolButton_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            var blockType = (WorkflowBlockType)button.Tag;
            
            // Create new block at a default position
            var newBlock = CreateBlock(blockType);
            newBlock.X = CanvasMargin + _currentWorkflow.Blocks.Count * (BlockWidth + 20);
            newBlock.Y = CanvasMargin;
            
            _currentWorkflow.Blocks.Add(newBlock);
            
            if (_currentWorkflow.Blocks.Count == 1)
            {
                _currentWorkflow.StartBlockId = newBlock.Id;
            }
            
            this.Invalidate();
        }

        private WorkflowBlock CreateBlock(WorkflowBlockType blockType)
        {
            return blockType switch
            {
                WorkflowBlockType.MacroBlock => new MacroBlock { Name = "New Macro" },
                WorkflowBlockType.ConditionalBlock => new ConditionalBlock { Name = "New Condition" },
                WorkflowBlockType.LoopBlock => new LoopBlock { Name = "New Loop" },
                WorkflowBlockType.DelayBlock => new DelayBlock { Name = "New Delay" },
                _ => throw new ArgumentException("Unknown block type")
            };
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            var clickedBlock = GetBlockAtPosition(e.Location);
            
            if (e.Button == MouseButtons.Left)
            {
                if (Control.ModifierKeys == Keys.Control && _selectedBlock != null)
                {
                    // Start connection mode
                    _isConnecting = true;
                    _connectionSource = _selectedBlock;
                }
                else if (clickedBlock != null)
                {
                    // Start dragging
                    _selectedBlock = clickedBlock;
                    _draggedBlock = clickedBlock;
                    _isDragging = true;
                    _dragOffset = new Point(e.X - clickedBlock.X, e.Y - clickedBlock.Y);
                }
                else
                {
                    _selectedBlock = null;
                }
            }
            else if (e.Button == MouseButtons.Right && clickedBlock != null)
            {
                ShowContextMenu(clickedBlock, e.Location);
            }
            
            this.Invalidate();
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            _mousePosition = e.Location;
            
            if (_isDragging && _draggedBlock != null)
            {
                _draggedBlock.X = SnapToGrid(e.X - _dragOffset.X);
                _draggedBlock.Y = SnapToGrid(e.Y - _dragOffset.Y);
                this.Invalidate();
            }
            else if (_isConnecting)
            {
                this.Invalidate(); // Redraw to show connection line
            }
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                _isDragging = false;
                _draggedBlock = null;
            }
            else if (_isConnecting)
            {
                var targetBlock = GetBlockAtPosition(e.Location);
                if (targetBlock != null && targetBlock != _connectionSource)
                {
                    // Create connection
                    if (!_connectionSource.NextBlocks.Contains(targetBlock.Id))
                    {
                        _connectionSource.NextBlocks.Add(targetBlock.Id);
                    }
                }
                _isConnecting = false;
                _connectionSource = null;
                this.Invalidate();
            }
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(this.BackColor);
            
            DrawGrid(g);
            DrawConnections(g);
            DrawBlocks(g);
            
            if (_isConnecting && _connectionSource != null)
            {
                DrawConnectionLine(g, GetBlockCenter(_connectionSource), _mousePosition, Color.Blue);
            }
        }

        private void DrawGrid(Graphics g)
        {
            using (var pen = new Pen(Color.LightBlue, 1))
            {
                for (int x = 0; x < this.Width; x += GridSize)
                {
                    g.DrawLine(pen, x, 0, x, this.Height);
                }
                for (int y = 0; y < this.Height; y += GridSize)
                {
                    g.DrawLine(pen, 0, y, this.Width, y);
                }
            }
        }

        private void DrawBlocks(Graphics g)
        {
            foreach (var block in _currentWorkflow.Blocks)
            {
                DrawBlock(g, block);
            }
        }

        private void DrawBlock(Graphics g, WorkflowBlock block)
        {
            var rect = new Rectangle(block.X, block.Y, BlockWidth, BlockHeight);
            var isSelected = block == _selectedBlock;
            var isStartBlock = block.Id == _currentWorkflow.StartBlockId;
            
            // Choose colors based on block type and state
            Color fillColor = GetBlockColor(block.Type);
            Color borderColor = isSelected ? Color.Red : (isStartBlock ? Color.Green : Color.Black);
            int borderWidth = isSelected ? 3 : (isStartBlock ? 2 : 1);
            
            // Draw block background
            using (var brush = new SolidBrush(fillColor))
            {
                g.FillRectangle(brush, rect);
            }
            
            // Draw block border
            using (var pen = new Pen(borderColor, borderWidth))
            {
                g.DrawRectangle(pen, rect);
            }
            
            // Draw block text
            var textRect = new Rectangle(rect.X + 5, rect.Y + 5, rect.Width - 10, rect.Height - 10);
            using (var brush = new SolidBrush(Color.Black))
            {
                var format = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                g.DrawString(block.Name, this.Font, brush, textRect, format);
            }
            
            // Draw connection points
            DrawConnectionPoints(g, block);
        }

        private void DrawConnectionPoints(Graphics g, WorkflowBlock block)
        {
            var center = GetBlockCenter(block);
            var pointSize = 6;
            
            // Output point (bottom)
            var outputPoint = new Point(center.X, block.Y + BlockHeight);
            var outputRect = new Rectangle(outputPoint.X - pointSize/2, outputPoint.Y - pointSize/2, pointSize, pointSize);
            g.FillEllipse(Brushes.Blue, outputRect);
            
            // Input point (top)
            var inputPoint = new Point(center.X, block.Y);
            var inputRect = new Rectangle(inputPoint.X - pointSize/2, inputPoint.Y - pointSize/2, pointSize, pointSize);
            g.FillEllipse(Brushes.Green, inputRect);
        }

        private void DrawConnections(Graphics g)
        {
            foreach (var block in _currentWorkflow.Blocks)
            {
                foreach (var nextBlockId in block.NextBlocks)
                {
                    var nextBlock = _currentWorkflow.Blocks.FirstOrDefault(b => b.Id == nextBlockId);
                    if (nextBlock != null)
                    {
                        var fromPoint = new Point(GetBlockCenter(block).X, block.Y + BlockHeight);
                        var toPoint = new Point(GetBlockCenter(nextBlock).X, nextBlock.Y);
                        DrawConnectionLine(g, fromPoint, toPoint, Color.Black);
                    }
                }
            }
        }

        private void DrawConnectionLine(Graphics g, Point from, Point to, Color color)
        {
            using (var pen = new Pen(color, 2))
            {
                // Draw curved connection line
                var controlPoint1 = new Point(from.X, from.Y + (to.Y - from.Y) / 3);
                var controlPoint2 = new Point(to.X, to.Y - (to.Y - from.Y) / 3);
                
                g.DrawBezier(pen, from, controlPoint1, controlPoint2, to);
                
                // Draw arrowhead
                DrawArrowHead(g, pen, controlPoint2, to);
            }
        }

        private void DrawArrowHead(Graphics g, Pen pen, Point from, Point to)
        {
            const int arrowLength = 10;
            const double arrowAngle = Math.PI / 6; // 30 degrees
            
            double angle = Math.Atan2(to.Y - from.Y, to.X - from.X);
            
            var arrowPoint1 = new Point(
                (int)(to.X - arrowLength * Math.Cos(angle - arrowAngle)),
                (int)(to.Y - arrowLength * Math.Sin(angle - arrowAngle))
            );
            
            var arrowPoint2 = new Point(
                (int)(to.X - arrowLength * Math.Cos(angle + arrowAngle)),
                (int)(to.Y - arrowLength * Math.Sin(angle + arrowAngle))
            );
            
            g.DrawLine(pen, to, arrowPoint1);
            g.DrawLine(pen, to, arrowPoint2);
        }

        private Color GetBlockColor(WorkflowBlockType blockType)
        {
            return blockType switch
            {
                WorkflowBlockType.MacroBlock => Color.LightBlue,
                WorkflowBlockType.ConditionalBlock => Color.LightYellow,
                WorkflowBlockType.LoopBlock => Color.LightGreen,
                WorkflowBlockType.DelayBlock => Color.LightPink,
                _ => Color.White
            };
        }

        private WorkflowBlock? GetBlockAtPosition(Point position)
        {
            return _currentWorkflow.Blocks
                .FirstOrDefault(b => new Rectangle(b.X, b.Y, BlockWidth, BlockHeight).Contains(position));
        }

        private Point GetBlockCenter(WorkflowBlock block)
        {
            return new Point(block.X + BlockWidth / 2, block.Y + BlockHeight / 2);
        }

        private int SnapToGrid(int value)
        {
            return ((value + GridSize / 2) / GridSize) * GridSize;
        }

        private void ShowContextMenu(WorkflowBlock block, Point location)
        {
            var contextMenu = new ContextMenuStrip();
            
            var editItem = new ToolStripMenuItem("Edit Properties");
            editItem.Click += (s, e) => EditBlockProperties(block);
            contextMenu.Items.Add(editItem);
            
            var deleteItem = new ToolStripMenuItem("Delete");
            deleteItem.Click += (s, e) => DeleteBlock(block);
            contextMenu.Items.Add(deleteItem);
            
            if (block.Id != _currentWorkflow.StartBlockId)
            {
                var setStartItem = new ToolStripMenuItem("Set as Start Block");
                setStartItem.Click += (s, e) => SetStartBlock(block);
                contextMenu.Items.Add(setStartItem);
            }
            
            contextMenu.Show(this, location);
        }

        private void Canvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && _selectedBlock != null)
            {
                DeleteBlock(_selectedBlock);
                e.Handled = true;
            }
        }

        private void EditBlockProperties(WorkflowBlock block)
        {
            using (var propertiesForm = new BlockPropertiesForm(block, _availableMacros, _currentWorkflow))
            {
                if (propertiesForm.ShowDialog() == DialogResult.OK)
                {
                    this.Invalidate();
                }
            }
        }

        private void DeleteBlock(WorkflowBlock block)
        {
            if (MessageBox.Show($"Delete block '{block.Name}'?", "Confirm Delete", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _currentWorkflow.Blocks.Remove(block);
                
                // Remove references to this block
                foreach (var otherBlock in _currentWorkflow.Blocks)
                {
                    otherBlock.NextBlocks.RemoveAll(id => id == block.Id);
                }
                
                // Update start block if necessary
                if (_currentWorkflow.StartBlockId == block.Id)
                {
                    _currentWorkflow.StartBlockId = _currentWorkflow.Blocks.FirstOrDefault()?.Id ?? "";
                }
                
                if (_selectedBlock == block)
                {
                    _selectedBlock = null;
                }
                
                this.Invalidate();
            }
        }

        private void SetStartBlock(WorkflowBlock block)
        {
            _currentWorkflow.StartBlockId = block.Id;
            this.Invalidate();
        }

        private void SaveWorkflow_Click(object sender, EventArgs e)
        {
            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Workflow Files (*.wf)|*.wf|All Files (*.*)|*.*";
                saveDialog.DefaultExt = "wf";
                
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var storage = new WorkflowStorage();
                        storage.ExportWorkflowAsync(_currentWorkflow.Id, saveDialog.FileName).Wait();
                        MessageBox.Show("Workflow saved successfully!", "Save Workflow", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to save workflow: {ex.Message}", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void LoadWorkflow_Click(object sender, EventArgs e)
        {
            using (var openDialog = new OpenFileDialog())
            {
                openDialog.Filter = "Workflow Files (*.wf)|*.wf|All Files (*.*)|*.*";
                
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var storage = new WorkflowStorage();
                        _currentWorkflow = storage.ImportWorkflowAsync(openDialog.FileName).Result;
                        this.Text = $"Workflow Designer - {_currentWorkflow.Name}";
                        this.Invalidate();
                        MessageBox.Show("Workflow loaded successfully!", "Load Workflow", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to load workflow: {ex.Message}", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void RunWorkflow_Click(object sender, EventArgs e)
        {
            if (_currentWorkflow.Blocks.Count == 0)
            {
                MessageBox.Show("Workflow is empty. Add some blocks first.", "Run Workflow", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            // This would integrate with the main form's workflow execution
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1000, 700);
            this.Name = "WorkflowDesigner";
            this.Text = "Workflow Designer";
            this.ResumeLayout(false);
        }
    }
}