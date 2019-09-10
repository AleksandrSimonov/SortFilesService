using Service.Logic;
using System;
using System.Configuration;
using System.IO;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
namespace Service
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            using (var writer = new StreamWriter(@"C:\Users\Александр\source\repos\Service\Service\bin\Debug\ServiceWork.txt", true, Encoding.UTF8))
            {
                writer.WriteLine(DateTime.Now + " служба " + this.ServiceName + " запущена");
            }
            Logger.Log.Info(" служба " + this.ServiceName +" запускается");
            var folder = ConfigurationManager.AppSettings["Folder"];
            string timer = ConfigurationManager.AppSettings["timer"];
            var sortFiles = new SortByExtension(folder, int.Parse(timer));
            sortFiles.Start();
            Logger.Log.Info(" служба " + this.ServiceName + " запусщена");
        }

        protected override void OnStop()
        {
            using (var writer = new StreamWriter(@"C:\Users\Александр\source\repos\Service\Service\bin\Debug\ServiceWork.txt", true, Encoding.UTF8))
            {
                writer.WriteLine(DateTime.Now + " служба " + this.ServiceName + " остановлена");
            }
        }
    }
}
