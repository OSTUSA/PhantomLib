using Xamarin.Forms;

namespace PhantomLib.Effects
{
    public class Kerning : RoutingEffect
    {
        public double LetterSpacing { get; set; }

        public Kerning() : base($"{ResolutionGroupName.PhantomLib}.{nameof(Kerning)}") { }
    }
}
