using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PhantomLib.DendencyInjection;

namespace PhantomLib.Models
{
    /// <summary>
    /// Base class for <see cref="IAttachable"/> implementations. This abstract
    /// class has helper methods to raise the <see cref="PropertyChanging"/> and
    /// <see cref="PropertyChanged"/> events to notify any listeners of property
    /// changes.
    /// </summary>
    public abstract class BaseAttachable : InjectablePageModel, IAttachable
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public event PropertyChangingEventHandler PropertyChanging = delegate { };

        /// <summary>
        /// Sets a property's value and triggers the <see cref="PropertyChanging"/>
        /// and <see cref="PropertyChanged"/> events.
        /// </summary>
        /// <returns><c>true</c>, if the property was changed, <c>false</c> otherwise.</returns>
        /// <param name="storage">The private backing variable for the property.</param>
        /// <param name="value">The desired value for the property.</param>
        /// <param name="propertyName">The property's name (defaults to CallerMemberName).</param>
        /// <typeparam name="T">The type of the parameter.</typeparam>
        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
                return false;

            OnPropertyChanging(propertyName);

            storage = value;

            OnPropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanging"/> event.
        /// </summary>
        /// <param name="propertyName">Property name (defaults to CallerMemberName).</param>
        protected void OnPropertyChanging([CallerMemberName]string propertyName = null)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">Property name (defaults to CallerMemberName).</param>
        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
