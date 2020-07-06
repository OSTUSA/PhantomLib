using System;
namespace PhantomLib.Effects
{
    [Flags]
    public enum SafeAreaFlags
    {
        None = 0,
        Left = 1,
        Top = 2,
        Right = 4,
        Bottom = 8
    }
}
