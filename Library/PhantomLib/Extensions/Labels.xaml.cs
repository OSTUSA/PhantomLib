using System;
using System.Collections.Generic;
using System.Linq;
using PhantomLib.Effects;
using Xamarin.Forms;

namespace PhantomLib.Extensions
{
    public partial class Labels
    {
        public Labels()
        {
            InitializeComponent();
        }

        #region Kerning

        public static readonly BindableProperty KerningProperty = BindableProperty.CreateAttached(nameof(Kerning), typeof(double), typeof(Labels), default(double), propertyChanged: OnKerningChanged);

        public static void OnKerningChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is View view))
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

        public static void SetKerning(BindableObject bindable, double value)
        {
            bindable.SetValue(KerningProperty, value);
        }

        #endregion
    }
}
