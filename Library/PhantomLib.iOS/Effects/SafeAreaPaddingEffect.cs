using System;
using System.Linq;
using PhantomLib.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(PhantomLib.iOS.Effects.SafeAreaPaddingEffect), nameof(PhantomLib.Effects.SafeAreaPaddingEffect))]
namespace PhantomLib.iOS.Effects
{
    public class SafeAreaPaddingEffect : PlatformEffect
    {
        private Thickness _padding;

        protected override void OnAttached()
        {
            try
            {
                if (Element is Layout layout)
                {
                    var effect =  Element.Effects.FirstOrDefault(x => x is PhantomLib.Effects.SafeAreaPaddingEffect) as PhantomLib.Effects.SafeAreaPaddingEffect;

                    if (effect != null)
                    {
                        _padding = layout.Padding;

                        if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
                        {
                            var safeArea = UIApplication.SharedApplication.Windows[0].SafeAreaInsets;

                            double left = layout.Padding.Left;

                            if (effect.Flags.HasFlag(SafeAreaFlags.Left))
                            {
                                left += safeArea.Left;
                            }

                            double top = layout.Padding.Top;

                            if (effect.Flags.HasFlag(SafeAreaFlags.Top))
                            {
                                top += safeArea.Top;
                            }

                            double right = layout.Padding.Right;

                            if (effect.Flags.HasFlag(SafeAreaFlags.Right))
                            {
                                right += safeArea.Right;
                            }

                            double bottom = layout.Padding.Bottom;

                            if (effect.Flags.HasFlag(SafeAreaFlags.Bottom))
                            {
                                bottom += safeArea.Bottom;
                            }

                            layout.Padding = new Thickness(left, top, right, bottom);
                        }
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"[PhantomLib.SafeAreaPaddingEffect] Cannot be attached to type {Element.GetType()}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[PhantomLib.SafeAreaPaddingEffect] Exception thrown: {ex.Message}\n{ex.StackTrace}");
            }
        }

        protected override void OnDetached()
        {
            if (Element is Layout layout)
            {
                layout.Padding = _padding;
            }
        }
    }
}
