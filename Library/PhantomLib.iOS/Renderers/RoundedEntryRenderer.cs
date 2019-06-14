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

                textField.Layer.CornerRadius = 5;
                textField.Layer.BorderWidth = 2;

                UpdateControlUI();

                // Add padding to the entry field
                textField.LeftView = new UIView(new CGRect(0, 0, 10, 0));
                textField.LeftViewMode = UITextFieldViewMode.Always;

                //add image if RightImageSource is defined else add pading
                if (string.IsNullOrEmpty(roundedEntry.RightImageSource))
                {
                    textField.RightView = new UIView(new CGRect(0, 0, 10, 0));
                    textField.RightViewMode = UITextFieldViewMode.Always;
                }
                else
                {
                    textField.RightView = GetImageView(roundedEntry.RightImageSource);
                    textField.RightViewMode = UITextFieldViewMode.WhileEditing;
                }

                SetReturnType(roundedEntry);

                textField.ShouldReturn += (txtField) => {
                    roundedEntry.OnNext();
                    return false;
                };

                textField.EditingDidBegin += TextField_FocusChanged;
                textField.EditingDidEnd += TextField_FocusChanged;
                textField.EditingChanged += TextField_FocusChanged;
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

        private UIView GetImageView(string imagePath)
        {
            var image = UIImage.FromBundle(imagePath).ImageWithRenderingMode(UIKit.UIImageRenderingMode.Automatic);
            // Make the view 10 wider than the image so that it has some padding.

            var imageButton = UIButton.FromType(UIButtonType.Custom);
            imageButton.Frame = new RectangleF(0, 0, (int)(image.Size.Width), (int)image.Size.Height);
            imageButton.SetImage(image, UIControlState.Normal);


            //Set up event handler for "Click" event ("TouchUpInside in iOS terminology)
            imageButton.TouchUpInside += (object sender, EventArgs e) => {
                RoundedEntry roundedEntry = (RoundedEntry)this.Element;

                if (roundedEntry.ShouldClearTextOnClick)
                {
                    roundedEntry.Text = string.Empty;
                    UpdateControlUI();
                }

                roundedEntry.RightImageTouchedDelegate(sender, e);
            };

            UIView view = new UIView(new System.Drawing.Rectangle(0, 0, (int)(image.Size.Width) + 10, (int)image.Size.Height));

            view.Add(imageButton);
            return view;
        }

    }
}
