using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SystemLocalStore.models
{
    public abstract class AbsTable : INotifyPropertyChanged
    {
        // Hold the ID here
        private Int64 _id = 0;

        public Action<string> changeEvent;

        private Dictionary<string, dynamic> _values = new Dictionary<string, dynamic>();

        public event PropertyChangedEventHandler PropertyChanged;

        private string[] ignoreColumns = { "id", "propertychanged", "_values" };

        public Int64 Id { get { return _id; } set { _id = value; } }

        public bool Exists()
        {
            return Id != null && default(Int64) != Id && 0 < Id;
        }

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
            if (changeEvent != null) changeEvent(prop_name);
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

        public long? InsertOrUpdate()
        {
            return DataAccess.InsertOrUpdate(this);
        }

        public static void InsertOrUpdate<T>(List<T> items)
        {
            foreach (var item in items) DataAccess.InsertOrUpdate(item);
        }

        public AbsTable InsertOrUpdate(bool returnObject) { InsertOrUpdate(); return this; }

        public bool Delete(Object parameters = null)
        {
            return DataAccess.Delete(this.GetType().Name, null == parameters ? this : parameters);
        }
        public static bool Delete<T>(Object parameters = null)
        {
            return DataAccess.Delete(typeof(T).Name, parameters);
        }

        public AbsTable ShallowCopy() { return (AbsTable)this.MemberwiseClone(); }

        public static T Load<T>(Object parameters = null)
        {
            return DataAccess.Load<T>(typeof(T).Name, parameters);
        }
        public static List<T> List<T>(Object parameters = null)
        {
            return DataAccess.LoadList<T>(typeof(T).Name, parameters);
        }
    }
}
