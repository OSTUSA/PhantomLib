using Android.Content;
using Android.OS;
using PhantomLib.Droid.Services;
using PhantomLib.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(AnimationsService))]
namespace PhantomLib.Droid.Services
{
    public class AnimationsService : IAnimationsService
    {

        public bool AnimationsEnabled()
        {
            float animatorSpeed;
            ContentResolver resolver = Android.App.Application.Context?.ContentResolver;


            if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBeanMr1)
            {
                animatorSpeed = Android.Provider.Settings.Global.GetFloat(
                         resolver,
                        Android.Provider.Settings.Global.AnimatorDurationScale,
                        0);
            }
            else
            {
                animatorSpeed = Android.Provider.Settings.System.GetFloat(
                        resolver,
#pragma warning disable CS0618 // Type or member is obsolete
                        Android.Provider.Settings.System.AnimatorDurationScale,
#pragma warning restore CS0618 // Type or member is obsolete
                        0);
            }
            return animatorSpeed > 0;
        }
    }
}
