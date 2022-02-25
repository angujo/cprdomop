using System.ServiceProcess;
using Util;

namespace SystemService
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args = null)
        {
            Logger.Info("Service Reached!");
            args = args ?? new string[] { };
            if (args.Length > 0) { Controller(args); }
            else
            {
                Logger.Info("Service Worker Reached!");
                Setting.DBDirectoryPath = args[0];
                Setting.LogDirectoryPath = args[1];

                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new OMOPService()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }

        public static void Controller(string[] args = null)
        {
            Logger.Info($"Service Controller Reached! ARGS:{string.Join(", ", args)}");
            if (args != null && args.Length == 1 && args[0].Length > 1
                && (args[0][0] == '-' || args[0][0] == '/'))
            {
                switch (args[0].Substring(1).ToLower())
                {
                    default:
                        break;
                    case "install":
                    case "i":
                        SelfInstaller.InstallMe();
                        break;
                    case "uninstall":
                    case "u":
                        SelfInstaller.UninstallMe();
                        break;
                    case "console":
                    case "c":
                        new OMOPService().Process();
                        break;
                }
            }
        }
    }
}
