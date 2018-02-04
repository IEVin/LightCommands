using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Input;

namespace LightCommands.WPF
{
    [ValueConversion(typeof(Enum), typeof(ICommand))]
    public class EnumToCommandConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as Enum)?.GetRoutedUICommand();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}