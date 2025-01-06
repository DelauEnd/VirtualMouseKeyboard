using System;

namespace VirtualMouseKeyboard.Behaviour.Configuration
{
    public class Configuration
    {
        private bool _showKeyboard = true;
        public bool ShowKeyboard { get => _showKeyboard; set => _showKeyboard = value; }
        
        private bool showMouse = true;
        public bool ShowMouse { get => showMouse; set => showMouse = value; }
        
        private int holdFrequency = 250;
        /// <summary>
        /// Miliseconds between keyboard key down being triggered while pressed down;
        /// </summary>
        public int HoldFrequency
        {
            get => holdFrequency;
            set
            {
                value = Math.Min(1000, Math.Max(0, value));
                holdFrequency = value;
            }
        }
    }
}
