using System;
using System.ComponentModel;

namespace PhantomLib.Models
{
    /// <summary>
    /// Interface for classes that will notify when their properties are changing
    /// or have changed.
    /// </summary>
    public interface IAttachable : INotifyPropertyChanged, INotifyPropertyChanging
    {

    }
}
