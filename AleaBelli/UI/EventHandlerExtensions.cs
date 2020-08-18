using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AleaBelli.UI
{
    /// <summary>TODO</summary>
    public static class EventHandlerExtensions
    {
        /// <summary>Raises an event in a standard (mostly thread-safe) manner.</summary>
        public static void Raise<T>(this EventHandler<T> @this, object sender, T e)
           where T : EventArgs
        {
            var handler = @this;
            if (handler != null) handler(sender, e);
        }

        /// <summary>Raises an event in a standard (mostly thread-safe) manner.</summary>
        public static void Raise<T>(this PropertyChangedEventHandler @this, object sender, T e)
           where T : PropertyChangedEventArgs
        {
            var handler = @this;
            if (handler != null) handler(sender, e);
        }

        /// <summary>Raises an event in a standard (mostly thread-safe) manner.</summary>
        public static void Raise<T>(this EventHandler @this, object sender, T e)
           where T : EventArgs
        {
            var handler = @this;
            if (handler != null) handler(sender, e);
        }
    }
}
