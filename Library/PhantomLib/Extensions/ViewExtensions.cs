using System.Linq;
using PhantomLib.Effects;
using Xamarin.Forms;

namespace PhantomLib.Extensions
{
    public class ViewExtensions
    {
        public static readonly BindableProperty KerningProperty = BindableProperty.CreateAttached(nameof(Kerning), typeof(double), typeof(ViewExtensions), default(double), propertyChanged: OnKerningChanged);

        public static void OnKerningChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as View;

            if (view == null)
                return;

            var letterSpacing = (double)newValue;
            if (letterSpacing > 0)
            {
                view.Effects.Add(new Kerning() { LetterSpacing = letterSpacing });
            }
            else
            {
                var toRemove = view.Effects.FirstOrDefault(x => x is Kerning);
                if (toRemove != null)
                {
                    view.Effects.Remove(toRemove);
                }
            }
        }

        public static double GetKerning(BindableObject bindable)
        {
            return (double)bindable.GetValue(KerningProperty);
        }
    }
}
