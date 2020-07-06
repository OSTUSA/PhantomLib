using System;
using System.Linq;
using PhantomLib.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(PhantomLib.iOS.Effects.ActivityIndicatorSizeEffect), nameof(PhantomLib.Effects.ActivityIndicatorSizeEffect))]
namespace PhantomLib.iOS.Effects
{
    public class ActivityIndicatorSizeEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var effect = Element.Effects.FirstOrDefault(x => x is PhantomLib.Effects.ActivityIndicatorSizeEffect) as PhantomLib.Effects.ActivityIndicatorSizeEffect;

            if (effect != null)
            {
                SetActivityIndicatorSize(effect.Size);
            }
        }

        protected override void OnDetached()
        {
            // Set back to default
            SetActivityIndicatorSize(ActivityIndicatorSize.Medium);
        }

        private void SetActivityIndicatorSize(ActivityIndicatorSize size)
        {
            try
            {
                if (Control is UIActivityIndicatorView control && Element is ActivityIndicator formsActivityIndicator)
                {
                    switch (size)
                    {
                        case ActivityIndicatorSize.Medium:
                            if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
                            {
                                control.ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.Medium;
                            }
                            else
                            {
                                control.ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.White;
                            }
                            break;
                        case ActivityIndicatorSize.Large:
                            if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
                            {
                                control.ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.Large;
                            }
                            else
                            {
                                control.ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.WhiteLarge;
                            }
                            break;
                    }

                    // Make sure to override the color after the style has been set
                    control.Color = formsActivityIndicator.Color == Color.Default ? null : formsActivityIndicator.Color.ToUIColor();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"[ActivityIndicatorSizeEffect] Cannot be attached to control of type '{Control.GetType()}'");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ActivityIndicatorSizeEffect] Exception thrown: {ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}
