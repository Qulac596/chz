using chz.Client.WebApiClient;
using chz.WindowsServices.MDLPClient;
using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
namespace chz.Client
{
    /// <summary>
    /// Представляет сирвис ИС МДЛП
    /// Документация: https://xn--80ajghhoc2aj1c8b.xn--p1ai/upload/iblock/200/IS-_Markirovka_.-MDLP.-Protokol-obmena-interfeysnogo-urovnya.pdf
    /// </summary>
    public abstract class MDLPClient : IMDLPClient
    {
        protected string MDLPErrorMessage => "Ошибка сервиса МДЛП.";

        protected IClient Client { get; private set; }

        /// <summary>
        /// Создает клиента
        /// </summary>
        /// <param name="httpClient">HttpClient</param>
        protected MDLPClient(IClient client, string url)
        {
            BaseUrl = url;
            Client = client;
        }

        /// <summary>
        /// Базовый Url
        /// </summary>
        public string BaseUrl { set; protected get; }

        /// <summary>
        /// Токен сессии
        /// </summary>
        public string AccesToken { set; protected get; }

        /// <summary>
        /// Получает код аутентификации
        /// Описание api в пункте 6.2.1 документации
        /// </summary>
        /// <param name="cliendId">
        /// Идентификатор клиента
        /// </param>
        /// <param name="clientSecret">
        /// Секретный ключ
        /// </param>
        /// <param name="userId">
        /// Уникальный идентификатор пользователя
        /// </param>
        /// <param name="authType">
        /// Тип аутентификации
        /// </param>
        /// <returns>
        /// Код для аутентификации
        /// </returns>
        public async Task<IAuntificationCode> GetCode(AuntificationUserInfo auntificationUserInfo)
        {
            const string url = "auth";
            return await PostResult<AuntificationCode>(url, auntificationUserInfo);
        }

        /// <summary>
        /// Получает ключ сессии
        /// Описание api в пункте 6.2.2 документации
        /// </summary>
        /// <param name="code">
        /// Код для аутентификации
        /// </param>
        /// <param name="signature">
        /// Открепленная подпись кода для аутентификации
        /// </param>
        /// <param name="password">
        /// Пароль пользователя
        /// </param>
        /// <returns>
        /// Токен сессии
        /// </returns>
        public async Task<ISessionToken> GetSessionToken(AuthenticatedUserInfo authenticatedUserInfo)
        {
            const string url = "token";
            return await PostResult<SessionToken>(url, authenticatedUserInfo);
        }

        /// <summary>
        /// Метод выхода из системы.
        /// Описание api в пункте 6.2.3 документации
        /// </summary>
        /// <returns></returns>
        public async Task Logout()
        {
            const string url = "auth/logout";
            await Get(url);
        }

        /// <summary>
        /// Загружает документ в виде строки
        /// </summary>
        /// <param name="url"></param>
        /// <returns>
        /// Квитанция
        /// </returns>
        public async Task<string> LoadStringContent(string url, string targetId = null)
        {
            var request = new Request(HttpMethod.Get, new Uri(url), targetId:targetId, accessToken: AccesToken);

            await Client.Send(request);

            if (request.Response.HttpStatusCode == HttpStatusCode.OK)
            {
                return request.Response.ResponseString;
            }

            throw new MDLPClientException(MDLPErrorMessage, request.Response.HttpStatusCode, request.Response.ResponseString);
        }

        /*
         * Выполняет Post запрос с возвращаемым результатом
         */
        protected async Task<T> PostResult<T>(string url, object requestObject, string targetId = null)
        {
            string json; Uri uri;

            GetParametrs(url, out json, out uri, requestObject);

            var request = new Request(HttpMethod.Post, uri, json, targetId, accessToken: AccesToken);

            return await GetResult<T>(request);
        }

        /*
         * Выполняет Post запрос без возвращаемого значения
         */
        protected async Task Post(string url, object requestObject, string targetId = null)
        {
            string json; Uri uri;

            GetParametrs(url, out json, out uri, requestObject);

            var request = new Request(HttpMethod.Post, uri, json, targetId, accessToken: AccesToken);

            await Client.Send(request);

            if (request.Response.HttpStatusCode != HttpStatusCode.OK)
            {
                throw new MDLPClientException(MDLPErrorMessage, request.Response.HttpStatusCode, request.Response.ResponseString);
            }
        }
        /*
         * Выполняет Get запрос с возвращаемым результатом
         */
        protected async Task<T> GetResult<T>(string url, string targetId = null)
        {
            string json; Uri uri;

            GetParametrs(url, out json, out uri);

            var request = new Request(HttpMethod.Get, uri, json, targetId, accessToken: AccesToken);

            await Client.Send(request);

            if (request.Response.HttpStatusCode == HttpStatusCode.OK)
            {
                return JsonSerializer.Deserialize<T>(request.Response.ResponseString);
            }

            throw new MDLPClientException(MDLPErrorMessage, request.Response.HttpStatusCode, request.Response.ResponseString);
        }

        /*
         * Выполняет Get запрос без возвращаемого результата
         */
        protected async Task Get(string url, object requestObject = null, string targetId = null)
        {
            string json; Uri uri;

            GetParametrs(url, out json, out uri, requestObject);

            var request = new Request(HttpMethod.Get, uri, json, targetId, accessToken: AccesToken);

            await Client.Send(request);

            if (request.Response.HttpStatusCode == HttpStatusCode.OK)
            {
                return;
            }

            throw new MDLPClientException(MDLPErrorMessage, request.Response.HttpStatusCode, request.Response.ResponseString);
        }

        private void GetParametrs(string url, out string json, out Uri uri, object requestObject = null)
        {
            if (requestObject != null)
            {
                json = JsonSerializer.Serialize(requestObject);
            }
            else
            {
                json = null;
            }

            uri = new Uri(BaseUrl + url);
        }

        protected async Task<T> GetResult<T>(Request request)
        {
            await Client.Send(request);

            if (request.Response.HttpStatusCode == HttpStatusCode.OK)
            {
                return JsonSerializer.Deserialize<T>(request.Response.ResponseString);
            }

            throw new MDLPClientException(MDLPErrorMessage, request.Response.HttpStatusCode, request.Response.ResponseString);
        }

        /*
         * Реализация интерфейса IDisposable
         */
        #region
        public void Dispose()
        {
            Client.Dispose();
        }
        #endregion
    }
}
