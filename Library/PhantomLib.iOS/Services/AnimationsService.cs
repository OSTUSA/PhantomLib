using PhantomLib.iOS.Services;
using PhantomLib.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(AnimationsService))]
namespace PhantomLib.iOS.Services
{
    public class AnimationsService : IAnimationsService
    {
        public bool AnimationsEnabled()
        {
            //iOS doesn't currently support disabling of animations
            return true;
        }
    }
}
