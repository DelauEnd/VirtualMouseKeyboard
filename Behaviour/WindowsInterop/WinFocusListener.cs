using System.Windows.Automation;
using System;
using System.Diagnostics;

namespace VirtualMouseKeyboard.Behaviour.WindowsInterop
{
    public class WinFocusListener
    {
        public event Action TextInputGotFocus;
        public event Action TextInputLostFocus;

        private AutomationElement _lastFocusedElement;
        private AutomationElement _lastSuccessfulFocus;

        private int _currentProcessId;
        public WinFocusListener()
        {
            _currentProcessId = Process.GetCurrentProcess().Id;
        }
        
        public void StartListening()
        {
            Automation.AddAutomationFocusChangedEventHandler(OnFocusChanged);
            Console.WriteLine("Started listening for text input focus.");
        }

        public void StopListening()
        {
            Automation.RemoveAutomationFocusChangedEventHandler(OnFocusChanged);
            Console.WriteLine("Stopped listening for text input focus.");
        }

        private void OnFocusChanged(object sender, AutomationFocusChangedEventArgs e)
        {
            var element = sender as AutomationElement;
            if (element != null)
            {
                try
                {
                    // Check if the focused element is a text input control
                    if (element.Current.ControlType == ControlType.Edit || element.Current.ControlType == ControlType.Document)
                    {
                        TextInputGotFocus?.Invoke();

                        _lastFocusedElement = element;
                    }
                    else
                    {
                        int elementProcessId = element.Current.ProcessId;

                        // Check if the focused element is part of the current application
                        if (elementProcessId != _currentProcessId)
                        {
                            TextInputLostFocus?.Invoke();

                            _lastFocusedElement = null;
                        }

                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"{ex.Message} || {ex.StackTrace}");
                }
            }
        }

        public void SetFocusOnLast()
        {
            try
            {
                if (_lastFocusedElement != null)
                {
                    _lastFocusedElement.SetFocus();
                    _lastSuccessfulFocus = _lastFocusedElement;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _lastFocusedElement = _lastSuccessfulFocus;
                SetFocusOnLast();
            }
        }
    }
}
