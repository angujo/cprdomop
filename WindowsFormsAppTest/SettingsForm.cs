using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Util;

namespace WindowsFormsAppTest
{
    public partial class SettingsForm : Form
    {
        private Setts setts = new Setts();
        public SettingsForm()
        {
            InitializeComponent();
            SetLoad();
            Binds();
        }

        protected void SetLoad()
        {
            setts.LogPath = Properties.Settings.Default.log_path ?? Path.Combine(Environment.CurrentDirectory, "logs");
            setts.DBPath = Properties.Settings.Default.db_path ?? Environment.CurrentDirectory;
        }

        protected void Binds()
        {
            tbDBPath.DataBindings.Add("Text", setts, "DBPath");
            tbLogPath.DataBindings.Add("Text", setts, "LogPath");
        }

        private void resetPaths()
        {
            if (DialogResult.OK == MessageBox.Show("Are you sure you wish to reset the settings path?\nYou will lose connection to the current paths!", "Path Reset", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                Properties.Settings.Default.log_path = Path.Combine(Environment.CurrentDirectory, "logs");
                Properties.Settings.Default.db_path = Environment.CurrentDirectory;

                Properties.Settings.Default.Save();
                SetLoad();
            }
        }

        private void SaveProps(string type)
        {
            try
            {
                string sp, dp = string.Empty;
                switch (type)
                {
                    case Items.DB_FILE_NAME:
                        sp = Items.DBFilePath(Properties.Settings.Default.db_path);
                        if (!string.IsNullOrEmpty(setts.DBPath) && !setts.DBPath.ToLower().Equals(Properties.Settings.Default.db_path.ToLower()) &&
                            File.Exists(sp) && Directory.Exists(setts.DBPath))
                        {
                            File.Copy(sp, dp = Path.Combine(setts.DBPath, Items.DB_FILE_NAME));
                            Properties.Settings.Default.db_path = setts.DBPath;
                        }
                        break;
                    case Items.LOG_FILE_NAME:
                        sp = Items.DBFilePath(Properties.Settings.Default.log_path);
                        if (!string.IsNullOrEmpty(setts.LogPath) && !setts.LogPath.ToLower().Equals(Properties.Settings.Default.log_path.ToLower()) &&
                            File.Exists(sp) && Directory.Exists(setts.LogPath))
                        {
                            File.Copy(sp, dp = Path.Combine(setts.LogPath, Items.LOG_FILE_NAME));
                            Properties.Settings.Default.db_path = setts.LogPath;
                            Properties.Settings.Default.log_path = setts.LogPath;
                        }
                        break;
                    default: return;
                }
                Console.WriteLine($"FROM : {sp} TO : {dp}");
                Properties.Settings.Default.Save();
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
                SaveProps(Items.DB_FILE_NAME);
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
                SaveProps(Items.LOG_FILE_NAME);
            }
        }

        private void llReset_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            resetPaths();
        }
    }

    internal class Setts : INotifyPropertyChanged
    {
        string _logpath;
        public string LogPath { get { return _logpath; } set { _logpath = value; OnChange("LogPath"); } }
        string _dbpath;
        public string DBPath { get { return _dbpath; } set { _dbpath = value; OnChange("DBPath"); } }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnChange(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(name));
        }
    }
}
