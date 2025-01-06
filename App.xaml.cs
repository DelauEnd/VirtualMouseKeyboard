using System.Windows;
using VirtualMouseKeyboard.Behaviour.Configuration;
using VirtualMouseKeyboard.Behaviour.WindowsInterop;

namespace VirtualMouseKeyboard
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static App Instance { get; private set; }

        public ConfigurationManager ConfigurationManager;
        public WinFocusListener InputFocusedListener;
        public KeyboardInterop KeyboardInterop;


        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Instance = this;

            ConfigurationManager = new ConfigurationManager();

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
