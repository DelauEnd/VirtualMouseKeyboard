using System;

namespace VirtualMouseKeyboard.Behaviour.Configuration
{
    public class Configuration
    {
        private bool _showKeyboard = true;
        public bool ShowKeyboard { get => _showKeyboard; set => _showKeyboard = value; }
     
        private int _holdFrequency = 100;
        /// <summary>
        /// Miliseconds between keyboard key down being triggered while pressed down;
        /// </summary>
        public int HoldFrequency
        {
            get => _holdFrequency;
            set
            {
                value = Math.Min(1000, Math.Max(0, value));
                _holdFrequency = value;
            }
        }
        
        private bool _showMouse = true;
        public bool ShowMouse { get => _showMouse; set => _showMouse = value; }

        private int _virtualMouseSensitivity;
        public int VirtualMouseSensitivity { get => _virtualMouseSensitivity; set => _virtualMouseSensitivity = value; }
    }
}
