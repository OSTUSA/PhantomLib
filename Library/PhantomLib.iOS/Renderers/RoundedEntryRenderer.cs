using System;
using System.ComponentModel;
using System.Drawing;
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

            if (this.Control != null && this.Element != null)
            {
                RoundedEntry roundedEntry = (RoundedEntry)this.Element;
                UITextField textField = (UITextField)this.Control;

                if(roundedEntry.ImageButtonType == RoundedEntryImageButton.Password)
                {
                    textField.SecureTextEntry = true;
                }

                textField.Layer.CornerRadius = 5;
                textField.Layer.BorderWidth = 2;
                // Add padding to the entry field
                textField.LeftView = new UIView(new CGRect(0, 0, 10, 0));
                textField.LeftViewMode = UITextFieldViewMode.Always;

                UpdateControlUI();

                SetImage();
                SetReturnType(roundedEntry);

                textField.EditingDidBegin += TextField_FocusChanged;
                textField.EditingDidEnd += TextField_FocusChanged;
                textField.EditingChanged += TextField_FocusChanged;
                textField.ShouldReturn += (txtField) => {
                    roundedEntry.OnNext();
                    return false;
                };
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            switch (e.PropertyName)
            {
                case nameof(RoundedEntry.AlwaysShowRightImage):
                case nameof(RoundedEntry.ErrorColor):
                case nameof(RoundedEntry.ShowError):
                case nameof(RoundedEntry.FocusedBackgroundColor):
                    UpdateControlUI();
                    break;
            }
        }

        private void TextField_FocusChanged(object sender, EventArgs eventArgs)
        {
            RoundedEntry roundedEntry = (RoundedEntry)this.Element;

            UpdateControlUI();

            roundedEntry.EntryIsFocused = roundedEntry.IsFocused;
            roundedEntry.EntryFocusChangedDelegate(sender, new FocusEventArgs(roundedEntry, roundedEntry.IsFocused));
        }

        private void UpdateControlUI()
        {
            RoundedEntry roundedEntry = (RoundedEntry)this.Element;
            UITextField textField = (UITextField)this.Control;

            //set stroke/border
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
            var type = roundedEntry.ReturnButtonType;

            switch (type)
            {
                case RoundedEntryReturn.Next:
                    Control.ReturnKeyType = UIReturnKeyType.Next;
                    break;
                case RoundedEntryReturn.Search:
                    Control.ReturnKeyType = UIReturnKeyType.Search;
                    break;
                case RoundedEntryReturn.Done:
                    Control.ReturnKeyType = UIReturnKeyType.Done;
                    break;
                default:
                    Control.ReturnKeyType = UIReturnKeyType.Default;
                    break;
            }
        }

        private void SetImage()
        {
            RoundedEntry roundedEntry = (RoundedEntry)this.Element;
            UITextField textField = (UITextField)this.Control;

            //add image if RightImageSource is defined else add pading
            if (string.IsNullOrEmpty(roundedEntry.RightImageSource))
            {
                textField.RightView = new UIView(new CGRect(0, 0, 10, 0));
                textField.RightViewMode = UITextFieldViewMode.Always;
            }
            else
            {
                //use hide password image if text is plain text
                if(roundedEntry.ImageButtonType == RoundedEntryImageButton.Password && !textField.SecureTextEntry)
                {
                    textField.RightView = GetImageView(roundedEntry.HidePasswordImageSource);
                }
                else
                {
                    textField.RightView = GetImageView(roundedEntry.RightImageSource);
                }

                if (roundedEntry.AlwaysShowRightImage)
                    textField.RightViewMode = UITextFieldViewMode.Always;
                else
                    textField.RightViewMode = UITextFieldViewMode.WhileEditing;
            }
        }

        private UIView GetImageView(string imagePath)
        {
            var image = UIImage.FromBundle(imagePath).ImageWithRenderingMode(UIKit.UIImageRenderingMode.Automatic);
            // Make the view 10 wider than the image so that it has some padding.

            var imageButton = UIButton.FromType(UIButtonType.Custom);
            imageButton.Frame = new RectangleF(0, 0, (int)(image.Size.Width), (int)image.Size.Height);
            imageButton.SetImage(image, UIControlState.Normal);

            //Set up event handler for "Click" event ("TouchUpInside in iOS terminology)
            imageButton.TouchUpInside += ImageButton_TouchUpInside;

            UIView view = new UIView(new System.Drawing.Rectangle(0, 0, (int)(image.Size.Width) + 10, (int)image.Size.Height));

            view.Add(imageButton);
            return view;
        }

        void ImageButton_TouchUpInside(object sender, EventArgs e)
        {
            RoundedEntry roundedEntry = (RoundedEntry)this.Element;

            switch (roundedEntry.ImageButtonType)
            {
                case RoundedEntryImageButton.ClearContents:
                    roundedEntry.Text = string.Empty;
                    UpdateControlUI();
                    break;
                case RoundedEntryImageButton.Password:
                    UITextField textField = (UITextField)this.Control;
                    textField.SecureTextEntry = !textField.SecureTextEntry;
                    SetImage();
                    break;
            }

            roundedEntry.RightImageTouchedDelegate(roundedEntry, e);
        }

    }
}
