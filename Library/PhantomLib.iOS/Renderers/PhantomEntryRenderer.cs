using System;
using System.ComponentModel;
using System.Drawing;
using CoreGraphics;
using PhantomLib.CustomControls;
using PhantomLib.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms.Internals;

[assembly: ExportRenderer(typeof(PhantomEntry), typeof(PhantomEntryRenderer))]
namespace PhantomLib.iOS.Renderers
{
    public class PhantomEntryRenderer : EntryRenderer
    {
        PhantomEntry _phantomEntry;
        UITextField _textField;
        UIButton _imageButton;

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (this.Control != null && this.Element != null && e.NewElement != null)
            {
                _phantomEntry = (PhantomEntry)this.Element;
                _textField = (UITextField)this.Control;

                if(_phantomEntry.ImageButtonType == PhantomEntryImageButton.Password)
                {
                    _phantomEntry.IsPassword = true;
                }

                _textField.Layer.CornerRadius = 5;
                _textField.Layer.BorderWidth = 2;
                // Add padding to the entry field
                _textField.LeftView = new UIView(new CGRect(0, 0, 10, 0));
                _textField.LeftViewMode = UITextFieldViewMode.Always;


                SetImage();
                SetReturnType();

                if(e.NewElement != null)
                {
                    //subscribe
                    _textField.EditingDidBegin += TextField_FocusChanged;
                    _textField.EditingDidEnd += TextField_FocusChanged;
                    _textField.EditingChanged += TextField_FocusChanged;
                    _textField.ShouldReturn += TextField_ShouldReturn;
                }


                UpdateControlUI();
            }

            if (e.OldElement != null)
            {
                // Unsubscribe
                _textField.EditingDidBegin -= TextField_FocusChanged;
                _textField.EditingDidEnd -= TextField_FocusChanged;
                _textField.EditingChanged -= TextField_FocusChanged;
                _textField.ShouldReturn -= TextField_ShouldReturn;
                if (_imageButton != null)
                    _imageButton.TouchUpInside -= ImageButton_TouchUpInside;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            switch (e.PropertyName)
            {
                case nameof(PhantomEntry.AlwaysShowRightImage):
                case nameof(PhantomEntry.ErrorColor):
                case nameof(PhantomEntry.ShowError):
                case nameof(PhantomEntry.FocusedBackgroundColor):
                    UpdateControlUI();
                    break;
            }
        }

        private void TextField_FocusChanged(object sender, EventArgs eventArgs)
        {
            UpdateControlUI();

            _phantomEntry.EntryIsFocused = _phantomEntry.IsFocused;
            _phantomEntry.EntryFocusChangedDelegate(sender, new FocusEventArgs(_phantomEntry, _phantomEntry.IsFocused));
        }

        private void UpdateControlUI()
        {

            //set stroke/border
            if (_phantomEntry.ShowError)
            {
                _textField.Layer.BorderColor = _phantomEntry.ErrorColor.ToCGColor();
            }
            else if (!_phantomEntry.ShowError && _phantomEntry.IsFocused)
            {
                _textField.Layer.BorderColor = _phantomEntry.FocusedBorderColor.ToCGColor();
            }
            else
            {
                _textField.Layer.BorderColor = Color.Transparent.ToCGColor();
            }

            //set background color
            _textField.BackgroundColor = _phantomEntry.IsFocused
                    ? _phantomEntry.FocusedBackgroundColor.ToUIColor()
                    : _phantomEntry.BackgroundColor.ToUIColor();
        }

        private void SetReturnType()
        {

            switch (_phantomEntry.ReturnButtonType)
            {
                case PhantomEntryReturn.Next:
                    _textField.ReturnKeyType = UIReturnKeyType.Next;
                    break;
                case PhantomEntryReturn.Search:
                    _textField.ReturnKeyType = UIReturnKeyType.Search;
                    break;
                case PhantomEntryReturn.Done:
                    _textField.ReturnKeyType = UIReturnKeyType.Done;
                    break;
                default:
                    _textField.ReturnKeyType = UIReturnKeyType.Default;
                    break;
            }
        }

        private void SetImage()
        {

            //add image if RightImageSource is defined else add pading
            if (string.IsNullOrEmpty(_phantomEntry.RightImageSource))
            {
                _textField.RightView = new UIView(new CGRect(0, 0, 10, 0));
                _textField.RightViewMode = UITextFieldViewMode.Always;
            }
            else
            {
                //use hide password image if text is plain text
                if(_phantomEntry.ImageButtonType == PhantomEntryImageButton.Password && !_phantomEntry.IsPassword)
                {
                    _textField.RightView = GetImageView(_phantomEntry.HidePasswordImageSource);
                }
                else
                {
                    _textField.RightView = GetImageView(_phantomEntry.RightImageSource);
                }

                if (_phantomEntry.AlwaysShowRightImage)
                    _textField.RightViewMode = UITextFieldViewMode.Always;
                else
                    _textField.RightViewMode = UITextFieldViewMode.WhileEditing;
            }
        }

        private UIView GetImageView(string imagePath)
        {
            var image = UIImage.FromBundle(imagePath).ImageWithRenderingMode(UIKit.UIImageRenderingMode.Automatic);
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
            switch (_phantomEntry.ImageButtonType)
            {
                case PhantomEntryImageButton.ClearContents:
                    _phantomEntry.Text = string.Empty;
                    UpdateControlUI();
                    break;
                case PhantomEntryImageButton.Password:
                    _phantomEntry.IsPassword = !_phantomEntry.IsPassword;
                    SetImage();
                    break;
            }

            _phantomEntry.RightImageTouchedDelegate(_phantomEntry, e);
        }

        bool TextField_ShouldReturn(UITextField textField)
        {
            _phantomEntry.OnNext();
            return false;
        }

    }
}
