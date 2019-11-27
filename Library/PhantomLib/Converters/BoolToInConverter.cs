using System;
using System.Globalization;

namespace PhantomLib.Converters
{
    public class BoolToInConverter : BaseOneWayConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool input)
            {
                return input == true ? 1 : 0;
            }

            throw new ArgumentException("Value must be of type bool");
        }
    }
}
