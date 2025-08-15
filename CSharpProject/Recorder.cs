using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace PyClickerRecorder
{
    public class Recorder
    {
        private IKeyboardMouseEvents _globalHook;
        private Stopwatch _stopwatch;
        private Point? _lastMousePosition;

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
            _lastMousePosition = null;
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
            _globalHook.MouseMoveExt += OnMouseMove;
            _globalHook.KeyDown += OnKeyDown;
            _globalHook.KeyUp += OnKeyUp;
        }

        private void Unsubscribe()
        {
            _globalHook.MouseDownExt -= OnMouseDown;
            _globalHook.MouseUpExt -= OnMouseUp;
            _globalHook.MouseWheelExt -= OnMouseWheel;
            _globalHook.MouseMoveExt -= OnMouseMove;
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
            _lastMousePosition = e.Location;
            AddAction(new MouseClickAction
            {
                Position = e.Location,
                Button = e.Button.ToString(),
                State = "Down"
            });
        }

        private void OnMouseUp(object sender, MouseEventExtArgs e)
        {
            _lastMousePosition = e.Location;
            AddAction(new MouseClickAction
            {
                Position = e.Location,
                Button = e.Button.ToString(),
                State = "Up"
            });
        }

        private void OnMouseWheel(object sender, MouseEventExtArgs e)
        {
            _lastMousePosition = e.Location;
            AddAction(new MouseScrollAction
            {
                Position = e.Location,
                Amount = e.Delta // e.g. 120 for one scroll up, -120 for one scroll down
            });
        }

        private void OnMouseMove(object sender, MouseEventExtArgs e)
        {
            const int minDistanceSquared = 5 * 5; // Only record if moved 5 pixels

            if (_lastMousePosition.HasValue)
            {
                var dx = e.Location.X - _lastMousePosition.Value.X;
                var dy = e.Location.Y - _lastMousePosition.Value.Y;
                if ((dx * dx + dy * dy) < minDistanceSquared)
                {
                    return; // Not moved enough, ignore event
                }
            }

            _lastMousePosition = e.Location;
            AddAction(new MouseMoveAction { Position = e.Location });
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
