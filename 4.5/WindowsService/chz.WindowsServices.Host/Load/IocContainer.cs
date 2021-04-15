using chz.Client.WebApiClient;
using chz.Infrastructure;
using chz.WebApiClient;
using chz.WindowsServices.Cryptography;
using chz.WindowsServices.LoadDocument.DataBase;
using chz.WindowsServices.Log;
using chz.WindowsServices.Workes;
using commoni.Infrastructure;
using Unity;

namespace chz.WindowsServices.Host.Load
{
    class IocContainer : IocContainerBase
    {

        internal IocContainer()
        {
            this.RegisterType<commoni.Infrastructure.IDataBase, DataAccess.ServiceLog.DataBase>();

            this.RegisterFactory<ILogger>((x) => new Logger(
               x.Resolve<Setting.ISettingProvider>().GetValue("LoadServiceLogFolder"),
              "LoadService",
              x.Resolve<Setting.ISettingProvider>(),
              x.Resolve<commoni.Infrastructure.IDataBase>()
               ), FactoryLifetime.Singleton);

            this.RegisterFactory<IRequestLogger>((x) => new RequestLogger(x.Resolve<ILogger>(),
                x.Resolve<Setting.ISettingProvider>().GetValue("LoadServiceLogFolder"),
                "LoadService",
              x.Resolve<Setting.ISettingProvider>(),
              x.Resolve<commoni.Infrastructure.IDataBase>()
                ));

            this.RegisterFactory<LoadDocument.DataBase.IDataBase>((x) => new DataAccess.LoadDocument.DataBase(
                x.Resolve<Setting.ISettingProvider>()),
                FactoryLifetime.Singleton);

            this.RegisterFactory<LoadDocument.MDLPClient.IMDLPClient>((x) => new Client.LoadDocument.MDLPClient(
                x.Resolve<IClient>(),
                  x.Resolve<Setting.ISettingProvider>().GetValue("Url")
                ));

            this.RegisterFactory<LoadDocument.Workers.MDLPClientAdapter>((x) => new LoadDocument.Workers.MDLPClientAdapter(
                (UnityContainer)x,
                x.Resolve<ILogger>(),
                x.Resolve<ISigner>(),
                x.Resolve<Setting.ISettingProvider>()
                ), FactoryLifetime.Singleton);

            this.RegisterFactory<ICommandWorker<IncomeDocument>>("ErrorManager", (x) => new LoadDocument.Workers.ErrorManager(
                x.Resolve<ILogger>(),
                x.Resolve<LoadDocument.Workers.MDLPClientAdapter>(),
                x.Resolve<LoadDocument.DataBase.IDataBase>(),
                (UnityContainer)x
                ), FactoryLifetime.Singleton);

            this.RegisterFactory<ICommandWorker<IncomeDocument>>("LinkLoader", (x) => new LoadDocument.Workers.LinkLoader(
               x.Resolve<ILogger>(),
               x.Resolve<LoadDocument.Workers.MDLPClientAdapter>(),
               x.Resolve<LoadDocument.DataBase.IDataBase>(),
               x.Resolve<ICommandWorker<IncomeDocument>>("DocLoader"),
               x.Resolve<ICommandWorker<IncomeDocument>>("ErrorManager")
                ), FactoryLifetime.Singleton);

            this.RegisterFactory<IWorker>("Scaner", (x) => new LoadDocument.Workers.Scanner(
                x.Resolve<ILogger>(),
                x.Resolve<Setting.ISettingProvider>(),
                x.Resolve<LoadDocument.Workers.MDLPClientAdapter>(),
                x.Resolve<LoadDocument.DataBase.IDataBase>(),
                x.Resolve<ICommandWorker<IncomeDocument>>("LinkLoader")
                ), FactoryLifetime.Singleton);

            this.RegisterFactory<ICommandWorker<IncomeDocument>>("DocLoader", (x) => new LoadDocument.Workers.DocLoader(
                 x.Resolve<ILogger>(),
                 x.Resolve<LoadDocument.Workers.MDLPClientAdapter>(),
                 x.Resolve<LoadDocument.DataBase.IDataBase>(),
                 x.Resolve<ICommandWorker<IncomeDocument>>("ErrorManager")
                 ), FactoryLifetime.Singleton);

            this.RegisterFactory<LoadDocument.Service>((x) => new LoadDocument.Service(
                x.Resolve<ILogger>(),
                x.Resolve<LoadDocument.Workers.MDLPClientAdapter>(),
                x.Resolve<IWorker>("Scaner"),
                x.Resolve<ICommandWorker<IncomeDocument>>("LinkLoader"),
                x.Resolve<ICommandWorker<IncomeDocument>>("DocLoader"),
                x.Resolve<ICommandWorker<IncomeDocument>>("ErrorManager")
                ));
        }
    }
}
