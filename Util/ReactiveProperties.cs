using System.Collections.Generic;
using System.ComponentModel;

namespace Util
{
    public abstract class ReactiveProperties : INotifyPropertyChanged
    {
        private Dictionary<string, object> _properties = new Dictionary<string, object>();

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnChange(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(name));
        }

        protected T getProperty<T>(string prop_name) { return _properties.ContainsKey(prop_name) ? (T)_properties[prop_name] : default(T); }
        protected void setProperty(string prop_name, object value) { _properties[prop_name] = value; OnChange(prop_name); }
    }
}
