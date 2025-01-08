using System;
using System.Timers;
using System.Windows;
using System.Windows.Controls;

namespace VirtualMouseKeyboard.Controls
{
    /// <summary>
    /// Логика взаимодействия для VirtualCursor.xaml
    /// </summary>
    public partial class VirtualCursor : UserControl
    {
        public static readonly DependencyProperty MouseProperty =
            DependencyProperty.Register(
                nameof(Mouse),           
                typeof(OnScreenMouse),    
                typeof(VirtualCursor),         
                new PropertyMetadata(default));  

        public OnScreenMouse Mouse
        {
            get => (OnScreenMouse) GetValue(MouseProperty);
            set => SetValue(MouseProperty, value);
        }

        private double _cursorPositionX;
        private double _cursorPositionY;

        private double _screenHeight;
        private double _screenWidth;

        private Vector _moveVector = new Vector(0, 0);

        private Timer _moveCursorTimer;

        public VirtualCursor()
        {
            Loaded += VirtualCursor_Loaded;

            _moveCursorTimer = new Timer(1);
            _moveCursorTimer.Elapsed += Timer_Elapsed;

            InitializeComponent();

            // Set default position
            _screenWidth = SystemParameters.PrimaryScreenWidth;
            _screenHeight = SystemParameters.PrimaryScreenHeight;

            _cursorPositionX = _screenWidth / 2;
            _cursorPositionY = _screenHeight / 2;

            SetCursorPosition(_cursorPositionX - ActualWidth / 2, _cursorPositionY - ActualHeight / 2);

            _moveCursorTimer.Start();
        }

        private void VirtualCursor_Loaded(object sender, RoutedEventArgs e)
        {
            if (Mouse != null)
            {
                Mouse.Joystick.JoystickMoved += Joystick_JoystickMoved;
                Mouse.LeftButton.ButtonDownChanged += LeftButtonClick;
                Mouse.RightButton.ButtonDownChanged += RightButtonClick;
                Mouse.LeftButton.ButtonDownChanged += LeftButtonClick;
                Mouse.Wheel.ScrollChanged += ScrollChanged;
            }
        }

        private void ScrollChanged(double scrollFactor)
        {
            double scrollAmount = MainWindow.Instance.ConfigurationManager.Configuration.VirtualMouseScrollAmount;


        }

        private void RightButtonClick(bool buttonDown)
        {

        }

        private void LeftButtonClick(bool buttonDown)
        {

        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            int maxSpeed = MainWindow.Instance.ConfigurationManager.Configuration.VirtualMouseSensitivity;

            double xStep = maxSpeed * _moveVector.X;
            double yStep = maxSpeed * _moveVector.Y;

            if (yStep == 0 && xStep == 0)
                return;

            _cursorPositionX += xStep;
            _cursorPositionY += yStep;

            _cursorPositionX = Math.Max(0, Math.Min(_screenWidth, _cursorPositionX));
            _cursorPositionY = Math.Max(0, Math.Min(_screenHeight, _cursorPositionY));

            SetCursorPosition(_cursorPositionX - ActualWidth / 2, _cursorPositionY - ActualHeight / 2);
        }

        public Vector GetScreenPosition()
        {
            return new Vector(_cursorPositionX - ActualWidth/2, _cursorPositionY - ActualHeight/2);
        }

        private void Joystick_JoystickMoved(Vector moveVec)
        {
            _moveVector.X = moveVec.X;
            _moveVector.Y = moveVec.Y;
        }

        public void SetCursorPosition(double x, double y)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                this.Margin = new Thickness(x, y, 0, 0);
            });
        }
    }
}
