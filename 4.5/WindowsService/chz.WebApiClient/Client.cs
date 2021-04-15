using chz.Client.WebApiClient;
using chz.WebApiClient;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace chz.WebApiClient
{
    /*
     * Представляет самый нижний слой доступа к сервису мдлп
     */
    public class Client : IClient
    {
        private readonly Encoding encoding;

        private const string mediaType = "application/json";

        private readonly HttpClient httpClient;

        private readonly IRequestLogger loger;

        private readonly chz.WindowsServices.Setting.ISettingProvider setting;

        public Client(IRequestLogger loger, chz.WindowsServices.Setting.ISettingProvider setting)
        {
            encoding = Encoding.UTF8;
            httpClient = new HttpClient();
            this.loger = loger;
            this.setting = setting;
        }

        /*
         * Отправляет запрос
         */
        public async Task Send(Request request)
        {
            var requestMessage = new HttpRequestMessage(request.Metchod, request.Uri);

            if (request.Json != null)
            {
                requestMessage.Content = new StringContent(request.Json, encoding, mediaType);
            }

            if (request.AccessToken != null)
            {
                requestMessage.Headers.Add("Authorization", "token " + request.AccessToken);
            }

            request.SendDateTime = DateTime.Now;

            var responseMessage = await httpClient.SendAsync(requestMessage);

            var statusCode = responseMessage.StatusCode;

            var stringResponse = await responseMessage.Content.ReadAsStringAsync();

            var responseDateTime = DateTime.Now;

            request.AddResponse(responseDateTime, statusCode, stringResponse);

            if (bool.Parse(setting.GetValue("Debug")) == true)
            {
                loger.Write(request);
            }

            return;
        }

        public void Dispose()
        {
            httpClient.Dispose();
        }
    }
}
