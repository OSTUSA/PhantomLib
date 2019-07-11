using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PhantomLib.CustomControls
{
    public class FloatingUltimateEntry : ContentView
    {
        public event EventHandler Completed;

        private Label FloatingLabel;

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(string), string.Empty, BindingMode.TwoWay, null, HandleBindingPropertyChangedDelegate);

        public static readonly BindableProperty FloatingTranslationLengthProperty = BindableProperty.Create(nameof(FloatingTranslationLength), typeof(int), typeof(FloatingUltimateEntry), 32);
        public static readonly BindableProperty FloatingTextProperty = BindableProperty.Create(nameof(FloatingText), typeof(string), typeof(string), string.Empty, BindingMode.TwoWay, null);
        public static readonly BindableProperty FloatingTextColorProperty = BindableProperty.Create(nameof(FloatingTextColor), typeof(Color), typeof(FloatingUltimateEntry), Color.Black);
        public static readonly BindableProperty FloatingTextEaseProperty = BindableProperty.Create(nameof(FloatingTextColor), typeof(Easing), typeof(FloatingUltimateEntry), Easing.Linear);

        public static readonly BindableProperty PlaceholderFontSizeProperty = BindableProperty.Create(nameof(PlaceholderFontSize), typeof(int), typeof(FloatingUltimateEntry), 18);
        public static readonly BindableProperty PlaceholderLeftMarginProperty = BindableProperty.Create(nameof(PlaceholderLeftMargin), typeof(int), typeof(FloatingUltimateEntry), 10);

        public static readonly BindableProperty TitleFontSizeProperty = BindableProperty.Create(nameof(PlaceholderFontSize), typeof(int), typeof(FloatingUltimateEntry), 14);
        public static readonly BindableProperty TitleLeftMarginProperty = BindableProperty.Create(nameof(TitleLeftMargin), typeof(int), typeof(FloatingUltimateEntry), 10);

        
        static async void HandleBindingPropertyChangedDelegate(BindableObject bindable, object oldValue, object newValue)
        {
            //handle text if its changed
            var control = bindable as FloatingUltimateEntry;
            if (!control.UltimateEntry.EntryIsFocused)
            {
                if (!string.IsNullOrEmpty((string)newValue))
                {
                   await control.TransitionToTitle(false);
                }
                else
                {
                    await control.TransitionToPlaceholder(false);
                }
            }
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public int FloatingTranslationLength
        {
            get => (int)GetValue(PlaceholderLeftMarginProperty);
            set => SetValue(PlaceholderLeftMarginProperty, value);
        }

        public int PlaceholderLeftMargin
        {
            get => (int)GetValue(FloatingTranslationLengthProperty);
            set => SetValue(FloatingTranslationLengthProperty, value);
        }

        public int TitleLeftMargin
        {
            get => (int)GetValue(TitleLeftMarginProperty);
            set => SetValue(TitleLeftMarginProperty, value);
        }

        public int PlaceholderFontSize
        {
            get => (int)GetValue(PlaceholderFontSizeProperty);
            set => SetValue(PlaceholderFontSizeProperty, value);
        }

        public int TitleFontSize
        {
            get => (int)GetValue(TitleFontSizeProperty);
            set => SetValue(TitleFontSizeProperty, value);
        }

        public Color FloatingTextColor
        {
            get => (Color)GetValue(FloatingTextColorProperty);
            set => SetValue(FloatingTextColorProperty, value);
        }

        public Easing FloatingTextEase
        {
            get => (Easing)GetValue(FloatingTextEaseProperty);
            set => SetValue(FloatingTextEaseProperty, value);
        }

        public string FloatingText
        {
            get => (string)GetValue(FloatingTextProperty);
            set => SetValue(FloatingTextProperty, value);
        }

        private UltimateEntry _ultimateEntry = new UltimateEntry();
        public UltimateEntry UltimateEntry
        {
            get => _ultimateEntry;
            set
            {
                _ultimateEntry = value;

                //get whatever height was defined and use that.
                //Use FloatingUltimateEntry first if it was defined
                if (HeightRequest > 0)
                    _ultimateEntry.HeightRequest = this.HeightRequest;
                else if ((int)_ultimateEntry.HeightRequest > 0)
                    HeightRequest = _ultimateEntry.HeightRequest;

                Render();
            }
        }

        public FloatingUltimateEntry()
        {
            FloatingLabel = new Label
            {
                TextColor = FloatingTextColor,
                VerticalOptions = LayoutOptions.Center,
            };
            FloatingLabel.GestureRecognizers.Add(new TapGestureRecognizer
            {
                NumberOfTapsRequired = 1,
                Command = new Command((obj) => Handle_Tapped())
            });

            FloatingLabel.TranslationX = PlaceholderLeftMargin;
            FloatingLabel.FontSize = PlaceholderFontSize;
            FloatingLabel.BackgroundColor = Color.Transparent;

            FloatingLabel.Text = FloatingText;

            Render();
        }

        private void Render()
        {
            Content = null;
            var grid = new Grid();
            UltimateEntry.IsEnabled = IsEnabled;
            FloatingLabel.IsEnabled = IsEnabled;
            grid.Children.Add(UltimateEntry);
            grid.Children.Add(FloatingLabel);
            Content = grid;

            UltimateEntry.EntryFocusChanged += Handle_Focus;
            UltimateEntry.TextChanged += UltimateEntry_TextChanged;
        }

        private void UltimateEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Text = e.NewTextValue;
        }

        async void Handle_Focus(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(Text))
            {
                if (e.IsFocused)
                {
                    await TransitionToTitle(true);
                }
                else
                {
                    await TransitionToPlaceholder(true);
                }
            }
        }

        async Task TransitionToTitle(bool animated)
        {
            if (animated)
            {
                var t1 = FloatingLabel.TranslateTo(TitleLeftMargin, 0-FloatingTranslationLength, 100);
                var t2 = SizeTo(TitleFontSize);
                await Task.WhenAll(t1, t2);
            }
            else
            {
                FloatingLabel.TranslationX = TitleLeftMargin;
                FloatingLabel.TranslationY = 0 - FloatingTranslationLength;
                FloatingLabel.FontSize = TitleFontSize;
            }
        }

        async Task TransitionToPlaceholder(bool animated)
        {
            if (animated)
            {
                var t1 = FloatingLabel.TranslateTo(PlaceholderLeftMargin, 0, 100);
                var t2 = SizeTo(PlaceholderFontSize);
                await Task.WhenAll(t1, t2);
            }
            else
            {
                FloatingLabel.TranslationX = PlaceholderLeftMargin;
                FloatingLabel.TranslationY = 0;
                FloatingLabel.FontSize = PlaceholderFontSize;
            }
        }

        void Handle_Tapped()
        {
            if (IsEnabled)
            {
                UltimateEntry.Focus();
            }
        }

        Task SizeTo(int fontSize)
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();

            // setup information for animation
            Action<double> callback = input => { FloatingLabel.FontSize = input; };
            double startingHeight = FloatingLabel.FontSize;
            double endingHeight = fontSize;
            uint rate = 5;
            uint length = 100;
            Easing easing = FloatingTextEase;

            // now start animation with all the setup information
            FloatingLabel.Animate("invis", callback, startingHeight, endingHeight, rate, length, easing, (v, c) => taskCompletionSource.SetResult(c));

            return taskCompletionSource.Task;
        }

        void Handle_Completed(object sender, EventArgs e)
        {
            Completed?.Invoke(this, e);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == nameof(FloatingText))
            {
                FloatingLabel.Text = FloatingText;
            }
            else if (propertyName == nameof(FloatingTextColor))
            {
                FloatingLabel.TextColor = FloatingTextColor;
            }
        }
    }
}
