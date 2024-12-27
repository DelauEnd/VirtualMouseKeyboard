using System.Windows;
using System.Windows.Interop;
using VirtualMouseKeyboard.Behaviour.WindowsInterop;

namespace VirtualMouseKeyboard
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Topmost = true;

            InitializeComponent();

            // Make window clickThrough
            var hwnd = new WindowInteropHelper(this).Handle;
            AppWindowHelper.SetWindowExTransparent(hwnd);

            // Assign focusListener events
            App.Instance.InputFocusedListener.TextInputGotFocus += InputFocusedListener_TextInputFocused;
            App.Instance.InputFocusedListener.TextInputLostFocus += InputFocusedListener_TextInputLostFocus;
        }
        private void InputFocusedListener_TextInputFocused()
        {
            Application.Dispatcher.Invoke(() =>
            {
                this.VirtualKeyboard.Visibility = Visibility.Visible;
            });
        }
        private void InputFocusedListener_TextInputLostFocus()
        {
            Application.Dispatcher.Invoke(() =>
            {
                this.VirtualKeyboard.Visibility = Visibility.Collapsed;
            });
        }
    }
}
