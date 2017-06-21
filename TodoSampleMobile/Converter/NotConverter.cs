using System;
using System.Globalization;
using Xamarin.Forms;

namespace TodoSampleMobile.Converter
{
    public class NotConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
               var eval = (bool) value;
                return !eval;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
