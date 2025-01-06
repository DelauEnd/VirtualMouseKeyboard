using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace VirtualMouseKeyboard.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public bool Inverted { get; set; } = false;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool booleanValue)
            {
                if (Inverted)
                    return booleanValue ? Visibility.Collapsed : Visibility.Visible;
                else
                    return booleanValue ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility visibilityValue)
            {
                if (Inverted)
                    return visibilityValue != Visibility.Visible;
                else
                    return visibilityValue == Visibility.Visible;
            }
            return false;
        }
    }
}
