using PhantomLib.Utilities;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(PhantomLib.iOS.Utilities.DeviceHelper))]
namespace PhantomLib.iOS.Utilities
{
    public class DeviceHelper : IDeviceHelper
    {
        private UIEdgeInsets _safeInsets;
        private bool _safeInsetsRetrieved;

        public Thickness GetSafeAreaInsets()
        {
            if (!_safeInsetsRetrieved)
            {
                try
                {
                    // try to retrieve the safe area insets from the OS
                    if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
                    {
                        var window = UIApplication.SharedApplication.KeyWindow ?? new UIWindow(UIScreen.MainScreen.Bounds);
                        _safeInsets = window.SafeAreaInsets;
                    }
                    else
                    {
                        _safeInsets = UIEdgeInsets.Zero;
                    }
                }
                catch
                {
                    _safeInsets = UIEdgeInsets.Zero;
                }

                _safeInsetsRetrieved = true;
            }

            return new Thickness(_safeInsets.Left, _safeInsets.Top, _safeInsets.Right, _safeInsets.Bottom);
        }

        public float GetDisplayWidth()
        {
            return (float)UIScreen.MainScreen.Bounds.Width;
        }

        public float GetDisplayHeight()
        {
            return (float)UIScreen.MainScreen.Bounds.Height;
        }
    }
}
