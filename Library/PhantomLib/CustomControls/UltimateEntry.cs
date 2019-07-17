using System;
using Xamarin.Forms;
using static PhantomLib.CustomControls.UltimateEntryProperties;

namespace PhantomLib.CustomControls
{
    public class UltimateEntry : Entry
    {
        public string RightImageSource
        {
            get => UltimateEntryProperties.GetRightImageSource(this);
            set => UltimateEntryProperties.SetRightImageSource(this, value);
        }
        public string ErrorImageSource
        {
            get => UltimateEntryProperties.GetErrorImageSource(this);
            set => UltimateEntryProperties.SetErrorImageSource(this, value);
        }

        public string HidePasswordImageSource
        {
            get => UltimateEntryProperties.GetHidePasswordImageSource(this);
            set => UltimateEntryProperties.SetHidePasswordImageSource(this, value);
        }

        public Thickness ThicknessPadding
        {
            get => UltimateEntryProperties.GetThicknessPadding(this);
            set => UltimateEntryProperties.SetThicknessPadding(this, value);
        }

        public bool ShowError
        {
            get => UltimateEntryProperties.GetShowError(this);
            set => UltimateEntryProperties.SetShowError(this, value);
        }

        public bool IsRoundedEntry
        {
            get => UltimateEntryProperties.GetIsRoundedEntry(this);
            set => UltimateEntryProperties.SetIsRoundedEntry(this, value);
        }

        public bool AlwaysShowRightImage
        {
            get => UltimateEntryProperties.GetAlwaysShowRightImage(this);
            set => UltimateEntryProperties.SetAlwaysShowRightImage(this, value);
        }

        public Color ErrorColor
        {
            get => UltimateEntryProperties.GetErrorColor(this);
            set => UltimateEntryProperties.SetErrorColor(this, value);
        }

        public Color FocusedBorderColor
        {
            get => UltimateEntryProperties.GetFocusedBorderColor(this);
            set => UltimateEntryProperties.SetFocusedBorderColor(this, value);
        }

        public Color FocusedBackgroundColor
        {
            get => UltimateEntryProperties.GetFocusedBackgroundColor(this);
            set => UltimateEntryProperties.SetFocusedBackgroundColor(this, value);
        }

        public bool EntryIsFocused
        {
            get => UltimateEntryProperties.GetEntryIsFocused(this);
            set => UltimateEntryProperties.SetEntryIsFocused(this, value);
        }

        public UltimateEntryReturn ReturnButtonType
        {
            get
            {
                var enumString = UltimateEntryProperties.GetReturnButton(this);
                Enum.TryParse(enumString, out UltimateEntryReturn returnButton);
                return returnButton;
            }
            set => UltimateEntryProperties.SetImageButtonType(this, value.ToString());
        }

        public UltimateEntryImageButton ImageButtonType
        {

            get
            {    
                var enumString = UltimateEntryProperties.GetImageButtonType(this);
                Enum.TryParse(enumString, out UltimateEntryImageButton imageButton);
                return imageButton;
            }
            set => UltimateEntryProperties.SetImageButtonType(this, value.ToString());
        }

        public UltimateEntry NextView
        {
            get => UltimateEntryProperties.GetNextView(this);
            set => UltimateEntryProperties.SetNextView(this, value);
        }

        public bool UseKeyboardPlaceholder
        {
            get => UltimateEntryProperties.GetUseKeyboardPlaceholder(this);
            set => UltimateEntryProperties.SetUseKeyboardPlaceholder(this, value);
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
