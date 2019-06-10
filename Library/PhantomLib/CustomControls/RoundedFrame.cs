using System;
using Xamarin.Forms;

namespace PhantomLib.CustomControls
{
    public class RoundedFrame : Frame
    {
        public static readonly BindableProperty RoundTopLeftProperty = BindableProperty.Create(nameof(RoundTopLeft), typeof(bool), typeof(RoundedFrame), false);
        public static readonly BindableProperty RoundTopRightProperty = BindableProperty.Create(nameof(RoundTopRight), typeof(bool), typeof(RoundedFrame), false);
        public static readonly BindableProperty RoundBottomLeftProperty = BindableProperty.Create(nameof(RoundBottomLeft), typeof(bool), typeof(RoundedFrame), false);
        public static readonly BindableProperty RoundBottomRightProperty = BindableProperty.Create(nameof(RoundBottomRight), typeof(bool), typeof(RoundedFrame), false);

        public bool RoundTopLeft
        {
            get { return (bool)GetValue(RoundTopLeftProperty); }
            set { SetValue(RoundTopLeftProperty, value); }
        }

        public bool RoundTopRight
        {
            get { return (bool)GetValue(RoundTopRightProperty); }
            set { SetValue(RoundTopLeftProperty, value); }
        }

        public bool RoundBottomLeft
        {
            get { return (bool)GetValue(RoundBottomLeftProperty); }
            set { SetValue(RoundTopLeftProperty, value); }
        }

        public bool RoundBottomRight
        {
            get { return (bool)GetValue(RoundBottomRightProperty); }
            set { SetValue(RoundTopLeftProperty, value); }
        }
    }
}
