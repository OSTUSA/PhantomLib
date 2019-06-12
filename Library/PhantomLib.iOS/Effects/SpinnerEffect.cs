using System;
using System.Linq;
using CoreAnimation;
using Foundation;
using PhantomLib.Effects;
using PhantomLib.iOS.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(SpinnerEffect), nameof(Spinner))]
namespace PhantomLib.iOS.Effects
{
    public class SpinnerEffect : PlatformEffect
    {
        private CABasicAnimation _rotation;
        private NSObject _willEnterForegroundObserver;

        protected override void OnAttached()
        {
            _willEnterForegroundObserver = NSNotificationCenter.DefaultCenter.AddObserver(UIApplication.WillEnterForegroundNotification, HandleNotification);

            Control?.AddSubview(new RotateSubview(() => RotateLayerInfinite(Control?.Layer)));
        }

        protected override void OnDetached()
        {
            Control.Layer.RemoveAllAnimations();
            _rotation = null;

            if (_willEnterForegroundObserver != null)
            {
                NSNotificationCenter.DefaultCenter.RemoveObserver(_willEnterForegroundObserver);
                _willEnterForegroundObserver = null;
            }
        }

        private void HandleNotification(NSNotification notification)
        {
            RotateLayerInfinite(Control.Layer);
        }

        private void RotateLayerInfinite(CALayer layer)
        {
            if (layer == null)
                return;

            var effect = (Spinner)Element.Effects.FirstOrDefault(x => x is Spinner);
            if (effect == null)
                return;

            _rotation = CABasicAnimation.FromKeyPath("transform.rotation");

            _rotation.From = new NSNumber(0);
            _rotation.To = new NSNumber(2 * Math.PI);
            _rotation.Duration = effect.Duration / 1000f;
            _rotation.RepeatCount = effect.RepeatCount <= 0 ? float.MaxValue : effect.RepeatCount;

            layer.RemoveAllAnimations();
            layer.AddAnimation(_rotation, "PartyTime");
        }

        private class RotateSubview : UIView
        {
            internal delegate void DoRotation();

            private readonly DoRotation _doRotate;

            public RotateSubview(
                DoRotation rotateLayer
            )
            {
                _doRotate = rotateLayer;
            }

            public override void WillMoveToWindow(UIWindow window)
            {
                base.WillMoveToWindow(window);

                if (window != null)
                    _doRotate();
            }
        }
    }
}
