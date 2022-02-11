using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace SystemStorage.models
{
    public abstract class AbsTable : INotifyPropertyChanged
    {
        private Int64 _id = 0;
        private Dictionary<string, dynamic> _values = new Dictionary<string, dynamic>();

        public event PropertyChangedEventHandler PropertyChanged;

        private string[] ignoreColumns = { "id", "propertychanged", "_values" };

        public Int64 Id { get { return _id; } set { _id = value; } }

        public string TableName()
        {
            return this.GetType().Name;
        }

        public List<string> FillableColumns()
        {
            return this.GetType().GetProperties().Select(p => p.Name).Where(n => !ignoreColumns.Contains(n.ToLower())).ToList();
        }

        protected void OnPropertyChanged(string prop_name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(prop_name));
        }

        protected dynamic getValue<T>(string key)
        {
            return _values.ContainsKey(key) ? _values[key] : default(T);
        }

        protected void setValue(string key, dynamic value)
        {
            _values[key] = value;
            OnPropertyChanged(key);
        }
    }
}
