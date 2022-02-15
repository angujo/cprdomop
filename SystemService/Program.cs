using System.ServiceProcess;

namespace SystemService
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new OMOPService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
