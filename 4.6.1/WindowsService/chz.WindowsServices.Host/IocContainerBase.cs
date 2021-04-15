using chz.Client.WebApiClient;
using chz.Cryptography;
using chz.WebApiClient;
using chz.WindowsServices.Cryptography;
using commoni.Setting;
using Unity;

namespace chz.WindowsServices.Host
{
    abstract class IocContainerBase : UnityContainer
    {
        protected IocContainerBase()
        {
            this.RegisterType<Setting.ISettingProvider, SettingProvider>(TypeLifetime.Singleton);

            this.RegisterFactory<ISigner>((x) => new Signer(x.Resolve<Setting.ISettingProvider>()), FactoryLifetime.Singleton);

            this.RegisterFactory<IClient>((x) => new chz.WebApiClient.Client(x.Resolve<IRequestLogger>(),
                x.Resolve<Setting.ISettingProvider>()));
        }
    }
}
