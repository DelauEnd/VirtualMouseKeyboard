using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace VirtualMouseKeyboard.Controls
{
    /// <summary>
    /// Логика взаимодействия для VirtualJoystick.xaml
    /// </summary>
    public partial class VirtualJoystick : UserControl
    {
        private double _joystickCenterX;
        private double _joystickCenterY;
        private double _joystickRadius;
        private double _stickRadius;

        private bool _captured = false;

        public double X { get; private set; }
        public double Y { get; private set; }

        public Action<Vector> JoystickMoved;

        public VirtualJoystick()
        {
            InitializeComponent();

            JoystickCanvas.SizeChanged += JoystickCanvas_SizeChanged;

            JoystickCanvas.MouseDown += JoystickCanvas_InputDown;
            JoystickCanvas.MouseUp += JoystickCanvas_InputUp;
            JoystickCanvas.TouchDown += JoystickCanvas_InputDown;
            JoystickCanvas.TouchUp += JoystickCanvas_InputUp;

            JoystickCanvas.MouseLeave += JoystickStick_MouseLeave;
        }

        private void JoystickStick_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!_captured)
                return;
            _captured = false;
            ResetJoystick();
        }

        private void JoystickCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            _joystickRadius = Math.Min(JoystickCanvas.ActualWidth, JoystickCanvas.ActualHeight) / 2;
            _stickRadius = _joystickRadius * 0.5; 

            _joystickCenterX = JoystickCanvas.ActualWidth / 2;
            _joystickCenterY = JoystickCanvas.ActualHeight / 2;

            JoystickBackground.Width = _joystickRadius * 2;
            JoystickBackground.Height = _joystickRadius * 2;
            JoystickBackground.Margin = new Thickness(_joystickCenterX - _joystickRadius, _joystickCenterY - _joystickRadius, 0, 0);

            JoystickStick.Width = _stickRadius * 2;
            JoystickStick.Height = _stickRadius * 2;
            JoystickStick.Margin = new Thickness(_joystickCenterX - _stickRadius, _joystickCenterY - _stickRadius, 0, 0);
        }

        private void JoystickCanvas_InputDown(object sender, RoutedEventArgs e)
        {
            if (_captured)
                return;

            _captured = true;
            var position = GetInputPosition(e);
            MoveStick(position);

            if (e is MouseButtonEventArgs)
                JoystickCanvas.MouseMove += JoystickCanvas_InputMove;
            else if (e is TouchEventArgs)
                JoystickCanvas.TouchMove += JoystickCanvas_InputMove;
        }

        private void JoystickCanvas_InputUp(object sender, RoutedEventArgs e)
        {
            if (!_captured)
                return;
            _captured = false;

            ResetJoystick();

            JoystickCanvas.MouseMove -= JoystickCanvas_InputMove;
            JoystickCanvas.TouchMove -= JoystickCanvas_InputMove;

            JoystickMoved?.Invoke(new Vector(X, Y));
        }

        private void JoystickCanvas_InputMove(object sender, RoutedEventArgs e)
        {
            if (!_captured)
                return;

            var position = GetInputPosition(e);
            MoveStick(position);
        }

        private Point GetInputPosition(RoutedEventArgs e)
        {
            if (e is MouseEventArgs mouseArgs)
                return mouseArgs.GetPosition(JoystickCanvas);
            else if (e is TouchEventArgs touchArgs)
                return touchArgs.GetTouchPoint(JoystickCanvas).Position;

            return new Point();
        }

        private void ResetJoystick()
        {
            X = 0;
            Y = 0;
            JoystickStick.Margin = new Thickness(_joystickCenterX - _stickRadius, _joystickCenterY - _stickRadius, 0, 0);
            JoystickMoved?.Invoke(new Vector(X, Y));
        }

        private void MoveStick(Point inputPosition)
        {
            // Calculate distance from center
            double deltaX = inputPosition.X - _joystickCenterX;
            double deltaY = inputPosition.Y - _joystickCenterY;

            // Constrain stick to joystick bounds
            double distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
            if (distance > _joystickRadius - _stickRadius)
            {
                double angle = Math.Atan2(deltaY, deltaX);
                deltaX = (_joystickRadius - _stickRadius) * Math.Cos(angle);
                deltaY = (_joystickRadius - _stickRadius) * Math.Sin(angle);
            }

            JoystickStick.Margin = new Thickness(_joystickCenterX + deltaX - _stickRadius, _joystickCenterY + deltaY - _stickRadius, 0, 0);

            X = deltaX / (_joystickRadius - _stickRadius);
            Y = deltaY / (_joystickRadius - _stickRadius);

            JoystickMoved?.Invoke(new Vector(X, Y));
        }
    }
}
