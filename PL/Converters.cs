using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using BO;
namespace PL
{
    public class ShippingStatusToTrueConverter : IValueConverter
    {
        public object Convert(
          object value,
          Type targetType,
          object parameter,
          CultureInfo culture)
        {
            DroneStatuses statusValue = (DroneStatuses)value;
            if (statusValue == DroneStatuses.Shipping)
            {
                return true;
            }
            else
            {
                return false;
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
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(
          object value,
          Type targetType,
          object parameter,
          CultureInfo culture)
        {
            bool boolValue = (bool)value;
            if (boolValue)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
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
    public class NotBooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(
          object value,
          Type targetType,
          object parameter,
          CultureInfo culture)
        {
            bool boolValue = (bool)value;
            if (!boolValue)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
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
