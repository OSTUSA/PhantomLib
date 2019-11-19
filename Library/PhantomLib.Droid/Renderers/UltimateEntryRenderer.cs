using System;
using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Util;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;
using Color = Xamarin.Forms.Color;
using PhantomLib.CustomControls;
using static PhantomLib.CustomControls.UltimateControl;
using PhantomLib.CustomControls.Enums;

[assembly: ExportRenderer(typeof(UltimateEntry), typeof(PhantomLib.Droid.Renderers.UltimateEntryRenderer))]
namespace PhantomLib.Droid.Renderers
{
    public class UltimateEntryRenderer : EntryRenderer
    {
        UltimateEntry _ultimateEntry;

        EditText _editText;
        Color _entryBackgroundColor;

        private Drawable _underlineDrawable;

        public UltimateEntryRenderer(Context context) : base(context)
        {
            _underlineDrawable = Resources.GetDrawable(Resource.Drawable.ExtEntryShape, null); 
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (this.Control != null && this.Element != null && e.NewElement != null)
            {
                _ultimateEntry = (UltimateEntry)this.Element;

                _editText = (EditText)this.Control;
                _entryBackgroundColor = _ultimateEntry.BackgroundColor;

                if (_ultimateEntry.ImageButton == UltimateEntryImageButton.Password)
                {
                    _ultimateEntry.IsPassword = true;
                }

                SetPadding();

                SetReturnType(_ultimateEntry.ReturnButton);

                // Switch the stroke color to blue if the field is in focus and it doesn't have a validation error
                _editText.FocusChange += EditText_FocusChange;

                // Update UI when text is changed
                _editText.TextChanged += EditText_TextChanged;

                UpdateControlUI();
            }

            if (e.OldElement != null)
            {
                // Unsubscribe
                _editText.FocusChange -= EditText_FocusChange;
                _editText.TextChanged -= EditText_TextChanged;
                _editText.EditorAction -= _editText_OnNext;

                if (_editText.HasOnClickListeners)
                {
                    _editText.SetOnTouchListener(null);
                }
            }
        }

        private int ConvertToDip(int size)
        {
            var a = (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, size, Context.Resources.DisplayMetrics);
            return a;
        }

        private void SetPadding()
        {
            _editText.SetPadding
            (
                ConvertToDip((int)_ultimateEntry.ThicknessPadding.Left),
                ConvertToDip((int)_ultimateEntry.ThicknessPadding.Top),
                ConvertToDip((int)_ultimateEntry.ThicknessPadding.Right),
                ConvertToDip((int)_ultimateEntry.ThicknessPadding.Bottom)
            );
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

        void EditText_FocusChange(object sender, FocusChangeEventArgs e)
        {
            UpdateControlUI();

            _ultimateEntry.EntryIsFocused = e.HasFocus;
            _ultimateEntry.EntryFocusChangedDelegate(sender, new FocusEventArgs(_ultimateEntry, e.HasFocus));

            AddKeyboardPlaceholder(_ultimateEntry.UseKeyboardPlaceholder && e.HasFocus);
        }

        public void UpdateControlUI()
        {
            // else remove the underline in the android entry field
            GradientDrawable gradientDrawable = new GradientDrawable();
            gradientDrawable.SetCornerRadius(5);

            //set stroke
            if (_ultimateEntry.ShowError)
            {
                gradientDrawable.SetStroke(4, _ultimateEntry.ErrorColor.ToAndroid());
                _underlineDrawable.SetColorFilter(_ultimateEntry.ErrorColor.ToAndroid(), PorterDuff.Mode.SrcIn);
            }
            else if (!_ultimateEntry.ShowError && _editText.IsFocused)
            {
                gradientDrawable.SetStroke(4, _ultimateEntry.FocusedBorderColor.ToAndroid());
                _underlineDrawable.SetColorFilter(_ultimateEntry.FocusedBorderColor.ToAndroid(), PorterDuff.Mode.SrcIn);
            }
            else
            {
                gradientDrawable.SetStroke(4, _ultimateEntry.UnFocusedBorderColor.ToAndroid());
                _underlineDrawable.SetColorFilter(_ultimateEntry.UnFocusedBorderColor.ToAndroid(), PorterDuff.Mode.SrcIn);
            }

            //get background color
            var bgColor = _editText.IsFocused
                    ? _ultimateEntry.FocusedBackgroundColor
                    : _entryBackgroundColor;

            //set background
            if (_ultimateEntry.IsRoundedEntry)
            {
                gradientDrawable.SetColor(bgColor.ToAndroid());
                _editText.Background = gradientDrawable;
            }
            else
            {
                _editText.SetBackground(_underlineDrawable);
                _ultimateEntry.BackgroundColor = bgColor;
            }

            //update image
            SetImage();
        }

        private void SetReturnType(UltimateEntryReturn type)
        {
            switch (type)
            {
                case UltimateEntryReturn.Next:
                    _editText.ImeOptions = ImeAction.Next;
                    _editText.SetImeActionLabel("Next", ImeAction.Next);
                    // Editor Action is called when the return button is pressed
                    _editText.EditorAction += _editText_OnNext;
                    break;
                case UltimateEntryReturn.Search:
                    _editText.ImeOptions = ImeAction.Search;
                    _editText.SetImeActionLabel("Search", ImeAction.Search);
                    break;
                default:
                    _editText.ImeOptions = ImeAction.Done;
                    _editText.SetImeActionLabel("Done", ImeAction.Done);
                    break;
            }
        }

        private void _editText_OnNext(object sender, TextView.EditorActionEventArgs e)
        {
            _ultimateEntry.OnNext();
        }

        public void SetImage()
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
            else if(!string.IsNullOrEmpty(_ultimateEntry.ImageSource))
            {
                imageSource = _ultimateEntry.ImageSource;
            }

            //clear image and return if developer didnt set imageSource  OR the entry is not focused and developer wants to hide image
            if (string.IsNullOrEmpty(imageSource) || !_ultimateEntry.AlwaysShowImage && !_editText.IsFocused)
            {
                _editText.SetCompoundDrawablesRelativeWithIntrinsicBounds(0, 0, 0, 0);
                return;
            }

            var imageResourceId = Resources.GetIdentifier(imageSource, "drawable", Context.PackageName);
            if (imageResourceId != 0)
            {
                if (!_editText.HasOnClickListeners)
                {
                    _editText.SetOnTouchListener(new OnDrawableTouchListener());
                }
                _editText.SetCompoundDrawablesRelativeWithIntrinsicBounds(0, 0, imageResourceId, 0);
            }
            else
            {
                throw new Exception($"Drawable {imageSource} was not found in package {Context.PackageName}");
            }
        }

        BoxView keyboardPlaceholder;
        private void AddKeyboardPlaceholder(bool shouldAdd)
        {
            if (_ultimateEntry != null && _ultimateEntry.Parent is StackLayout stackLayout)
            {
                if (shouldAdd)
                {
                    // Get the Height of the screen
                    DisplayMetrics displayMetrics = new DisplayMetrics();
                    FormsAppCompatActivity _activity = (FormsAppCompatActivity)Context;
                    _activity.WindowManager.DefaultDisplay.GetMetrics(displayMetrics);
                    double screenHeight = displayMetrics.HeightPixels / displayMetrics.Density;

                    // Calculate the height of the area below the phantom entry field
                    double height = screenHeight - _ultimateEntry.Y - _ultimateEntry.Height;
                    height = height >= 0 ? height : 0;

                    keyboardPlaceholder = new BoxView() { HeightRequest = height, HorizontalOptions = LayoutOptions.FillAndExpand };
                    stackLayout.Children.Add(keyboardPlaceholder);
                }
                else if (keyboardPlaceholder != null)
                {
                    stackLayout.Children.Remove(keyboardPlaceholder);
                    keyboardPlaceholder = null;
                }
            }
        }

        void EditText_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            UpdateControlUI();
        }

    }

    public class OnDrawableTouchListener : Java.Lang.Object, Android.Views.View.IOnTouchListener
    {
        public bool OnTouch(Android.Views.View v, MotionEvent e)
        {
            if (v is EditText && e.Action == MotionEventActions.Up)
            {
                EditText editText = (EditText)v;
                var drawablesArray = editText.GetCompoundDrawables();

                //only continue if image exists
                if (drawablesArray[2] == null) return false;

                // Check to see if the clear button was clicked
                if (e.RawX >= (editText.Right - editText.PaddingRight - drawablesArray[2].Bounds.Width()))
                {
                    var ultimateEntryRenderer = (UltimateEntryRenderer)editText.Parent;
                    if (ultimateEntryRenderer != null)
                    {
                        var ultimateEntry = (UltimateEntry)ultimateEntryRenderer.Element;
                        if (ultimateEntry !=null)
                        {
                            switch (ultimateEntry.ImageButton)
                            {
                                case UltimateEntryImageButton.ClearContents:
                                    ultimateEntry.Text = string.Empty;
                                    break;
                                //toggle transformation
                                case UltimateEntryImageButton.Password:
                                    ultimateEntry.IsPassword = !ultimateEntry.IsPassword;
                                    break;
                            }
                            ultimateEntryRenderer.UpdateControlUI();
                            //ultimateEntry.RightImageTouchedDelegate(ultimateEntry, new EventArgs());
                        }
                    }
                    return true;
                }
            }
            return false;
        }
    }
}
