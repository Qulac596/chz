using chz.WindowsServices.Cryptography;
using chz.WindowsServices.Log;
using chz.WindowsServices.MDLPClient;
using chz.WindowsServices.Setting;
using System;
using System.Threading;
using Unity;

namespace chz.WindowsServices.Workes
{
    /*
     * Адаптер к сервису МДЛП. Предоставляет необходимый api для других workes
     */
    public class MDLPClientAdapterBase<TClient> : ServiceWorker, IWorker where TClient : IMDLPClient
    {
        private const string NewTokenMessage = "MDLPClientAdapter: Новый токен получен.";

        private const AuthType authType = AuthType.SIGNED_CODE;

        private const int SecondCount = 60;
        private const int MillisecondCount = 1000;
        private const double Procent = 0.9;
        private const int ErrorTokenLifeTime = 10;
        private int TokenLifeTime;

        private ISigner signer;
        private ISettingProvider setting;

        protected UnityContainer UnityContainer { get; private set; }

        protected ReaderWriterLockSlim ReaderWriterLockSlim { get; set; }

        protected TClient Client { get; set; }

        protected MDLPClientAdapterBase(UnityContainer unityContainer, ILogger log, ISigner signer, ISettingProvider setting) : base(log)
        {
            UnityContainer = unityContainer;
            this.signer = signer;
            this.setting = setting;
            Client = UnityContainer.Resolve<TClient>();
            ReaderWriterLockSlim = new ReaderWriterLockSlim();
        }


        protected override void OnStart()
        {

            var sessionToken = GetToken();

            if (sessionToken != null)
            {
                /*
                 * Устанавливаем токен доступа на клиенте.
                 */
                Client.AccesToken = sessionToken.Token;

                /*
                 * Устанавливаем время жизни токена
                 */
                TokenLifeTime = (int)(sessionToken.LifeTime * SecondCount * Procent);

                /*
                 * Меняем url клиента c http на https
                 */
                Client.BaseUrl = setting.GetValue("HttpsUrl");

                /*
                 * Пишем сообщение в лог
                 */
                Log.WriteInfo(NewTokenMessage);
            }
            else
            {
                /*
                 * При не удаче устанавливаем короткий тайм-аут
                 */
                TokenLifeTime = ErrorTokenLifeTime;
            }
        }

        protected override void OnStop()
        {
            Logout();
            Client.Dispose();
        }

        /*
         * Перезапускает адаптер
         */
        public void Reboot()
        {
            ReaderWriterLockSlim.EnterWriteLock();
            Stop();
            Client = UnityContainer.Resolve<TClient>();
            Start();
            ReaderWriterLockSlim.ExitWriteLock();
        }


        protected override void Execute()
        {
            if (SleepTokenLifeTime() == false)
            {
                return;
            }

            ReaderWriterLockSlim.EnterWriteLock();

            Client.BaseUrl = setting.GetValue("Url");

            var sessionToken = GetToken();

            if (sessionToken != null)
            {
                Client.AccesToken = sessionToken.Token;

                TokenLifeTime = (int)(sessionToken.LifeTime * SecondCount * Procent);

                Client.BaseUrl = setting.GetValue("HttpsUrl");

                Log.WriteInfo(NewTokenMessage);
            }
            else
            {
                TokenLifeTime = ErrorTokenLifeTime;
            }

            ReaderWriterLockSlim.ExitWriteLock();
        }

        private ISessionToken GetToken()
        {
            /*
            * Получаем код
            */
            var code = GetCode();

            if (code == null)
            {
                return null;
            }

            /*
             * Подписываем код
             */
            var sign = GetSign(code);

            if (sign == null)
            {
                return null;
            }

            /*
             * Получаем токен доступа в время его жизни
             */
            return GetSessionToken(code, sign);
        }

        /*
        * Получает код
        */
        private string GetCode()
        {
            try
            {
                var auntificationUserInfo = new AuntificationUserInfo(setting.GetValue("ClientId"), setting.GetValue("ClientSecret"),
                    setting.GetValue("UserId"), authType);

                return Client.GetCode(auntificationUserInfo).Result.Code;
            }
            catch (AggregateException e)
            {
                Log.WriteError(e.InnerException);
                return null;
            }
        }

        /*
         * Подпиывает код
         */
        private string GetSign(string code)
        {
            try
            {
                return signer.Sign(code);
            }
            catch (CryptographyException e)
            {
                Log.WriteError(e);
                return null;
            }
        }

        /*
         * Получает токен
         */
        private ISessionToken GetSessionToken(string code, string signature)
        {
            try
            {
                var authenticatedUserInfo = new AuthenticatedUserInfo(code, signature, null);
                return Client.GetSessionToken(authenticatedUserInfo).Result;
            }
            catch (AggregateException ex)
            {
                Log.WriteError(ex.InnerException);
                return null;
            }
        }

        /*
        * Выполняем выход из сервиса МДЛП
         */
        private void Logout()
        {
            try
            {
                Client.Logout().Wait();
            }
            catch (AggregateException e)
            {
                Log.WriteError(e.InnerException);
            }
        }

        private bool SleepTokenLifeTime()
        {
            for (; TokenLifeTime > 0; TokenLifeTime--)
            {
                Thread.Sleep(MillisecondCount);
                lock (IsRunLocker)
                {
                    if (IsRun == false)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
