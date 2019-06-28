using System;
using Xamarin.Forms;

namespace PhantomLib.CustomControls
{
    public enum RoundedEntryReturn
    {
        Next,
        Done,
        Search
    }

    public enum RoundedEntryImageButton
    {
        None,
        ClearContents,
        Password,
    }

    public class RoundedEntry : Entry
    {
        public static readonly BindableProperty ImageButtonProperty = BindableProperty.Create(nameof(ImageButtonType), typeof(RoundedEntryImageButton), typeof(RoundedEntry), RoundedEntryImageButton.None);
        public static readonly BindableProperty ReturnButtonProperty = BindableProperty.Create(nameof(ReturnButtonType), typeof(RoundedEntryReturn), typeof(RoundedEntry), RoundedEntryReturn.Done);
        public static readonly BindableProperty NextViewProperty = BindableProperty.Create(nameof(NextView), typeof(RoundedEntry), typeof(RoundedEntry));
        public static readonly BindableProperty ShowErrorProperty = BindableProperty.Create(nameof(ShowError), typeof(bool), typeof(RoundedEntry), false);
        public static readonly BindableProperty ErrorColorProperty = BindableProperty.Create(nameof(ErrorColor), typeof(Color), typeof(RoundedEntry), Color.Red);
        public static readonly BindableProperty FocusedBackgroundColorProperty = BindableProperty.Create(nameof(FocusedBackgroundColor), typeof(Color), typeof(RoundedEntry), new Color(255, 255, 255, 0.2));
        public static readonly BindableProperty FocusedBorderColorProperty = BindableProperty.Create(nameof(FocusedBackgroundColor), typeof(Color), typeof(RoundedEntry), Color.DimGray);
        public static readonly BindableProperty EntryIsFocusedProperty = BindableProperty.Create(nameof(EntryIsFocused), typeof(bool), typeof(RoundedEntry), false);
        public static readonly BindableProperty UseKeyboardPlaceholderProperty = BindableProperty.Create(nameof(UseKeyboardPlaceholder), typeof(bool), typeof(RoundedEntry), false);
        public static readonly BindableProperty RightImageSourceProperty = BindableProperty.Create(nameof(RightImageSource), typeof(string), typeof(RoundedEntry), string.Empty);
        public static readonly BindableProperty HidePasswordImageSourceProperty = BindableProperty.Create(nameof(HidePasswordImageSource), typeof(string), typeof(RoundedEntry), string.Empty);
        public static readonly BindableProperty AlwaysShowRightImageProperty = BindableProperty.Create(nameof(AlwaysShowRightImage), typeof(bool), typeof(RoundedEntry), true);

        public string RightImageSource
        {
            get => (string)GetValue(RightImageSourceProperty);
            set => SetValue(RightImageSourceProperty, value);
        }

        public string HidePasswordImageSource
        {
            get => (string)GetValue(HidePasswordImageSourceProperty);
            set => SetValue(HidePasswordImageSourceProperty, value);
        }

        public bool ShowError
        {
            get => (bool)GetValue(ShowErrorProperty);
            set => SetValue(ShowErrorProperty, value);
        }

        public bool AlwaysShowRightImage
        {
            get => (bool)GetValue(AlwaysShowRightImageProperty);
            set => SetValue(AlwaysShowRightImageProperty, value);
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

        public RoundedEntryReturn ReturnButtonType
        {
            get => (RoundedEntryReturn)GetValue(ReturnButtonProperty);
            set => SetValue(ReturnButtonProperty, value);
        }

        public RoundedEntryImageButton ImageButtonType
        {
            get => (RoundedEntryImageButton)GetValue(ImageButtonProperty);
            set => SetValue(ImageButtonProperty, value);
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
