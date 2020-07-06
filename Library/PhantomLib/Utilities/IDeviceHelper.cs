using Xamarin.Forms;

namespace PhantomLib.Utilities
{
    public interface IDeviceHelper
    {
        Thickness GetSafeAreaInsets();

        float GetDisplayWidth();

        float GetDisplayHeight();
    }
}
