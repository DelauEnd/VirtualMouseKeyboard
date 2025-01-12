using System.Runtime.InteropServices;
using System.Threading;

namespace VirtualMouseKeyboard.Behaviour.WindowsInterop
{
    public class MouseInterop
    {
        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        private const int MOUSEEVENTF_MOVE = 0x0001;
        private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const int MOUSEEVENTF_LEFTUP = 0x0004;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const int MOUSEEVENTF_RIGHTUP = 0x0010;

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int X;
            public int Y;
        }

        public void SimulateMouseAction(int x, int y, bool leftButton)
        {
            GetCursorPos(out POINT originalPosition);

            try
            {
                SetCursorPos(x, y);

                int  mouseEvent = leftButton ? MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP : MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP;

                mouse_event(mouseEvent, x, y, 0, 0);
                Thread.Sleep(10);
            }
            finally
            {
                SetCursorPos(originalPosition.X, originalPosition.Y);
            }
        }
    }
}
