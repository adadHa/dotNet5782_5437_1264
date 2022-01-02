using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
namespace PL
{
    public class BooleanToOppositeBooleanConverter : IValueConverter
    {
        public object Convert(
          object value,
          Type targetType,
          object parameter,
          CultureInfo culture)
        {
            bool boolValue = (bool)value;
            if (boolValue == true)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public object ConvertBack(
          object value,
          Type targetType,
          object parameter,
          CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
