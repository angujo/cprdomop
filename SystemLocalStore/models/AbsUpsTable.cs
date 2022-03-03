using Util;

namespace SystemLocalStore.models
{
    public abstract class AbsUpsTable : AbsTable
    {
        public static string[] UpsColumns() { return new string[] { "Id" }; }
        public static string UpsIndex() { return null; }

        public void UpSert()
        {
            try
            {
                DataAccess.Upsert(this);
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                throw;
            }
        }
    }
}
