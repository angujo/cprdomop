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
            setts.LogPath =Setting.LogDirectoryPath ?? Path.Combine(Environment.CurrentDirectory, "logs");
            setts.DBPath = Setting.DBDirectoryPath ?? Environment.CurrentDirectory;
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
                Setting.LogDirectoryPath = Path.Combine(Environment.CurrentDirectory, "logs");
                Setting.DBDirectoryPath = Environment.CurrentDirectory;
                SetLoad();
            }
        }

        private void SaveProps(string type)
        {
            try
            {
                string dp = string.Empty;
                switch (type)
                {
                    case Setting.DB_FILE_NAME:
                        if (!string.IsNullOrEmpty(setts.DBPath) && !setts.DBPath.ToLower().Equals(Setting.DBDirectoryPath.ToLower()) &&
                            File.Exists(Setting.DBFilePath) && Directory.Exists(setts.DBPath))
                        {
                            File.Copy(Setting.DBFilePath,  Path.Combine(setts.DBPath, Setting.DB_FILE_NAME));
                            Setting.DBDirectoryPath = setts.DBPath;
                        }
                        break;
                    case Setting.LOG_FILE_NAME:
                        if (!string.IsNullOrEmpty(setts.LogPath) && !setts.LogPath.ToLower().Equals(Setting.LogDirectoryPath.ToLower()) &&
                            File.Exists(Setting.LogFilePath) && Directory.Exists(setts.LogPath))
                        {
                            File.Copy(Setting.LogFilePath, Path.Combine(setts.LogPath, Setting.LOG_FILE_NAME));
                            Setting.LogDirectoryPath = setts.LogPath;
                        }
                        break;
                    default: return;
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
                SaveProps(Setting.DB_FILE_NAME);
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
                SaveProps(Setting.LOG_FILE_NAME);
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
