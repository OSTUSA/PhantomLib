using System;
using PhantomLib.Utilities;
using Xamarin.Forms;

[assembly: Dependency(typeof(PhantomLib.Droid.Utilities.DeviceHelper))]
namespace PhantomLib.Droid.Utilities
{
    public class DeviceHelper : IDeviceHelper
    {
        public Thickness GetSafeAreaInsets()
        {
            // Android handles safe areas and display cutouts a little differently
            // than iOS: https://developer.android.com/guide/topics/display-cutout
            // We don't need to adjust the padding for cutouts because this is
            // done by the OS.
            return new Thickness(0);
        }
    }
}
