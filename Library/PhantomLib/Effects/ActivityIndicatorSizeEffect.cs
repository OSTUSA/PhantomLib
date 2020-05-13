using Xamarin.Forms;

namespace PhantomLib.Effects
{
    public class ActivityIndicatorSizeEffect : RoutingEffect
    {
        public ActivityIndicatorSize Size { get; set; }

        public ActivityIndicatorSizeEffect() : base($"{ResolutionGroupName.PhantomLib}.{nameof(ActivityIndicatorSizeEffect)}") { }
    }
}
