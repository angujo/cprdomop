using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using SystemLocalStore;
using SystemLocalStore.models;
using WindowsFormsAppTest.extensions;
using WindowsFormsAppTest.models;
using WindowsFormsAppTest.utils;

namespace WindowsFormsAppTest
{
    public partial class ConfigControl : UserControl
    {
        ConfigVar settings;
        SourceFiles sourceFiles;
        Dictionary<string, FolderBrowserDialog> browserDlg = new Dictionary<string, FolderBrowserDialog>();
        Dictionary<string, OpenFileDialog> fileDlg = new Dictionary<string, OpenFileDialog>();
        WorkLoad workLoad;
        List<Control> inputControls;
        readonly string inputRegex = @"(btn|tb)(\w+)(Browse|Dir|File)";

        public ConfigControl(WorkLoad wl)
        {
            workLoad = wl;
            InitializeComponent();
            settings = new ConfigVar();
            sourceFiles = new SourceFiles();

            sourceFiles.changeFunc = () =>
            {
                btnSaveFiles.Enabled = false;
                btnLock.Enabled = false;
            };
            inputControls = Util.containerControls(pnLoaders).Where(cnt => Regex.Match(cnt.Name, inputRegex, RegexOptions.IgnoreCase).Success).ToList();
            loaderEvents();
            if (workLoad.FilesLocked)
            {
                workLocked();
                sourceFiles.populatePaths(SourceFile.List<SourceFile>(new { WorkLoadId = workLoad.Id }));//  DataAccess.loadSourceFiles(workLoad));
            }
        }

        private void loaderEvents()
        {
            foreach (var loader in inputControls)
            {
                if (!Regex.Match(loader.Name, inputRegex, RegexOptions.IgnoreCase).Success) continue;
                var isFileBased = Regex.Match(loader.Name, @"File$", RegexOptions.IgnoreCase).Success;
                var rootName = Regex.Replace(loader.Name, inputRegex, "$2").FirstCharToUpper();
                var propertyName = rootName + (isFileBased ? "File" : "Dir");

                PropertyInfo propInfo = sourceFiles.GetType().GetProperty(propertyName);
                if (null == propInfo) continue;
                if (loader is Button)
                {
                    var _loader = (Button)loader;
                    if (isFileBased)
                    {
                        var lbl = Util.containerControls(pnLoaders).Find(cntr => cntr.Name.Equals($"lb{rootName}Path"));
                        if (null != lbl) ((Label)lbl).DataBindings.Add("Text", sourceFiles, propertyName);
                    }
                    (_loader).Click += new EventHandler((oSender, eArgs) =>
                    {
                        if (isFileBased)
                        {
                            OpenFileDialog dlg;
                            if (!fileDlg.TryGetValue(_loader.Name, out dlg))
                            {
                                using (dlg = new OpenFileDialog()) ;
                                var p = (string)propInfo.GetValue(sourceFiles);
                                dlg.InitialDirectory = !String.IsNullOrEmpty(p) ? Directory.GetParent(p).FullName : Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                                dlg.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                                dlg.FilterIndex = 2;
                                dlg.RestoreDirectory = true;
                                dlg.Title = $"File for {rootName}";
                                fileDlg.Add(_loader.Name, dlg);
                            }

                            if (dlg.ShowDialog() == DialogResult.OK)
                            {
                                propInfo.SetValue(sourceFiles, dlg.FileName);
                            }
                        }
                        else
                        {
                            FolderBrowserDialog dialog;
                            if (!browserDlg.TryGetValue(_loader.Name, out dialog))
                            {
                                dialog = new FolderBrowserDialog();
                                dialog.SelectedPath = (string)(propInfo.GetValue(sourceFiles) ?? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                                browserDlg.Add(_loader.Name, dialog);
                            }
                            if (dialog.ShowDialog() == DialogResult.OK)
                            {
                                propInfo.SetValue(sourceFiles, dialog.SelectedPath);
                            }
                        }

                    });
                }
                if (loader is TextBox)
                {
                    ((TextBox)loader).DataBindings.Add("Text", sourceFiles, propertyName);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.SelectedPath = settings.ConfRootDir ?? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                settings.ConfRootDir = dialog.SelectedPath;
            }
        }

        private void ConfigControl_Resize(object sender, EventArgs e)
        {
            // pnFileConfigs.MaximumSize.Height = this.Height;
        }

        public class ConfigVar : INotifyPropertyChanged
        {
            string conf_root_dir;
            public string ConfRootDir
            {
                get { return conf_root_dir; }
                set { conf_root_dir = value; InvokePropertyChanged(new PropertyChangedEventArgs("ConfRootDir")); }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            public void InvokePropertyChanged(PropertyChangedEventArgs args)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null) handler(this, args);
            }
        }

        private void btnSurceFileAnalysis(object sender, EventArgs e)
        {
            tbLogger.Text = Util.LogLine(null);
            var err = false;
            foreach (Exception ex in sourceFiles.validationErrors())
            {
                if (!err)
                {
                    err = true;
                    tbLogger.ForeColor = err ? Color.Red : Color.Black;
                }
                tbLogger.AppendText(Util.LogLine(ex.Message));
            }
            if (err) return;
            sourceFiles.isValidated = true;
            tbLogger.ForeColor = Color.Green;
            foreach (var dtl in sourceFiles.analysisDetails())
            {
                tbLogger.AppendText(Util.LogLine($"{dtl.Key}\t{dtl.Value}"));
            }
            btnSaveFiles.Enabled = true;
        }

        private async void btnSaveRef_Click(object sender, EventArgs e)
        {
            if (!sourceFiles.isValidated)
            {
                MessageBox.Show(null, "Entry Files and Directories are not validated!\nValidate the entries before saving!", "Source File Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            pbSaving.Visible = true;
            btnAnalyse.Enabled = false;
            btnSaveFiles.Enabled = false;
            try
            {
                await Task.Run(() =>
                {
                    DataAccess.CleanSourceFiles(workLoad);
                    foreach (var sf in sourceFiles.processedFiles(workLoad.Id))
                    {
                        sf.InsertOrUpdate();
                    }
                });
                MessageBox.Show(null, "It is recommended to lock the files before processing can be initiated.\nEnsure all file paths are as expected!", "Source File Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLock.Enabled = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                pbSaving.Visible = false;
                btnAnalyse.Enabled = true;
                btnSaveFiles.Enabled = true;
            }
        }

        private async void btnLock_Click(object sender, EventArgs e)
        {
            if (DialogResult.Cancel == MessageBox.Show(null, "You are about to lock source file references from further modification.\nIt is NOT RECOMMENDED to modify files on file system once lock has been done.\nWish to proceed?", "Source File Lock", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)) return;
            pbSaving.Visible = true;
            try
            {
                await Task.Run(() =>
                {
                    DataAccess.LockWorkLoad(workLoad);
                });
                workLoad.FilesLocked = true;
                workLocked();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void workLocked()
        {
            foreach (var cnt in inputControls) cnt.Enabled = false;
            pbSaving.Visible = false;
            btnAnalyse.Enabled = false;
            btnSaveFiles.Enabled = false;
            btnLock.Enabled = false;
        }
    }
}