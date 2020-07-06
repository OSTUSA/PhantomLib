using System;
using System.Linq;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(PhantomLib.iOS.Effects.RightImageEffect), PhantomLib.Effects.RightImageEffect.EFFECT_NAME)]
namespace PhantomLib.iOS.Effects
{
    public class RightImageEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var effect = Element.Effects.FirstOrDefault(x => x is PhantomLib.Effects.RightImageEffect) as PhantomLib.Effects.RightImageEffect;

            if (effect != null)
            {
                SetRightImage(effect.Source, effect.TintColor);
            }
        }

        protected override void OnDetached()
        {
            SetRightImage(string.Empty, Color.Default);
        }

        private void SetRightImage(string rightImage, Color tintColor)
        {
            try
            {
                if (Control is UITextField textField)
                {
                    if (!string.IsNullOrEmpty(rightImage))
                    {
                        var image = UIImage.FromBundle(rightImage);

                        if (tintColor != Color.Default)
                        {
                            image = image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);

                            textField.RightView = new UIImageView(image)
                            {
                                TintColor = tintColor.ToUIColor()
                            };
                        }
                        else
                        {
                            textField.RightView = new UIImageView(image);
                        }

                        // Make sure the clear button and right view are not active at the same time
                        textField.RightViewMode = textField.ClearButtonMode.Opposite();
                    }
                    else
                    {
                        textField.RightView = null;
                        textField.RightViewMode = UITextFieldViewMode.Never;
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"[RightImageEffect] Cannot be attached to control of type '{Control.GetType()}'");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[RightImageEffect] Exception thrown: {ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}
