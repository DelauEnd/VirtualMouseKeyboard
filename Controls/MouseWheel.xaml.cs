using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace VirtualMouseKeyboard.Controls
{
    /// <summary>
    /// Логика взаимодействия для MouseWheel.xaml
    /// </summary>
    public partial class MouseWheel : UserControl
    {
        private double _scrollCenter;
        private double _scrollSize;

        public Action<double> ScrollChanged;

        public MouseWheel()
        {
            InitializeComponent();
            SizeChanged += MouseWheel_SizeChanged;
        }

        private void MouseWheel_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            Wheel.Height = _scrollSize = WheelHolder.ActualHeight / 2;

            _scrollCenter = (WheelHolder.ActualHeight / 2) - (Wheel.Height / 2);
            Wheel.Margin = new System.Windows.Thickness(0,_scrollCenter,0,0);
        }

        public void ChangeState(bool keyState, bool mouseInput)
        {
            if (keyState)
            {
                if (mouseInput)
                {
                    this.MouseMove += Wheel_MouseMove;
                }
                else
                {
                    this.TouchMove += Wheel_TouchMove;
                }
            }
            if (!keyState)
            {
                if (mouseInput)
                {
                    this.MouseMove -= Wheel_MouseMove;
                }
                else
                {
                    this.TouchMove -= Wheel_TouchMove;
                }

                Wheel.Margin = new System.Windows.Thickness(0, _scrollCenter, 0, 0);
                ScrollChanged?.Invoke(0);
            }
        }

        private void Wheel_TouchMove(object sender, TouchEventArgs e)
        {
            Scroll(e.GetTouchPoint(WheelHolder).Position.Y);
        }

        private void Wheel_MouseMove(object sender, MouseEventArgs e)
        {
            Scroll(e.GetPosition(WheelHolder).Y);
        }

        public void Scroll(double deltaY)
        {
            double scrollAmount = _scrollCenter - _scrollSize / 2 + deltaY - _scrollCenter;

            double topBorder = 0;
            double bottomBorder = WheelHolder.ActualHeight - _scrollSize;

            if (scrollAmount < topBorder)
            {
                scrollAmount = topBorder;
            }
            else if (scrollAmount > bottomBorder)
            {
                scrollAmount = bottomBorder;
            }

            Wheel.Margin = new System.Windows.Thickness(0, scrollAmount, 0, 0);

            double scrollRange = bottomBorder - topBorder;
            double scrollFactor = (scrollAmount - _scrollCenter) / scrollRange * 2;

            scrollFactor = Math.Max(-1, Math.Min(1, scrollFactor));
            ScrollChanged?.Invoke(scrollFactor);
        }

        private void Wheel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ChangeState(true, true);
        }

        private void Wheel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ChangeState(false, true);
        }

        private void Wheel_TouchDown(object sender, TouchEventArgs e)
        {
            ChangeState(true, false);
        }

        private void Wheel_TouchUp(object sender, TouchEventArgs e)
        {
            ChangeState(false, false);
        }

        private void Wheel_TouchLeave(object sender, TouchEventArgs e)
        {
            ChangeState(false, false);
        }

        private void Wheel_MouseLeave(object sender, MouseEventArgs e)
        {
            ChangeState(false, true);
        }
    }
}
