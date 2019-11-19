using System;
using PhantomLib.CustomControls.Enums;
using Xamarin.Forms;

namespace PhantomLib.CustomControls
{
    public class UltimateEntry : Entry
    {
        public static readonly BindableProperty ImageButtonProperty = BindableProperty.Create(nameof(ImageButton), typeof(UltimateEntryImageButton), typeof(UltimateControl), UltimateEntryImageButton.None);
        public static readonly BindableProperty ShowErrorProperty = BindableProperty.Create(nameof(ShowError), typeof(bool), typeof(UltimateControl), false);
        public static readonly BindableProperty IsRoundedEntryProperty = BindableProperty.Create(nameof(IsRoundedEntry), typeof(bool), typeof(UltimateControl), false);
        public static readonly BindableProperty ThicknessPaddingProperty = BindableProperty.Create(nameof(ThicknessPadding), typeof(Thickness), typeof(UltimateControl), new Thickness(20, 10));
        public static readonly BindableProperty ReturnButtonProperty = BindableProperty.Create(nameof(ReturnButton), typeof(UltimateEntryReturn), typeof(UltimateControl), UltimateEntryReturn.Done);
        public static readonly BindableProperty NextViewProperty = BindableProperty.Create(nameof(NextView), typeof(UltimateEntry), typeof(UltimateEntry));

        public static readonly BindableProperty EntryIsFocusedProperty = BindableProperty.Create(nameof(EntryIsFocused), typeof(bool), typeof(UltimateControl), false);
        public static readonly BindableProperty UseKeyboardPlaceholderProperty = BindableProperty.Create(nameof(UseKeyboardPlaceholder), typeof(bool), typeof(UltimateControl), false);

        //colors
        public static readonly BindableProperty ErrorColorProperty = BindableProperty.Create(nameof(ErrorColor), typeof(Color), typeof(UltimateControl), Color.Red);
        public static readonly BindableProperty FocusedBorderColorProperty = BindableProperty.Create(nameof(FocusedBorderColor), typeof(Color), typeof(UltimateControl), Color.DimGray);
        public static readonly BindableProperty UnFocusedBorderColorProperty = BindableProperty.Create(nameof(UnFocusedBorderColor), typeof(Color), typeof(UltimateControl), Color.Transparent);
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

        public Color UnFocusedBorderColor
        {
            get => (Color)GetValue(UnFocusedBorderColorProperty);
            set => SetValue(UnFocusedBorderColorProperty, value);
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

        public UltimateEntry NextView
        {
            get => (UltimateEntry)GetValue(NextViewProperty);
            set
            {
                ReturnButton = UltimateEntryReturn.Next;
                SetValue(NextViewProperty, value);
            }
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

        public void EntryFocusChangedDelegate(object sender, FocusEventArgs focusEventArgs)
        {
            EntryFocusChanged?.Invoke(sender, focusEventArgs);
        }
    }
}
