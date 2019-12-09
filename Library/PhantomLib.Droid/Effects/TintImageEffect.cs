using System.Linq;
using Android.Graphics;
using Android.Widget;
using Java.Lang;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(PhantomLib.Droid.Effects.TintImageEffect), nameof(PhantomLib.Effects.TintImageEffect))]
namespace PhantomLib.Droid.Effects
{
    public class TintImageEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                var effect = (PhantomLib.Effects.TintImageEffect)Element.Effects.FirstOrDefault(e => e is PhantomLib.Effects.TintImageEffect);

                if (effect == null || !(Control is ImageView image))
                    return;

                var filter = new PorterDuffColorFilter(effect.TintColor.ToAndroid(), PorterDuff.Mode.SrcIn);
                image.SetColorFilter(filter);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"An error occurred when setting the {typeof(PhantomLib.Effects.TintImageEffect)} effect: {ex}");
            }
        }

        protected override void OnDetached()
        {
        }
    }
}

