using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemLocalStore
{
    public static class AccessExtension
    {
        public static T Load<T>(this T abs)
        {
            return (T)abs;
        }
    }
}
