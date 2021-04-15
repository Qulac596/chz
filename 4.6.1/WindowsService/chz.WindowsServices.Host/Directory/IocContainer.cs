using chz.Client.WebApiClient;
using chz.Infrastructure;
using chz.WebApiClient;
using chz.WindowsServices.Cryptography;
using chz.WindowsServices.Log;
using chz.WindowsServices.Workes;
using commoni.Infrastructure;
using System.Collections.Generic;
using Unity;

namespace chz.WindowsServices.Host.Directory
{
    class IocContainer : IocContainerBase
    {
        internal IocContainer()
        {

            this.RegisterType<commoni.Infrastructure.IDataBase, DataAccess.ServiceLog.DataBase>();

            this.RegisterFactory<ILogger>((x) => new Logger(
               x.Resolve<Setting.ISettingProvider>().GetValue("DirectoryLoaderServiceLogFolder"),
              "DirectoryLoaderService",
              x.Resolve<Setting.ISettingProvider>(),
              x.Resolve<commoni.Infrastructure.IDataBase>()
               ), FactoryLifetime.Singleton);

            this.RegisterFactory<IRequestLogger>((x) => new RequestLogger(x.Resolve<ILogger>(),
                x.Resolve<Setting.ISettingProvider>().GetValue("DirectoryLoaderServiceLogFolder"),
                 "DirectoryLoaderService",
              x.Resolve<Setting.ISettingProvider>(),
              x.Resolve<commoni.Infrastructure.IDataBase>()
                ));

            this.RegisterFactory<DirectoryLoader.DataBase.IDataBase>((x) => new DataAccess.DirectoryLoader.DataBase(
                x.Resolve<Setting.ISettingProvider>()
                ), FactoryLifetime.Singleton);

            this.RegisterFactory<DirectoryLoader.MDLPClient.IMDLPClient>((x) => new Client.DirectoryLoader.MDLPClient(
                  x.Resolve<IClient>(),
                  x.Resolve<Setting.ISettingProvider>().GetValue("Url")
                ));

            this.RegisterFactory<DirectoryLoader.Workes.MDLPClientAdapter>((x) => new DirectoryLoader.Workes.MDLPClientAdapter(
                (UnityContainer)x,
                x.Resolve<ILogger>(),
                x.Resolve<ISigner>(),
                x.Resolve<Setting.ISettingProvider>()
                ), FactoryLifetime.Singleton);

            this.RegisterFactory<ICommandWorker<DirectoryLoader.DataBase.Request>>("ErrorManager", (x) => new DirectoryLoader.Workes.ErrorManager(
                x.Resolve<ILogger>(),
                 x.Resolve<DirectoryLoader.Workes.MDLPClientAdapter>(),
                 x.Resolve<DirectoryLoader.DataBase.IDataBase>(),
                 (UnityContainer)x), FactoryLifetime.Singleton);

            this.RegisterFactory<ICommandWorker<IEnumerable<DirectoryLoader.DataBase.Request>>>("BranchLoader", (x) => new DirectoryLoader.Workes.BranchLoader(
                 x.Resolve<ILogger>(),
                 x.Resolve<DirectoryLoader.DataBase.IDataBase>(),
                 x.Resolve<DirectoryLoader.Workes.MDLPClientAdapter>(),
                 x.Resolve<ICommandWorker<DirectoryLoader.DataBase.Request>>("ErrorManager")), FactoryLifetime.Singleton);

            this.RegisterFactory<IWorker>("Signaller", (x) => new DirectoryLoader.Workes.Signaller(
                 x.Resolve<DirectoryLoader.DataBase.IDataBase>(),
                 x.Resolve<Setting.ISettingProvider>(),
                 x.Resolve<ILogger>(),
                 x.Resolve<ICommandWorker<IEnumerable<DirectoryLoader.DataBase.Request>>>("BranchLoader")
                 ), FactoryLifetime.Singleton);

            this.RegisterFactory<DirectoryLoader.Service>((x) => new DirectoryLoader.Service(
                x.Resolve<ILogger>(),
                x.Resolve<DirectoryLoader.Workes.MDLPClientAdapter>(),
                x.Resolve<IWorker>("Signaller"),
                x.Resolve<ICommandWorker<IEnumerable<DirectoryLoader.DataBase.Request>>>("BranchLoader"),
                x.Resolve<ICommandWorker<DirectoryLoader.DataBase.Request>>("ErrorManager")
                ));
        }
    }
}
