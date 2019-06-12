using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System.Linq;
using Android.OS;
using Android.Widget;
using PhantomLib.Effects;

[assembly: ExportEffect(typeof(PhantomLib.Droid.Effects.KerningEffect), nameof(Kerning))]
namespace PhantomLib.Droid.Effects
{
    public class KerningEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            // Before Lollipop (5.0), setLetterSpacing() was not supported by Android
            if (Build.VERSION.SdkInt < BuildVersionCodes.Lollipop)
                return;

            var effect = (Kerning)Element.Effects.FirstOrDefault(x => x is Kerning);
            if (effect == null)
                return;

            var textView = Control as TextView;
            if (textView == null)
                return;

            var label = Element as Label;
            if (label == null)
                return;

            textView.LetterSpacing = (float)(effect.LetterSpacing / label.FontSize);
        }

        protected override void OnDetached() { }
    }
}
