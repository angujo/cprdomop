using System;
using System.Windows.Forms;
using SystemLocalStore;
using SystemLocalStore.models;

namespace WindowsFormsAppTest
{
    public partial class DBSchemaControl : UserControl
    {
        private DBSchema schema;
        public DBSchemaControl()
        {
            InitializeComponent();
        }

        public async void loadSchema(string s_type, WorkLoad workLoad)
        {
            //pbProgress.Visible = true;
            /* await Task.Run(() =>
              {
              });*/
            schema = DataAccess.loadSchema(workLoad, s_type.ToLower()) ?? new DBSchema();
            schema.SchemaType = s_type;
            schema.WorkLoadId = workLoad.Id;
            //pbProgress.Visible = false;
            DoBindings();
        }

        private void DoBindings()
        {
            gbSchemaHolder.Text = $"{schema.SchemaType} Schema";
            tbServer.Name = $"{tbServer.Name}_{schema.SchemaType}";
            tbServer.DataBindings.Add("Text", schema, "Server");
            tbPort.Name = $"{tbPort.Name}_{schema.SchemaType}";
            tbPort.DataBindings.Add("Value", schema, "Port");
            tbDatabase.Name = $"{tbDatabase.Name}_{schema.SchemaType}";
            tbDatabase.DataBindings.Add("Text", schema, "DBName");
            tbSchema.Name = $"{tbSchema.Name}_{schema.SchemaType}";
            tbSchema.DataBindings.Add("Text", schema, "SchemaName");
            tbUsername.Name = $"{tbUsername.Name}_{schema.SchemaType}";
            tbUsername.DataBindings.Add("Text", schema, "Username");
            tbPassword.Name = $"{tbPassword.Name}_{schema.SchemaType}";
            tbPassword.DataBindings.Add("Text", schema, "Password");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            pbProgress.Visible = true;
            try
            {
                Console.WriteLine("Schema Save Clicked");
                DataAccess.InsertOrUpdate(schema);
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                pbProgress.Visible = false;
            }
        }
    }
}
