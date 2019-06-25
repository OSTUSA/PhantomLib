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
        private int _imageResourceId = 0;

        public RoundedEntryRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (this.Control != null && this.Element != null)
            {
                RoundedEntry roundedEntry = (RoundedEntry)this.Element;
                EditText editText = (EditText)this.Control;
    
                if(roundedEntry.ImageButtonType == RoundedEntryImageButton.Password)
                {
                    editText.TransformationMethod = PasswordTransformationMethod.Instance;
                }

                if (!string.IsNullOrEmpty(roundedEntry.RightImageSource))
                {
                    SetImage(roundedEntry.RightImageSource);
                }

                editText.SetPadding(50, 50, 50, 50);

                SetReturnType(roundedEntry.ReturnButtonType);

                // Switch the stroke color to blue if the field is in focus and it doesn't have a validation error
                editText.FocusChange += EditText_FocusChange;

                // Update UI when text is changed
                editText.TextChanged += (object sender, Android.Text.TextChangedEventArgs textChangedEventArgs) =>
                {
                    UpdateControlUI();
                };

                UpdateControlUI();
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
            RoundedEntry roundedEntry = (RoundedEntry)this.Element;
            roundedEntry.EntryIsFocused = e.HasFocus;

            UpdateControlUI();
            AddKeyboardPlaceholder(roundedEntry, roundedEntry.UseKeyboardPlaceholder && e.HasFocus);

            roundedEntry.EntryFocusChangedDelegate(sender, new FocusEventArgs(roundedEntry, e.HasFocus));
        }

        public void UpdateControlUI()
        {
            RoundedEntry roundedEntry = (RoundedEntry)this.Element;
            FormsEditText editText = (FormsEditText)this.Control;

            // Remove the underline in the android entry field
            GradientDrawable gradientDrawable = new GradientDrawable();
            gradientDrawable.SetCornerRadius(5);

            //set stroke
            if (roundedEntry.ShowError)
            {
                gradientDrawable.SetStroke(4, roundedEntry.ErrorColor.ToAndroid());
            }
            else if (!roundedEntry.ShowError && editText.IsFocused)
            {
                gradientDrawable.SetStroke(4, roundedEntry.FocusedBorderColor.ToAndroid());
            }
            else
            {
                gradientDrawable.SetStroke(4, Color.Transparent.ToAndroid());
            }

            //set background color
            var bgColor = editText.IsFocused
                    ? roundedEntry.FocusedBackgroundColor.ToAndroid()
                    : roundedEntry.BackgroundColor.ToAndroid();
            gradientDrawable.SetColor(bgColor);

            //handle toggling visibility to image on focus
            if (editText.IsFocused)
            {
                if (!roundedEntry.AlwaysShowRightImage)
                {
                    editText.SetCompoundDrawablesRelativeWithIntrinsicBounds(0, 0, _imageResourceId, 0);
                }
            }
            else
            {
                if (!roundedEntry.AlwaysShowRightImage)
                {
                    editText.SetCompoundDrawablesRelativeWithIntrinsicBounds(0, 0, 0, 0);
                }
            }

            Control.Background = gradientDrawable;
        }

        private void SetReturnType(RoundedEntryReturn type)
        {
            var roundedEntry = (RoundedEntry)this.Element;
            var editText = (EditText)this.Control;

            switch (type)
            {
                case RoundedEntryReturn.Next:
                    editText.ImeOptions = ImeAction.Next;
                    editText.SetImeActionLabel("Next", ImeAction.Next);
                    // Editor Action is called when the return button is pressed
                    Control.EditorAction += (object sender, Android.Widget.TextView.EditorActionEventArgs eventArgs) =>
                    {
                        roundedEntry.OnNext();
                    };
                    break;
                case RoundedEntryReturn.Search:
                    editText.ImeOptions = ImeAction.Search;
                    editText.SetImeActionLabel("Search", ImeAction.Search);
                    break;
                default:
                    editText.ImeOptions = ImeAction.Done;
                    editText.SetImeActionLabel("Done", ImeAction.Done);
                    break;
            }
        }

        public void SetImage(string imageSource)
        {

            EditText editText = (EditText)this.Control;
            _imageResourceId = Resources.GetIdentifier(imageSource, "drawable", Context.PackageName);

            if (_imageResourceId != 0)
            {
                if (!editText.HasOnClickListeners)
                {
                    editText.SetOnTouchListener(new OnDrawableTouchListener());
                }
                editText.SetCompoundDrawablesRelativeWithIntrinsicBounds(0, 0, _imageResourceId, 0);
            }

            UpdateControlUI();
        }

        BoxView keyboardPlaceholder;
        private void AddKeyboardPlaceholder(RoundedEntry roundedEntry, bool shouldAdd)
        {
            if (roundedEntry != null && roundedEntry.Parent is StackLayout stackLayout)
            {
                if (shouldAdd)
                {
                    // Get the Height of the screen
                    DisplayMetrics displayMetrics = new DisplayMetrics();
                    FormsAppCompatActivity _activity = (FormsAppCompatActivity)Context;
                    _activity.WindowManager.DefaultDisplay.GetMetrics(displayMetrics);
                    double screenHeight = displayMetrics.HeightPixels / displayMetrics.Density;

                    // Calculate the height of the area below the rounded entry field
                    double height = screenHeight - roundedEntry.Y - roundedEntry.Height;
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
                                    //if plain text
                                    if(editText.TransformationMethod == null)
                                    {
                                        roundedEntryRenderer.SetImage(roundedEntry.RightImageSource);
                                        editText.TransformationMethod = PasswordTransformationMethod.Instance;
                                    }
                                    //else secure bullets
                                    else
                                    {
                                        roundedEntryRenderer.SetImage(roundedEntry.HidePasswordImageSource);
                                        editText.TransformationMethod = null;
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
