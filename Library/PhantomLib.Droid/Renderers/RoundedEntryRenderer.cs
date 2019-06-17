using System;
using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Util;
using Android.Views.InputMethods;
using PhantomLib.CustomControls;
using PhantomLib.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(RoundedEntry), typeof(RoundedEntryRenderer))]
namespace PhantomLib.Droid.Renderers
{
    public class RoundedEntryRenderer : EntryRenderer
    {
        public RoundedEntryRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (this.Control != null && this.Element != null)
            {
                RoundedEntry roundedEntry = (RoundedEntry)this.Element;
                FormsEditText editText = (FormsEditText)this.Control;

                UpdateControlUI();

                editText.SetPadding(50, 50, 50, 50);

                //add image if RightImageSource is defined
                //TODO

                RoundedEntryReturnType type = roundedEntry.ReturnButton;
                SetReturnType(type);

                if (type == RoundedEntryReturnType.Next)
                {
                    // Editor Action is called when the return button is pressed
                    editText.EditorAction += (object sender, Android.Widget.TextView.EditorActionEventArgs eventArgs) =>
                    {
                        roundedEntry.OnNext();
                    };
                }

                // Switch the stroke color to blue if the field is in focus and it doesn't have a validation error
                editText.FocusChange += EditText_FocusChange;

                // Update the stroke color when the text changes and the user has clicked the submit button once 
                editText.TextChanged += (object sender, Android.Text.TextChangedEventArgs textChangedEventArgs) =>
                {
                    UpdateControlUI();
                };
            }
        }

        void EditText_FocusChange(object sender, FocusChangeEventArgs e)
        {
            RoundedEntry roundedEntry = (RoundedEntry)this.Element;
            FormsEditText editText = (FormsEditText)this.Control;
            UpdateControlUI();
            AddKeyboardPlaceholder(roundedEntry, roundedEntry.UseKeyboardPlaceholder && e.HasFocus);

            roundedEntry.EntryIsFocused = e.HasFocus;
            roundedEntry.EntryFocusChangedDelegate(sender, new FocusEventArgs(roundedEntry, e.HasFocus));
        }

        private void UpdateControlUI()
        {
            RoundedEntry roundedEntry = (RoundedEntry)this.Element;

            // Remove the underline in the android entry field
            GradientDrawable gradientDrawable = new GradientDrawable();
            gradientDrawable.SetCornerRadius(5);

            //set stroke
            if (roundedEntry.ShowError)
            {
                gradientDrawable.SetStroke(4, roundedEntry.ErrorColor.ToAndroid());
            }
            else if (!roundedEntry.ShowError && roundedEntry.IsFocused)
            {
                gradientDrawable.SetStroke(4, roundedEntry.FocusedBorderColor.ToAndroid());
            }
            else
            {
                gradientDrawable.SetStroke(4, Color.Transparent.ToAndroid());
            }

            //set background color
            if (roundedEntry.IsFocused)
            {
                gradientDrawable.SetColor(roundedEntry.FocusedBackgroundColor.ToAndroid());
            }
            else
            {
                gradientDrawable.SetColor(roundedEntry.BackgroundColor.ToAndroid());
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
            switch (type)
            {
                case RoundedEntryReturnType.Next:
                    Control.ImeOptions = ImeAction.Next;
                    Control.SetImeActionLabel("Next", ImeAction.Next);
                   
                    break;
                case RoundedEntryReturnType.Search:
                    Control.ImeOptions = ImeAction.Search;
                    Control.SetImeActionLabel("Search", ImeAction.Search);
                    break;
                default:
                    Control.ImeOptions = ImeAction.Done;
                    Control.SetImeActionLabel("Done", ImeAction.Done);
                    break;
            }
        }
    }

}
