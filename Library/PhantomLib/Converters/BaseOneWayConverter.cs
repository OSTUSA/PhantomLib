using System;
using System.Globalization;
using Xamarin.Forms;

namespace PhantomLib.Converters
{
    public abstract class BaseOneWayConverter : IValueConverter
    {
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException($"{GetType().Name} is a one-way converter");
        }
    }
}
