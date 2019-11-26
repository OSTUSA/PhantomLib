using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PhantomLib.CustomControls
{
    public partial class FloatingLabel : Grid
    {
        // Attached property to map an ultimate entry to this control.
        public static BindableProperty AttachedEntryProperty =
            BindableProperty.CreateAttached("AttachedEntry", typeof(FloatingLabel), typeof(Nullable), null, propertyChanged: HandleEntryChanged);

        public static FloatingLabel GetAttachedEntry(BindableObject view)
        {
            return (FloatingLabel)view.GetValue(AttachedEntryProperty);
        }

        public static void SetAttachedEntry(BindableObject view, UltimateEntry entry)
        {
            view.SetValue(AttachedEntryProperty, entry);
        }

        static void HandleEntryChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue is FloatingLabel uc)
            {
                uc.UltimateEntry = (UltimateEntry)bindable;
                uc.UltimateEntry.FocusedBackgroundColor = uc.FocusedBackgroundColor;
                uc.UltimateEntry.BackgroundColor = uc.BackgroundColor;
                uc.UltimateEntry.HideBackgroundColor = true;
            }
        }

        private UltimateEntry _ultimateEntry;
        public UltimateEntry UltimateEntry
        {
            get => _ultimateEntry;
            set
            {
                _ultimateEntry = value;

                // Check to see if the entry already has text set. If so,
                // we need to float the placeholder.
                if (!string.IsNullOrEmpty(value.Text))
                {
                    EntryIsFocused = false;
                    AnimatePlaceholder(_ultimateEntry);
                }

                _ultimateEntry.EntryFocusChanged += _ultimateEntry_EntryFocusChanged;

                // Set the left on ThicknessPadding so that the text in the entry is always
                // left aligned to the floating label.
                _ultimateEntry.ThicknessPadding = new Thickness(this.FloatingLeftMargin, _ultimateEntry.ThicknessPadding.Top, _ultimateEntry.ThicknessPadding.Right, _ultimateEntry.ThicknessPadding.Bottom);
            }
        }

        private void _ultimateEntry_EntryFocusChanged(object sender, FocusEventArgs e)
        {
            EntryIsFocused = e.IsFocused;
            AnimatePlaceholder((UltimateEntry)sender);
        }

        public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(FloatingLabel), Color.DarkGray);
        public static readonly BindableProperty PlaceholderTextProperty = BindableProperty.Create(nameof(PlaceholderText), typeof(string), typeof(FloatingLabel), string.Empty);

        public static readonly BindableProperty FocusedBackgroundColorProperty = BindableProperty.Create(nameof(FocusedBackgroundColor), typeof(Color), typeof(FloatingLabel), Color.Transparent);
        public static readonly BindableProperty UnFocusedBackgroundColorProperty = BindableProperty.Create(nameof(UnFocusedBackgroundColor), typeof(Color?), typeof(FloatingLabel), null);

        public static readonly BindableProperty IsFloatingProperty = BindableProperty.Create(nameof(IsFloating), typeof(bool), typeof(FloatingLabel), false);

        // These properties modify the floating text that is placed above the entry field
        public static readonly BindableProperty FloatingPlaceholderFontSizeProperty = BindableProperty.Create(nameof(FloatingPlaceholderFontSize), typeof(int), typeof(FloatingLabel), 18);
        public static readonly BindableProperty FloatingTextEaseProperty = BindableProperty.Create(nameof(FloatingTextEase), typeof(Easing), typeof(FloatingLabel), Easing.Linear);

        /// <summary>
        /// How much vertical space should be allocated for the floating label.
        /// </summary>
        public static readonly BindableProperty FloatingSpaceProperty = BindableProperty.Create(nameof(FloatingSpace), typeof(int), typeof(FloatingLabel), 20);
        
        // These properties modify the floating label
        // TODO Change over to ThicknessPadding property
        public static readonly BindableProperty FloatingTopMarginProperty = BindableProperty.Create(nameof(FloatingTopMargin), typeof(int), typeof(FloatingLabel), 4);
        public static readonly BindableProperty FloatingLeftMarginProperty = BindableProperty.Create(nameof(FloatingLeftMargin), typeof(int), typeof(FloatingLabel), 0);
        public static readonly BindableProperty FloatingFontSizeProperty = BindableProperty.Create(nameof(FloatingFontSize), typeof(int), typeof(FloatingLabel), 14);

        public FloatingLabel()
        {
            InitializeComponent();

            Label.TranslationX = FloatingLeftMargin;
        }

        public Color FocusedBackgroundColor
        {
            get => (Color)GetValue(FocusedBackgroundColorProperty);
            set => SetValue(FocusedBackgroundColorProperty, value);
        }

        public Color? UnFocusedBackgroundColor
        {
            get => (Color?)GetValue(UnFocusedBackgroundColorProperty);
            set => SetValue(UnFocusedBackgroundColorProperty, value);
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

        public int FloatingFontSize
        {
            get => (int)GetValue(FloatingFontSizeProperty);
            set => SetValue(FloatingFontSizeProperty, value);
        }

        public int FloatingLeftMargin
        {
            get => (int)GetValue(FloatingLeftMarginProperty);
            set => SetValue(FloatingLeftMarginProperty, value);
        }

        public int FloatingTopMargin
        {
            get => (int)GetValue(FloatingTopMarginProperty);
            set => SetValue(FloatingTopMarginProperty, value);
        }

        private bool _entryIsFocused;
        public bool EntryIsFocused
        {
            get => _entryIsFocused;
            set
            {
                _entryIsFocused = value;
                BackgroundColor = _entryIsFocused ? FocusedBackgroundColor : UnFocusedBackgroundColor ?? Color.Transparent;

                // Set the background color to transparent so that it doesn't
                // interfere with the FloatingLabel BackgroundColor.
                UltimateEntry.BackgroundColor = Color.Transparent;
            }
        }

        private void AnimatePlaceholder(UltimateEntry entry)
        {
            if (EntryIsFocused || !string.IsNullOrEmpty(UltimateEntry?.Text))
            {
                TransitionToFloating();
                IsFloating = true;
            }
            else if (string.IsNullOrEmpty(entry?.Text))
            {
                TransitionToPlaceholder();
                IsFloating = false;
            }
        }

        private async Task TransitionToFloating()
        {
            int floatingTransitionHeight = FloatingPlaceholderFontSize - FloatingTopMargin;

            var textTranslationAnimation = Label.TranslateTo(FloatingLeftMargin, 0 - floatingTransitionHeight); 
            var textResizeAnimation = SizeTo(FloatingFontSize);
            await Task.WhenAll(textTranslationAnimation, textResizeAnimation);
            
        }

        private async Task TransitionToPlaceholder(bool animated = true)
        {
            var transition1 = Label.TranslateTo(FloatingLeftMargin, 0);
            var transition2 = SizeTo(FloatingPlaceholderFontSize);
            await Task.WhenAll(transition1, transition2);
        }

        private Task SizeTo(int fontSize)
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();

            // setup information for animation
            void callback(double input) { Label.FontSize = input; }
            double startingHeight = Label.FontSize;
            double endingHeight = fontSize;
            uint rate = 5;
            uint length = 250;
            Easing easing = FloatingTextEase;

            // now start animation with all the setup information
            Label.Animate("invis", callback, startingHeight, endingHeight, rate, length, easing, (v, c) => taskCompletionSource.SetResult(c));

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
                    Device.BeginInvokeOnMainThread(() => RaiseChild(placeholderLabel));
                }
            }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(BackgroundColor) && UnFocusedBackgroundColor == null)
            {
                UnFocusedBackgroundColor = BackgroundColor;
            }
        }
    }
}
