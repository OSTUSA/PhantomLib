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

[assembly: ExportRenderer(typeof(RoundedEntry), typeof(RoundedEntryRenderer))]
namespace PhantomLib.Droid.Renderers
{
    public class RoundedEntryRenderer : EntryRenderer
    {
        private int _rightImageResourceId = 0;

        public RoundedEntryRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (this.Control != null && this.Element != null)
            {
                RoundedEntry roundedEntry = (RoundedEntry)this.Element;
                EditText editText = (EditText)this.Control;

                editText.SetPadding(50, 50, 50, 50);
                UpdateControlUI();

                SetImage(roundedEntry.RightImageSource);
                SetReturnType(roundedEntry.ReturnButton);

                // Switch the stroke color to blue if the field is in focus and it doesn't have a validation error
                editText.FocusChange += EditText_FocusChange;

                // Update UI when text is changed
                editText.TextChanged += (object sender, Android.Text.TextChangedEventArgs textChangedEventArgs) =>
                {
                    UpdateControlUI();
                };
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var roundedEntry = (RoundedEntry)this.Element;

            switch (e.PropertyName)
            {
                case nameof(roundedEntry.RightImageSource):
                case nameof(roundedEntry.AlwaysShowRightImage):
                    SetImage(roundedEntry.RightImageSource);
                    break;
                case nameof(roundedEntry.ErrorColor):
                case nameof(roundedEntry.ShowError):
                case nameof(roundedEntry.FocusedBackgroundColor):
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

            //set background color and handle image
            if (editText.IsFocused)
            {
                gradientDrawable.SetColor(roundedEntry.FocusedBackgroundColor.ToAndroid());
                //add image
                if (!roundedEntry.AlwaysShowRightImage)
                {
                    editText.SetCompoundDrawablesRelativeWithIntrinsicBounds(0, 0, _rightImageResourceId, 0);
                }
            }
            else
            {
                gradientDrawable.SetColor(roundedEntry.BackgroundColor.ToAndroid());
                //remove image
                if (!roundedEntry.AlwaysShowRightImage)
                {
                    editText.SetCompoundDrawablesRelativeWithIntrinsicBounds(0, 0, 0, 0);
                }
            }

            Control.Background = gradientDrawable;
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

        private void SetReturnType(RoundedEntryReturnType type)
        {
            var roundedEntry = (RoundedEntry)this.Element;
            var editText = (EditText)this.Control;

            switch (type)
            {
                case RoundedEntryReturnType.Next:
                    editText.ImeOptions = ImeAction.Next;
                    editText.SetImeActionLabel("Next", ImeAction.Next);
                    // Editor Action is called when the return button is pressed
                    Control.EditorAction += (object sender, Android.Widget.TextView.EditorActionEventArgs eventArgs) =>
                    {
                        roundedEntry.OnNext();
                    };
                    break;
                case RoundedEntryReturnType.Search:
                    editText.ImeOptions = ImeAction.Search;
                    editText.SetImeActionLabel("Search", ImeAction.Search);
                    break;
                default:
                    editText.ImeOptions = ImeAction.Done;
                    editText.SetImeActionLabel("Done", ImeAction.Done);
                    break;
            }
        }

        private void SetImage(string rightImageSource)
        {
            RoundedEntry roundedEntry = (RoundedEntry)this.Element;
            EditText editText = (EditText)this.Control;

            //add image if RightImageSource is defined
            if (roundedEntry.RightImageSource != null)
            {
                var resourceId = Resources.GetIdentifier(roundedEntry.RightImageSource, "drawable", Context.PackageName);

                if (resourceId != 0)
                {
                    _rightImageResourceId = resourceId;
                    editText.SetOnTouchListener(new OnDrawableTouchListener());
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
                            if(roundedEntry.ShouldClearTextOnClick)
                                editText.Text = "";

                            roundedEntry.RightImageTouchedDelegate(v, new EventArgs());
                        }

                        roundedEntryRenderer.UpdateControlUI();
                    }
                    return true;
                }
            }
            return false;
        }
    }

}
