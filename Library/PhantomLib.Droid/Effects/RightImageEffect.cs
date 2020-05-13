using System;
using System.Linq;
using Android.Graphics.Drawables;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(PhantomLib.Droid.Effects.RightImageEffect), PhantomLib.Effects.RightImageEffect.EFFECT_NAME)]
namespace PhantomLib.Droid.Effects
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
                if (Control is EditText editText)
                {
                    Drawable imageDrawable = null;

                    if (!string.IsNullOrEmpty(rightImage))
                    {
                        rightImage = rightImage.ToLowerInvariant();

                        int imageId = editText.Context.Resources.GetIdentifier(rightImage, "drawable", editText.Context.PackageName);
                        imageDrawable = editText.Context.GetDrawable(imageId);

                        if (tintColor != Color.Default)
                        {
                            imageDrawable.SetColorFilter(new Android.Graphics.PorterDuffColorFilter(tintColor.ToAndroid(), Android.Graphics.PorterDuff.Mode.SrcIn));
                        }
                    }

                    editText.SetCompoundDrawablesRelativeWithIntrinsicBounds(null, null, imageDrawable, null);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[CustomEntryRenderer] Exception thrown: {ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}
