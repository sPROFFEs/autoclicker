using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;

namespace PyClickerRecorder
{
    public partial class MainForm : Form
    {
        private readonly Player _player;
        private readonly Recorder _recorder;
        private List<RecordedAction> _recordedActions;
        private bool _isPlaying;
        private bool _isRecording;
        private CancellationTokenSource _playbackCts;
        private readonly IKeyboardMouseEvents _globalHook;

        // Scheduler components
        private enum ScheduleMode { SpecificTime, TimeRange }
        private ScheduleMode _scheduleMode;
        private System.Windows.Forms.Timer _scheduleTimer;
        private DateTime _scheduledTime;
        private DateTime _scheduledEndTime;
        private bool _isScheduleEnabled;

        public MainForm()
        {
            InitializeComponent();

            // Set window and tray icon from embedded resource
            try
            {
                var assembly = System.Reflection.Assembly.GetExecutingAssembly();
                using (var stream = assembly.GetManifestResourceStream("PyClickerRecorder.icon.ico"))
                {
                    if (stream != null)
                    {
                        var icon = new Icon(stream);
                        this.Icon = icon;
                        trayIcon.Icon = icon;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error if icon fails to load, but don't crash
                Console.WriteLine($"Failed to load icon from embedded resource: {ex.Message}");
            }

            _player = new Player();
            _recorder = new Recorder();
            _recordedActions = new List<RecordedAction>();
            _isPlaying = false;
            _isRecording = false;

            // Wire up event handlers
            this.playButton.Click += PlayButton_Click;
            this.recordButton.Click += RecordButton_Click;
            this.saveMenuItem.Click += SaveMenuItem_Click;
            this.loadMenuItem.Click += LoadMenuItem_Click;
            this.speedTrackBar.ValueChanged += SpeedTrackBar_ValueChanged;
            this.infiniteLoopCheckBox.CheckedChanged += InfiniteLoopCheckBox_CheckedChanged;

            // Tray Icon Handlers
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.trayIcon.DoubleClick += new System.EventHandler(this.trayIcon_DoubleClick);
            this.showMenuItem.Click += new System.EventHandler(this.showMenuItem_Click);
            this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);


            // Global Hotkeys
            _globalHook = Gma.System.MouseKeyHook.Hook.GlobalEvents();
            _globalHook.KeyDown += GlobalHook_KeyDown;

            // Scheduler Setup
            _scheduleTimer = new System.Windows.Forms.Timer();
            _scheduleTimer.Interval = 1000; // Check every second
            _scheduleTimer.Tick += new System.EventHandler(this._scheduleTimer_Tick);
            this.setScheduleButton.Click += new System.EventHandler(this.setScheduleButton_Click);
            this.scheduleEnableCheckBox.CheckedChanged += new System.EventHandler(this.scheduleEnableCheckBox_CheckedChanged);
            this.runAtTimeRadioButton.CheckedChanged += new System.EventHandler(this.scheduleMode_CheckedChanged);
            this.runInRangeRadioButton.CheckedChanged += new System.EventHandler(this.scheduleMode_CheckedChanged);
            _isScheduleEnabled = false;
            _scheduleMode = ScheduleMode.SpecificTime;
        }

        private void GlobalHook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                RecordButton_Click(this, EventArgs.Empty);
                e.Handled = true; // Prevents the key press from being passed to other applications
            }
            else if (e.KeyCode == Keys.F7)
            {
                PlayButton_Click(this, EventArgs.Empty);
                e.Handled = true;
            }
        }

        private void RecordButton_Click(object sender, EventArgs e)
        {
            if (_isRecording)
            {
                // Stop recording
                _recorder.Stop();
                _isRecording = false;
                recordButton.Text = "Start Recording (F6)";
                statusLabel.Text = "Recording Stopped.";

                // Get actions and update listbox
                _recordedActions = _recorder.Actions;
                UpdateActionsListBox();
            }
            else
            {
                // Start recording
                if (_isPlaying) return; // Don't record while playing

                _isRecording = true;
                recordButton.Text = "Stop Recording (F6)";
                statusLabel.Text = "Recording...";
                _recorder.Start();
            }
        }

        private void UpdateActionsListBox()
        {
            actionsListBox.Items.Clear();
            if (_recordedActions == null) return;

            foreach (var action in _recordedActions)
            {
                string description = action switch
                {
                    MouseMoveAction move => $"Mouse Move to {move.Position}",
                    MouseClickAction click => $"Mouse {click.Button} {click.State} at {click.Position}",
                    MouseScrollAction scroll => $"Mouse Scroll {scroll.Amount} at {scroll.Position}",
                    KeyAction key => $"Key {key.State}: {key.Key}",
                    DelayAction delay => $"Delay for {delay.Duration:F2}s",
                    _ => action.GetType().Name
                };
                actionsListBox.Items.Add(description);
            }
        }

        private void InfiniteLoopCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            loopCountNumeric.Enabled = !infiniteLoopCheckBox.Checked;
        }

        private void SpeedTrackBar_ValueChanged(object sender, EventArgs e)
        {
            // Scale trackbar value (1-20) to a speed factor (e.g., 0.1x to 2.0x)
            double speedFactor = speedTrackBar.Value / 10.0;
            speedValueLabel.Text = $"{speedFactor:F1}x";
        }

        private async void PlayButton_Click(object sender, EventArgs e)
        {
            if (_isPlaying)
            {
                if (_playbackCts != null) _playbackCts.Cancel();
                return;
            }

            if (_recordedActions == null || _recordedActions.Count == 0)
            {
                MessageBox.Show("No actions to play.", "Playback", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _isPlaying = true;
            _playbackCts = new CancellationTokenSource();
            playButton.Text = "Stop Playback (F7)";
            statusLabel.Text = "Playing...";

            try
            {
                int loopCount = infiniteLoopCheckBox.Checked ? 0 : (int)loopCountNumeric.Value;
                double speedFactor = speedTrackBar.Value / 10.0;

                await _player.Play(_recordedActions, loopCount, speedFactor, _playbackCts.Token);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during playback: {ex.Message}", "Playback Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isPlaying = false;
                playButton.Text = "Start Playback (F7)";
                statusLabel.Text = "Ready";
            }
        }

        private void SaveMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                saveFileDialog.Title = "Save Macro";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var options = new JsonSerializerOptions { WriteIndented = true, TypeInfoResolver = new PolymorphicTypeResolver() };
                        string jsonString = JsonSerializer.Serialize(_recordedActions, options);
                        File.WriteAllText(saveFileDialog.FileName, jsonString);
                        statusLabel.Text = $"Saved: {Path.GetFileName(saveFileDialog.FileName)}";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to save file: {ex.Message}", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void LoadMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                openFileDialog.Title = "Load Macro";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string jsonString = File.ReadAllText(openFileDialog.FileName);
                        var options = new JsonSerializerOptions { TypeInfoResolver = new PolymorphicTypeResolver() };
                        _recordedActions = JsonSerializer.Deserialize<List<RecordedAction>>(jsonString, options);
                        UpdateActionsListBox();
                        statusLabel.Text = $"Loaded: {Path.GetFileName(openFileDialog.FileName)}";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to load file: {ex.Message}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                this.ShowInTaskbar = false;
                trayIcon.Visible = true;
                trayIcon.ShowBalloonTip(1000, "PyClickerRecorder", "Application minimized to tray.", ToolTipIcon.Info);
            }
        }

        private void trayIcon_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.ShowInTaskbar = true;
            this.WindowState = FormWindowState.Normal;
            trayIcon.Visible = false;
        }

        private void showMenuItem_Click(object sender, EventArgs e)
        {
            trayIcon_DoubleClick(sender, e);
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Scheduler Event Handlers
        private void scheduleMode_CheckedChanged(object sender, EventArgs e)
        {
            if (runAtTimeRadioButton.Checked)
            {
                _scheduleMode = ScheduleMode.SpecificTime;
                scheduleDateLabel.Visible = true;
                scheduleDatePicker.Visible = true;
                scheduleTimeLabel.Text = "Start Time:";
                scheduleEndTimeLabel.Visible = false;
                scheduleEndTimePicker.Visible = false;
            }
            else if (runInRangeRadioButton.Checked)
            {
                _scheduleMode = ScheduleMode.TimeRange;
                scheduleDateLabel.Visible = false;
                scheduleDatePicker.Visible = false;
                scheduleTimeLabel.Text = "Start Time:";
                scheduleEndTimeLabel.Visible = true;
                scheduleEndTimePicker.Visible = true;
            }
        }

        private void setScheduleButton_Click(object sender, EventArgs e)
        {
            if (_scheduleMode == ScheduleMode.SpecificTime)
            {
                DateTime date = scheduleDatePicker.Value.Date;
                DateTime time = scheduleTimePicker.Value;
                _scheduledTime = date.Add(time.TimeOfDay);

                if (_scheduledTime < DateTime.Now)
                {
                    MessageBox.Show("The scheduled time cannot be in the past.", "Invalid Time", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                MessageBox.Show($"Macro scheduled to run at: {_scheduledTime}", "Schedule Set", MessageBoxButtons.OK, MessageBoxIcon.Information);
                statusLabel.Text = $"Scheduled for {_scheduledTime}";
            }
            else // TimeRange mode
            {
                _scheduledTime = DateTime.Now.Date.Add(scheduleTimePicker.Value.TimeOfDay);
                _scheduledEndTime = DateTime.Now.Date.Add(scheduleEndTimePicker.Value.TimeOfDay);

                if (_scheduledEndTime <= _scheduledTime)
                {
                    MessageBox.Show("End time must be after start time.", "Invalid Time Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                MessageBox.Show($"Macro scheduled to run between {_scheduledTime:T} and {_scheduledEndTime:T}.", "Schedule Set", MessageBoxButtons.OK, MessageBoxIcon.Information);
                statusLabel.Text = $"Scheduled for range: {_scheduledTime:T} - {_scheduledEndTime:T}";
            }
        }

        private void scheduleEnableCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _isScheduleEnabled = scheduleEnableCheckBox.Checked;

            // Toggle UI controls' state
            runAtTimeRadioButton.Enabled = !_isScheduleEnabled;
            runInRangeRadioButton.Enabled = !_isScheduleEnabled;
            scheduleDatePicker.Enabled = !_isScheduleEnabled;
            scheduleTimePicker.Enabled = !_isScheduleEnabled;
            scheduleEndTimePicker.Enabled = !_isScheduleEnabled;
            setScheduleButton.Enabled = !_isScheduleEnabled;

            if (_isScheduleEnabled)
            {
                // Validation before starting timer
                if (_scheduleMode == ScheduleMode.SpecificTime && (_scheduledTime == default(DateTime) || _scheduledTime < DateTime.Now))
                {
                    MessageBox.Show("Please set a valid future time before enabling the schedule.", "Schedule Not Set", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    scheduleEnableCheckBox.Checked = false;
                    return;
                }
                 if (_scheduleMode == ScheduleMode.TimeRange && (_scheduledTime == default(DateTime) || _scheduledEndTime == default(DateTime)))
                {
                    MessageBox.Show("Please set a valid time range before enabling the schedule.", "Schedule Not Set", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    scheduleEnableCheckBox.Checked = false;
                    return;
                }

                _scheduleTimer.Start();
                statusLabel.Text = "Schedule enabled.";
            }
            else
            {
                _scheduleTimer.Stop();
                if (_isPlaying) // Stop playback if schedule is disabled during a range run
                {
                    PlayButton_Click(this, EventArgs.Empty);
                }
                statusLabel.Text = "Schedule disabled.";
            }
        }

        private void _scheduleTimer_Tick(object sender, EventArgs e)
        {
            if (!_isScheduleEnabled) return;

            DateTime now = DateTime.Now;

            if (_scheduleMode == ScheduleMode.SpecificTime)
            {
                if (now >= _scheduledTime)
                {
                    _scheduleTimer.Stop();
                    scheduleEnableCheckBox.Checked = false;
                    statusLabel.Text = $"Running scheduled macro at {now:T}...";
                    PlayButton_Click(this, EventArgs.Empty);
                }
            }
            else // TimeRange mode
            {
                DateTime todayStartTime = DateTime.Now.Date.Add(_scheduledTime.TimeOfDay);
                DateTime todayEndTime = DateTime.Now.Date.Add(_scheduledEndTime.TimeOfDay);

                bool inRange = (now >= todayStartTime && now < todayEndTime);

                if (inRange && !_isPlaying)
                {
                    // Start playing on a loop
                    infiniteLoopCheckBox.Checked = true;
                    statusLabel.Text = $"In range. Starting playback loop at {now:T}.";
                    PlayButton_Click(this, EventArgs.Empty);
                }
                else if (!inRange && _isPlaying)
                {
                    // Stop playing
                    statusLabel.Text = $"Out of range. Stopping playback at {now:T}.";
                    PlayButton_Click(this, EventArgs.Empty);
                    infiniteLoopCheckBox.Checked = false; // Restore setting
                }
            }
        }
    }
}
