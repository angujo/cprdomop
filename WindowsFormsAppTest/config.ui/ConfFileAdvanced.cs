﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsAppTest.extensions;
using WindowsFormsAppTest.models;

namespace WindowsFormsAppTest.config.ui
{
    public partial class ConfFileAdvanced : UserControl
    {
        private SourceFiles sourceFiles;

        public ConfFileAdvanced() { InitializeComponent(); }

        public ConfFileAdvanced(SourceFiles source)
        {
            InitializeComponent();
            sourceFiles = source;
            loadControlEvents();
        }

        private void loadControlEvents()
        {
            var regexStr = @"(btn|tb)(\w+)(Browse|Dir)";
            utils.Util.containerControls(pnHolder, cntr =>
            {
                if (!Regex.Match(cntr.Name, regexStr, RegexOptions.IgnoreCase).Success) return;
                var keyName = Regex.Replace(cntr.Name, regexStr, "$2").ToLower().FirstCharToUpper() + "Dir";
                PropertyInfo propInfo = sourceFiles.GetType().GetProperty(keyName);
                if (null == propInfo) return;
                if (cntr is Button)
                {
                    var _loader = (Button)cntr;
                    (_loader).Click += new EventHandler((oSender, eArgs) =>
                    {
                        FolderBrowserDialog dialog = new FolderBrowserDialog();
                        dialog.SelectedPath = (string)(propInfo.GetValue(sourceFiles) ?? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            propInfo.SetValue(sourceFiles, dialog.SelectedPath);
                        }
                    });
                }
                if (cntr is TextBox)
                {
                    ((TextBox)cntr).DataBindings.Add("Text", sourceFiles, keyName);
                }
            });
        }
    }
}
