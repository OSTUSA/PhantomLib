using System;
using System.Globalization;

namespace PhantomLib.Converters
{
    public class ToUpperConverter : BaseOneWayConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString()?.ToUpper(culture);
        }
    }
}
