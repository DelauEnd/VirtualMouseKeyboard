using System.Windows;
using System.Windows.Interop;
using VirtualMouseKeyboard.Behaviour.Configuration;
using VirtualMouseKeyboard.Behaviour.WindowsInterop;

namespace VirtualMouseKeyboard
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Instance { get; private set; }

        public ConfigurationManager ConfigurationManager { get; }

        public MainWindow()
        {
            Instance = this;

            Topmost = true;
            ConfigurationManager = App.Instance.ConfigurationManager;
            InitializeComponent();

            // Make window clickThrough
            var hwnd = new WindowInteropHelper(this).Handle;
            AppWindowHelper.SetWindowExTransparent(hwnd);
        }
    }
}
