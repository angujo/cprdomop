namespace SystemLocalStore.models
{
    public abstract class AbsUpsTable : AbsTable
    {
        public static string[] UpsColumns() { return new string[] { "Id" }; }

        public void UpSert()
        {
            DataAccess.Upsert(this);
        }
    }
}
