using System;
using System.Globalization;
using Xamarin.Forms;

namespace TodoSampleMobile.Converter
{
    public class OkEditImageSourceConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var eval = (bool) value;
            if (eval)
            {
                return "ok.png";
            }
            return "pen.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
