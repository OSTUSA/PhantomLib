using System;
using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Util;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using PhantomLib.CustomControls;
using PhantomLib.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System.Linq;
using Android.Text.Method;

[assembly: ExportRenderer(typeof(RoundedEntry), typeof(RoundedEntryRenderer))]
namespace PhantomLib.Droid.Renderers
{
    public class RoundedEntryRenderer : EntryRenderer
    {
        RoundedEntry _roundedEntry;
        EditText _editText;
        private int _imageResourceId = 0;

        public RoundedEntryRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (this.Control != null && this.Element != null && e.NewElement != null)
            {
                _roundedEntry = (RoundedEntry)this.Element;
                _editText = (EditText)this.Control;

                if(_roundedEntry.ImageButtonType == RoundedEntryImageButton.Password)
                {
                    _roundedEntry.IsPassword = true;
                }

                if (!string.IsNullOrEmpty(_roundedEntry.RightImageSource))
                {
                    SetImage(_roundedEntry.RightImageSource);
                }

                _editText.SetPadding(50, 50, 50, 50);

                SetReturnType(_roundedEntry.ReturnButtonType);

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
                if (_editText.HasOnClickListeners)
                {
                    _editText.SetOnTouchListener(null);
                }
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

        void EditText_FocusChange(object sender, FocusChangeEventArgs e)
        {
            _roundedEntry.EntryIsFocused = e.HasFocus;

            UpdateControlUI();
            AddKeyboardPlaceholder(_roundedEntry.UseKeyboardPlaceholder && e.HasFocus);

            _roundedEntry.EntryFocusChangedDelegate(sender, new FocusEventArgs(_roundedEntry, e.HasFocus));
        }

        public void UpdateControlUI()
        {
            // Remove the underline in the android entry field
            GradientDrawable gradientDrawable = new GradientDrawable();
            gradientDrawable.SetCornerRadius(5);

            //set stroke
            if (_roundedEntry.ShowError)
            {
                gradientDrawable.SetStroke(4, _roundedEntry.ErrorColor.ToAndroid());
            }
            else if (!_roundedEntry.ShowError && _editText.IsFocused)
            {
                gradientDrawable.SetStroke(4, _roundedEntry.FocusedBorderColor.ToAndroid());
            }
            else
            {
                gradientDrawable.SetStroke(4, Color.Transparent.ToAndroid());
            }

            //set background color
            var bgColor = _editText.IsFocused
                    ? _roundedEntry.FocusedBackgroundColor.ToAndroid()
                    : _roundedEntry.BackgroundColor.ToAndroid();
            gradientDrawable.SetColor(bgColor);

            //handle toggling visibility to image on focus
            if (_editText.IsFocused)
            {
                if (!_roundedEntry.AlwaysShowRightImage)
                {
                    _editText.SetCompoundDrawablesRelativeWithIntrinsicBounds(0, 0, _imageResourceId, 0);
                }
            }
            else
            {
                if (!_roundedEntry.AlwaysShowRightImage)
                {
                    _editText.SetCompoundDrawablesRelativeWithIntrinsicBounds(0, 0, 0, 0);
                }
            }

            Control.Background = gradientDrawable;
        }

        private void SetReturnType(RoundedEntryReturn type)
        {
            switch (type)
            {
                case RoundedEntryReturn.Next:
                    _editText.ImeOptions = ImeAction.Next;
                    _editText.SetImeActionLabel("Next", ImeAction.Next);
                    // Editor Action is called when the return button is pressed
                    Control.EditorAction += (object sender, Android.Widget.TextView.EditorActionEventArgs eventArgs) =>
                    {
                        _roundedEntry.OnNext();
                    };
                    break;
                case RoundedEntryReturn.Search:
                    _editText.ImeOptions = ImeAction.Search;
                    _editText.SetImeActionLabel("Search", ImeAction.Search);
                    break;
                default:
                    _editText.ImeOptions = ImeAction.Done;
                    _editText.SetImeActionLabel("Done", ImeAction.Done);
                    break;
            }
        }

        public void SetImage(string imageSource)
        {
            _imageResourceId = Resources.GetIdentifier(imageSource, "drawable", Context.PackageName);

            if (_imageResourceId != 0)
            {
                if (!_editText.HasOnClickListeners)
                {
                    _editText.SetOnTouchListener(new OnDrawableTouchListener());
                }
                _editText.SetCompoundDrawablesRelativeWithIntrinsicBounds(0, 0, _imageResourceId, 0);
            }

            UpdateControlUI();
        }

        BoxView keyboardPlaceholder;
        private void AddKeyboardPlaceholder(bool shouldAdd)
        {
            if (_roundedEntry != null && _roundedEntry.Parent is StackLayout stackLayout)
            {
                if (shouldAdd)
                {
                    // Get the Height of the screen
                    DisplayMetrics displayMetrics = new DisplayMetrics();
                    FormsAppCompatActivity _activity = (FormsAppCompatActivity)Context;
                    _activity.WindowManager.DefaultDisplay.GetMetrics(displayMetrics);
                    double screenHeight = displayMetrics.HeightPixels / displayMetrics.Density;

                    // Calculate the height of the area below the rounded entry field
                    double height = screenHeight - _roundedEntry.Y - _roundedEntry.Height;
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
                    var roundedEntryRenderer = (RoundedEntryRenderer)editText.Parent;
                    if (roundedEntryRenderer != null)
                    {
                        var roundedEntry = (RoundedEntry)roundedEntryRenderer.Element;
                        if (roundedEntry !=null)
                        {
                            switch (roundedEntry.ImageButtonType)
                            {
                                case RoundedEntryImageButton.ClearContents:
                                    roundedEntry.Text = string.Empty;
                                    break;
                                
                                //toggle transformation and image on every click
                                case RoundedEntryImageButton.Password:
                                    if(roundedEntry.IsPassword)
                                    {
                                        roundedEntryRenderer.SetImage(roundedEntry.HidePasswordImageSource);
                                        roundedEntry.IsPassword = false;
                                    }
                                    else
                                    {
                                        roundedEntryRenderer.SetImage(roundedEntry.RightImageSource);
                                        roundedEntry.IsPassword = true;
                                    }
                                    break;
                            }
                            roundedEntryRenderer.UpdateControlUI();
                            roundedEntry.RightImageTouchedDelegate(roundedEntry, new EventArgs());
                        }

                    }
                    return true;
                }
            }
            return false;
        }
    }

}
