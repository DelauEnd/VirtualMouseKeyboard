using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VirtualMouseKeyboard.Behaviour.WindowsInterop;

namespace VirtualMouseKeyboard
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static App Instance;
        public WinFocusListener InputFocusedListener;
        public KeyboardInterop KeyboardInterop;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Instance = this;

            // Add Win focus listener
            InputFocusedListener = new WinFocusListener();
            InputFocusedListener.StartListening();

            KeyboardInterop = new KeyboardInterop();
        }
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            // Remove Win focus listener
            InputFocusedListener.StopListening();
        }
    }
}
