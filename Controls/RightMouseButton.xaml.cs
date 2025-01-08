using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace VirtualMouseKeyboard.Controls
{
    /// <summary>
    /// Логика взаимодействия для RightMouseButton.xaml
    /// </summary>
    public partial class RightMouseButton : UserControl
    {
        private bool _partiallySelected;
        private readonly DispatcherTimer _keyHoldTimer;

        public Action<bool> ButtonDownChanged;

        public RightMouseButton()
        {
            InitializeComponent();

            _keyHoldTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(App.Instance.ConfigurationManager.Configuration.HoldFrequency)
            };
            _keyHoldTimer.Tick += (s, e) => ButtonDownChanged?.Invoke(true);
        }

        public void ChangeState(bool keyState)
        {
            if (_partiallySelected && !keyState)
            {
                SetKeyUp();
            }
            if (keyState)
            {
                SetKeyDown();
            }
        }

        private void SetKeyDown()
        {
            _partiallySelected = true;

            DefaultBackground.Visibility = Visibility.Collapsed;
            ClickedBackground.Visibility = Visibility.Visible;

            App.Instance.InputFocusedListener.SetFocusOnLast();

            ButtonDownChanged?.Invoke(true);

            _keyHoldTimer.Start();
        }

        private void SetKeyUp()
        {
            _partiallySelected = false;

            DefaultBackground.Visibility = Visibility.Visible;
            ClickedBackground.Visibility = Visibility.Collapsed;

            App.Instance.InputFocusedListener.SetFocusOnLast();

            ButtonDownChanged?.Invoke(false);

            _keyHoldTimer.Stop();
        }

        private void Key_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ChangeState(true);
        }

        private void Key_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ChangeState(false);
        }

        private void Key_TouchDown(object sender, TouchEventArgs e)
        {
            ChangeState(true);
        }

        private void Key_TouchUp(object sender, TouchEventArgs e)
        {
            ChangeState(false);
        }

        private void Key_TouchLeave(object sender, TouchEventArgs e)
        {
            ChangeState(false);
        }

        private void Key_MouseLeave(object sender, MouseEventArgs e)
        {
            ChangeState(false);
        }
    }
}
