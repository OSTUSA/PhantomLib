using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PhantomLib.CustomControls
{
    public class UltimateControl : ContentView
    {
        public event EventHandler Completed;

        private Label FloatingLabel;
        private BoxView LabelBuffer;
        private int _floatingTransitionLength;

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(UltimateControl), string.Empty, BindingMode.TwoWay, null, HandleBindingPropertyChangedDelegate);
        
        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(string), string.Empty, BindingMode.TwoWay, null);
        public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(UltimateControl), Color.DarkGray);
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(UltimateControl), Color.Black);
        //floating properties
        public static readonly BindableProperty IsFloatingProperty = BindableProperty.Create(nameof(IsFloating), typeof(bool), typeof(UltimateControl), true);
        public static readonly BindableProperty FloatingSpaceProperty = BindableProperty.Create(nameof(FloatingSpace), typeof(int), typeof(UltimateControl), 10);
        public static readonly BindableProperty FloatingTextEaseProperty = BindableProperty.Create(nameof(FloatingTextEase), typeof(Easing), typeof(UltimateControl), Easing.Linear);


        public static readonly BindableProperty FloatingPlaceholderFontSizeProperty = BindableProperty.Create(nameof(FloatingPlaceholderFontSize), typeof(int), typeof(UltimateControl), 18);
        public static readonly BindableProperty FloatingPlaceholderLeftMarginProperty = BindableProperty.Create(nameof(FloatingPlaceholderLeftMargin), typeof(int), typeof(UltimateControl), 15);
        public static readonly BindableProperty TitleFontSizeProperty = BindableProperty.Create(nameof(TitleFontSize), typeof(int), typeof(UltimateControl), 14);
        public static readonly BindableProperty TitleLeftMarginProperty = BindableProperty.Create(nameof(TitleLeftMargin), typeof(int), typeof(UltimateControl), 15);

        static async void HandleBindingPropertyChangedDelegate(BindableObject bindable, object oldValue, object newValue)
        {
            //handle text if its changed programatically. Dont show transition animation in this case
            if(bindable is UltimateControl ultimateControl)
            {
                if (!ultimateControl.EntryIsFocused)
                {
                    if (!string.IsNullOrEmpty((string)newValue))
                    {
                        await ultimateControl.TransitionToTitle(false);
                    }
                    else
                    {
                        await ultimateControl.TransitionToPlaceholder(false);
                    }
                }
            }
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public bool IsFloating
        {
            get => (bool)GetValue(IsFloatingProperty);
            set => SetValue(IsFloatingProperty, value);
        }

        public int FloatingSpace
        {
            get => (int)GetValue(FloatingSpaceProperty);
            set => SetValue(FloatingSpaceProperty, value);
        }

        public int FloatingPlaceholderLeftMargin
        {
            get => (int)GetValue(FloatingPlaceholderLeftMarginProperty);
            set => SetValue(FloatingPlaceholderLeftMarginProperty, value);
        }

        public int TitleLeftMargin
        {
            get => (int)GetValue(TitleLeftMarginProperty);
            set => SetValue(TitleLeftMarginProperty, value);
        }

        public int FloatingPlaceholderFontSize
        {
            get => (int)GetValue(FloatingPlaceholderFontSizeProperty);
            set => SetValue(FloatingPlaceholderFontSizeProperty, value);
        }

        public int TitleFontSize
        {
            get => (int)GetValue(TitleFontSizeProperty);
            set => SetValue(TitleFontSizeProperty, value);
        }

        public Color PlaceholderColor
        {
            get => (Color)GetValue(PlaceholderColorProperty);
            set => SetValue(PlaceholderColorProperty, value);
        }

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public Easing FloatingTextEase
        {
            get => (Easing)GetValue(FloatingTextEaseProperty);
            set => SetValue(FloatingTextEaseProperty, value);
        }

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        private UltimateEntry _ultimateEntry;
        public UltimateEntry UltimateEntryInstance
        {
            get => _ultimateEntry;
            set => _ultimateEntry = value;
        }

        public UltimateControl()
        {
            UltimateEntryInstance = new UltimateEntry(this);
            UltimateEntryInstance.TextColor = TextColor;

            FloatingLabel = new Label
            {
                TextColor = PlaceholderColor,
                VerticalOptions = LayoutOptions.Center,
            };

            LabelBuffer = new BoxView
            {
                BackgroundColor = UltimateEntryInstance.BackgroundColor,
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
            FloatingLabel.TranslationX = FloatingPlaceholderLeftMargin;
            FloatingLabel.FontSize = FloatingPlaceholderFontSize;
            FloatingLabel.BackgroundColor = Color.Transparent;

            _floatingTransitionLength = FloatingPlaceholderFontSize + FloatingSpace;

            UltimateEntryInstance.TextChanged += UltimateEntry_TextChanged;

            Render();
        }

        private void Render()
        {
            Content = null;

            var floatingHeight = 0;

            //handle placeholder and floatingHeight for grid
            if (IsFloating)
            {
                UltimateEntryInstance.Placeholder = string.Empty;
                FloatingLabel.Text = Placeholder;
                floatingHeight = TitleFontSize + FloatingSpace;
                //This is to keep color inside of rounded entry
                LabelBuffer.BackgroundColor = IsRoundedEntry
                    ? Color.Transparent
                    : UltimateEntryInstance.BackgroundColor;
            }
            else
            {
                UltimateEntryInstance.Placeholder = Placeholder;
            }

            //styling and visibility
            UltimateEntryInstance.IsEnabled = IsEnabled;
            FloatingLabel.IsEnabled = IsFloating && IsEnabled;
            FloatingLabel.IsVisible = IsFloating;

            //build grid
            var grid = new Grid { RowSpacing = 0 };

            //this rows height is 0 if !IsFloating so we just see entry
            grid.RowDefinitions.Add(new RowDefinition { Height = floatingHeight });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            grid.Children.Add(LabelBuffer, 0, 0);
            grid.Children.Add(UltimateEntryInstance, 0, 1);
            grid.Children.Add(FloatingLabel, 0, 1);

            Content = grid;
        }

        private void UltimateEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Text = e.NewTextValue;
        }

        async void Handle_Focus_Delegate(object sender, FocusEventArgs e)
        {
            LabelBuffer.BackgroundColor = UltimateEntryInstance.BackgroundColor;
            if (string.IsNullOrEmpty(Text))
            {
                if (EntryIsFocused)
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
                var t1 = FloatingLabel.TranslateTo(TitleLeftMargin, 0 - _floatingTransitionLength, 100);
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
                var t1 = FloatingLabel.TranslateTo(FloatingPlaceholderLeftMargin, 0, 100);
                var t2 = SizeTo(FloatingPlaceholderFontSize);
                await Task.WhenAll(t1, t2);
            }
            else
            {
                FloatingLabel.TranslationX = FloatingPlaceholderLeftMargin;
                FloatingLabel.TranslationY = 0;
                FloatingLabel.FontSize = FloatingPlaceholderFontSize;
            }

        }

        private void Handle_Buffer_Tapped()
        {
            if (EntryIsFocused)
                UltimateEntryInstance.Unfocus();
            else
                UltimateEntryInstance.Focus();
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

            switch (propertyName)
            {
                case nameof(Text):
                    UltimateEntryInstance.Text = Text;
                    break;
                case nameof(PlaceholderColor):
                    FloatingLabel.TextColor = PlaceholderColor;
                    UltimateEntryInstance.PlaceholderColor = PlaceholderColor;
                    break;
                case nameof(TextColor):
                    UltimateEntryInstance.TextColor = TextColor;
                    break;
                case nameof(IsFloating):
                case nameof(Placeholder):
                case nameof(IsRoundedEntry):
                    Render();
                    break;
            }
        }

        // Ultimate Entry members defined in outer scope so parent can see them too.
        //
        public enum UltimateEntryReturn
        {
            Next,
            Done,
            Search
        }
        public enum UltimateEntryImageButton
        {
            None,
            ClearContents,
            Password,
        }

        public static readonly BindableProperty ImageButtonProperty = BindableProperty.Create(nameof(ImageButton), typeof(UltimateEntryImageButton), typeof(UltimateControl), UltimateEntryImageButton.None);
        public static readonly BindableProperty ShowErrorProperty = BindableProperty.Create(nameof(ShowError), typeof(bool), typeof(UltimateControl), false);
        public static readonly BindableProperty IsRoundedEntryProperty = BindableProperty.Create(nameof(IsRoundedEntry), typeof(bool), typeof(UltimateControl), false);
        public static readonly BindableProperty ThicknessPaddingProperty = BindableProperty.Create(nameof(ThicknessPadding), typeof(Thickness), typeof(UltimateControl), new Thickness(20, 10));
        public static readonly BindableProperty ReturnButtonProperty = BindableProperty.Create(nameof(ReturnButton), typeof(UltimateEntryReturn), typeof(UltimateControl), UltimateEntryReturn.Done);
        public static readonly BindableProperty NextViewProperty = BindableProperty.Create(nameof(NextView), typeof(UltimateControl), typeof(UltimateEntry));

        public static readonly BindableProperty EntryIsFocusedProperty = BindableProperty.Create(nameof(EntryIsFocused), typeof(bool), typeof(UltimateControl), false);
        public static readonly BindableProperty UseKeyboardPlaceholderProperty = BindableProperty.Create(nameof(UseKeyboardPlaceholder), typeof(bool), typeof(UltimateControl), false);
        //colors
        public static readonly BindableProperty ErrorColorProperty = BindableProperty.Create(nameof(ErrorColor), typeof(Color), typeof(UltimateControl), Color.Red);
        public static readonly BindableProperty FocusedBorderColorProperty = BindableProperty.Create(nameof(FocusedBorderColor), typeof(Color), typeof(UltimateControl), Color.DimGray);
        public static readonly BindableProperty FocusedBackgroundColorProperty = BindableProperty.Create(nameof(FocusedBackgroundColor), typeof(Color), typeof(UltimateControl), new Color(255, 255, 255, 0.2));
        //image
        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(nameof(ImageSource), typeof(string), typeof(UltimateControl), string.Empty);
        public static readonly BindableProperty ErrorImageSourceProperty = BindableProperty.Create(nameof(ErrorImageSource), typeof(string), typeof(UltimateControl), string.Empty);
        public static readonly BindableProperty HidePasswordImageSourceProperty = BindableProperty.Create(nameof(HidePasswordImageSource), typeof(string), typeof(UltimateControl), string.Empty);
        public static readonly BindableProperty AlwaysShowImageProperty = BindableProperty.Create(nameof(AlwaysShowImage), typeof(bool), typeof(UltimateControl), true);

        public event EventHandler<FocusEventArgs> EntryFocusChanged;
        public event EventHandler<EventArgs> RightImageTouched;

        public string ImageSource
        {
            get => (string)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }
        public string ErrorImageSource
        {
            get => (string)GetValue(ErrorImageSourceProperty);
            set => SetValue(ErrorImageSourceProperty, value);
        }

        public string HidePasswordImageSource
        {
            get => (string)GetValue(HidePasswordImageSourceProperty);
            set => SetValue(HidePasswordImageSourceProperty, value);
        }

        public Thickness ThicknessPadding
        {
            get => (Thickness)GetValue(ThicknessPaddingProperty);
            set => SetValue(ThicknessPaddingProperty, value);
        }

        public bool ShowError
        {
            get => (bool)GetValue(ShowErrorProperty);
            set => SetValue(ShowErrorProperty, value);
        }

        public bool IsRoundedEntry
        {
            get => (bool)GetValue(IsRoundedEntryProperty);
            set => SetValue(IsRoundedEntryProperty, value);
        }

        public bool AlwaysShowImage
        {
            get => (bool)GetValue(AlwaysShowImageProperty);
            set => SetValue(AlwaysShowImageProperty, value);
        }

        public Color ErrorColor
        {
            get => (Color)GetValue(ErrorColorProperty);
            set => SetValue(ErrorColorProperty, value);
        }

        public Color FocusedBorderColor
        {
            get => (Color)GetValue(FocusedBorderColorProperty);
            set => SetValue(FocusedBorderColorProperty, value);
        }

        public Color FocusedBackgroundColor
        {
            get => (Color)GetValue(FocusedBackgroundColorProperty);
            set => SetValue(FocusedBackgroundColorProperty, value);
        }

        public bool EntryIsFocused
        {
            get => (bool)GetValue(EntryIsFocusedProperty);
            set => SetValue(EntryIsFocusedProperty, value);
        }

        public UltimateEntryReturn ReturnButton
        {
            get => (UltimateEntryReturn)GetValue(ReturnButtonProperty);
            set => SetValue(ReturnButtonProperty, value);
        }

        public UltimateEntryImageButton ImageButton
        {
            get => (UltimateEntryImageButton)GetValue(ImageButtonProperty);
            set => SetValue(ImageButtonProperty, value);
        }

        public UltimateControl NextView
        {
            get => (UltimateControl)GetValue(NextViewProperty);
            set => SetValue(NextViewProperty, value);
        }

        public bool UseKeyboardPlaceholder
        {
            get => (bool)GetValue(UseKeyboardPlaceholderProperty);
            set => SetValue(UseKeyboardPlaceholderProperty, value);
        }
        public void OnNext()
        {
            if (NextView != null)
            {
                NextView.UltimateEntryInstance.Focus();
            }
        }

        public class UltimateEntry : Entry
        {
            private UltimateControl _parentUltimateControl;
            public UltimateControl ParentUltimateControl
            {
                get => _parentUltimateControl;
                set => _parentUltimateControl = value;
            }

            public UltimateEntry(UltimateControl parent)
            {
                ParentUltimateControl = parent;
            }

            public void OnNextDelegate()
            {
                ParentUltimateControl.OnNext();
            }

            public void EntryFocusChangedDelegate(object sender, FocusEventArgs focusEventArgs)
            {
                ParentUltimateControl.EntryFocusChanged?.Invoke(sender, focusEventArgs);
                ParentUltimateControl.Handle_Focus_Delegate(sender, focusEventArgs);
            }

            public void RightImageTouchedDelegate(object sender, EventArgs focusEventArgs)
            {
                ParentUltimateControl.RightImageTouched?.Invoke(sender, focusEventArgs);
            }
        }
    }
}
