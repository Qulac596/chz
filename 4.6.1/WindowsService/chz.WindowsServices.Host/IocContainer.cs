using System.ServiceProcess;
using Unity;

namespace chz.WindowsServices.Host
{
    public class IocContainer : UnityContainer
    {
        public IocContainer()
        {

            this.RegisterType<Directory.IocContainer>();
            this.RegisterType<Load.IocContainer>();
            this.RegisterType<Unload.IocContainer>();

            this.RegisterFactory<ServiceBase[]>((x) => new ServiceBase[] {
                x.Resolve<Directory.IocContainer>().Resolve<DirectoryLoader.Service>(),
                x.Resolve<Load.IocContainer>().Resolve<LoadDocument.Service>(),
                x.Resolve<Unload.IocContainer>().Resolve<UnloadDocument.Service>()
            });
        }
    }
}
