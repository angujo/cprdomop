using System;
using System.Configuration.Install;
using System.Reflection;
using Util;

namespace SystemService
{
    public class SelfInstaller
    {
        private static readonly string _exePath = Assembly.GetExecutingAssembly().Location;
        public static bool InstallMe()
        {
            try
            {
               ProjectInstaller projectInstaller = new ProjectInstaller();
                 //string[] cmdline = { string.Format("/assemblypath={0} \"arg1\" \"arg2\"", _exePath) };
                 string[] cmdline = { string.Format("/assemblypath={0}", _exePath) };
                 projectInstaller.Context = new InstallContext(null, cmdline);

                 projectInstaller.Install(new System.Collections.Specialized.ListDictionary());

                Logger.Info($"Ready to install!::{_exePath}");
                /*  ManagedInstallerClass.InstallHelper(new string[] { _exePath });*/
                Console.WriteLine("Am an installer");
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }
            return true;
        }

        public static bool UninstallMe()
        {
            try
            {
                /*ProjectInstaller projectInstaller = new ProjectInstaller();
                //string[] cmdline = { string.Format("/assemblypath={0} \"arg1\" \"arg2\"", _exePath) };
                string[] cmdline = { string.Format("/assemblypath={0}", _exePath) };
                projectInstaller.Context = new InstallContext(null, cmdline);

                projectInstaller.Uninstall(new System.Collections.Specialized.ListDictionary());*/

                Logger.Info($"Ready to uninstall!::{_exePath}");
                ManagedInstallerClass.InstallHelper(new string[] { "/u", _exePath });
                Console.WriteLine("Am an uninstaller");
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }
            return true;
        }
    }
}
