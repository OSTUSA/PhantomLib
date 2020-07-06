using System.ComponentModel;
using System.Linq;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using PhantomLib.iOS.Renderers;
using PhantomLib.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(GradientContentView), typeof(GradientContentViewRenderer))]
namespace PhantomLib.iOS.Renderers
{
    public class GradientContentViewRenderer : ViewRenderer
    {
        NSNumber[] _startLocations;

        private CAGradientLayer _gradientLayer;

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                return;
            }

            UpdateGradientLayer();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == GradientContentView.ColorsProperty.PropertyName ||
                    e.PropertyName == GradientContentView.StartPointProperty.PropertyName ||
                    e.PropertyName == GradientContentView.EndPointProperty.PropertyName ||
                    e.PropertyName == GradientContentView.LocationsProperty.PropertyName ||
                    e.PropertyName == GradientContentView.CornerRadiusProperty.PropertyName)
            {
                UpdateGradientLayer();
            }
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            if (_gradientLayer != null && NativeView != null)
            {
                CATransaction.Begin();

                CATransaction.DisableActions = true;

                _gradientLayer.Frame = NativeView.Bounds;

                CATransaction.Commit();
            }
        }

        private void UpdateGradientLayer()
        {
            if (Element is GradientContentView element)
            {
                if (element.BackgroundColor == Color.Default && element.Colors != null)
                {
                    if (_gradientLayer == null)
                    {
                        _gradientLayer = new CAGradientLayer();
                    }

                    _gradientLayer.NeedsDisplayOnBoundsChange = true;
                    _gradientLayer.MasksToBounds = true;
                    _gradientLayer.CornerRadius = element.CornerRadius;

                    _gradientLayer.StartPoint = new CGPoint(element.StartPoint.X, element.StartPoint.Y);
                    _gradientLayer.EndPoint = new CGPoint(element.EndPoint.X, element.EndPoint.Y);

                    _gradientLayer.Colors = element.Colors.Select(x => x.ToCGColor()).ToArray();

                    _startLocations = element.Locations.Select(x => new NSNumber(x)).ToArray();

                    _gradientLayer.Locations = _startLocations;

                    if (_gradientLayer.SuperLayer == null)
                    {
                        NativeView.Layer.InsertSublayer(_gradientLayer, 0);
                    }
                }
                else
                {
                    _gradientLayer?.RemoveFromSuperLayer();
                }
            }
        }
    }
}
