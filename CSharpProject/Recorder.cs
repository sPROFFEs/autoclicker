using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace PyClickerRecorder
{
    public class Recorder
    {
        private IKeyboardMouseEvents _globalHook;
        private Stopwatch _stopwatch;

        public List<RecordedAction> Actions { get; private set; }
        public bool IsRecording { get; private set; }

        public Recorder()
        {
            Actions = new List<RecordedAction>();
            _stopwatch = new Stopwatch();
        }

        public void Start()
        {
            if (IsRecording) return;

            Actions.Clear();
            IsRecording = true;
            _stopwatch.Restart();

            _globalHook = Hook.GlobalEvents();
            Subscribe();
        }

        public void Stop()
        {
            if (!IsRecording) return;

            Unsubscribe();
            _globalHook.Dispose();
            IsRecording = false;
            _stopwatch.Stop();
        }

        private void Subscribe()
        {
            _globalHook.MouseDownExt += OnMouseDown;
            _globalHook.MouseUpExt += OnMouseUp;
            _globalHook.MouseWheelExt += OnMouseWheel;
            // Note: MouseMove is very performance-intensive. We'll skip it for now
            // to keep the example simple, but it could be added here.

            _globalHook.KeyDown += OnKeyDown;
            _globalHook.KeyUp += OnKeyUp;
        }

        private void Unsubscribe()
        {
            _globalHook.MouseDownExt -= OnMouseDown;
            _globalHook.MouseUpExt -= OnMouseUp;
            _globalHook.MouseWheelExt -= OnMouseWheel;
            _globalHook.KeyDown -= OnKeyDown;
            _globalHook.KeyUp -= OnKeyUp;
        }

        private void AddAction(RecordedAction action)
        {
            // Record delay since last action
            var elapsed = _stopwatch.Elapsed.TotalSeconds;
            if (Actions.Count > 0)
            {
                var lastActionTime = Actions[Actions.Count - 1].TimeOffset;
                var delay = elapsed - lastActionTime;
                if (delay > 0.01) // Only record significant delays
                {
                    Actions.Add(new DelayAction { Duration = delay, TimeOffset = elapsed });
                }
            }

            action.TimeOffset = elapsed;
            Actions.Add(action);
        }

        private void OnMouseDown(object sender, MouseEventExtArgs e)
        {
            AddAction(new MouseClickAction
            {
                Position = e.Location,
                Button = e.Button.ToString(),
                State = "Down"
            });
        }

        private void OnMouseUp(object sender, MouseEventExtArgs e)
        {
            AddAction(new MouseClickAction
            {
                Position = e.Location,
                Button = e.Button.ToString(),
                State = "Up"
            });
        }

        private void OnMouseWheel(object sender, MouseEventExtArgs e)
        {
            AddAction(new MouseScrollAction
            {
                Position = e.Location,
                Amount = e.Delta // e.g. 120 for one scroll up, -120 for one scroll down
            });
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            // We can add logic here to ignore hotkeys F6/F7 if needed
            AddAction(new KeyAction
            {
                Key = e.KeyCode.ToString(),
                State = "Press"
            });
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            AddAction(new KeyAction
            {
                Key = e.KeyCode.ToString(),
                State = "Release"
            });
        }
    }
}
