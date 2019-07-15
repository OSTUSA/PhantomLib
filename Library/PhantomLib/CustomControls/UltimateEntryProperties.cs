using System;
using System.ComponentModel;
using Xamarin.Forms;
using static Xamarin.Forms.BindableProperty;

namespace PhantomLib.CustomControls
{
    public static class UltimateEntryProperties
    {
        //Abstract the properties so that the FloatingUltimateEntry and UltimateEntry reflect same BindableProperties and
        //so FloatingUltimateEntry can accept UltimateEntry in xaml
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

        public static readonly BindableProperty RightImageSourceProperty = CreateAttached(nameof(UltimateEntry.RightImageSource), typeof(string), typeof(UltimateEntry), string.Empty);
        public static string GetRightImageSource(Element element) => (string)element.GetValue(RightImageSourceProperty);
        public static void SetRightImageSource(Element element, string value) => element.SetValue(RightImageSourceProperty, value);

        public static readonly BindableProperty ImageButtonProperty = CreateAttached(nameof(UltimateEntry.ImageButtonType), typeof(UltimateEntryImageButton), typeof(UltimateEntry), UltimateEntryImageButton.None);
        public static UltimateEntryImageButton GetImageButtonType(Element element) => (UltimateEntryImageButton)element.GetValue(ImageButtonProperty);
        public static void SetImageButtonType(Element element, UltimateEntryImageButton value) => element.SetValue(ImageButtonProperty, value);

        public static readonly BindableProperty ReturnButtonProperty = CreateAttached(nameof(UltimateEntry.ReturnButtonType), typeof(UltimateEntryReturn), typeof(UltimateEntry), UltimateEntryReturn.Done);
        public static UltimateEntryReturn GetReturnButton(Element element) => (UltimateEntryReturn)element.GetValue(ReturnButtonProperty);
        public static void SetReturnButton(Element element, UltimateEntryReturn value) => element.SetValue(ReturnButtonProperty, value);

        public static readonly BindableProperty NextViewProperty = CreateAttached(nameof(UltimateEntry.NextView), typeof(UltimateEntry), typeof(UltimateEntry), null);
        public static UltimateEntry GetNextView(Element element) => (UltimateEntry)element.GetValue(NextViewProperty);
        public static void SetNextView(Element element, UltimateEntry value) => element.SetValue(NextViewProperty, value);

        public static readonly BindableProperty ShowErrorProperty = CreateAttached(nameof(UltimateEntry.ShowError), typeof(bool), typeof(UltimateEntry), false);
        public static bool GetShowError(Element element) => (bool)element.GetValue(ShowErrorProperty);
        public static void SetShowError(Element element, bool value) => element.SetValue(ShowErrorProperty, value);

        public static readonly BindableProperty ErrorColorProperty = CreateAttached(nameof(UltimateEntry.ErrorColor), typeof(Color), typeof(UltimateEntry), Color.Red);
        public static Color GetErrorColor(Element element) => (Color)element.GetValue(ErrorColorProperty);
        public static void SetErrorColor(Element element, Color value) => element.SetValue(ErrorColorProperty, value);

        public static readonly BindableProperty FocusedBackgroundColorProperty = CreateAttached(nameof(UltimateEntry.FocusedBackgroundColor), typeof(Color), typeof(UltimateEntry), new Color(255, 255, 255, 0.2));
        public static Color GetFocusedBackgroundColor(Element element) => (Color)element.GetValue(FocusedBackgroundColorProperty);
        public static void SetFocusedBackgroundColor(Element element, Color value) => element.SetValue(FocusedBackgroundColorProperty, value);

        public static readonly BindableProperty FocusedBorderColorProperty = CreateAttached(nameof(UltimateEntry.FocusedBorderColor), typeof(Color), typeof(UltimateEntry), Color.DimGray);
        public static Color GetFocusedBorderColor(Element element) => (Color)element.GetValue(FocusedBorderColorProperty);
        public static void SetFocusedBorderColor(Element element, Color value) => element.SetValue(FocusedBorderColorProperty, value);

        public static readonly BindableProperty EntryIsFocusedProperty = CreateAttached(nameof(UltimateEntry.EntryIsFocused), typeof(bool), typeof(UltimateEntry), false);
        public static bool GetEntryIsFocused(Element element) => (bool)element.GetValue(EntryIsFocusedProperty);
        public static void SetEntryIsFocused(Element element, bool value) => element.SetValue(EntryIsFocusedProperty, value);

        public static readonly BindableProperty UseKeyboardPlaceholderProperty = CreateAttached(nameof(UltimateEntry.UseKeyboardPlaceholder), typeof(bool), typeof(UltimateEntry), false);
        public static bool GetUseKeyboardPlaceholder(Element element) => (bool)element.GetValue(UseKeyboardPlaceholderProperty);
        public static void SetUseKeyboardPlaceholder(Element element, bool value) => element.SetValue(UseKeyboardPlaceholderProperty, value);

        public static readonly BindableProperty HidePasswordImageSourceProperty = CreateAttached(nameof(UltimateEntry.HidePasswordImageSource), typeof(string), typeof(UltimateEntry), string.Empty);
        public static string GetHidePasswordImageSource(Element element) => (string)element.GetValue(HidePasswordImageSourceProperty);
        public static void SetHidePasswordImageSource(Element element, string value) => element.SetValue(HidePasswordImageSourceProperty, value);

        public static readonly BindableProperty AlwaysShowRightImageProperty = CreateAttached(nameof(UltimateEntry.AlwaysShowRightImage), typeof(bool), typeof(UltimateEntry), true);
        public static bool GetAlwaysShowRightImage(Element element) => (bool)element.GetValue(AlwaysShowRightImageProperty);
        public static void SetAlwaysShowRightImage(Element element, bool value) => element.SetValue(AlwaysShowRightImageProperty, value);

        public static readonly BindableProperty IsRoundedEntryProperty = CreateAttached(nameof(UltimateEntry.IsRoundedEntry), typeof(bool), typeof(UltimateEntry), false);
        public static bool GetIsRoundedEntry(Element element) => (bool)element.GetValue(IsRoundedEntryProperty);
        public static void SetIsRoundedEntry(Element element, bool value) => element.SetValue(IsRoundedEntryProperty, value);

        public static readonly BindableProperty ThicknessPaddingProperty = CreateAttached(nameof(UltimateEntry.ThicknessPadding), typeof(Thickness), typeof(UltimateEntry), new Thickness(20, 10));
        public static Thickness GetThicknessPadding(Element element) => (Thickness)element.GetValue(ThicknessPaddingProperty);
        public static void SetThicknessPadding(Element element, Thickness value) => element.SetValue(ThicknessPaddingProperty, value);

        public static readonly BindableProperty ErrorImageSourceProperty = CreateAttached(nameof(UltimateEntry.ErrorImageSource), typeof(string), typeof(UltimateEntry), string.Empty);
        public static string GetErrorImageSource(Element element) => (string)element.GetValue(ErrorImageSourceProperty);
        public static void SetErrorImageSource(Element element, string value) => element.SetValue(ErrorImageSourceProperty, value);

        public static void OnPropertyChanged(string propertyName, UltimateEntry ultimateEntry, Element bindingElement)
        {
            switch (propertyName)
            {
                case nameof(UltimateEntry.RightImageSource):
                    SetRightImageSource(ultimateEntry, GetRightImageSource(bindingElement));
                    break;
                case nameof(UltimateEntry.ImageButtonType):
                    SetImageButtonType(ultimateEntry, GetImageButtonType(bindingElement));
                    break;
                case nameof(UltimateEntry.ReturnButtonType):
                    SetReturnButton(ultimateEntry, GetReturnButton(bindingElement));
                    break;
                case nameof(UltimateEntry.NextView):
                    SetNextView(ultimateEntry, GetNextView(bindingElement));
                    break;
                case nameof(UltimateEntry.ShowError):
                    SetShowError(ultimateEntry, GetShowError(bindingElement));
                    break;
                case nameof(UltimateEntry.ErrorColor):
                    SetErrorColor(ultimateEntry, GetErrorColor(bindingElement));
                    break;
                case nameof(UltimateEntry.FocusedBackgroundColor):
                    SetFocusedBackgroundColor(ultimateEntry, GetFocusedBackgroundColor(bindingElement));
                    break;
                case nameof(UltimateEntry.FocusedBorderColor):
                    SetFocusedBorderColor(ultimateEntry, GetFocusedBorderColor(bindingElement));
                    break;
                case nameof(UltimateEntry.EntryIsFocused):
                    SetEntryIsFocused(ultimateEntry, GetEntryIsFocused(bindingElement));
                    break;
                case nameof(UltimateEntry.UseKeyboardPlaceholder):
                    SetUseKeyboardPlaceholder(ultimateEntry, GetUseKeyboardPlaceholder(bindingElement));
                    break;
                case nameof(UltimateEntry.HidePasswordImageSource):
                    SetHidePasswordImageSource(ultimateEntry, GetHidePasswordImageSource(bindingElement));
                    break;
                case nameof(UltimateEntry.AlwaysShowRightImage):
                    SetAlwaysShowRightImage(ultimateEntry, GetAlwaysShowRightImage(bindingElement));
                    break;
                case nameof(UltimateEntry.IsRoundedEntry):
                    SetIsRoundedEntry(ultimateEntry, GetIsRoundedEntry(bindingElement));
                    break;
                case nameof(UltimateEntry.ThicknessPadding):
                    SetThicknessPadding(ultimateEntry, GetThicknessPadding(bindingElement));
                    break;
                case nameof(UltimateEntry.ErrorImageSource):
                    SetErrorImageSource(ultimateEntry, GetErrorImageSource(bindingElement));
                    break;
            }
        }
    }
}
