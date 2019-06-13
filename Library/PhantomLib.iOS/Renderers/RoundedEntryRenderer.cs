using System;
using System.ComponentModel;
using CoreGraphics;
using PhantomLib.CustomControls;
using PhantomLib.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(RoundedEntry), typeof(RoundedEntryRenderer))]
namespace PhantomLib.iOS.Renderers
{
    public class RoundedEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null && this.Element != null)
            {
                RoundedEntry roundedEntry = (RoundedEntry)Element;

                Control.Layer.CornerRadius = 5;
                Control.Layer.BorderWidth = 2;

                UpdateControlUI();

                // Add padding to the entry field
                Control.LeftView = new UIView(new CGRect(0, 0, 10, 0));
                ////TODO add clear image
                //add padding to the entry field


                Control.LeftViewMode = UITextFieldViewMode.Always;
                Control.RightViewMode = UITextFieldViewMode.Always;


                SetReturnType(roundedEntry);

                Control.ShouldReturn += (textField) => {
                    roundedEntry.OnNext();
                    return false;
                };

                // Switch the stroke color if the field is in focus and it doesn't have a validation error
                Control.EditingDidBegin += Control_FocusChanged;
                Control.EditingDidEnd += Control_FocusChanged;

                // Update the stroke color when the text changes and the user has clicked the submit button once 
                Control.EditingChanged += (object sender, EventArgs eventArgs) =>
                {
                    UpdateControlUI();
                };
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            RoundedEntry roundedEntry = (RoundedEntry)Element;

            if (e.PropertyName == RoundedEntry.RightImageSourceProperty.PropertyName)
            {
                if (roundedEntry.RightImageSource != null)
                {
                    //Control.RightView = new UIView(new UIImage(""));
                }
                else
                {
                    Control.RightView = new UIView(new CGRect(0, 0, 10, 0));
                }
            }
        }

        private void Control_FocusChanged(object sender, EventArgs eventArgs)
        {
            RoundedEntry roundedEntry = (RoundedEntry)Element;

            UpdateControlUI();

            roundedEntry.EntryIsFocused = roundedEntry.IsFocused;
            roundedEntry.EntryFocusChangedDelegate(sender, new FocusEventArgs(roundedEntry, roundedEntry.IsFocused));
        }

        private void UpdateControlUI()
        {
            RoundedEntry roundedEntry = (RoundedEntry)this.Element;

            if (roundedEntry.ShowError)
            {
                Control.Layer.BorderColor = roundedEntry.ErrorColor.ToCGColor();
            }
            else if (!roundedEntry.ShowError && roundedEntry.IsFocused)
            {
                Control.Layer.BorderColor = roundedEntry.FocusedBorderColor.ToCGColor();
            }
            else
            {
                Control.Layer.BorderColor = Color.Transparent.ToCGColor();
            }

            //set background color
            Control.BackgroundColor = roundedEntry.IsFocused
                    ? roundedEntry.FocusedBackgroundColor.ToUIColor()
                    : roundedEntry.BackgroundColor.ToUIColor();

        }

        private void SetReturnType(RoundedEntry roundedEntry)
        {
            var type = roundedEntry.ReturnButton;

            switch (type)
            {
                case RoundedEntryReturnType.Next:
                    Control.ReturnKeyType = UIReturnKeyType.Next;
                    break;
                case RoundedEntryReturnType.Search:
                    Control.ReturnKeyType = UIReturnKeyType.Search;
                    break;
                case RoundedEntryReturnType.Done:
                    Control.ReturnKeyType = UIReturnKeyType.Done;
                    break;
                default:
                    Control.ReturnKeyType = UIReturnKeyType.Default;
                    break;
            }
        }

    }
}
