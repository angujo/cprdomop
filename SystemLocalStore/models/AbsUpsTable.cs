using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemLocalStore.models
{
    public abstract class AbsUpsTable : AbsTable
    {
        public abstract string[] UpsColumns();

        public void UpSert()
        {
            DataAccess.Upsert(this);
        }
    }
}
