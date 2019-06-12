using System;
using System.Linq;
using Android.Views;
using Android.Views.Animations;
using PhantomLib.Droid.Effects;
using PhantomLib.Effects;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.ExportEffect(typeof(SpinnerEffect), nameof(Spinner))]
namespace PhantomLib.Droid.Effects
{
    public class SpinnerEffect : PlatformEffect
    {
        private Android.Views.Animations.Animation _rotation;
        private ViewTreeObserver _viewTreeObserver;
        private bool _attached;

        protected override void OnAttached()
        {
            _attached = true;
            _viewTreeObserver = Control.ViewTreeObserver;
            _viewTreeObserver.GlobalLayout += OnGlobalLayout;
        }

        protected override void OnDetached()
        {
            if (_viewTreeObserver != null)
            {
                if (_viewTreeObserver.IsAlive)
                {
                    _viewTreeObserver.GlobalLayout -= OnGlobalLayout;
                }
                else
                {
                    _viewTreeObserver.Dispose();
                }
            }

            if (Control?.Animation is Android.Views.Animations.Animation)
            {
                Control.Animation.Cancel();
                Control.Animation = null;
            }
        }

        private void RotateLayerInfinite()
        {
            if (_rotation == null)
            {
                PhantomLib.Effects.Spinner effect = (PhantomLib.Effects.Spinner)Element.Effects.FirstOrDefault(x => x is PhantomLib.Effects.Spinner);
                if (effect == null)
                {
                    return;
                }

                _rotation = new RotateAnimation(0, 360, Dimension.RelativeToSelf, 0.5f, Dimension.RelativeToSelf, 0.5f)
                {
                    Interpolator = new LinearInterpolator(),
                    Duration = effect.Duration,
                    RepeatCount = effect.RepeatCount <= 0 ? -1 : effect.RepeatCount
                };

                Control.StartAnimation(_rotation);
            }
        }

        private void OnGlobalLayout(object sender, EventArgs e)
        {
            try
            {
                if (_attached && Control.Height > 1)
                {
                    RotateLayerInfinite();
                }
            }
            catch (ObjectDisposedException)
            {
                OnDetached();
            }
        }
    }
}