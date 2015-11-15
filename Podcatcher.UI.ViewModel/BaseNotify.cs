using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podcatcher.UI.ViewModel
{
    public abstract class BaseNotify : INotifyPropertyChanged
    {
        public delegate void PropertyPreChangedEventHandler(BaseNotify sender, string propertyName);

        protected void RaisePropertyChanged(string propertyName)
        {
            if(PrePropertyChanged != null)
            {
                PrePropertyChanged(this, propertyName);
            }
            else
            {
                InvokeHander(propertyName);
            }
        }
        /// <summary>
        /// Invokes the PropertyChanged event immediately on the current thread.
        /// Some UI frameworks may fail if this is a background thread.
        /// </summary>
        /// <param name="propertyName"></param>
        public void InvokeHander(string propertyName)
        {
            var handler = this.PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        /// <summary>
        /// Event fired before PropertyChanged event. Subscribe to this
        /// to control exactly how PropertyChanged is fired, such as from
        /// which thread. If this is not subscribed PropertyChanged will fire
        /// from the current thread.
        /// </summary>
        public static event PropertyPreChangedEventHandler PrePropertyChanged;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
