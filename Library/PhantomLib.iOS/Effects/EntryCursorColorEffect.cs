using System;
using System.Linq;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(PhantomLib.iOS.Effects.EntryCursorColorEffect), nameof(PhantomLib.Effects.EntryCursorColorEffect))]
namespace PhantomLib.iOS.Effects
{
    public class EntryCursorColorEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var effect = Element.Effects.FirstOrDefault(x => x is PhantomLib.Effects.EntryCursorColorEffect) as PhantomLib.Effects.EntryCursorColorEffect;

            if (effect != null)
            {
                SetCursorColor(effect.CursorColor);
            }
        }

        protected override void OnDetached()
        {
            SetCursorColor(Color.Default);
        }

        private void SetCursorColor(Color color)
        {
            try
            {
                if (Control is UITextField textField)
                {
                    if (color == Color.Default)
                    {
                        // Reset to system tint
                        textField.TintColor = UIColor.SystemBlueColor;
                    }
                    else
                    {
                        textField.TintColor = color.ToUIColor();
                    }
                }
                else if (Control is UITextView textView)
                {
                    if (color == Color.Default)
                    {
                        textView.TintColor = UIColor.SystemBlueColor;
                    }
                    else
                    {
                        textView.TintColor = color.ToUIColor();
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"[EntryClearButtonModeEffect] Cannot be attached to control of type '{Control.GetType()}'");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[EntryClearButtonModeEffect] Exception thrown: {ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}