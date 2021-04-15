using System.ServiceProcess;
using Unity;

namespace chz.WindowsServices.Host
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        static void Main()
        {
            /*
             * Устанавливаем текущий каталог как у текущего домена
             */
            System.IO.Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);

            var iocContainer = new IocContainer();

            var services = iocContainer.Resolve<ServiceBase[]>();

            /*
             * Передаем службы на исполнение
             */
            ServiceBase.Run(services);
        }
    }
}
