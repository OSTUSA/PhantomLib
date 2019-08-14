using System;
using System.Collections.Generic;
using System.Linq;
using PhantomLib.Effects;
using Xamarin.Forms;

namespace PhantomLib.Extensions
{
    public partial class Images
    {
        public Images()
        {
            InitializeComponent();
        }

        #region ImageTint

        public static readonly BindableProperty ImageColorProperty = BindableProperty.CreateAttached(nameof(TintImageEffect), typeof(Color), typeof(Images), default(Color), propertyChanged: OnImageColorChanged);

        public static void OnImageColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is View view))
                return;

            var newColor = (Color)newValue;
            if (newColor != default(Color))
            {
                view.Effects.Add(new TintImageEffect() {  TintColor = newColor });
            }
            else
            {
                var toRemove = view.Effects.FirstOrDefault(x => x is TintImageEffect);
                if (toRemove != null)
                {
                    view.Effects.Remove(toRemove);
                }
            }
        }

        public static Color GetImageColor(BindableObject bindable)
        {
            return (Color)bindable.GetValue(ImageColorProperty);
        }

        public static void SetImageColor(BindableObject bindable, Color value)
        {
            bindable.SetValue(ImageColorProperty, value);
        }

        #endregion
    }
}
