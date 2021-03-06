using System;
using System.Collections.Generic;
using System.Linq;
using SystemLocalStore.models;
using Util;

namespace SystemLocalStore
{
    public class QueueItem<T> where T : AbsUpsTable
    {
        public bool IsScheduled { get; set; }
        public bool IsCompleted { get; set; }
        protected T item { get; set; }

        private Object loadKeys;
        private Object properties;

        public QueueItem(T obj = null) { item = obj; }

        private QueueItem<T> Open() { IsScheduled = true; IsCompleted = false; return this; }
        public QueueItem<T> Close() { IsCompleted = true; IsScheduled = false; return this; }

        private T Load()
        {
            var cond=new List<string>();
            loadKeys.RunProperties((k, v) => {
            cond.Add($"{k} = @{k}");
            });
            return SysDB<T>.LoadOrNew(string.Join(" AND ",cond),loadKeys);
        }

        public QueueItem<T> SetItem(T itm) { item = itm; return this; }

        private void SetLoader()
        {
            if (null == properties) return;
            if (null == loadKeys) loadKeys = ObjectExtension.AnObject();
            var cols = (string[])typeof(T).GetMethod("UpsColumns").Invoke(null, null);
            // var props=
            properties.RunProperties((name, value) =>
            {
                if (!cols.Contains(name)) return;
                loadKeys.SetProperties(name, value);
            });
        }

        public QueueItem<T> SetProperties(object parameters)
        {
            Open();
            if (null == properties) properties = ObjectExtension.AnObject(parameters);
            else properties.SetProperties(parameters);
            return this;
        }

        public void Commit()
        {
            if (null == item) item = Load();
            SetLoader();
            item.SetProperties(properties).SetProperties(loadKeys);
            item.UpSert();
            Close();
        }
    }
}
