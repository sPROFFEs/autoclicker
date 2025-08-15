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
            this.components = new System.ComponentModel.Container();
            this.trayContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
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
            this.scheduleGroupBox = new System.Windows.Forms.GroupBox();
            this.scheduleDatePicker = new System.Windows.Forms.DateTimePicker();
            this.scheduleTimePicker = new System.Windows.Forms.DateTimePicker();
            this.scheduleEnableCheckBox = new System.Windows.Forms.CheckBox();
            this.setScheduleButton = new System.Windows.Forms.Button();
            this.scheduleDateLabel = new System.Windows.Forms.Label();
            this.scheduleTimeLabel = new System.Windows.Forms.Label();
            this.runAtTimeRadioButton = new System.Windows.Forms.RadioButton();
            this.runInRangeRadioButton = new System.Windows.Forms.RadioButton();
            this.scheduleEndTimePicker = new System.Windows.Forms.DateTimePicker();
            this.scheduleEndTimeLabel = new System.Windows.Forms.Label();


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

            //
            // recordPlayTab
            //
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

            //
            // settingsTab
            //
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

            //
            // scheduleGroupBox
            //
            this.scheduleGroupBox.Controls.Add(this.runAtTimeRadioButton);
            this.scheduleGroupBox.Controls.Add(this.runInRangeRadioButton);
            this.scheduleGroupBox.Controls.Add(this.scheduleDateLabel);
            this.scheduleGroupBox.Controls.Add(this.scheduleDatePicker);
            this.scheduleGroupBox.Controls.Add(this.scheduleTimeLabel);
            this.scheduleGroupBox.Controls.Add(this.scheduleTimePicker);
            this.scheduleGroupBox.Controls.Add(this.scheduleEndTimeLabel);
            this.scheduleGroupBox.Controls.Add(this.scheduleEndTimePicker);
            this.scheduleGroupBox.Controls.Add(this.scheduleEnableCheckBox);
            this.scheduleGroupBox.Controls.Add(this.setScheduleButton);
            this.scheduleGroupBox.Location = new System.Drawing.Point(20, 120);
            this.scheduleGroupBox.Name = "scheduleGroupBox";
            this.scheduleGroupBox.Size = new System.Drawing.Size(560, 220);
            this.scheduleGroupBox.TabIndex = 20;
            this.scheduleGroupBox.TabStop = false;
            this.scheduleGroupBox.Text = "Task Scheduler";
            this.settingsTab.Controls.Add(this.scheduleGroupBox);

            //
            // runAtTimeRadioButton
            //
            this.runAtTimeRadioButton.AutoSize = true;
            this.runAtTimeRadioButton.Checked = true;
            this.runAtTimeRadioButton.Location = new System.Drawing.Point(15, 25);
            this.runAtTimeRadioButton.Name = "runAtTimeRadioButton";
            this.runAtTimeRadioButton.Size = new System.Drawing.Size(150, 24);
            this.runAtTimeRadioButton.TabStop = true;
            this.runAtTimeRadioButton.Text = "Run at specific time";
            this.runAtTimeRadioButton.UseVisualStyleBackColor = true;

            //
            // runInRangeRadioButton
            //
            this.runInRangeRadioButton.AutoSize = true;
            this.runInRangeRadioButton.Location = new System.Drawing.Point(180, 25);
            this.runInRangeRadioButton.Name = "runInRangeRadioButton";
            this.runInRangeRadioButton.Size = new System.Drawing.Size(130, 24);
            this.runInRangeRadioButton.Text = "Run in time range";
            this.runInRangeRadioButton.UseVisualStyleBackColor = true;

            //
            // scheduleDateLabel
            //
            this.scheduleDateLabel.AutoSize = true;
            this.scheduleDateLabel.Location = new System.Drawing.Point(35, 60);
            this.scheduleDateLabel.Name = "scheduleDateLabel";
            this.scheduleDateLabel.Size = new System.Drawing.Size(44, 20);
            this.scheduleDateLabel.Text = "Date:";

            //
            // scheduleDatePicker
            //
            this.scheduleDatePicker.Location = new System.Drawing.Point(120, 55);
            this.scheduleDatePicker.Name = "scheduleDatePicker";
            this.scheduleDatePicker.Size = new System.Drawing.Size(250, 27);

            //
            // scheduleTimeLabel
            //
            this.scheduleTimeLabel.AutoSize = true;
            this.scheduleTimeLabel.Location = new System.Drawing.Point(35, 95);
            this.scheduleTimeLabel.Name = "scheduleTimeLabel";
            this.scheduleTimeLabel.Size = new System.Drawing.Size(80, 20);
            this.scheduleTimeLabel.Text = "Start Time:";

            //
            // scheduleTimePicker
            //
            this.scheduleTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.scheduleTimePicker.Location = new System.Drawing.Point(120, 90);
            this.scheduleTimePicker.Name = "scheduleTimePicker";
            this.scheduleTimePicker.ShowUpDown = true;
            this.scheduleTimePicker.Size = new System.Drawing.Size(120, 27);

            //
            // scheduleEndTimeLabel
            //
            this.scheduleEndTimeLabel.AutoSize = true;
            this.scheduleEndTimeLabel.Location = new System.Drawing.Point(35, 130);
            this.scheduleEndTimeLabel.Name = "scheduleEndTimeLabel";
            this.scheduleEndTimeLabel.Size = new System.Drawing.Size(75, 20);
            this.scheduleEndTimeLabel.Text = "End Time:";
            this.scheduleEndTimeLabel.Visible = false;

            //
            // scheduleEndTimePicker
            //
            this.scheduleEndTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.scheduleEndTimePicker.Location = new System.Drawing.Point(120, 125);
            this.scheduleEndTimePicker.Name = "scheduleEndTimePicker";
            this.scheduleEndTimePicker.ShowUpDown = true;
            this.scheduleEndTimePicker.Size = new System.Drawing.Size(120, 27);
            this.scheduleEndTimePicker.Visible = false;

            //
            // scheduleEnableCheckBox
            //
            this.scheduleEnableCheckBox.AutoSize = true;
            this.scheduleEnableCheckBox.Location = new System.Drawing.Point(15, 175);
            this.scheduleEnableCheckBox.Name = "scheduleEnableCheckBox";
            this.scheduleEnableCheckBox.Size = new System.Drawing.Size(135, 24);
            this.scheduleEnableCheckBox.Text = "Enable Scheduling";

            //
            // setScheduleButton
            //
            this.setScheduleButton.Location = new System.Drawing.Point(160, 170);
            this.setScheduleButton.Name = "setScheduleButton";
            this.setScheduleButton.Size = new System.Drawing.Size(155, 35);
            this.setScheduleButton.Text = "Set Schedule";
            this.setScheduleButton.UseVisualStyleBackColor = true;

            //
            // actionsListBox
            //
            this.actionsListBox.FormattingEnabled = true;
            this.actionsListBox.ItemHeight = 20;
            this.actionsListBox.Location = new System.Drawing.Point(6, 58);
            this.actionsListBox.Name = "actionsListBox";
            this.actionsListBox.Size = new System.Drawing.Size(590, 304);
            this.actionsListBox.TabIndex = 2;

            //
            // playButton
            //
            this.playButton.Location = new System.Drawing.Point(174, 15);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(162, 37);
            this.playButton.TabIndex = 1;
            this.playButton.Text = "Start Playback (F7)";
            this.playButton.UseVisualStyleBackColor = true;

            //
            // recordButton
            //
            this.recordButton.Location = new System.Drawing.Point(6, 15);
            this.recordButton.Name = "recordButton";
            this.recordButton.Size = new System.Drawing.Size(162, 37);
            this.recordButton.TabIndex = 0;
            this.recordButton.Text = "Start Recording (F6)";
            this.recordButton.UseVisualStyleBackColor = true;

            //
            // Settings Tab Controls
            //
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

            //
            // statusStrip
            //
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 451);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(634, 26);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";

            //
            // statusLabel
            //
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(53, 20);
            this.statusLabel.Text = "Ready";

            //
            // trayContextMenu
            //
            this.trayContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.trayContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showMenuItem,
            this.exitMenuItem});
            this.trayContextMenu.Name = "trayContextMenu";
            this.trayContextMenu.Size = new System.Drawing.Size(112, 52);
            //
            // showMenuItem
            //
            this.showMenuItem.Name = "showMenuItem";
            this.showMenuItem.Size = new System.Drawing.Size(111, 24);
            this.showMenuItem.Text = "Show";
            //
            // exitMenuItem
            //
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.Size = new System.Drawing.Size(111, 24);
            this.exitMenuItem.Text = "Exit";
            //
            // trayIcon
            //
            this.trayIcon.ContextMenuStrip = this.trayContextMenu;
            this.trayIcon.Text = "PyClickerRecorder";
            this.trayIcon.Visible = true;
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
            this.Text = "PyClickerRecorder";
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

        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.ContextMenuStrip trayContextMenu;
        private System.Windows.Forms.ToolStripMenuItem showMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
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
        private System.Windows.Forms.GroupBox scheduleGroupBox;
        private System.Windows.Forms.DateTimePicker scheduleDatePicker;
        private System.Windows.Forms.DateTimePicker scheduleTimePicker;
        private System.Windows.Forms.CheckBox scheduleEnableCheckBox;
        private System.Windows.Forms.Button setScheduleButton;
        private System.Windows.Forms.Label scheduleDateLabel;
        private System.Windows.Forms.Label scheduleTimeLabel;
        private System.Windows.Forms.RadioButton runAtTimeRadioButton;
        private System.Windows.Forms.RadioButton runInRangeRadioButton;
        private System.Windows.Forms.DateTimePicker scheduleEndTimePicker;
        private System.Windows.Forms.Label scheduleEndTimeLabel;
    }
}
