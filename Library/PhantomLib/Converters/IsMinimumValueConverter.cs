using System;
using System.Globalization;

namespace PhantomLib.Converters
{
    public class IsMinimumValueConverter : BaseOneWayConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // try parsing minValue from the ConverterParameter
            // otherwise, this will defualt to 0
            int.TryParse(parameter?.ToString(), out int minValue);

            if (!(value is int intValue))
                return false;

            return intValue >= minValue;
        }
    }
}
