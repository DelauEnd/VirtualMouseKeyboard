using System;
using System.Runtime.InteropServices;
using System.Windows;
using VirtualMouseKeyboard.Behaviour.Utils;

namespace VirtualMouseKeyboard.Behaviour.WindowsInterop
{
    public class KeyboardInterop
    {
        [DllImport("user32.dll")]
        private static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        [StructLayout(LayoutKind.Sequential)]
        private struct INPUT
        {
            public int type;
            public InputUnion u;
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct InputUnion
        {
            [FieldOffset(0)] public MOUSEINPUT mi;
            [FieldOffset(0)] public KEYBDINPUT ki;
            [FieldOffset(0)] public HARDWAREINPUT hi;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct HARDWAREINPUT
        {
            public uint uMsg;
            public ushort wParamL;
            public ushort wParamH;
        }

        private const int INPUT_KEYBOARD = 1;
        private const uint KEYEVENTF_KEYUP = 0x0002;

        public void KeyDown(string key)
        {
            if (KeyCodeMap.KeyboardCodes.TryGetValue(key, out ushort keyCode))
            {
                INPUT[] inputs = new INPUT[]
                {
                    new INPUT
                    {
                        type = INPUT_KEYBOARD,
                        u = new InputUnion
                        {
                            ki = new KEYBDINPUT
                            {
                                wVk = keyCode,
                                dwFlags = 0
                            }
                        }
                    }
                };
                SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));
            }
            else
            {
                MessageBox.Show("Key not found " + key);
            }
        }

        public void KeyUp(string key)
        {
            if (KeyCodeMap.KeyboardCodes.TryGetValue(key, out ushort keyCode))
            {
                INPUT[] inputs = new INPUT[]
                {
                    new INPUT
                    {
                        type = INPUT_KEYBOARD,
                        u = new InputUnion
                        {
                            ki = new KEYBDINPUT
                            {
                                wVk = keyCode,
                                dwFlags = KEYEVENTF_KEYUP
                            }
                        }
                    }
                };
                SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));
            }
            else
            {
                MessageBox.Show("Key not found " + key);
            }
        }
    }
}
