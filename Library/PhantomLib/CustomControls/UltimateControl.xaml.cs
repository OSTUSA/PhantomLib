using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PhantomLib.CustomControls
{
    public partial class UltimateControl : Grid
    {
        public static readonly BindableProperty EntryIsFocusedProperty = BindableProperty.Create(nameof(EntryIsFocused), typeof(bool), typeof(UltimateControl), false);
        public static readonly BindableProperty EntryTextProperty = BindableProperty.Create(nameof(EntryText), typeof(string), typeof(UltimateControl), string.Empty);

        public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(UltimateControl), Color.DarkGray);
        public static readonly BindableProperty PlaceholderTextProperty = BindableProperty.Create(nameof(PlaceholderText), typeof(string), typeof(UltimateControl), string.Empty);
        public static readonly BindableProperty IsPlaceholderVisibleProperty = BindableProperty.Create(nameof(IsPlaceholderVisible), typeof(bool), typeof(UltimateControl), true);

        // Determines whether or not we will float the placeholder above the entry
        public static readonly BindableProperty ShouldFloatProperty = BindableProperty.Create(nameof(ShouldFloat), typeof(bool), typeof(UltimateControl), false);

        public static readonly BindableProperty IsFloatingProperty = BindableProperty.Create(nameof(IsFloating), typeof(bool), typeof(UltimateControl), false);

        // These properties modify the floating text that is placed above the entry field
        public static readonly BindableProperty FloatingPlaceholderFontSizeProperty = BindableProperty.Create(nameof(FloatingPlaceholderFontSize), typeof(int), typeof(UltimateControl), 18);
        public static readonly BindableProperty FloatingTextEaseProperty = BindableProperty.Create(nameof(FloatingTextEase), typeof(Easing), typeof(UltimateControl), Easing.Linear);
        public static readonly BindableProperty FloatingSpaceProperty = BindableProperty.Create(nameof(FloatingSpace), typeof(int), typeof(UltimateControl), 20);
        public static readonly BindableProperty FloatingPlaceholderLeftMarginProperty = BindableProperty.Create(nameof(FloatingPlaceholderLeftMargin), typeof(int), typeof(UltimateControl), 15);

        // These properties modify the placeholder text this is displayed inside the entry field
        public static readonly BindableProperty TitleLeftMarginProperty = BindableProperty.Create(nameof(TitleLeftMargin), typeof(int), typeof(UltimateControl), 15);
        public static readonly BindableProperty TitleFontSizeProperty = BindableProperty.Create(nameof(TitleFontSize), typeof(int), typeof(UltimateControl), 14);

        public UltimateControl()
        {
            InitializeComponent();

            FloatingLabel.TranslationX = FloatingPlaceholderLeftMargin;
        }

        public Color PlaceholderColor
        {
            get => (Color)GetValue(PlaceholderColorProperty);
            set => SetValue(PlaceholderColorProperty, value);
        }

        public string PlaceholderText
        {
            get => (string)GetValue(PlaceholderTextProperty);
            set => SetValue(PlaceholderTextProperty, value);
        }

        public bool IsPlaceholderVisible
        {
            get => (bool)GetValue(IsPlaceholderVisibleProperty);
            set => SetValue(IsPlaceholderVisibleProperty, value);
        }

        public bool ShouldFloat
        {
            get => (bool)GetValue(ShouldFloatProperty);
            set => SetValue(ShouldFloatProperty, value);
        }

        public bool IsFloating
        {
            get => (bool)GetValue(IsFloatingProperty);
            set => SetValue(IsFloatingProperty, value);
        }

        public int FloatingPlaceholderFontSize
        {
            get => (int)GetValue(FloatingPlaceholderFontSizeProperty);
            set => SetValue(FloatingPlaceholderFontSizeProperty, value);
        }

        public Easing FloatingTextEase
        {
            get => (Easing)GetValue(FloatingTextEaseProperty);
            set => SetValue(FloatingTextEaseProperty, value);
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

        public int TitleFontSize
        {
            get => (int)GetValue(TitleFontSizeProperty);
            set => SetValue(TitleFontSizeProperty, value);
        }

        public int TitleLeftMargin
        {
            get => (int)GetValue(TitleLeftMarginProperty);
            set => SetValue(TitleLeftMarginProperty, value);
        }

        public string EntryText
        {
            get => (string)GetValue(EntryTextProperty);
            set
            {
                SetValue(EntryTextProperty, value);
                IsPlaceholderVisible = GetPlaceholderVisibility();

                if (!IsFloating && ShouldFloat && !string.IsNullOrEmpty(EntryText))
                {
                    AnimatePlaceholder();
                }
            }
        }

        public bool EntryIsFocused
        {
            get => (bool)GetValue(EntryIsFocusedProperty);
            set 
            {
                SetValue(EntryIsFocusedProperty, value);

                if (ShouldFloat)
                {
                    AnimatePlaceholder();
                }
            }
        }

        private bool GetPlaceholderVisibility() => !ShouldFloat && !string.IsNullOrEmpty(EntryText) ? false : true;

        private void AnimatePlaceholder()
        {
            if (EntryIsFocused || !string.IsNullOrEmpty(EntryText))
            {
                TransitionToTitle();
                IsFloating = true; 
            }
            else if (string.IsNullOrEmpty(EntryText))
            {
                TransitionToPlaceholder();
                IsFloating = false;
            }
        }

        private async Task TransitionToTitle()
        {
            int floatingTransitionLength = FloatingPlaceholderFontSize + FloatingSpace;

            var textTranslationAnimation = FloatingLabel.TranslateTo(TitleLeftMargin, 0 - floatingTransitionLength, 100);
            var textResizeAnimation = SizeTo(TitleFontSize);
            await Task.WhenAll(textTranslationAnimation, textResizeAnimation);
        }

        private async Task TransitionToPlaceholder()
        {
            var t1 = FloatingLabel.TranslateTo(FloatingPlaceholderLeftMargin, 0, 100);
            var t2 = SizeTo(FloatingPlaceholderFontSize);
            await Task.WhenAll(t1, t2);
        }

        private Task SizeTo(int fontSize)
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();

            // setup information for animation
            void callback(double input) { FloatingLabel.FontSize = input; }
            double startingHeight = FloatingLabel.FontSize;
            double endingHeight = fontSize;
            uint rate = 5;
            uint length = 100;
            Easing easing = FloatingTextEase;

            // now start animation with all the setup information
            FloatingLabel.Animate("invis", callback, startingHeight, endingHeight, rate, length, easing, (v, c) => taskCompletionSource.SetResult(c));

            return taskCompletionSource.Task;
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (this != null && Children != null && Children.Count == 3)
            {
                if (Children[1] is Label placeholderLabel && placeholderLabel.Parent is Grid grid)
                {
                    // Sends the placeholder to the top of the stack so it is visible on top of the entry field
                    // Collection must be updated on the main
                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() => RaiseChild(placeholderLabel));
                }
            }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == EntryIsFocusedProperty.PropertyName)
            {
                EntryIsFocused = EntryIsFocused;
            }
            else if (propertyName == EntryTextProperty.PropertyName)
            {
                EntryText = EntryText;
            }
        }
    }
}
