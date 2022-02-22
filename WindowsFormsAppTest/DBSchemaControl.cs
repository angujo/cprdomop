using DatabaseProcessor;
using System;
using System.Drawing;
using System.Windows.Forms;
using SystemLocalStore;
using SystemLocalStore.models;
using Util;

namespace WindowsFormsAppTest
{
    public partial class DBSchemaControl : UserControl
    {
        private DBSchema schema;
        public DBSchemaControl()
        {
            InitializeComponent();
        }

        public void loadSchema(string s_type, WorkLoad workLoad)
        {
            //pbProgress.Visible = true;

            schema = SysDB<DBSchema>.Load(new { WorkLoadId = workLoad.Id, SchemaType = s_type }) ?? new DBSchema();// DataAccess.loadSchema(workLoad, s_type.ToLower()) ?? new DBSchema();
            if (!schema.Exists())
            {
                schema.SchemaType = s_type;
                schema.WorkLoadId = (long)workLoad.Id;
            }

            schema.changeEvent = propName =>
            {
                btnSave.Enabled = true;
                btnTest.Enabled = false;
                lbSave.ForeColor = Color.Red;
                lbSave.Text = "Save Required!";
            };
            btnTest.Enabled = !schema.TestSuccess;
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
                schema.TestSuccess = false;
                schema.InsertOrUpdate();
                btnSave.Enabled = false;
                btnTest.Enabled = true;
                lbSave.ForeColor = Color.Green;
                lbSave.Text = "All Save!";
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                pbProgress.Visible = false;
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            pbProgress.Visible = true;
            var retry = false;
            try
            {
                if (DBMSystem.GetDBMSystem(schema).TestConnection()) MessageBox.Show(null, "Connection Successful!", "Connection Test", MessageBoxButtons.OK, MessageBoxIcon.Information);
                schema.TestSuccess = true;
                schema.InsertOrUpdate();
                btnTest.Enabled = false;
            }
            catch (Exception ex)
            {
                retry = MessageBox.Show(null, $"{ex.Message}\n{ex.StackTrace}".Truncate(400), "Connection Test", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry;
            }
            finally
            {
                pbProgress.Visible = false;
                if (retry) btnTest_Click(sender, e);
            }
        }
    }
}
