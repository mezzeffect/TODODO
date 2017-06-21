using System;
using System.Globalization;
using Xamarin.Forms;

namespace TodoSampleMobile.Converter
{
    public class WidthConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return App.Width * System.Convert.ToInt32(parameter) / 100;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}