using System;
using System.ComponentModel;
using System.Drawing;
using CoreGraphics;
using PhantomLib.CustomControls;
using PhantomLib.CustomControls.Enums;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(UltimateEntry), typeof(PhantomLib.iOS.Renderers.UltimateEntryRenderer))]
namespace PhantomLib.iOS.Renderers
{
    public class UltimateEntryRenderer : EntryRenderer
    {
        UltimateEntry _ultimateEntry;

        UITextField _textField;
        UIButton _imageButton;

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (this.Control != null && this.Element != null && e.NewElement != null)
            {
                _ultimateEntry = (UltimateEntry)this.Element;
                _textField = (UITextField)this.Control;

                if (_ultimateEntry.ImageButton == UltimateEntryImageButton.Password)
                {
                    _ultimateEntry.IsPassword = true;
                }

                _textField.Layer.CornerRadius = 5;
                _textField.Layer.BorderWidth = 2;

                // Add padding to the entry field
                _textField.LeftView = new UIView(new CGRect(0, 0, 10, 0));
                _textField.LeftViewMode = UITextFieldViewMode.Always;

                SetReturnType();

                //subscribe
                _textField.EditingDidBegin += TextField_FocusChanged;
                _textField.EditingDidEnd += TextField_FocusChanged;
                _textField.EditingChanged += TextField_FocusChanged;
                _textField.ShouldReturn += TextField_ShouldReturn;
                _ultimateEntry.PropertyChanged += OnElementPropertyChanged;

                UpdateControlUI();
            }

            if (e.OldElement != null)
            {
                // Unsubscribe
                _textField.EditingDidBegin -= TextField_FocusChanged;
                _textField.EditingDidEnd -= TextField_FocusChanged;
                _textField.EditingChanged -= TextField_FocusChanged;
                _textField.ShouldReturn -= TextField_ShouldReturn;
                _ultimateEntry.PropertyChanged -= OnElementPropertyChanged;

                if (_imageButton != null)
                    _imageButton.TouchUpInside -= ImageButton_TouchUpInside;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            switch (e.PropertyName)
            {
                case nameof(UltimateEntry.AlwaysShowImage):
                case nameof(UltimateEntry.ErrorColor):
                case nameof(UltimateEntry.ShowError):
                case nameof(UltimateEntry.FocusedBackgroundColor):
                case nameof(UltimateEntry.ImageSource):
                case nameof(UltimateEntry.HidePasswordImageSource):
                case nameof(UltimateEntry.ErrorImageSource):
                    UpdateControlUI();
                    break;
            }
        }

        private void TextField_FocusChanged(object sender, EventArgs eventArgs)
        {
            UpdateControlUI();

            _ultimateEntry.EntryIsFocused = _ultimateEntry.IsFocused;
            _ultimateEntry.EntryFocusChangedDelegate(sender, new FocusEventArgs(_ultimateEntry, _ultimateEntry.IsFocused));
        }

        private void UpdateControlUI()
        {
            //set stroke/border
            if (_ultimateEntry.ShowError)
            {
                _textField.Layer.BorderColor = _ultimateEntry.ErrorColor.ToCGColor();
            }
            else if (!_ultimateEntry.ShowError && _ultimateEntry.IsFocused)
            {
                _textField.Layer.BorderColor = _ultimateEntry.FocusedBorderColor.ToCGColor();
            }
            else
            {
                _textField.Layer.BorderColor = _ultimateEntry.UnFocusedBorderColor.ToCGColor();
            }

            //set background color
            _textField.BackgroundColor = _ultimateEntry.IsFocused
                    ? _ultimateEntry.FocusedBackgroundColor.ToUIColor()
                    : _ultimateEntry.BackgroundColor.ToUIColor();

            SetImage();
        }

        private void SetReturnType()
        {
            switch (_ultimateEntry.ReturnButton)
            {
                case UltimateEntryReturn.Next:
                    _textField.ReturnKeyType = UIReturnKeyType.Next;
                    break;
                case UltimateEntryReturn.Search:
                    _textField.ReturnKeyType = UIReturnKeyType.Search;
                    break;
                case UltimateEntryReturn.Done:
                    _textField.ReturnKeyType = UIReturnKeyType.Done;
                    break;
                default:
                    _textField.ReturnKeyType = UIReturnKeyType.Default;
                    break;
            }
        }

        private void SetImage()
        {
            string imageSource = "";

            //if error and error image is provided
            if (_ultimateEntry.ShowError && !string.IsNullOrEmpty(_ultimateEntry.ErrorImageSource))
            {
                imageSource = _ultimateEntry.ErrorImageSource;
            }
            //handle Password image if its a password
            else if (_ultimateEntry.ImageButton == UltimateEntryImageButton.Password)
            {
                imageSource = _ultimateEntry.IsPassword
                    ? _ultimateEntry.HidePasswordImageSource
                    : _ultimateEntry.ImageSource;
            }
            //lastly use RightImageSource if it exists
            else if (!string.IsNullOrEmpty(_ultimateEntry.ImageSource))
            {
                imageSource = _ultimateEntry.ImageSource;
            }

            //set padding in place of image if developer didnt set imageSource
            if (string.IsNullOrEmpty(imageSource))
            {
                _textField.RightView = new UIView(new CGRect(0, 0, 10, 0));
                _textField.RightViewMode = UITextFieldViewMode.Always;
            }
            else
            {
                _textField.RightView = GetImageView(imageSource);
                _textField.RightViewMode = _ultimateEntry.AlwaysShowImage
                        ? UITextFieldViewMode.Always
                        : UITextFieldViewMode.WhileEditing;
            }
        }

        private UIView GetImageView(string imageSource)
        {
            var image = UIImage.FromBundle(imageSource).ImageWithRenderingMode(UIKit.UIImageRenderingMode.Automatic);
            // Make the view 10 wider than the image so that it has some padding.

            _imageButton = UIButton.FromType(UIButtonType.Custom);
            _imageButton.Frame = new RectangleF(0, 0, (int)(image.Size.Width), (int)image.Size.Height);
            _imageButton.SetImage(image, UIControlState.Normal);

            //Set up event handler for "Click" event ("TouchUpInside in iOS terminology)
            _imageButton.TouchUpInside += ImageButton_TouchUpInside;

            UIView view = new UIView(new System.Drawing.Rectangle(0, 0, (int)(image.Size.Width) + 10, (int)image.Size.Height));

            view.Add(_imageButton);
            return view;
        }

        void ImageButton_TouchUpInside(object sender, EventArgs e)
        {
            switch (_ultimateEntry.ImageButton)
            {
                case UltimateEntryImageButton.ClearContents:
                    _ultimateEntry.Text = string.Empty;
                    break;
                case UltimateEntryImageButton.Password:
                    _ultimateEntry.IsPassword = !_ultimateEntry.IsPassword;
                    break;
            }

            UpdateControlUI();
            //_ultimateEntry.RightImageTouchedDelegate(_ultimateEntry, e);
        }

        bool TextField_ShouldReturn(UITextField textField)
        {
            _ultimateEntry.OnNext();
            return false;
        }
    }
}
