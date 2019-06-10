using System;
using CoreAnimation;
using PhantomLib.CustomControls;
using PhantomLib.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(RoundedFrame), typeof(RoundedFrameRenderer))]
namespace PhantomLib.iOS.Renderers
{
    public class RoundedFrameRenderer : FrameRenderer
    {
        public override void LayoutSublayersOfLayer(CALayer layer)
        {
            base.LayoutSublayersOfLayer(layer);

            var roundedFrame = Element as RoundedFrame;

            CACornerMask corners = new CACornerMask();
            if (roundedFrame.RoundTopLeft)
                corners = CACornerMask.MinXMinYCorner;
            if (roundedFrame.RoundTopRight)
                corners = corners | CACornerMask.MaxXMinYCorner;
            if (roundedFrame.RoundBottomLeft)
                corners = corners | CACornerMask.MaxXMaxYCorner;
            if (roundedFrame.RoundBottomRight)
                corners = corners | CACornerMask.MinXMaxYCorner;

            this.ClipsToBounds = true;
            this.Layer.MaskedCorners = corners;
        }
    }
}
