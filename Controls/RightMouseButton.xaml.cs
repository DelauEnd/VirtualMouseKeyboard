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

        public Action<bool> ButtonDownChanged;

        public RightMouseButton()
        {
            InitializeComponent();
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

            ButtonDownChanged?.Invoke(true);
        }

        private void SetKeyUp()
        {
            _partiallySelected = false;

            DefaultBackground.Visibility = Visibility.Visible;
            ClickedBackground.Visibility = Visibility.Collapsed;

            ButtonDownChanged?.Invoke(false);
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
