using Xamarin.Forms;

namespace PhantomLib.Effects
{
    public class RightImageEffect : RoutingEffect
    {
        public const string EFFECT_NAME = "RightImageEffect";

        public string Source { get; set; }

        public Color TintColor { get; set; }

        public RightImageEffect() : base($"{ResolutionGroupName.PhantomLib}.{nameof(RightImageEffect)}") { }
    }
}
