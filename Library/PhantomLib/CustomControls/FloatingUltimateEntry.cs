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
        private BoxView LabelBuffer;
        private int _floatingTransitionLength;

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(string), string.Empty, BindingMode.TwoWay, null, HandleBindingPropertyChangedDelegate);

        public static readonly BindableProperty FloatingSpaceProperty = BindableProperty.Create(nameof(FloatingSpace), typeof(int), typeof(FloatingUltimateEntry), 10);
        public static readonly BindableProperty FloatingTextProperty = BindableProperty.Create(nameof(FloatingText), typeof(string), typeof(string), string.Empty, BindingMode.TwoWay, null);
        public static readonly BindableProperty FloatingTextColorProperty = BindableProperty.Create(nameof(FloatingTextColor), typeof(Color), typeof(FloatingUltimateEntry), Color.DarkGray);
        public static readonly BindableProperty FloatingTextEaseProperty = BindableProperty.Create(nameof(FloatingTextColor), typeof(Easing), typeof(FloatingUltimateEntry), Easing.Linear);

        public static readonly BindableProperty PlaceholderFontSizeProperty = BindableProperty.Create(nameof(PlaceholderFontSize), typeof(int), typeof(FloatingUltimateEntry), 18);
        public static readonly BindableProperty PlaceholderLeftMarginProperty = BindableProperty.Create(nameof(PlaceholderLeftMargin), typeof(int), typeof(FloatingUltimateEntry), 15);

        public static readonly BindableProperty TitleFontSizeProperty = BindableProperty.Create(nameof(PlaceholderFontSize), typeof(int), typeof(FloatingUltimateEntry), 14);
        public static readonly BindableProperty TitleLeftMarginProperty = BindableProperty.Create(nameof(TitleLeftMargin), typeof(int), typeof(FloatingUltimateEntry), 15);

        
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

        public int FloatingSpace
        {
            get => (int)GetValue(FloatingSpaceProperty);
            set => SetValue(FloatingSpaceProperty, value);
        }

        public int PlaceholderLeftMargin
        {
            get => (int)GetValue(PlaceholderLeftMarginProperty);
            set => SetValue(PlaceholderLeftMarginProperty, value);
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

                UltimateEntry.EntryFocusChanged -= Handle_Focus_Delegate;
                UltimateEntry.TextChanged -= UltimateEntry_TextChanged;

                UltimateEntry.EntryFocusChanged += Handle_Focus_Delegate;
                UltimateEntry.TextChanged += UltimateEntry_TextChanged;

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
            
            LabelBuffer = new BoxView
            {
                BackgroundColor = UltimateEntry.BackgroundColor,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            var gesture = new TapGestureRecognizer
            {
                NumberOfTapsRequired = 1,
                Command = new Command((obj) => Handle_Buffer_Tapped())
            };

            LabelBuffer.GestureRecognizers.Add(gesture);
            FloatingLabel.GestureRecognizers.Add(gesture);
            FloatingLabel.TranslationX = PlaceholderLeftMargin;
            FloatingLabel.FontSize = PlaceholderFontSize;
            FloatingLabel.BackgroundColor = Color.Transparent;

            FloatingLabel.Text = FloatingText;

            _floatingTransitionLength = PlaceholderFontSize + FloatingSpace;

            Render();
        }

        private void Render()
        {
            Content = null;

            var grid = new Grid { RowSpacing = 0 };

            LabelBuffer.BackgroundColor = UltimateEntry.BackgroundColor;

            UltimateEntry.IsEnabled = IsEnabled;
            FloatingLabel.IsEnabled = IsEnabled;

            grid.RowDefinitions.Add(new RowDefinition { Height = TitleFontSize+FloatingSpace});
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });

            grid.Children.Add(LabelBuffer, 0, 0);
            grid.Children.Add(UltimateEntry, 0, 1);
            grid.Children.Add(FloatingLabel, 0 ,1);

            Content = grid;
        }

        private void UltimateEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Text = e.NewTextValue;
        }

        async void Handle_Focus_Delegate(object sender, FocusEventArgs e)
        {
            LabelBuffer.BackgroundColor = UltimateEntry.BackgroundColor;
            if (string.IsNullOrEmpty(Text))
            {
                if (_ultimateEntry.EntryIsFocused)
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
                var t1 = FloatingLabel.TranslateTo(TitleLeftMargin, 0- _floatingTransitionLength, 100);
                var t2 = SizeTo(TitleFontSize);
                await Task.WhenAll(t1, t2);
            }
            else
            {
                FloatingLabel.TranslationX = TitleLeftMargin;
                FloatingLabel.TranslationY = 0 - _floatingTransitionLength;
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

        private void Handle_Buffer_Tapped()
        {
            if (_ultimateEntry.EntryIsFocused)
                UltimateEntry.Unfocus();
            else
                UltimateEntry.Focus();
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
