using System;
using PhantomLib.CustomControls.Enums;
using Xamarin.Forms;

namespace PhantomLib.CustomControls
{
    public class UltimateEntry : Entry
    {
        public static readonly BindableProperty ImageButtonProperty = BindableProperty.Create(nameof(ImageButton), typeof(UltimateEntryImageButton), typeof(UltimateEntry), UltimateEntryImageButton.None);
        public static readonly BindableProperty ImageTintColorProperty = BindableProperty.Create(nameof(ImageTintColor), typeof(Color), typeof(UltimateEntry), default(Color));
        public static readonly BindableProperty ErrorImageTintColorProperty = BindableProperty.Create(nameof(ErrorImageTintColor), typeof(Color), typeof(UltimateEntry), default(Color));
        public static readonly BindableProperty UnderlineColorProperty = BindableProperty.Create(nameof(UnderlineColor), typeof(Color), typeof(UltimateEntry), default(Color));

        public static readonly BindableProperty ShowErrorProperty = BindableProperty.Create(nameof(ShowError), typeof(bool), typeof(UltimateEntry), false);
        public static readonly BindableProperty ThicknessPaddingProperty = BindableProperty.Create(nameof(ThicknessPadding), typeof(Thickness), typeof(UltimateEntry), new Thickness(20, 10));
        public static readonly BindableProperty ReturnButtonProperty = BindableProperty.Create(nameof(ReturnButton), typeof(UltimateEntryReturn), typeof(UltimateEntry), UltimateEntryReturn.Done);
        public static readonly BindableProperty NextViewProperty = BindableProperty.Create(nameof(NextView), typeof(UltimateEntry), typeof(UltimateEntry));

        public static readonly BindableProperty HideBackgroundColorProperty = BindableProperty.Create(nameof(ShowError), typeof(bool), typeof(UltimateEntry), false);
        public static readonly BindableProperty UseKeyboardPlaceholderProperty = BindableProperty.Create(nameof(UseKeyboardPlaceholder), typeof(bool), typeof(UltimateEntry), false);

        //colors
        public static readonly BindableProperty ErrorColorProperty = BindableProperty.Create(nameof(ErrorColor), typeof(Color), typeof(UltimateEntry), Color.Red);
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(UltimateEntry), default(Color));
        public static readonly BindableProperty FocusedBackgroundColorProperty = BindableProperty.Create(nameof(FocusedBackgroundColor), typeof(Color), typeof(UltimateEntry), default(Color));

        //image
        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(nameof(ImageSource), typeof(string), typeof(UltimateEntry), string.Empty);
        public static readonly BindableProperty ErrorImageSourceProperty = BindableProperty.Create(nameof(ErrorImageSource), typeof(string), typeof(UltimateEntry), string.Empty);
        public static readonly BindableProperty HidePasswordImageSourceProperty = BindableProperty.Create(nameof(HidePasswordImageSource), typeof(string), typeof(UltimateEntry), string.Empty);
        public static readonly BindableProperty AlwaysShowImageProperty = BindableProperty.Create(nameof(AlwaysShowImage), typeof(bool), typeof(UltimateEntry), true);

        public event EventHandler<FocusEventArgs> EntryFocusChanged;

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

        public bool HideBackgroundColor
        {
            get => (bool)GetValue(HideBackgroundColorProperty);
            set => SetValue(HideBackgroundColorProperty, value);
        }

        public bool ShowError
        {
            get => (bool)GetValue(ShowErrorProperty);
            set => SetValue(ShowErrorProperty, value);
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

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public Color FocusedBackgroundColor
        {
            get => (Color)GetValue(FocusedBackgroundColorProperty);
            set => SetValue(FocusedBackgroundColorProperty, value);
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

        public Color ImageTintColor
        {
            get => (Color)GetValue(ImageTintColorProperty);
            set => SetValue(ImageTintColorProperty, value);
        }

        public Color ErrorImageTintColor
        {
            get => (Color)GetValue(ErrorImageTintColorProperty);
            set => SetValue(ErrorImageTintColorProperty, value);
        }

        public Color UnderlineColor
        {
            get => (Color)GetValue(UnderlineColorProperty);
            set => SetValue(UnderlineColorProperty, value);
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
