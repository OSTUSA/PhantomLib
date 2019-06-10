using System;
using Xamarin.Forms;

namespace PhantomLib.Effects
{
    public class Kerning : RoutingEffect
    {
        public Kerning()
         : base($"OST.PhantomLib.{nameof(Kerning)}")
        {
        }

        public double LetterSpacing { get; set; }
    }
}
