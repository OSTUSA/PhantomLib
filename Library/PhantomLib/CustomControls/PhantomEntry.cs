using System;
using Xamarin.Forms;

namespace PhantomLib.CustomControls
{
    public enum PhantomEntryReturn
    {
        Next,
        Done,
        Search
    }

    public enum PhantomEntryImageButton
    {
        None,
        ClearContents,
        Password,
    }

    public class PhantomEntry : Entry
    {
        public static readonly BindableProperty ImageButtonProperty = BindableProperty.Create(nameof(ImageButtonType), typeof(PhantomEntryImageButton), typeof(PhantomEntry), PhantomEntryImageButton.None);
        public static readonly BindableProperty ReturnButtonProperty = BindableProperty.Create(nameof(ReturnButtonType), typeof(PhantomEntryReturn), typeof(PhantomEntry), PhantomEntryReturn.Done);
        public static readonly BindableProperty NextViewProperty = BindableProperty.Create(nameof(NextView), typeof(PhantomEntry), typeof(PhantomEntry));
        public static readonly BindableProperty ShowErrorProperty = BindableProperty.Create(nameof(ShowError), typeof(bool), typeof(PhantomEntry), false);
        public static readonly BindableProperty ErrorColorProperty = BindableProperty.Create(nameof(ErrorColor), typeof(Color), typeof(PhantomEntry), Color.Red);
        public static readonly BindableProperty FocusedBackgroundColorProperty = BindableProperty.Create(nameof(FocusedBackgroundColor), typeof(Color), typeof(PhantomEntry), new Color(255, 255, 255, 0.2));
        public static readonly BindableProperty FocusedBorderColorProperty = BindableProperty.Create(nameof(FocusedBackgroundColor), typeof(Color), typeof(PhantomEntry), Color.DimGray);
        public static readonly BindableProperty EntryIsFocusedProperty = BindableProperty.Create(nameof(EntryIsFocused), typeof(bool), typeof(PhantomEntry), false);
        public static readonly BindableProperty UseKeyboardPlaceholderProperty = BindableProperty.Create(nameof(UseKeyboardPlaceholder), typeof(bool), typeof(PhantomEntry), false);
        public static readonly BindableProperty RightImageSourceProperty = BindableProperty.Create(nameof(RightImageSource), typeof(string), typeof(PhantomEntry), string.Empty);
        public static readonly BindableProperty HidePasswordImageSourceProperty = BindableProperty.Create(nameof(HidePasswordImageSource), typeof(string), typeof(PhantomEntry), string.Empty);
        public static readonly BindableProperty AlwaysShowRightImageProperty = BindableProperty.Create(nameof(AlwaysShowRightImage), typeof(bool), typeof(PhantomEntry), true);

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

        public PhantomEntryReturn ReturnButtonType
        {
            get => (PhantomEntryReturn)GetValue(ReturnButtonProperty);
            set => SetValue(ReturnButtonProperty, value);
        }

        public PhantomEntryImageButton ImageButtonType
        {
            get => (PhantomEntryImageButton)GetValue(ImageButtonProperty);
            set => SetValue(ImageButtonProperty, value);
        }

        public PhantomEntry NextView
        {
            get => (PhantomEntry)GetValue(NextViewProperty);
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
