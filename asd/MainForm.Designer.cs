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
            components = new System.ComponentModel.Container();
            trayContextMenu = new ContextMenuStrip(components);
            showMenuItem = new ToolStripMenuItem();
            exitMenuItem = new ToolStripMenuItem();
            trayIcon = new NotifyIcon(components);
            menuStrip = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            loadMenuItem = new ToolStripMenuItem();
            saveMenuItem = new ToolStripMenuItem();
            mainTabControl = new TabControl();
            recordPlayTab = new TabPage();
            actionsListBox = new ListBox();
            playButton = new Button();
            recordButton = new Button();
            saveMacroButton = new Button();
            settingsTab = new TabPage();
            speedValueLabel = new Label();
            speedTrackBar = new TrackBar();
            speedLabel = new Label();
            infiniteLoopCheckBox = new CheckBox();
            loopCountNumeric = new NumericUpDown();
            loopLabel = new Label();
            scheduleGroupBox = new GroupBox();
            runAtTimeRadioButton = new RadioButton();
            runInRangeRadioButton = new RadioButton();
            runOnStartupRadioButton = new RadioButton();
            repeatDailyCheckBox = new CheckBox();
            scheduleDateLabel = new Label();
            scheduleDatePicker = new DateTimePicker();
            scheduleTimeLabel = new Label();
            scheduleTimePicker = new DateTimePicker();
            scheduleEndTimeLabel = new Label();
            scheduleEndTimePicker = new DateTimePicker();
            setScheduleButton = new Button();
            savedMacrosTab = new TabPage();
            savedMacrosListView = new ListView();
            newMacroButton = new Button();
            editMacroButton = new Button();
            deleteMacroButton = new Button();
            renameMacroButton = new Button();
            runMacroButton = new Button();
            workflowsTab = new TabPage();
            workflowsListView = new ListView();
            newWorkflowButton = new Button();
            editWorkflowButton = new Button();
            deleteWorkflowButton = new Button();
            renameWorkflowButton = new Button();
            runWorkflowButton = new Button();
            userSettingsTab = new TabPage();
            darkModeCheckBox = new CheckBox();
            startWithWindowsCheckBox = new CheckBox();
            statusStrip = new StatusStrip();
            statusLabel = new ToolStripStatusLabel();
            trayContextMenu.SuspendLayout();
            menuStrip.SuspendLayout();
            mainTabControl.SuspendLayout();
            recordPlayTab.SuspendLayout();
            settingsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)speedTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)loopCountNumeric).BeginInit();
            scheduleGroupBox.SuspendLayout();
            savedMacrosTab.SuspendLayout();
            workflowsTab.SuspendLayout();
            userSettingsTab.SuspendLayout();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // trayContextMenu
            // 
            trayContextMenu.ImageScalingSize = new Size(20, 20);
            trayContextMenu.Items.AddRange(new ToolStripItem[] { showMenuItem, exitMenuItem });
            trayContextMenu.Name = "trayContextMenu";
            trayContextMenu.Size = new Size(104, 48);
            // 
            // showMenuItem
            // 
            showMenuItem.Name = "showMenuItem";
            showMenuItem.Size = new Size(103, 22);
            showMenuItem.Text = "Show";
            // 
            // exitMenuItem
            // 
            exitMenuItem.Name = "exitMenuItem";
            exitMenuItem.Size = new Size(103, 22);
            exitMenuItem.Text = "Exit";
            // 
            // trayIcon
            // 
            trayIcon.ContextMenuStrip = trayContextMenu;
            trayIcon.Text = "PyClicker - Pro";
            trayIcon.Visible = true;
            // 
            // menuStrip
            // 
            menuStrip.ImageScalingSize = new Size(20, 20);
            menuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Padding = new Padding(5, 2, 0, 2);
            menuStrip.Size = new Size(555, 24);
            menuStrip.TabIndex = 2;
            menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { loadMenuItem, saveMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "&File";
            // 
            // loadMenuItem
            // 
            loadMenuItem.Name = "loadMenuItem";
            loadMenuItem.Size = new Size(123, 22);
            loadMenuItem.Text = "&Load...";
            // 
            // saveMenuItem
            // 
            saveMenuItem.Name = "saveMenuItem";
            saveMenuItem.Size = new Size(123, 22);
            saveMenuItem.Text = "Save &As...";
            // 
            // mainTabControl
            // 
            mainTabControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mainTabControl.Controls.Add(recordPlayTab);
            mainTabControl.Controls.Add(settingsTab);
            mainTabControl.Controls.Add(savedMacrosTab);
            mainTabControl.Controls.Add(workflowsTab);
            mainTabControl.Controls.Add(userSettingsTab);
            mainTabControl.Location = new Point(10, 23);
            mainTabControl.Margin = new Padding(3, 2, 3, 2);
            mainTabControl.Name = "mainTabControl";
            mainTabControl.SelectedIndex = 0;
            mainTabControl.Size = new Size(534, 309);
            mainTabControl.TabIndex = 0;
            // 
            // recordPlayTab
            // 
            recordPlayTab.Controls.Add(actionsListBox);
            recordPlayTab.Controls.Add(playButton);
            recordPlayTab.Controls.Add(recordButton);
            recordPlayTab.Controls.Add(saveMacroButton);
            recordPlayTab.Location = new Point(4, 24);
            recordPlayTab.Margin = new Padding(3, 2, 3, 2);
            recordPlayTab.Name = "recordPlayTab";
            recordPlayTab.Padding = new Padding(3, 2, 3, 2);
            recordPlayTab.Size = new Size(526, 281);
            recordPlayTab.TabIndex = 0;
            recordPlayTab.Text = "Record & Play";
            recordPlayTab.UseVisualStyleBackColor = true;
            // 
            // actionsListBox
            // 
            actionsListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            actionsListBox.FormattingEnabled = true;
            actionsListBox.ItemHeight = 15;
            actionsListBox.Location = new Point(5, 44);
            actionsListBox.Margin = new Padding(3, 2, 3, 2);
            actionsListBox.Name = "actionsListBox";
            actionsListBox.Size = new Size(517, 229);
            actionsListBox.TabIndex = 2;
            // 
            // playButton
            // 
            playButton.Location = new Point(152, 11);
            playButton.Margin = new Padding(3, 2, 3, 2);
            playButton.Name = "playButton";
            playButton.Size = new Size(142, 28);
            playButton.TabIndex = 1;
            playButton.Text = "Start Playback (F7)";
            playButton.UseVisualStyleBackColor = true;
            // 
            // recordButton
            // 
            recordButton.Location = new Point(5, 11);
            recordButton.Margin = new Padding(3, 2, 3, 2);
            recordButton.Name = "recordButton";
            recordButton.Size = new Size(142, 28);
            recordButton.TabIndex = 0;
            recordButton.Text = "Start Recording (F6)";
            recordButton.UseVisualStyleBackColor = true;
            // 
            // saveMacroButton
            // 
            saveMacroButton.Location = new Point(299, 11);
            saveMacroButton.Margin = new Padding(3, 2, 3, 2);
            saveMacroButton.Name = "saveMacroButton";
            saveMacroButton.Size = new Size(142, 28);
            saveMacroButton.TabIndex = 3;
            saveMacroButton.Text = "Save Macro";
            saveMacroButton.UseVisualStyleBackColor = true;
            // 
            // settingsTab
            // 
            settingsTab.Controls.Add(speedValueLabel);
            settingsTab.Controls.Add(speedTrackBar);
            settingsTab.Controls.Add(speedLabel);
            settingsTab.Controls.Add(infiniteLoopCheckBox);
            settingsTab.Controls.Add(loopCountNumeric);
            settingsTab.Controls.Add(loopLabel);
            settingsTab.Controls.Add(scheduleGroupBox);
            settingsTab.Location = new Point(4, 24);
            settingsTab.Margin = new Padding(3, 2, 3, 2);
            settingsTab.Name = "settingsTab";
            settingsTab.Padding = new Padding(3, 2, 3, 2);
            settingsTab.Size = new Size(526, 281);
            settingsTab.TabIndex = 1;
            settingsTab.Text = "Settings";
            settingsTab.UseVisualStyleBackColor = true;
            // 
            // speedValueLabel
            // 
            speedValueLabel.AutoSize = true;
            speedValueLabel.Location = new Point(289, 45);
            speedValueLabel.Name = "speedValueLabel";
            speedValueLabel.Size = new Size(27, 15);
            speedValueLabel.TabIndex = 0;
            speedValueLabel.Text = "1.0x";
            // 
            // speedTrackBar
            // 
            speedTrackBar.Location = new Point(105, 41);
            speedTrackBar.Margin = new Padding(3, 2, 3, 2);
            speedTrackBar.Maximum = 20;
            speedTrackBar.Minimum = 1;
            speedTrackBar.Name = "speedTrackBar";
            speedTrackBar.Size = new Size(175, 45);
            speedTrackBar.TabIndex = 1;
            speedTrackBar.Value = 10;
            // 
            // speedLabel
            // 
            speedLabel.AutoSize = true;
            speedLabel.Location = new Point(18, 45);
            speedLabel.Name = "speedLabel";
            speedLabel.Size = new Size(92, 15);
            speedLabel.TabIndex = 2;
            speedLabel.Text = "Playback Speed:";
            // 
            // infiniteLoopCheckBox
            // 
            infiniteLoopCheckBox.AutoSize = true;
            infiniteLoopCheckBox.Location = new Point(175, 14);
            infiniteLoopCheckBox.Margin = new Padding(3, 2, 3, 2);
            infiniteLoopCheckBox.Name = "infiniteLoopCheckBox";
            infiniteLoopCheckBox.Size = new Size(93, 19);
            infiniteLoopCheckBox.TabIndex = 3;
            infiniteLoopCheckBox.Text = "Infinite Loop";
            // 
            // loopCountNumeric
            // 
            loopCountNumeric.Location = new Point(105, 14);
            loopCountNumeric.Margin = new Padding(3, 2, 3, 2);
            loopCountNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            loopCountNumeric.Name = "loopCountNumeric";
            loopCountNumeric.Size = new Size(52, 23);
            loopCountNumeric.TabIndex = 4;
            loopCountNumeric.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // loopLabel
            // 
            loopLabel.AutoSize = true;
            loopLabel.Location = new Point(18, 15);
            loopLabel.Name = "loopLabel";
            loopLabel.Size = new Size(73, 15);
            loopLabel.TabIndex = 5;
            loopLabel.Text = "Loop Count:";
            // 
            // scheduleGroupBox
            // 
            scheduleGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            scheduleGroupBox.Controls.Add(runAtTimeRadioButton);
            scheduleGroupBox.Controls.Add(runInRangeRadioButton);
            scheduleGroupBox.Controls.Add(runOnStartupRadioButton);
            scheduleGroupBox.Controls.Add(repeatDailyCheckBox);
            scheduleGroupBox.Controls.Add(scheduleDateLabel);
            scheduleGroupBox.Controls.Add(scheduleDatePicker);
            scheduleGroupBox.Controls.Add(scheduleTimeLabel);
            scheduleGroupBox.Controls.Add(scheduleTimePicker);
            scheduleGroupBox.Controls.Add(scheduleEndTimeLabel);
            scheduleGroupBox.Controls.Add(scheduleEndTimePicker);
            scheduleGroupBox.Controls.Add(setScheduleButton);
            scheduleGroupBox.Location = new Point(18, 90);
            scheduleGroupBox.Margin = new Padding(3, 2, 3, 2);
            scheduleGroupBox.Name = "scheduleGroupBox";
            scheduleGroupBox.Padding = new Padding(3, 2, 3, 2);
            scheduleGroupBox.Size = new Size(490, 157);
            scheduleGroupBox.TabIndex = 20;
            scheduleGroupBox.TabStop = false;
            scheduleGroupBox.Text = "Task Scheduler";
            // 
            // runAtTimeRadioButton
            // 
            runAtTimeRadioButton.AutoSize = true;
            runAtTimeRadioButton.Checked = true;
            runAtTimeRadioButton.Location = new Point(13, 19);
            runAtTimeRadioButton.Margin = new Padding(3, 2, 3, 2);
            runAtTimeRadioButton.Name = "runAtTimeRadioButton";
            runAtTimeRadioButton.Size = new Size(129, 19);
            runAtTimeRadioButton.TabIndex = 0;
            runAtTimeRadioButton.TabStop = true;
            runAtTimeRadioButton.Text = "Run at specific time";
            runAtTimeRadioButton.UseVisualStyleBackColor = true;
            // 
            // runInRangeRadioButton
            // 
            runInRangeRadioButton.AutoSize = true;
            runInRangeRadioButton.Location = new Point(158, 19);
            runInRangeRadioButton.Margin = new Padding(3, 2, 3, 2);
            runInRangeRadioButton.Name = "runInRangeRadioButton";
            runInRangeRadioButton.Size = new Size(119, 19);
            runInRangeRadioButton.TabIndex = 1;
            runInRangeRadioButton.Text = "Run in time range";
            runInRangeRadioButton.UseVisualStyleBackColor = true;
            // 
            // runOnStartupRadioButton
            // 
            runOnStartupRadioButton.AutoSize = true;
            runOnStartupRadioButton.Location = new Point(13, 41);
            runOnStartupRadioButton.Margin = new Padding(3, 2, 3, 2);
            runOnStartupRadioButton.Name = "runOnStartupRadioButton";
            runOnStartupRadioButton.Size = new Size(155, 19);
            runOnStartupRadioButton.TabIndex = 2;
            runOnStartupRadioButton.Text = "Run on Windows startup";
            runOnStartupRadioButton.UseVisualStyleBackColor = true;
            // 
            // repeatDailyCheckBox
            // 
            repeatDailyCheckBox.AutoSize = true;
            repeatDailyCheckBox.Location = new Point(289, 19);
            repeatDailyCheckBox.Margin = new Padding(3, 2, 3, 2);
            repeatDailyCheckBox.Name = "repeatDailyCheckBox";
            repeatDailyCheckBox.Size = new Size(91, 19);
            repeatDailyCheckBox.TabIndex = 3;
            repeatDailyCheckBox.Text = "Repeat Daily";
            repeatDailyCheckBox.UseVisualStyleBackColor = true;
            // 
            // scheduleDateLabel
            // 
            scheduleDateLabel.AutoSize = true;
            scheduleDateLabel.Location = new Point(31, 68);
            scheduleDateLabel.Name = "scheduleDateLabel";
            scheduleDateLabel.Size = new Size(34, 15);
            scheduleDateLabel.TabIndex = 4;
            scheduleDateLabel.Text = "Date:";
            // 
            // scheduleDatePicker
            // 
            scheduleDatePicker.Location = new Point(105, 64);
            scheduleDatePicker.Margin = new Padding(3, 2, 3, 2);
            scheduleDatePicker.Name = "scheduleDatePicker";
            scheduleDatePicker.Size = new Size(219, 23);
            scheduleDatePicker.TabIndex = 5;
            // 
            // scheduleTimeLabel
            // 
            scheduleTimeLabel.AutoSize = true;
            scheduleTimeLabel.Location = new Point(31, 94);
            scheduleTimeLabel.Name = "scheduleTimeLabel";
            scheduleTimeLabel.Size = new Size(64, 15);
            scheduleTimeLabel.TabIndex = 6;
            scheduleTimeLabel.Text = "Start Time:";
            // 
            // scheduleTimePicker
            // 
            scheduleTimePicker.Format = DateTimePickerFormat.Time;
            scheduleTimePicker.Location = new Point(105, 90);
            scheduleTimePicker.Margin = new Padding(3, 2, 3, 2);
            scheduleTimePicker.Name = "scheduleTimePicker";
            scheduleTimePicker.ShowUpDown = true;
            scheduleTimePicker.Size = new Size(106, 23);
            scheduleTimePicker.TabIndex = 7;
            // 
            // scheduleEndTimeLabel
            // 
            scheduleEndTimeLabel.AutoSize = true;
            scheduleEndTimeLabel.Location = new Point(31, 120);
            scheduleEndTimeLabel.Name = "scheduleEndTimeLabel";
            scheduleEndTimeLabel.Size = new Size(60, 15);
            scheduleEndTimeLabel.TabIndex = 8;
            scheduleEndTimeLabel.Text = "End Time:";
            scheduleEndTimeLabel.Visible = false;
            // 
            // scheduleEndTimePicker
            // 
            scheduleEndTimePicker.Format = DateTimePickerFormat.Time;
            scheduleEndTimePicker.Location = new Point(105, 116);
            scheduleEndTimePicker.Margin = new Padding(3, 2, 3, 2);
            scheduleEndTimePicker.Name = "scheduleEndTimePicker";
            scheduleEndTimePicker.ShowUpDown = true;
            scheduleEndTimePicker.Size = new Size(106, 23);
            scheduleEndTimePicker.TabIndex = 9;
            scheduleEndTimePicker.Visible = false;
            // 
            // setScheduleButton
            // 
            setScheduleButton.Location = new Point(244, 116);
            setScheduleButton.Margin = new Padding(3, 2, 3, 2);
            setScheduleButton.Name = "setScheduleButton";
            setScheduleButton.Size = new Size(136, 26);
            setScheduleButton.TabIndex = 10;
            setScheduleButton.Text = "Set Schedule";
            setScheduleButton.UseVisualStyleBackColor = true;
            // 
            // savedMacrosTab
            // 
            savedMacrosTab.Controls.Add(savedMacrosListView);
            savedMacrosTab.Controls.Add(newMacroButton);
            savedMacrosTab.Controls.Add(editMacroButton);
            savedMacrosTab.Controls.Add(deleteMacroButton);
            savedMacrosTab.Controls.Add(renameMacroButton);
            savedMacrosTab.Controls.Add(runMacroButton);
            savedMacrosTab.Location = new Point(4, 24);
            savedMacrosTab.Margin = new Padding(3, 2, 3, 2);
            savedMacrosTab.Name = "savedMacrosTab";
            savedMacrosTab.Padding = new Padding(3, 2, 3, 2);
            savedMacrosTab.Size = new Size(526, 281);
            savedMacrosTab.TabIndex = 2;
            savedMacrosTab.Text = "Saved Macros";
            savedMacrosTab.UseVisualStyleBackColor = true;
            // 
            // savedMacrosListView
            // 
            savedMacrosListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            savedMacrosListView.CheckBoxes = true;
            savedMacrosListView.Location = new Point(5, 4);
            savedMacrosListView.Margin = new Padding(3, 2, 3, 2);
            savedMacrosListView.Name = "savedMacrosListView";
            savedMacrosListView.Size = new Size(517, 241);
            savedMacrosListView.TabIndex = 0;
            savedMacrosListView.UseCompatibleStateImageBehavior = false;
            savedMacrosListView.View = View.Details;
            // 
            // newMacroButton
            // 
            newMacroButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            newMacroButton.Location = new Point(5, 249);
            newMacroButton.Margin = new Padding(3, 2, 3, 2);
            newMacroButton.Name = "newMacroButton";
            newMacroButton.Size = new Size(82, 22);
            newMacroButton.TabIndex = 1;
            newMacroButton.Text = "New";
            newMacroButton.UseVisualStyleBackColor = true;
            // 
            // editMacroButton
            // 
            editMacroButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            editMacroButton.Location = new Point(93, 249);
            editMacroButton.Margin = new Padding(3, 2, 3, 2);
            editMacroButton.Name = "editMacroButton";
            editMacroButton.Size = new Size(82, 22);
            editMacroButton.TabIndex = 2;
            editMacroButton.Text = "Edit";
            editMacroButton.UseVisualStyleBackColor = true;
            // 
            // deleteMacroButton
            // 
            deleteMacroButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            deleteMacroButton.Location = new Point(180, 249);
            deleteMacroButton.Margin = new Padding(3, 2, 3, 2);
            deleteMacroButton.Name = "deleteMacroButton";
            deleteMacroButton.Size = new Size(82, 22);
            deleteMacroButton.TabIndex = 3;
            deleteMacroButton.Text = "Delete";
            deleteMacroButton.UseVisualStyleBackColor = true;
            // 
            // renameMacroButton
            // 
            renameMacroButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            renameMacroButton.Location = new Point(268, 249);
            renameMacroButton.Margin = new Padding(3, 2, 3, 2);
            renameMacroButton.Name = "renameMacroButton";
            renameMacroButton.Size = new Size(82, 22);
            renameMacroButton.TabIndex = 4;
            renameMacroButton.Text = "Rename";
            renameMacroButton.UseVisualStyleBackColor = true;
            // 
            // runMacroButton
            // 
            runMacroButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            runMacroButton.Location = new Point(350, 249);
            runMacroButton.Margin = new Padding(3, 2, 3, 2);
            runMacroButton.Name = "runMacroButton";
            runMacroButton.Size = new Size(82, 22);
            runMacroButton.TabIndex = 5;
            runMacroButton.Text = "Run";
            runMacroButton.UseVisualStyleBackColor = true;
            // 
            // workflowsTab
            // 
            workflowsTab.Controls.Add(workflowsListView);
            workflowsTab.Controls.Add(newWorkflowButton);
            workflowsTab.Controls.Add(editWorkflowButton);
            workflowsTab.Controls.Add(deleteWorkflowButton);
            workflowsTab.Controls.Add(renameWorkflowButton);
            workflowsTab.Controls.Add(runWorkflowButton);
            workflowsTab.Location = new Point(4, 24);
            workflowsTab.Margin = new Padding(3, 2, 3, 2);
            workflowsTab.Name = "workflowsTab";
            workflowsTab.Padding = new Padding(3, 2, 3, 2);
            workflowsTab.Size = new Size(526, 281);
            workflowsTab.TabIndex = 3;
            workflowsTab.Text = "Workflows";
            workflowsTab.UseVisualStyleBackColor = true;
            // 
            // workflowsListView
            // 
            workflowsListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            workflowsListView.CheckBoxes = true;
            workflowsListView.Location = new Point(5, 4);
            workflowsListView.Margin = new Padding(3, 2, 3, 2);
            workflowsListView.Name = "workflowsListView";
            workflowsListView.Size = new Size(517, 241);
            workflowsListView.TabIndex = 0;
            workflowsListView.UseCompatibleStateImageBehavior = false;
            workflowsListView.View = View.Details;
            // 
            // newWorkflowButton
            // 
            newWorkflowButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            newWorkflowButton.Location = new Point(5, 249);
            newWorkflowButton.Margin = new Padding(3, 2, 3, 2);
            newWorkflowButton.Name = "newWorkflowButton";
            newWorkflowButton.Size = new Size(82, 22);
            newWorkflowButton.TabIndex = 1;
            newWorkflowButton.Text = "New";
            newWorkflowButton.UseVisualStyleBackColor = true;
            // 
            // editWorkflowButton
            // 
            editWorkflowButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            editWorkflowButton.Location = new Point(93, 249);
            editWorkflowButton.Margin = new Padding(3, 2, 3, 2);
            editWorkflowButton.Name = "editWorkflowButton";
            editWorkflowButton.Size = new Size(82, 22);
            editWorkflowButton.TabIndex = 2;
            editWorkflowButton.Text = "Edit";
            editWorkflowButton.UseVisualStyleBackColor = true;
            // 
            // deleteWorkflowButton
            // 
            deleteWorkflowButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            deleteWorkflowButton.Location = new Point(180, 249);
            deleteWorkflowButton.Margin = new Padding(3, 2, 3, 2);
            deleteWorkflowButton.Name = "deleteWorkflowButton";
            deleteWorkflowButton.Size = new Size(82, 22);
            deleteWorkflowButton.TabIndex = 3;
            deleteWorkflowButton.Text = "Delete";
            deleteWorkflowButton.UseVisualStyleBackColor = true;
            // 
            // renameWorkflowButton
            // 
            renameWorkflowButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            renameWorkflowButton.Location = new Point(268, 249);
            renameWorkflowButton.Margin = new Padding(3, 2, 3, 2);
            renameWorkflowButton.Name = "renameWorkflowButton";
            renameWorkflowButton.Size = new Size(82, 22);
            renameWorkflowButton.TabIndex = 4;
            renameWorkflowButton.Text = "Rename";
            renameWorkflowButton.UseVisualStyleBackColor = true;
            // 
            // runWorkflowButton
            // 
            runWorkflowButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            runWorkflowButton.Location = new Point(350, 249);
            runWorkflowButton.Margin = new Padding(3, 2, 3, 2);
            runWorkflowButton.Name = "runWorkflowButton";
            runWorkflowButton.Size = new Size(82, 22);
            runWorkflowButton.TabIndex = 5;
            runWorkflowButton.Text = "Run";
            runWorkflowButton.UseVisualStyleBackColor = true;
            // 
            // userSettingsTab
            // 
            userSettingsTab.Controls.Add(darkModeCheckBox);
            userSettingsTab.Controls.Add(startWithWindowsCheckBox);
            userSettingsTab.Location = new Point(4, 24);
            userSettingsTab.Margin = new Padding(3, 2, 3, 2);
            userSettingsTab.Name = "userSettingsTab";
            userSettingsTab.Padding = new Padding(3, 2, 3, 2);
            userSettingsTab.Size = new Size(526, 281);
            userSettingsTab.TabIndex = 3;
            userSettingsTab.Text = "User Settings";
            userSettingsTab.UseVisualStyleBackColor = true;
            // 
            // darkModeCheckBox
            // 
            darkModeCheckBox.AutoSize = true;
            darkModeCheckBox.Location = new Point(18, 15);
            darkModeCheckBox.Margin = new Padding(3, 2, 3, 2);
            darkModeCheckBox.Name = "darkModeCheckBox";
            darkModeCheckBox.Size = new Size(84, 19);
            darkModeCheckBox.TabIndex = 0;
            darkModeCheckBox.Text = "Dark Mode";
            darkModeCheckBox.UseVisualStyleBackColor = true;
            // 
            // startWithWindowsCheckBox
            // 
            startWithWindowsCheckBox.AutoSize = true;
            startWithWindowsCheckBox.Location = new Point(18, 38);
            startWithWindowsCheckBox.Margin = new Padding(3, 2, 3, 2);
            startWithWindowsCheckBox.Name = "startWithWindowsCheckBox";
            startWithWindowsCheckBox.Size = new Size(128, 19);
            startWithWindowsCheckBox.TabIndex = 1;
            startWithWindowsCheckBox.Text = "Start with Windows";
            startWithWindowsCheckBox.UseVisualStyleBackColor = true;
            // 
            // statusStrip
            // 
            statusStrip.ImageScalingSize = new Size(20, 20);
            statusStrip.Items.AddRange(new ToolStripItem[] { statusLabel });
            statusStrip.Location = new Point(0, 336);
            statusStrip.Name = "statusStrip";
            statusStrip.Padding = new Padding(1, 0, 12, 0);
            statusStrip.Size = new Size(555, 22);
            statusStrip.TabIndex = 1;
            statusStrip.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(39, 17);
            statusLabel.Text = "Ready";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(555, 358);
            Controls.Add(statusStrip);
            Controls.Add(mainTabControl);
            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
            Margin = new Padding(3, 2, 3, 2);
            Name = "MainForm";
            Text = "PyClicker - Pro";
            trayContextMenu.ResumeLayout(false);
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            mainTabControl.ResumeLayout(false);
            recordPlayTab.ResumeLayout(false);
            settingsTab.ResumeLayout(false);
            settingsTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)speedTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)loopCountNumeric).EndInit();
            scheduleGroupBox.ResumeLayout(false);
            scheduleGroupBox.PerformLayout();
            savedMacrosTab.ResumeLayout(false);
            workflowsTab.ResumeLayout(false);
            userSettingsTab.ResumeLayout(false);
            userSettingsTab.PerformLayout();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
        private System.Windows.Forms.Button setScheduleButton;
        private System.Windows.Forms.Label scheduleDateLabel;
        private System.Windows.Forms.Label scheduleTimeLabel;
        private System.Windows.Forms.RadioButton runAtTimeRadioButton;
        private System.Windows.Forms.RadioButton runInRangeRadioButton;
        private System.Windows.Forms.RadioButton runOnStartupRadioButton;
        private System.Windows.Forms.DateTimePicker scheduleEndTimePicker;
        private System.Windows.Forms.Label scheduleEndTimeLabel;
        private System.Windows.Forms.CheckBox repeatDailyCheckBox;
        private System.Windows.Forms.TabPage savedMacrosTab;
        private System.Windows.Forms.ListView savedMacrosListView;
        private System.Windows.Forms.Button newMacroButton;
        private System.Windows.Forms.Button editMacroButton;
        private System.Windows.Forms.Button deleteMacroButton;
        private System.Windows.Forms.Button runMacroButton;
        private System.Windows.Forms.Button renameMacroButton;
        private System.Windows.Forms.Button saveMacroButton;
        private System.Windows.Forms.TabPage workflowsTab;
        private System.Windows.Forms.ListView workflowsListView;
        private System.Windows.Forms.Button newWorkflowButton;
        private System.Windows.Forms.Button editWorkflowButton;
        private System.Windows.Forms.Button deleteWorkflowButton;
        private System.Windows.Forms.Button renameWorkflowButton;
        private System.Windows.Forms.Button runWorkflowButton;
        private System.Windows.Forms.TabPage userSettingsTab;
        private System.Windows.Forms.CheckBox darkModeCheckBox;
        private System.Windows.Forms.CheckBox startWithWindowsCheckBox;
    }
}
