using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SystemLocalStore
{
    internal class SimpleCRUDResolver : SimpleCRUD.ITableNameResolver,SimpleCRUD.IColumnNameResolver
    {
        public string ResolveColumnName(PropertyInfo propertyInfo)
        {
            return propertyInfo.Name.ToLower();
        }

        public string ResolveTableName(Type type)
        {
            return type.Name.ToLower();
        }
    }
}
