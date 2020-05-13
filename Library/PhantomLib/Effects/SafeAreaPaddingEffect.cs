using Xamarin.Forms;

namespace PhantomLib.Effects
{
    public class SafeAreaPaddingEffect : RoutingEffect
    {
        public SafeAreaFlags Flags { get; set; }

        public SafeAreaPaddingEffect() : base($"{ResolutionGroupName.PhantomLib}.{nameof(SafeAreaPaddingEffect)}") { }
    }
}
