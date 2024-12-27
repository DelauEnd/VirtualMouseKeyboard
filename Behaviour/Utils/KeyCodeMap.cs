using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMouseKeyboard.Behaviour.Utils
{
    public class KeyCodeMap
    {
        public static Dictionary<string, ushort> KeyboardCodes = new Dictionary<string, ushort>()
        {
            { "`", 0xC0 },    // Oem3
            { "1", 0x31 },
            { "2", 0x32 },
            { "3", 0x33 },
            { "4", 0x34 },
            { "5", 0x35 },
            { "6", 0x36 },
            { "7", 0x37 },
            { "8", 0x38 },
            { "9", 0x39 },
            { "0", 0x30 },
            { "-", 0xBD },    // OemMinus
            { "+", 0xBB },    // OemPlus
            { "Backspace", 0x08 },
            { "Tab", 0x09 },
            { "Q", 0x51 },
            { "W", 0x57 },
            { "E", 0x45 },
            { "R", 0x52 },
            { "T", 0x54 },
            { "Y", 0x59 },
            { "U", 0x55 },
            { "I", 0x49 },
            { "O", 0x4F },
            { "P", 0x50 },
            { "[", 0xDB },    // OemOpenBrackets
            { "]", 0xDD },    // OemCloseBrackets
            { "\\", 0xDC },   // Oem5
            { "Caps Lock", 0x14 },
            { "A", 0x41 },
            { "S", 0x53 },
            { "D", 0x44 },
            { "F", 0x46 },
            { "G", 0x47 },
            { "H", 0x48 },
            { "J", 0x4A },
            { "K", 0x4B },
            { "L", 0x4C },
            { ":", 0xBA },    // Oem1
            { "\"", 0xDE },   // Oem7
            { "Enter", 0x0D },
            { "Shift", 0x10 },
            { "Z", 0x5A },
            { "X", 0x58 },
            { "C", 0x43 },
            { "V", 0x56 },
            { "B", 0x42 },
            { "N", 0x4E },
            { "M", 0x4D },
            { "<", 0xBC },    // OemComma
            { ">", 0xBE },    // OemPeriod
            { "?", 0xBF },    // OemQuestion
            { "Control", 0x11 },
            { "WIN", 0x5B },  // Meta
            { "Alt", 0x12 },
            { "Space", 0x20 },
            { "Hide", 0x00 }  // Placeholder for custom action
        };
}
}
