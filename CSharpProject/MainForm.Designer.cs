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
            this.repetitionGroupBox = new System.Windows.Forms.GroupBox();
            this.speedGroupBox = new System.Windows.Forms.GroupBox();
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
            this.repetitionGroupBox.SuspendLayout();
            this.speedGroupBox.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loopCountNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speedTrackBar)).BeginInit();
            this.SuspendLayout();

            // MenuStrip and File Menu setup... (same as before)
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.fileToolStripMenuItem });
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(634, 28);
            this.menuStrip.TabIndex = 2;
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.loadMenuItem, this.saveMenuItem });
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "&File";
            this.loadMenuItem.Name = "loadMenuItem";
            this.loadMenuItem.Size = new System.Drawing.Size(128, 26);
            this.loadMenuItem.Text = "&Load...";
            this.saveMenuItem.Name = "saveMenuItem";
            this.saveMenuItem.Size = new System.Drawing.Size(128, 26);
            this.saveMenuItem.Text = "&Save...";

            // MainTabControl setup... (same as before)
            this.mainTabControl.Controls.Add(this.recordPlayTab);
            this.mainTabControl.Controls.Add(this.settingsTab);
            this.mainTabControl.Location = new System.Drawing.Point(12, 31);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(610, 412);
            this.mainTabControl.TabIndex = 0;

            // Record & Play Tab setup... (same as before)
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
            this.actionsListBox.Location = new System.Drawing.Point(6, 58);
            this.actionsListBox.Size = new System.Drawing.Size(590, 304);
            this.playButton.Location = new System.Drawing.Point(174, 15);
            this.recordButton.Location = new System.Drawing.Point(6, 15);

            //
            // settingsTab (NOW WITH GROUPBOXES)
            //
            this.settingsTab.Controls.Add(this.repetitionGroupBox);
            this.settingsTab.Controls.Add(this.speedGroupBox);
            this.settingsTab.Location = new System.Drawing.Point(4, 29);
            this.settingsTab.Name = "settingsTab";
            this.settingsTab.Padding = new System.Windows.Forms.Padding(10);
            this.settingsTab.Size = new System.Drawing.Size(602, 379);
            this.settingsTab.TabIndex = 1;
            this.settingsTab.Text = "Settings";
            this.settingsTab.UseVisualStyleBackColor = true;

            //
            // repetitionGroupBox
            //
            this.repetitionGroupBox.Controls.Add(this.loopLabel);
            this.repetitionGroupBox.Controls.Add(this.loopCountNumeric);
            this.repetitionGroupBox.Controls.Add(this.infiniteLoopCheckBox);
            this.repetitionGroupBox.Location = new System.Drawing.Point(13, 13);
            this.repetitionGroupBox.Name = "repetitionGroupBox";
            this.repetitionGroupBox.Size = new System.Drawing.Size(576, 70);
            this.repetitionGroupBox.TabIndex = 0;
            this.repetitionGroupBox.TabStop = false;
            this.repetitionGroupBox.Text = "Playback Repetition";

            //
            // speedGroupBox
            //
            this.speedGroupBox.Controls.Add(this.speedLabel);
            this.speedGroupBox.Controls.Add(this.speedTrackBar);
            this.speedGroupBox.Controls.Add(this.speedValueLabel);
            this.speedGroupBox.Location = new System.Drawing.Point(13, 90);
            this.speedGroupBox.Name = "speedGroupBox";
            this.speedGroupBox.Size = new System.Drawing.Size(576, 100);
            this.speedGroupBox.TabIndex = 1;
            this.speedGroupBox.TabStop = false;
            this.speedGroupBox.Text = "Playback Speed";

            // Controls now inside repetitionGroupBox
            this.loopLabel.Location = new System.Drawing.Point(15, 30);
            this.loopCountNumeric.Location = new System.Drawing.Point(115, 28);
            this.infiniteLoopCheckBox.Location = new System.Drawing.Point(195, 29);

            // Controls now inside speedGroupBox
            this.speedLabel.Location = new System.Drawing.Point(15, 45);
            this.speedTrackBar.Location = new System.Drawing.Point(115, 40);
            this.speedValueLabel.Location = new System.Drawing.Point(325, 45);

            // StatusStrip setup... (same as before)
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.statusLabel });
            this.statusStrip.Location = new System.Drawing.Point(0, 451);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(634, 26);

            // MainForm setup... (same as before)
            this.ClientSize = new System.Drawing.Size(634, 477);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "PyClickerRecorder";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.mainTabControl.ResumeLayout(false);
            this.recordPlayTab.ResumeLayout(false);
            this.settingsTab.ResumeLayout(false);
            this.repetitionGroupBox.ResumeLayout(false);
            this.repetitionGroupBox.PerformLayout();
            this.speedGroupBox.ResumeLayout(false);
            this.speedGroupBox.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loopCountNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speedTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        // Final list of member variables
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
        private System.Windows.Forms.GroupBox repetitionGroupBox;
        private System.Windows.Forms.GroupBox speedGroupBox;
    }
}
