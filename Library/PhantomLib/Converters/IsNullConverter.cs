using System;
using System.Globalization;
using Xamarin.Forms;

namespace PhantomLib.Converters
{
    public class IsNullConverter : BaseOneWayConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null;
        }
    }
}
