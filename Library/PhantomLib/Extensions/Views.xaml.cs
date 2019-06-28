using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace PhantomLib.Extensions
{
    public partial class Views
    {
        public Views()
        {
            InitializeComponent();
        }

        #region TapBackgroundColor

        /// <summary>
        /// The tap background color property.
        /// Setting this attached property will add a tap gesture recognizer
        /// that adjusts the background color of the view to the desired value.
        /// </summary>
        public static readonly BindableProperty TapBackgroundColorProperty = BindableProperty.CreateAttached("TapBackgroundColor", typeof(Color), typeof(Views), default(Color), propertyChanged: OnTapBackgroundColorPropertyChanged);
        public static Color GetTapBackgroundColor(BindableObject bindable)
        {
            return (Color)bindable.GetValue(TapBackgroundColorProperty);
        }

        public static void SetTapBackgroundColor(BindableObject bindable, Color value)
        {
            bindable.SetValue(TapBackgroundColorProperty, value);
        }

        private static void OnTapBackgroundColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is View view))
                return;

            var color = GetTapBackgroundColor(view);
            var existing = GetTapBackgroundColorTapGestureRecognizer(view);
            if (color == default(Color) && existing != null && view.GestureRecognizers.Contains(existing))
            {
                // if we do not want to change the background on selection but there
                // is an existing tap gesture recognizer, remove the recognizer
                view.GestureRecognizers.Remove(existing);
                SetTapBackgroundColorTapGestureRecognizer(view, null);
            }
            else if (color != default(Color) && existing == null)
            {
                // if we want to change the background on selection but there is
                // no existing tap gesture recognizer, add the recognizer
                var gestureRecognizer = new TapGestureRecognizer()
                {
                    Command = new Command(async (p) => await OnTap(p)),
                    CommandParameter = view
                };

                view.GestureRecognizers.Add(gestureRecognizer);
                SetTapBackgroundColorTapGestureRecognizer(view, gestureRecognizer);
            }
        }

        /// <summary>
        /// The tap background color tap gesture recognizer property.
        /// This property is private and is a helper to <see cref="TapBackgroundColorProperty"/>.
        /// This way we know which gesture recognizer is associated with the background color.
        /// </summary>
        private static readonly BindableProperty TapBackgroundColorTapGestureRecognizerProperty = BindableProperty.CreateAttached("TapBackgroundColorTapGestureRecognizer", typeof(TapGestureRecognizer), typeof(Views), default(TapGestureRecognizer));
        public static TapGestureRecognizer GetTapBackgroundColorTapGestureRecognizer(BindableObject bindable)
        {
            return (TapGestureRecognizer)bindable.GetValue(TapBackgroundColorTapGestureRecognizerProperty);
        }

        public static void SetTapBackgroundColorTapGestureRecognizer(BindableObject bindable, TapGestureRecognizer value)
        {
            bindable.SetValue(TapBackgroundColorTapGestureRecognizerProperty, value);
        }

        private static async Task OnTap(object parameter)
        {
            if (!(parameter is View view))
                return;

            var originalColor = view.BackgroundColor;
            var tapColor = GetTapBackgroundColor(view);

            // disable the view while animating happening so the animation doesn't run twice
            view.IsEnabled = false;

            await view.BackgroundColorTo(tapColor, length: 100);
            await view.BackgroundColorTo(originalColor);

            view.IsEnabled = true;
        }

        #endregion
    }
}
