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

        // Public properties to get X and Y values
        public double X { get; private set; }
        public double Y { get; private set; }

        // Event to notify whenever joystick values change
        public event EventHandler<JoystickEventArgs> JoystickMoved;

        public VirtualJoystick()
        {
            InitializeComponent();

            // Set up the joystick size and stick size when the size of the control is set
            JoystickCanvas.SizeChanged += JoystickCanvas_SizeChanged;

            // Event handlers for mouse and touch input
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
            // Adjust joystick and stick sizes based on control size
            _joystickRadius = Math.Min(JoystickCanvas.ActualWidth, JoystickCanvas.ActualHeight) / 2;
            _stickRadius = _joystickRadius * 0.5; // Stick size is 0.5 of joystick size

            // Set the center coordinates
            _joystickCenterX = JoystickCanvas.ActualWidth / 2;
            _joystickCenterY = JoystickCanvas.ActualHeight / 2;

            // Set background and stick sizes and position them at the center
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

            // Hook up MouseMove or TouchMove depending on the input method
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

            // Unhook MouseMove or TouchMove
            JoystickCanvas.MouseMove -= JoystickCanvas_InputMove;
            JoystickCanvas.TouchMove -= JoystickCanvas_InputMove;

            // Notify listeners that the joystick has moved
            JoystickMoved?.Invoke(this, new JoystickEventArgs(X, Y));
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
            // Handle both mouse and touch input
            if (e is MouseEventArgs mouseArgs)
                return mouseArgs.GetPosition(JoystickCanvas);
            else if (e is TouchEventArgs touchArgs)
                return touchArgs.GetTouchPoint(JoystickCanvas).Position;

            return new Point();
        }

        private void ResetJoystick()
        {
            // Reset position to center when input is released
            X = 0;
            Y = 0;
            JoystickStick.Margin = new Thickness(_joystickCenterX - _stickRadius, _joystickCenterY - _stickRadius, 0, 0);
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

            // Set the new position for the stick
            JoystickStick.Margin = new Thickness(_joystickCenterX + deltaX - _stickRadius, _joystickCenterY + deltaY - _stickRadius, 0, 0);

            // Update X and Y values
            X = deltaX / (_joystickRadius - _stickRadius);
            Y = deltaY / (_joystickRadius - _stickRadius);

            // Notify listeners about the movement
            JoystickMoved?.Invoke(this, new JoystickEventArgs(X, Y));
        }
    }

    // Custom event arguments to pass X and Y values
    public class JoystickEventArgs : EventArgs
    {
        public double X { get; }
        public double Y { get; }

        public JoystickEventArgs(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}
