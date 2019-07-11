using System;
using Xamarin.Forms;

namespace PhantomLib.CustomControls
{
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

    public class UltimateEntry : Entry
    {
        public static readonly BindableProperty ImageButtonProperty = BindableProperty.Create(nameof(ImageButtonType), typeof(UltimateEntryImageButton), typeof(UltimateEntry), UltimateEntryImageButton.None);
        public static readonly BindableProperty ReturnButtonProperty = BindableProperty.Create(nameof(ReturnButtonType), typeof(UltimateEntryReturn), typeof(UltimateEntry), UltimateEntryReturn.Done);
        public static readonly BindableProperty NextViewProperty = BindableProperty.Create(nameof(NextView), typeof(UltimateEntry), typeof(UltimateEntry));
        public static readonly BindableProperty ShowErrorProperty = BindableProperty.Create(nameof(ShowError), typeof(bool), typeof(UltimateEntry), false);
        public static readonly BindableProperty ErrorColorProperty = BindableProperty.Create(nameof(ErrorColor), typeof(Color), typeof(UltimateEntry), Color.Red);
        public static readonly BindableProperty FocusedBackgroundColorProperty = BindableProperty.Create(nameof(FocusedBackgroundColor), typeof(Color), typeof(UltimateEntry), new Color(255, 255, 255, 0.2));
        public static readonly BindableProperty FocusedBorderColorProperty = BindableProperty.Create(nameof(FocusedBorderColor), typeof(Color), typeof(UltimateEntry), Color.DimGray);
        public static readonly BindableProperty EntryIsFocusedProperty = BindableProperty.Create(nameof(EntryIsFocused), typeof(bool), typeof(UltimateEntry), false);
        public static readonly BindableProperty UseKeyboardPlaceholderProperty = BindableProperty.Create(nameof(UseKeyboardPlaceholder), typeof(bool), typeof(UltimateEntry), false);
        public static readonly BindableProperty RightImageSourceProperty = BindableProperty.Create(nameof(RightImageSource), typeof(string), typeof(UltimateEntry), string.Empty);
        public static readonly BindableProperty HidePasswordImageSourceProperty = BindableProperty.Create(nameof(HidePasswordImageSource), typeof(string), typeof(UltimateEntry), string.Empty);
        public static readonly BindableProperty AlwaysShowRightImageProperty = BindableProperty.Create(nameof(AlwaysShowRightImage), typeof(bool), typeof(UltimateEntry), true);
        public static readonly BindableProperty IsRoundedEntryProperty = BindableProperty.Create(nameof(IsRoundedEntry), typeof(bool), typeof(UltimateEntry), false);
        public static readonly BindableProperty ThicknessPaddingProperty = BindableProperty.Create(nameof(ThicknessPadding), typeof(Thickness), typeof(UltimateEntry), new Thickness(20,10));

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

        public UltimateEntryReturn ReturnButtonType
        {
            get => (UltimateEntryReturn)GetValue(ReturnButtonProperty);
            set => SetValue(ReturnButtonProperty, value);
        }

        public UltimateEntryImageButton ImageButtonType
        {
            get => (UltimateEntryImageButton)GetValue(ImageButtonProperty);
            set => SetValue(ImageButtonProperty, value);
        }

        public UltimateEntry NextView
        {
            get => (UltimateEntry)GetValue(NextViewProperty);
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
