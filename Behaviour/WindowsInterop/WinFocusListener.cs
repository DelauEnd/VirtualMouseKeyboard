using System.Windows.Automation;
using System;
using System.Diagnostics;

namespace VirtualMouseKeyboard.Behaviour.WindowsInterop
{
    public class WinFocusListener
    {
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
                    int elementProcessId = element.Current.ProcessId;

                    if (elementProcessId != _currentProcessId)
                    {
                        _lastFocusedElement = element;
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
                if (_lastFocusedElement == _lastSuccessfulFocus)
                    return;
                _lastFocusedElement = _lastSuccessfulFocus;
                SetFocusOnLast();
            }
        }
    }
}
