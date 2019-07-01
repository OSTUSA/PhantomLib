using System;
using System.Globalization;

namespace PhantomLib.Converters
{
    public class IsNullOrWhitespaceConverter : BaseOneWayConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.IsNullOrWhiteSpace(value?.ToString());
        }
    }
}
