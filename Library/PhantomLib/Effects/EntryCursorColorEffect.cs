using Xamarin.Forms;

namespace PhantomLib.Effects
{
    public class EntryCursorColorEffect : RoutingEffect
    {
        public Color CursorColor { get; set; }

        public EntryCursorColorEffect() : base($"{ResolutionGroupName.PhantomLib}.{nameof(EntryCursorColorEffect)}") { }
    }
}
