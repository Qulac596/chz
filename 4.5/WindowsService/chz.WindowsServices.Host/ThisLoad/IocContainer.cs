using chz.Client.WebApiClient;
using chz.Infrastructure;
using chz.WebApiClient;
using chz.WindowsServices.Cryptography;
using chz.WindowsServices.ThisLoadDocument.DataBase;
using chz.WindowsServices.Log;
using chz.WindowsServices.Workes;
using commoni.Infrastructure;
using Unity;

namespace chz.WindowsServices.Host.ThisLoad
{
    class IocContainer : IocContainerBase
    {

        internal IocContainer()
        {
            this.RegisterType<commoni.Infrastructure.IDataBase, DataAccess.ServiceLog.DataBase>();

            this.RegisterFactory<ILogger>((x) => new Logger(
               x.Resolve<Setting.ISettingProvider>().GetValue("ThisLoadServiceLogFolder"),
              "ThisLoadService",
              x.Resolve<Setting.ISettingProvider>(),
              x.Resolve<commoni.Infrastructure.IDataBase>()
               ), FactoryLifetime.Singleton);

            this.RegisterFactory<IRequestLogger>((x) => new RequestLogger(x.Resolve<ILogger>(),
                x.Resolve<Setting.ISettingProvider>().GetValue("ThisLoadServiceLogFolder"),
                "ThisLoadService",
              x.Resolve<Setting.ISettingProvider>(),
              x.Resolve<commoni.Infrastructure.IDataBase>()
                ));


            this.RegisterFactory<WindowsServices.ThisLoadDocument.DataBase.IDataBase>((x) => new DataAccess.ThisLoadDocument.DataBase(
                x.Resolve<Setting.ISettingProvider>()),
                FactoryLifetime.Singleton);

            this.RegisterFactory<WindowsServices.ThisLoadDocument.MDLPClient.IMDLPClient>((x) => new Client.ThisLoadDocument.MDLPClient(
                x.Resolve<IClient>(),
                  x.Resolve<Setting.ISettingProvider>().GetValue("Url")
                ));

            this.RegisterFactory<WindowsServices.ThisLoadDocument.Workers.MDLPClientAdapter>((x) => new WindowsServices.ThisLoadDocument.Workers.MDLPClientAdapter(
                (UnityContainer)x,
                x.Resolve<ILogger>(),
                x.Resolve<ISigner>(),
                x.Resolve<Setting.ISettingProvider>()
                ), FactoryLifetime.Singleton);

            this.RegisterFactory<ICommandWorker<IncomeDocument>>("ErrorManager", (x) => new WindowsServices.ThisLoadDocument.Workers.ErrorManager(
                x.Resolve<ILogger>(),
                x.Resolve<WindowsServices.ThisLoadDocument.Workers.MDLPClientAdapter>(),
                x.Resolve<WindowsServices.ThisLoadDocument.DataBase.IDataBase>(),
                (UnityContainer)x
                ), FactoryLifetime.Singleton);

            this.RegisterFactory<ICommandWorker<IncomeDocument>>("LinkLoader", (x) => new WindowsServices.ThisLoadDocument.Workers.LinkLoader(
               x.Resolve<ILogger>(),
               x.Resolve<WindowsServices.ThisLoadDocument.Workers.MDLPClientAdapter>(),
               x.Resolve<WindowsServices.ThisLoadDocument.DataBase.IDataBase>(),
               x.Resolve<ICommandWorker<IncomeDocument>>("DocLoader"),
               x.Resolve<ICommandWorker<IncomeDocument>>("ErrorManager")
                ), FactoryLifetime.Singleton);

            this.RegisterFactory<IWorker>("Scaner", (x) => new WindowsServices.ThisLoadDocument.Workers.Scanner(
                x.Resolve<ILogger>(),
                x.Resolve<Setting.ISettingProvider>(),
                x.Resolve<WindowsServices.ThisLoadDocument.Workers.MDLPClientAdapter>(),
                x.Resolve<WindowsServices.ThisLoadDocument.DataBase.IDataBase>(),
                x.Resolve<ICommandWorker<IncomeDocument>>("LinkLoader")
                ), FactoryLifetime.Singleton);

            this.RegisterFactory<ICommandWorker<IncomeDocument>>("DocLoader", (x) => new WindowsServices.ThisLoadDocument.Workers.DocLoader(
                 x.Resolve<ILogger>(),
                 x.Resolve<WindowsServices.ThisLoadDocument.Workers.MDLPClientAdapter>(),
                 x.Resolve<WindowsServices.ThisLoadDocument.DataBase.IDataBase>(),
                 x.Resolve<ICommandWorker<IncomeDocument>>("ErrorManager")
                 ), FactoryLifetime.Singleton);

            this.RegisterFactory<ThisLoad.Service>((x) => new ThisLoad.Service(
                x.Resolve<ILogger>(),
                x.Resolve<WindowsServices.ThisLoadDocument.Workers.MDLPClientAdapter>(),
                x.Resolve<IWorker>("Scaner"),
                x.Resolve<ICommandWorker<IncomeDocument>>("LinkLoader"),
                x.Resolve<ICommandWorker<IncomeDocument>>("DocLoader"),
                x.Resolve<ICommandWorker<IncomeDocument>>("ErrorManager")
                ));
        }
    }
}
