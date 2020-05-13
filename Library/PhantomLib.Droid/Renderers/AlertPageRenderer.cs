using Android.Content;
using Android.Views;
using PhantomLib.Droid.Renderers;
using PhantomLib.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AlertPage), typeof(AlertPageRenderer))]
namespace PhantomLib.Droid.Renderers
{
    public class AlertPageRenderer : PageRenderer, Android.Views.View.IOnTouchListener
    {
        public AlertPageRenderer(Context context) : base(context) { }

        protected override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();

            if (Parent is ViewGroup parent)
            {
                Android.Views.View parentBackgroundView = parent.GetChildAt(0);

                if (parentBackgroundView != null)
                {
                    parentBackgroundView.SetBackgroundColor(Color.Transparent.ToAndroid());
                }
            }

            ViewGroup.SetOnTouchListener(this);

            FrameRenderer frame = FindFrame(ViewGroup);

            if (frame != null)
            {
                frame.SetOnTouchListener(this);
            }
        }

        public bool OnTouch(Android.Views.View v, MotionEvent e)
        {
            if (e.Action == MotionEventActions.Down)
            {
                // Absorbs "Down" before AlertPageRenderer and takes control of the touch event
                return true;
            }

            if (e.Action == MotionEventActions.Up)
            {
                // Only perform the tap command if the AlertPageRenderer was tapped and the touch has been lifted
                if (v is AlertPageRenderer && Element is AlertPage page && page.IsTapCommandEnabled)
                {
                    page.TappedCommand?.Execute(null);

                    return true;
                }
            }

            return false;
        }

        private FrameRenderer FindFrame(ViewGroup viewGroup)
        {
            if (viewGroup == null)
            {
                return null;
            }

            if (viewGroup is FrameRenderer frameViewRenderer)
            {
                // Found the frame view
                return frameViewRenderer;
            }

            if (viewGroup.ChildCount > 0)
            {
                // Iterate through children
                for (int i = 0; i < viewGroup.ChildCount; i++)
                {
                    var childView = viewGroup.GetChildAt(i);

                    frameViewRenderer = FindFrame(childView as ViewGroup);

                    if (frameViewRenderer != null)
                    {
                        return frameViewRenderer;
                    }
                }
            }

            // View group is not a card view itself and neither are any of its children
            return null;
        }
    }
}
