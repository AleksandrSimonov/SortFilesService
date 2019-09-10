using System.ServiceProcess;
using System;
using Service.Logic;
using System.Threading;
using System.Threading.Tasks;
namespace Service
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        private  static void Main()
        {
            Logger.InitLogger();

            if (Environment.UserInteractive)
            {
                var sortByExtension = new SortByExtension(@"C:\Users\Александр\Documents\Стажировка Navicon\Exercise4", 10000);
                sortByExtension.Start();
                Logger.Log.Info(" служба "  + " запусщена");
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new Service1()
                };
                ServiceBase.Run(ServicesToRun);
            }
            while (true);
        }
    }
}
