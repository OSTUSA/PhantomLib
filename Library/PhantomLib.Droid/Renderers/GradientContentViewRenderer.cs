using System.ComponentModel;
using System.Linq;
using Android.Content;
using Android.Graphics;
using Android.Util;
using PhantomLib.Droid.Renderers;
using PhantomLib.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(GradientContentView), typeof(GradientContentViewRenderer))]
namespace PhantomLib.Droid.Renderers
{
    public class GradientContentViewRenderer : ViewRenderer
    {
        private LinearGradient _gradientShader;

        public GradientContentViewRenderer(Context context) : base(context)
        {
            SetWillNotDraw(false);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == GradientContentView.ColorsProperty.PropertyName ||
                e.PropertyName == GradientContentView.LocationsProperty.PropertyName ||
                e.PropertyName == GradientContentView.StartPointProperty.PropertyName ||
                e.PropertyName == GradientContentView.EndPointProperty.PropertyName)
            {
                // End the animation if the gradient properties change
                UpdateGradientShader();
            }
        }

        protected override void OnDraw(Canvas canvas)
        {
            if (Element is GradientContentView element)
            {
                canvas.Save();

                if (element.CornerRadius > 0)
                {
                    float cornerRadius = TypedValue.ApplyDimension(ComplexUnitType.Dip, element.CornerRadius, Context.Resources.DisplayMetrics);

                    var bounds = new RectF(0, 0, Width, Height);

                    var path = new Path();
                    path.Reset();
                    path.AddRoundRect(bounds, cornerRadius, cornerRadius, Path.Direction.Cw);
                    path.Close();

                    canvas.ClipPath(path);
                }
                else
                {
                    element.IsClippedToBounds = true;
                }

                if (_gradientShader == null)
                {
                    UpdateGradientShader();
                }

                var paint = new Paint
                {
                    Dither = true
                };

                paint.SetShader(_gradientShader);

                canvas.DrawPaint(paint);

                canvas.Restore();

                base.OnDraw(canvas);
            }
        }

        private void UpdateGradientShader()
        {
            if (Element is GradientContentView element)
            {
                float x0 = (float)element.StartPoint.X * Width;
                float y0 = (float)element.StartPoint.Y * Height;

                float x1 = (float)element.EndPoint.X * Width;
                float y1 = (float)element.EndPoint.Y * Height;

                int[] colors = element.Colors.Select(x => x.ToAndroid().ToArgb()).ToArray();

                float[] locations = element.Locations;

                _gradientShader = new LinearGradient(x0, y0, x1, y1, colors, locations, Shader.TileMode.Clamp);

                Invalidate();
            }
        }
    }
}
