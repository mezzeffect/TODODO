using System;
using System.Globalization;
using Xamarin.Forms;

namespace TodoSampleMobile.Converter
{
    public class YesNoConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var eval = (bool) value;
            if (eval)
            {
                return "Yes";
            }
            return "No";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
