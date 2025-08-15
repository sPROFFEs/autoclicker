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

        public MainForm()
        {
            InitializeComponent();

            // Set window icon
            try
            {
                this.Icon = new Icon("../icon.ico");
            }
            catch (FileNotFoundException)
            {
                // Icon file not found, continue without it.
                Console.WriteLine("icon.ico not found. Skipping icon set.");
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

            // Global Hotkeys
            _globalHook = Gma.System.MouseKeyHook.Hook.GlobalEvents();
            _globalHook.KeyDown += GlobalHook_KeyDown;
        }

        private void GlobalHook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                recordButton.PerformClick();
                e.Handled = true; // Prevents the key press from being passed to other applications
            }
            else if (e.KeyCode == Keys.F7)
            {
                playButton.PerformClick();
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
    }
}
