using chz.WindowsServices.Host;
using System;
using System.ServiceProcess;
using Unity;

namespace ConsoleServiceTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var iocContainer = new IocContainer();

            var services = iocContainer.Resolve<ServiceBase[]>();

            var unloadService = (chz.WindowsServices.UnloadDocument.Service)services[2];
            var loadService = (chz.WindowsServices.LoadDocument.Service)services[1];
            var requestSevice = (chz.WindowsServices.DirectoryLoader.Service)services[0];

            var thisLoadService = (chz.WindowsServices.Host.ThisLoad.Service)services[3];


            unloadService.StartService();
            loadService.StartService();
            requestSevice.StartService();

            thisLoadService.StartService();
            Console.ReadKey();


            unloadService.StopService();
            loadService.StopService();
            requestSevice.StopService();

            thisLoadService.StopService();
            Console.ReadKey();
        }
    }
}
