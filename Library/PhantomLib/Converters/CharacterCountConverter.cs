using System;
using System.Globalization;

namespace PhantomLib.Converters
{
    public class CharacterCountConverter : BaseOneWayConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string valueStr))
                return 0;

            return valueStr.Length;
        }
    }
}
