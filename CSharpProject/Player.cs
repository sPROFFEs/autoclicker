using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PyClickerRecorder
{
    public class Player
    {
        private bool _isPlaying = false;
        private CancellationTokenSource _cancellationTokenSource;

        public async Task Play(List<RecordedAction> actions, CancellationToken token)
        {
            if (_isPlaying) return;

            _isPlaying = true;
            try
            {
                foreach (var action in actions)
                {
                    token.ThrowIfCancellationRequested();
                    await ExecuteAction(action);
                }
            }
            catch (OperationCanceledException)
            {
                // Playback was stopped by the user
            }
            finally
            {
                _isPlaying = false;
            }
        }

        public void Stop()
        {
            if (_isPlaying && _cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
            }
        }

        private async Task ExecuteAction(RecordedAction action)
        {
            switch (action)
            {
                case MouseMoveAction move:
                    NativeMethods.SetCursorPos(move.Position.X, move.Position.Y);
                    break;

                case MouseClickAction click:
                    NativeMethods.SetCursorPos(click.Position.X, click.Position.Y);
                    uint flag = 0;
                    if (click.Button.ToLower() == "left")
                    {
                        flag = (click.State.ToLower() == "down") ? NativeMethods.MOUSEEVENTF_LEFTDOWN : NativeMethods.MOUSEEVENTF_LEFTUP;
                    }
                    else if (click.Button.ToLower() == "right")
                    {
                        flag = (click.State.ToLower() == "down") ? NativeMethods.MOUSEEVENTF_RIGHTDOWN : NativeMethods.MOUSEEVENTF_RIGHTUP;
                    }
                    NativeMethods.mouse_event(flag, 0, 0, 0, IntPtr.Zero);
                    break;

                case MouseScrollAction scroll:
                    NativeMethods.SetCursorPos(scroll.Position.X, scroll.Position.Y);
                    NativeMethods.mouse_event(NativeMethods.MOUSEEVENTF_WHEEL, 0, 0, (uint)scroll.Amount, IntPtr.Zero);
                    break;

                case KeyAction key:
                    // This is a simplified mapping. A full implementation would need a large dictionary
                    // or a more robust way to handle special keys from the original Python 'keyboard' library.
                    short vkCode = NativeMethods.VkKeyScan(key.Key[0]);
                    byte virtualKeyCode = (byte)(vkCode & 0xff);

                    uint keyFlag = (key.State.ToLower() == "press") ? NativeMethods.KEYEVENTF_KEYDOWN : NativeMethods.KEYEVENTF_KEYUP;
                    NativeMethods.keybd_event(virtualKeyCode, 0, keyFlag, IntPtr.Zero);
                    break;

                case DelayAction delay:
                    await Task.Delay(TimeSpan.FromSeconds(delay.Duration));
                    break;
            }
        }
    }
}
