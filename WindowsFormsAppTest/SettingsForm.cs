using DatabaseProcessor;
using DatabaseProcessor.postgres;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using SystemLocalStore.models;
using Util;

namespace WindowsFormsAppTest
{
    public partial class SettingsForm : Form
    {
        private Setts setts = new Setts();
        public SettingsForm()
        {
            InitializeComponent();
            Binds();
            SetLoad();
        }

        protected void SetLoad()
        {
            Setting.SetSysPassword();
            setts.LogPath = Setting.LogDirectoryPath ?? Path.Combine(Environment.CurrentDirectory, "logs");
            setts.DBPath = Setting.DBDirectoryPath ?? Environment.CurrentDirectory;
            setts.DBServer = Setting.DBServer;
            setts.DBSchema = Setting.DBSchema;
            setts.DBName = Setting.DBName;
            setts.DBUsername = Setting.DBUsername;
            setts.DBPort = Setting.DBPort;
            setts.DBPass = Setting.DBPassword;
            setts.ServiceName = Setting.ServiceName;
            ConnectionChanged(Setting.DBConnection == (int)DBMSType.SQLITE ? "sqlite" : "postgres", true);
            if (Setting.DBConnection == (int)DBMSType.SQLITE) rbSQLite.Checked = true; else rbPostgreSQL.Checked = true;
        }

        protected void Binds()
        {
            tbDBPath.DataBindings.Add("Text", setts, "DBPath");
            tbLogPath.DataBindings.Add("Text", setts, "LogPath");
            tbDBServer.DataBindings.Add("Text", setts, "DBServer");
            tbDBSchema.DataBindings.Add("Text", setts, "DBSchema");
            tbDBName.DataBindings.Add("Text", setts, "DBName");
            tbDBUsername.DataBindings.Add("Text", setts, "DBUsername");
            tbDBPort.DataBindings.Add("Text", setts, "DBPort");
            tbDBPassword.DataBindings.Add("Text", setts, "DBPass");
            tbService.DataBindings.Add("Text", setts, "ServiceName");
        }

        private void resetPaths()
        {
            if (DialogResult.OK == MessageBox.Show("Are you sure you wish to reset the settings path?\nYou will lose connection to the current paths!", "Path Reset", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                Setting.LogDirectoryPath = Path.Combine(Environment.CurrentDirectory, "logs");
                Setting.DBDirectoryPath = Environment.CurrentDirectory;
                // SetLoad();
            }
        }

        private void SaveProps(PropType? type)
        {
            try
            {
                switch (type)
                {
                    case PropType.DB:
                        if (!string.IsNullOrEmpty(setts.DBPath) && !setts.DBPath.ToLower().Equals(Setting.DBDirectoryPath.ToLower()) &&
                            File.Exists(Setting.DBFilePath) && Directory.Exists(setts.DBPath))
                        {
                            File.Copy(Setting.DBFilePath, Path.Combine(setts.DBPath, Setting.DB_FILE_NAME));
                            Setting.DBDirectoryPath = setts.DBPath;
                        }
                        break;
                    case PropType.LOG:
                        if (!string.IsNullOrEmpty(setts.LogPath) && !setts.LogPath.ToLower().Equals(Setting.LogDirectoryPath.ToLower()) &&
                            File.Exists(Setting.LogFilePath) && Directory.Exists(setts.LogPath))
                        {
                            File.Copy(Setting.LogFilePath, Path.Combine(setts.LogPath, Setting.LOG_FILE_NAME));
                            Setting.LogDirectoryPath = setts.LogPath;
                        }
                        break;
                    default:
                        Setting.DBServer = setts.DBServer;
                        Setting.DBSchema = setts.DBSchema;
                        Setting.DBName = setts.DBName;
                        Setting.DBUsername = setts.DBUsername;
                        Setting.DBPort = setts.DBPort;
                        Setting.DBPassword = setts.DBPass;
                        Setting.ServiceName = setts.ServiceName;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Logger.Exception(ex);
            }

        }

        private void btnDBPath_Click(object sender, EventArgs e)
        {
            var path = getPath();
            if (null != path)
            {
                setts.DBPath = path;
                SaveProps(PropType.DB);
            }
        }

        private string getPath()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.SelectedPath = Environment.CurrentDirectory;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.SelectedPath;
            }
            return null;
        }

        private void btnLogPath_Click(object sender, EventArgs e)
        {
            var path = getPath();
            if (null != path)
            {
                setts.LogPath = path;
                SaveProps(PropType.LOG);
            }
        }

        private void llReset_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            resetPaths();
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            var rb = (RadioButton)sender;
            if (rb.Checked)
            {
                ConnectionChanged(rb.Tag.ToString().ToLower());
            }
        }

        private void ConnectionChanged(string name, bool load = false)
        {
            switch (name)
            {
                case "sqlite":
                    btnDBPath.Enabled = true;
                    PgControlsEnable(false);
                    if (!load) Setting.DBConnection = (int)DBMSType.SQLITE;
                    break;
                case "postgres":
                    btnDBPath.Enabled = false;
                    PgControlsEnable(true);
                    if (!load) Setting.DBConnection = (int)DBMSType.POSTGRESQL;
                    break;
                default:
                    btnDBPath.Enabled = false;
                    PgControlsEnable(false);
                    break;
            }
        }

        private void PgControlsEnable(bool isEnabled)
        {
            foreach (var ctr in gbPgsql.Controls)
            {
                if (typeof(TextBox) != ctr.GetType()) continue;
                ((TextBox)ctr).Enabled = isEnabled;
            }
        }

        private void tbDBServer_TextChanged(object sender, EventArgs e)
        {
            SaveProps(null);
            // Console.WriteLine(((TextBox)sender).Text);
        }

        private void btnPgSave_Click(object sender, EventArgs e)
        {
            SaveProps(null);
            try
            {
                var db = new PostgreSql(new DBSchema
                {
                    DBName = Setting.DBName,
                    Port = int.Parse(Setting.DBPort),
                    Password = Setting.DBPassword,
                    SchemaName = Setting.DBSchema,
                    Username = Setting.DBUsername,
                    Server = Setting.DBServer,
                });
                if (db.TestConnection()) db.RunQuery(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "system-pgsql.sql")));
                MessageBox.Show("Connection & Configuration Successful!", "Test", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\n{ex.StackTrace.Truncate()}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // throw;
            }
        }
    }

    internal class Setts : ReactiveProperties
    {
        public string LogPath { get { return getProperty<string>("LogPath"); } set { setProperty("LogPath", value); } }
        public string DBPath { get { return getProperty<string>("DBPath"); } set { setProperty("DBPath", value); } }
        public string ServiceName { get { return getProperty<string>("ServiceName"); } set { setProperty("ServiceName", value); } }
        public string DBConn { get { return getProperty<string>("DBConn"); } set { setProperty("DBConn", value); } }
        public string DBServer { get { return getProperty<string>("DBServer"); } set { setProperty("DBServer", value); } }
        public string DBPort { get { return getProperty<string>("DBPort"); } set { setProperty("DBPort", value); } }
        public string DBName { get { return getProperty<string>("DBName"); } set { setProperty("DBName", value); } }
        public string DBSchema { get { return getProperty<string>("DBSchema"); } set { setProperty("DBSchema", value); } }
        public string DBUsername
        {
            get { return getProperty<string>("DBUsername"); }
            set
            {
                setProperty("DBUsername", value);
            }
        }
        public string DBPass
        {
            get { return getProperty<string>("DBPass"); }
            set
            {
                setProperty("DBPass", value);
            }
        }
    }

    internal enum PropType
    {
        LOG,
        DB,
    }
}
