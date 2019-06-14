using System;
using Xamarin.Forms;

namespace PhantomLib.CustomControls
{
    public enum RoundedEntryReturnType
    {
        Next,
        Done,
        Search
    }

    public class RoundedEntry : Entry
    {
        public static readonly BindableProperty StrokeColorProperty = BindableProperty.Create("StrokeColor", typeof(Color), typeof(RoundedEntry), Color.Transparent);
        public static readonly BindableProperty ReturnButtonProperty = BindableProperty.Create("ReturnButton", typeof(RoundedEntryReturnType), typeof(RoundedEntry), RoundedEntryReturnType.Done);
        public static readonly BindableProperty NextViewProperty = BindableProperty.Create("NextView", typeof(RoundedEntry), typeof(RoundedEntry));
        public static readonly BindableProperty ShowErrorProperty = BindableProperty.Create("ShowError", typeof(bool), typeof(RoundedEntry), false);
        public static readonly BindableProperty ErrorColorProperty = BindableProperty.Create("ErrorColor", typeof(Color), typeof(RoundedEntry), Color.Red);
        public static readonly BindableProperty FocusedBackgroundColorProperty = BindableProperty.Create("FocusedBackgroundColor", typeof(Color), typeof(RoundedEntry), new Color(255,255,255,0.2));
        public static readonly BindableProperty FocusedBorderColorProperty = BindableProperty.Create("FocusedBackgroundColor", typeof(Color), typeof(RoundedEntry), Color.DimGray);
        public static readonly BindableProperty EntryIsFocusedProperty = BindableProperty.Create("EntryIsFocused", typeof(bool), typeof(RoundedEntry), false);
        public static readonly BindableProperty UseKeyboardPlaceholderProperty = BindableProperty.Create("UseKeyboardPlaceholder", typeof(bool), typeof(RoundedEntry), false);
        public static readonly BindableProperty RightImageSourceProperty = BindableProperty.Create(nameof(RightImageSource), typeof(string), typeof(RoundedEntry), string.Empty);
        public static readonly BindableProperty ShouldClearTextOnClickProperty = BindableProperty.Create(nameof(ShouldClearTextOnClick), typeof(bool), typeof(RoundedEntry), false);


        public string RightImageSource
        {
            get => (string)GetValue(RightImageSourceProperty);
            set => SetValue(RightImageSourceProperty, value);
        }

        public bool ShowError
        {
            get => (bool)GetValue(ShowErrorProperty);
            set => SetValue(ShowErrorProperty, value);
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

        public bool ShouldClearTextOnClick
        {
            get => (bool)GetValue(ShouldClearTextOnClickProperty);
            set => SetValue(ShouldClearTextOnClickProperty, value);
        }

        public RoundedEntryReturnType ReturnButton
        {
            get => (RoundedEntryReturnType)GetValue(ReturnButtonProperty);
            set => SetValue(ReturnButtonProperty, value);
        }

        public RoundedEntry NextView
        {
            get => (RoundedEntry)GetValue(NextViewProperty);
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
                NextView.Focus();
            }
        }

        public event EventHandler<FocusEventArgs> EntryFocusChanged;

        public void EntryFocusChangedDelegate(object sender, FocusEventArgs focusEventArgs)
        {
            if (EntryFocusChanged != null)
            {
                EntryFocusChanged.Invoke(sender, focusEventArgs);
            }
        }

        public event EventHandler<EventArgs> RightImageTouched;

        public void RightImageTouchedDelegate(object sender, EventArgs focusEventArgs)
        {
            if (RightImageTouched != null)
            {
                RightImageTouched.Invoke(sender, focusEventArgs);
            }
        }
    }

}
