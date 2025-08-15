namespace PyClickerRecorder
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.recordPlayTab = new System.Windows.Forms.TabPage();
            this.settingsTab = new System.Windows.Forms.TabPage();
            this.actionsListBox = new System.Windows.Forms.ListBox();
            this.playButton = new System.Windows.Forms.Button();
            this.recordButton = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.loopLabel = new System.Windows.Forms.Label();
            this.loopCountNumeric = new System.Windows.Forms.NumericUpDown();
            this.infiniteLoopCheckBox = new System.Windows.Forms.CheckBox();
            this.speedLabel = new System.Windows.Forms.Label();
            this.speedTrackBar = new System.Windows.Forms.TrackBar();
            this.speedValueLabel = new System.Windows.Forms.Label();

            this.menuStrip.SuspendLayout();
            this.mainTabControl.SuspendLayout();
            this.recordPlayTab.SuspendLayout();
            this.settingsTab.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loopCountNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speedTrackBar)).BeginInit();
            this.SuspendLayout();

            //
            // menuStrip
            //
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(634, 28);
            this.menuStrip.TabIndex = 2;
            this.menuStrip.Text = "menuStrip1";

            //
            // fileToolStripMenuItem
            //
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadMenuItem,
            this.saveMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "&File";

            //
            // loadMenuItem
            //
            this.loadMenuItem.Name = "loadMenuItem";
            this.loadMenuItem.Size = new System.Drawing.Size(128, 26);
            this.loadMenuItem.Text = "&Load...";

            //
            // saveMenuItem
            //
            this.saveMenuItem.Name = "saveMenuItem";
            this.saveMenuItem.Size = new System.Drawing.Size(128, 26);
            this.saveMenuItem.Text = "&Save...";

            //
            // mainTabControl
            //
            this.mainTabControl.Controls.Add(this.recordPlayTab);
            this.mainTabControl.Controls.Add(this.settingsTab);
            this.mainTabControl.Location = new System.Drawing.Point(12, 31); // Adjusted for menu bar
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(610, 412); // Adjusted for menu bar
            this.mainTabControl.TabIndex = 0;

            // (The rest of the control initializations are the same as before)
            this.recordPlayTab.Controls.Add(this.actionsListBox);
            this.recordPlayTab.Controls.Add(this.playButton);
            this.recordPlayTab.Controls.Add(this.recordButton);
            this.recordPlayTab.Location = new System.Drawing.Point(4, 29);
            this.recordPlayTab.Name = "recordPlayTab";
            this.recordPlayTab.Padding = new System.Windows.Forms.Padding(3);
            this.recordPlayTab.Size = new System.Drawing.Size(602, 379);
            this.recordPlayTab.TabIndex = 0;
            this.recordPlayTab.Text = "Record & Play";
            this.recordPlayTab.UseVisualStyleBackColor = true;
            this.settingsTab.Controls.Add(this.speedValueLabel);
            this.settingsTab.Controls.Add(this.speedTrackBar);
            this.settingsTab.Controls.Add(this.speedLabel);
            this.settingsTab.Controls.Add(this.infiniteLoopCheckBox);
            this.settingsTab.Controls.Add(this.loopCountNumeric);
            this.settingsTab.Controls.Add(this.loopLabel);
            this.settingsTab.Location = new System.Drawing.Point(4, 29);
            this.settingsTab.Name = "settingsTab";
            this.settingsTab.Padding = new System.Windows.Forms.Padding(3);
            this.settingsTab.Size = new System.Drawing.Size(602, 379);
            this.settingsTab.TabIndex = 1;
            this.settingsTab.Text = "Settings";
            this.settingsTab.UseVisualStyleBackColor = true;
            this.actionsListBox.FormattingEnabled = true;
            this.actionsListBox.ItemHeight = 20;
            this.actionsListBox.Location = new System.Drawing.Point(6, 58);
            this.actionsListBox.Name = "actionsListBox";
            this.actionsListBox.Size = new System.Drawing.Size(590, 304);
            this.actionsListBox.TabIndex = 2;
            this.playButton.Location = new System.Drawing.Point(174, 15);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(162, 37);
            this.playButton.TabIndex = 1;
            this.playButton.Text = "Start Playback (F7)";
            this.playButton.UseVisualStyleBackColor = true;
            this.recordButton.Location = new System.Drawing.Point(6, 15);
            this.recordButton.Name = "recordButton";
            this.recordButton.Size = new System.Drawing.Size(162, 37);
            this.recordButton.TabIndex = 0;
            this.recordButton.Text = "Start Recording (F6)";
            this.recordButton.UseVisualStyleBackColor = true;
            this.loopLabel.AutoSize = true;
            this.loopLabel.Location = new System.Drawing.Point(20, 20);
            this.loopLabel.Name = "loopLabel";
            this.loopLabel.Text = "Loop Count:";
            this.loopCountNumeric.Location = new System.Drawing.Point(120, 18);
            this.loopCountNumeric.Name = "loopCountNumeric";
            this.loopCountNumeric.Size = new System.Drawing.Size(60, 27);
            this.loopCountNumeric.Minimum = 1;
            this.loopCountNumeric.Value = 1;
            this.infiniteLoopCheckBox.AutoSize = true;
            this.infiniteLoopCheckBox.Location = new System.Drawing.Point(200, 19);
            this.infiniteLoopCheckBox.Name = "infiniteLoopCheckBox";
            this.infiniteLoopCheckBox.Text = "Infinite Loop";
            this.speedLabel.AutoSize = true;
            this.speedLabel.Location = new System.Drawing.Point(20, 60);
            this.speedLabel.Name = "speedLabel";
            this.speedLabel.Text = "Playback Speed:";
            this.speedTrackBar.Location = new System.Drawing.Point(120, 55);
            this.speedTrackBar.Name = "speedTrackBar";
            this.speedTrackBar.Size = new System.Drawing.Size(200, 56);
            this.speedTrackBar.Minimum = 1;
            this.speedTrackBar.Maximum = 20;
            this.speedTrackBar.Value = 10;
            this.speedValueLabel.AutoSize = true;
            this.speedValueLabel.Location = new System.Drawing.Point(330, 60);
            this.speedValueLabel.Name = "speedValueLabel";
            this.speedValueLabel.Text = "1.0x";
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 451);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(634, 26);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(53, 20);
            this.statusLabel.Text = "Ready";

            //
            // MainForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 477);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "PyClickerRecorder (C# Version)";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.mainTabControl.ResumeLayout(false);
            this.recordPlayTab.ResumeLayout(false);
            this.settingsTab.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loopCountNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speedTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage recordPlayTab;
        private System.Windows.Forms.TabPage settingsTab;
        private System.Windows.Forms.Button recordButton;
        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.ListBox actionsListBox;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.Label loopLabel;
        private System.Windows.Forms.NumericUpDown loopCountNumeric;
        private System.Windows.Forms.CheckBox infiniteLoopCheckBox;
        private System.Windows.Forms.Label speedLabel;
        private System.Windows.Forms.TrackBar speedTrackBar;
        private System.Windows.Forms.Label speedValueLabel;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadMenuItem;
    }
}
