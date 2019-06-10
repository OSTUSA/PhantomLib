using Foundation;
using PhantomLib.Effects;
using PhantomLib.iOS.Effects;
using System.Linq;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(KerningEffect), nameof(Kerning))]
namespace PhantomLib.iOS.Effects
{
    public class KerningEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var effect = (Kerning)Element.Effects.FirstOrDefault(x => x is Kerning);
            if (effect == null)
                return;

            if (Control is UILabel)
            {
                var label = Control as UILabel;

                if (label.AttributedText != null && !string.IsNullOrEmpty(label.AttributedText.Value))
                {
                    NSRange effectiveRange;
                    var attr = new NSMutableDictionary(label.AttributedText.GetAttributes(0, out effectiveRange));
                    attr.SetValueForKey(new NSNumber(effect.LetterSpacing), UIStringAttributeKey.KerningAdjustment);

                    label.AttributedText = new NSAttributedString(label.AttributedText.Value, attr);
                }
            }
            else if (Control is UITextView)
            {
                var textView = Control as UITextView;

                if (textView.AttributedText != null && !string.IsNullOrEmpty(textView.AttributedText.Value))
                {
                    NSRange effectiveRange;
                    var attr = new NSMutableDictionary(textView.AttributedText.GetAttributes(0, out effectiveRange));
                    attr.SetValueForKey(new NSNumber(effect.LetterSpacing), UIStringAttributeKey.KerningAdjustment);

                    textView.AttributedText = new NSAttributedString(textView.AttributedText.Value, attr);
                }
            }
        }

        protected override void OnDetached() { }
    }
}
