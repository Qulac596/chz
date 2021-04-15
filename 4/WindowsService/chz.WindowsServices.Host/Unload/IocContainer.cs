using chz.Client.WebApiClient;
using chz.Infrastructure;
using chz.WebApiClient;
using chz.WindowsServices.Cryptography;
using chz.WindowsServices.Log;
using chz.WindowsServices.UnloadDocument.Workers;
using chz.WindowsServices.Workes;
using commoni.Infrastructure;
using System.Collections.Generic;
using Unity;

namespace chz.WindowsServices.Host.Unload
{
    class IocContainer : IocContainerBase
    {

        internal IocContainer()
        {
            this.RegisterType<IDataBase, DataAccess.ServiceLog.DataBase>();

            this.RegisterFactory<ILogger>((x) => new Logger(
               x.Resolve<Setting.ISettingProvider>().GetValue("UloadServiceLogFolder"),
              "UloadService",
              x.Resolve<Setting.ISettingProvider>(),
              x.Resolve<IDataBase>()
               ), FactoryLifetime.Singleton);

            this.RegisterFactory<IRequestLogger>((x) => new RequestLogger(x.Resolve<ILogger>(),
                x.Resolve<Setting.ISettingProvider>().GetValue("UloadServiceLogFolder"),
                 "UloadService",
              x.Resolve<Setting.ISettingProvider>(),
              x.Resolve<IDataBase>()
                ));


            this.RegisterFactory<UnloadDocument.MDLPClient.IMDLPClient>((x) => new Client.UnloadDocument.MDLPClient(
                 x.Resolve<IClient>(),
                  x.Resolve<Setting.ISettingProvider>().GetValue("Url")
                ));

            this.RegisterFactory<UnloadDocument.DataBase.IDataBase>((x) => new DataAccess.UnloadDocument.DataBase(
                x.Resolve<Setting.ISettingProvider>()),
                FactoryLifetime.Singleton);

            this.RegisterFactory<MDLPClientAdapter>((x) => new MDLPClientAdapter(
                (UnityContainer)x,
                x.Resolve<ILogger>(),
                x.Resolve<ISigner>(),
                x.Resolve<Setting.ISettingProvider>()
                ), FactoryLifetime.Singleton);

            this.RegisterFactory<ICommandWorker<IEnumerable<UnloadDocument.DataBase.OutcomeDocument>>>("Canceler", (x) => new Canceler(
                x.Resolve<UnloadDocument.DataBase.IDataBase>(),
                x.Resolve<ILogger>(),
                x.Resolve<MDLPClientAdapter>(),
                x.Resolve<ICommandWorker<UnloadDocument.DataBase.OutcomeDocument>>("ErrorManager")
                ), FactoryLifetime.Singleton);

            this.RegisterFactory<ICommandWorker<UnloadDocument.DataBase.OutcomeDocument>>("LinkLoader", (x) => new LinkLoader(
                x.Resolve<UnloadDocument.DataBase.IDataBase>(),
                x.Resolve<ILogger>(),
                x.Resolve<MDLPClientAdapter>(),
                x.Resolve<ICommandWorker<UnloadDocument.DataBase.OutcomeDocument>>("ErrorManager"),
                x.Resolve<ICommandWorker<UnloadDocument.DataBase.OutcomeDocument>>("TicketLoader")
                ), FactoryLifetime.Singleton);

            this.RegisterFactory<ICommandWorker<UnloadDocument.DataBase.OutcomeDocument>>("StatusInspector", (x) => new StatusInspector(
                 x.Resolve<UnloadDocument.DataBase.IDataBase>(),
                x.Resolve<ILogger>(),
                x.Resolve<MDLPClientAdapter>(),
                x.Resolve<ICommandWorker<UnloadDocument.DataBase.OutcomeDocument>>("ErrorManager"),
                x.Resolve<ICommandWorker<UnloadDocument.DataBase.OutcomeDocument>>("LinkLoader")
                ), FactoryLifetime.Singleton);

            this.RegisterFactory<ICommandWorker<UnloadDocument.DataBase.OutcomeDocument>>("TicketLoader", (x) => new TicketLoader(
                 x.Resolve<UnloadDocument.DataBase.IDataBase>(),
                x.Resolve<ILogger>(),
                x.Resolve<MDLPClientAdapter>(),
                x.Resolve<ICommandWorker<UnloadDocument.DataBase.OutcomeDocument>>("ErrorManager")
                ), FactoryLifetime.Singleton);

            this.RegisterFactory<ICommandWorker<IEnumerable<UnloadDocument.DataBase.OutcomeDocument>>>("Unloader", (x) => new Unloader(
                x.Resolve<UnloadDocument.DataBase.IDataBase>(),
                x.Resolve<ILogger>(),
                x.Resolve<MDLPClientAdapter>(),
                x.Resolve<ICommandWorker<UnloadDocument.DataBase.OutcomeDocument>>("ErrorManager"),
                x.Resolve<ICommandWorker<UnloadDocument.DataBase.OutcomeDocument>>("StatusInspector"),
                x.Resolve<ISigner>()
                ), FactoryLifetime.Singleton);

            this.RegisterFactory<ICommandWorker<UnloadDocument.DataBase.OutcomeDocument>>("ErrorManager", (x) => new ErrorManager(
                 x.Resolve<ILogger>(),
                 x.Resolve<MDLPClientAdapter>(),
                 x.Resolve<UnloadDocument.DataBase.IDataBase>(),
                 (UnityContainer)x
                 ), FactoryLifetime.Singleton);

            this.RegisterFactory<IWorker>("Signaller", (x) => new Signaller(
                 x.Resolve<Setting.ISettingProvider>(),
                 x.Resolve<UnloadDocument.DataBase.IDataBase>(),
                 x.Resolve<ILogger>(),
                 x.Resolve<ICommandWorker<IEnumerable<UnloadDocument.DataBase.OutcomeDocument>>>("Canceler"),
                 x.Resolve<ICommandWorker<IEnumerable<UnloadDocument.DataBase.OutcomeDocument>>>("Unloader")
                ), FactoryLifetime.Singleton);

            this.RegisterFactory<UnloadDocument.Service>((x) => new UnloadDocument.Service(
                 x.Resolve<ILogger>(),
                 x.Resolve<MDLPClientAdapter>(),
                 x.Resolve<IWorker>("Signaller"),
                 x.Resolve<ICommandWorker<IEnumerable<UnloadDocument.DataBase.OutcomeDocument>>>("Canceler"),
                 x.Resolve<ICommandWorker<IEnumerable<UnloadDocument.DataBase.OutcomeDocument>>>("Unloader"),
                 x.Resolve<ICommandWorker<UnloadDocument.DataBase.OutcomeDocument>>("StatusInspector"),
                 x.Resolve<ICommandWorker<UnloadDocument.DataBase.OutcomeDocument>>("LinkLoader"),
                 x.Resolve<ICommandWorker<UnloadDocument.DataBase.OutcomeDocument>>("TicketLoader"),
                 x.Resolve<ICommandWorker<UnloadDocument.DataBase.OutcomeDocument>>("ErrorManager")
                ));
        }
    }
}
