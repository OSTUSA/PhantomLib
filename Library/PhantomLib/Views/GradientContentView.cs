using Xamarin.Forms;

namespace PhantomLib.Views
{
    public class GradientContentView : ContentView
    {
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(float), typeof(GradientContentView), 0f);

        public float CornerRadius
        {
            get { return (float)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly BindableProperty ColorsProperty = BindableProperty.Create(nameof(Colors), typeof(Color[]), typeof(GradientContentView), new Color[] { Color.Default, Color.Default });

        public Color[] Colors
        {
            get { return (Color[])GetValue(ColorsProperty); }
            set { SetValue(ColorsProperty, value); }
        }

        public static readonly BindableProperty LocationsProperty = BindableProperty.Create(nameof(Locations), typeof(float[]), typeof(GradientContentView), new float[] { 0f, 1f });

        public float[] Locations
        {
            get { return (float[])GetValue(LocationsProperty); }
            set { SetValue(LocationsProperty, value); }
        }

        public static readonly BindableProperty StartPointProperty = BindableProperty.Create(nameof(StartPoint), typeof(Point), typeof(GradientContentView), new Point(0.5d, 0d));

        public Point StartPoint
        {
            get { return (Point)GetValue(StartPointProperty); }
            set { SetValue(StartPointProperty, value); }
        }

        public static readonly BindableProperty EndPointProperty = BindableProperty.Create(nameof(EndPoint), typeof(Point), typeof(GradientContentView), new Point(0.5d, 1d));

        public Point EndPoint
        {
            get { return (Point)GetValue(EndPointProperty); }
            set { SetValue(EndPointProperty, value); }
        }
    }
}
