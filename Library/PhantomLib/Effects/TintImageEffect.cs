using Xamarin.Forms;

namespace PhantomLib.Effects
{
    public class TintImageEffect : RoutingEffect
    {
        public Color TintColor { get; set; }

        public TintImageEffect() : base($"OST.PhantomLib.{nameof(TintImageEffect)}")
        {
        }
    }
}
