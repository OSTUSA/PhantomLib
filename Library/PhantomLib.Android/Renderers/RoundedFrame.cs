using Android.Content;
using Android.Graphics.Drawables;
using PhantomLib.Android.Renderers;
using PhantomLib.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(RoundedFrame), typeof(CustomFrameRenderer))]
namespace PhantomLib.Android.Renderers
{
    public class CustomFrameRenderer : FrameRenderer
    {
        public CustomFrameRenderer(Context context)
            : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null && e.OldElement == null)
            {
                GradientDrawable drawable = new GradientDrawable();

                var roundedFrame = e.NewElement as RoundedFrame;
                float[] radius = new float[8];

                radius[0] = roundedFrame.RoundTopLeft ? Context.ToPixels(roundedFrame.CornerRadius) : 0;     //Top Left corner
                radius[1] = roundedFrame.RoundTopLeft ? Context.ToPixels(roundedFrame.CornerRadius) : 0;     //Top Left corner
                radius[2] = roundedFrame.RoundTopRight ? Context.ToPixels(roundedFrame.CornerRadius) : 0;    //Top Right corner
                radius[3] = roundedFrame.RoundTopRight ? Context.ToPixels(roundedFrame.CornerRadius) : 0;    //Top Right corner
                radius[4] = roundedFrame.RoundBottomRight ? Context.ToPixels(roundedFrame.CornerRadius) : 0; //Bottom Right corner
                radius[5] = roundedFrame.RoundBottomRight ? Context.ToPixels(roundedFrame.CornerRadius) : 0; //Bottom Right corner
                radius[6] = roundedFrame.RoundBottomLeft ? Context.ToPixels(roundedFrame.CornerRadius) : 0;  //Bottom Left corner
                radius[7] = roundedFrame.RoundBottomLeft ? Context.ToPixels(roundedFrame.CornerRadius) : 0;  //Bottom Left corner
                drawable.SetCornerRadii(radius);
                drawable.SetColor(roundedFrame.BackgroundColor.ToAndroid());
                this.SetBackground(drawable);
            }
        }
    }
}
