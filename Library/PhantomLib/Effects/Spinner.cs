using System;
using Xamarin.Forms;
namespace PhantomLib.Effects
{
    public class Spinner : RoutingEffect
    {
        public Spinner()
         : base($"OST.PhantomLib.{nameof(Spinner)}")
        {
        }

        public int Duration { get; set; }
        public int RepeatCount { get; set; }
    }
}
