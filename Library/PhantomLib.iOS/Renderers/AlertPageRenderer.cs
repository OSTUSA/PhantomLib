using Foundation;
using PhantomLib.iOS.Renderers;
using PhantomLib.Pages;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(AlertPage), typeof(AlertPageRenderer))]
namespace PhantomLib.iOS.Renderers
{
    public class AlertPageRenderer : PageRenderer, IUIGestureRecognizerDelegate
    {
        public override void DidMoveToParentViewController(UIViewController parent)
        {
            base.DidMoveToParentViewController(parent);

            if (parent != null)
            {
                parent.ModalPresentationStyle = UIModalPresentationStyle.OverCurrentContext;
            }
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            var tapGestureRecognizer = new UITapGestureRecognizer(() =>
            {
                if (Element is AlertPage page && page.IsTapCommandEnabled)
                {
                    page.TappedCommand?.Execute(null);
                }
            });

            tapGestureRecognizer.Delegate = this;

            View.AddGestureRecognizer(tapGestureRecognizer);

            if (ParentViewController?.View != null)
            {
                ParentViewController.View.BackgroundColor = UIColor.Clear;
            }
        }

        [Export("gestureRecognizer:shouldReceiveTouch:")]
        public bool ShouldReceiveTouch(UIGestureRecognizer gestureRecognizer, UITouch touch)
        {
            // Disable taps on subviews
            return touch.View == View;
        }
    }
}
