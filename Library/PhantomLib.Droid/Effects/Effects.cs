using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.Android;

namespace PhantomLib.Droid.Effects
{
    [Preserve(AllMembers = true)]
    public static class Effects
    {
#pragma warning disable 414
        static List<PlatformEffect> allEffects = new List<PlatformEffect>();
#pragma warning restore 414

        /// <summary>
        /// On iOS, we need to reference the effects in order for them to be available. This class does that.
        /// </summary>
        public static void Init()
        {
            allEffects = new List<PlatformEffect>(typeof(Effects).Assembly.GetTypes().Where(t => typeof(PlatformEffect).IsAssignableFrom(t)).Select(t => (PlatformEffect)Activator.CreateInstance(t)));
        }
    }
}
