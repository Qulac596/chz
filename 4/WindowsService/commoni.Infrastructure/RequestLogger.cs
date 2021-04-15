using chz.Client.WebApiClient;
using chz.WebApiClient;
using chz.WindowsServices.Log;
using chz.WindowsServices.Setting;
using System;
using System.IO;

namespace commoni.Infrastructure
{
    public class RequestLogger : LoggerBase, IRequestLogger
    {
        private const string Patch = "/Requests";
        private readonly ILogger logger;

        public RequestLogger(ILogger logger, string folderName, string serviceName, ISettingProvider setting, IDataBase dataBase) :
            base(folderName + Patch, serviceName, setting, dataBase)
        {
            this.logger = logger;
        }

        public void Write(Request request)
        {

            var message = "TargerId: " + (request.TargetId ?? "Отсуствует") + "\r\n" +
             "Дата отправки запроса: " + request.SendDateTime.Value + "\r\n" +
              "Guid: " + request.Guid + "\r\n" +
             "Url: " + request.Uri + "\r\n" +
            "Метод: " + request.Metchod + "\r\n" +
            "Токен доступа: " + (request.AccessToken ?? "Отсуствует") + "\r\n" +
            "Json: " + (request.Json ?? "Отсуствует") + "\r\n\r\n";

            var response = request.Response;

            message += "Дата получения ответа: " + response.DateTime + "\r\n" +
            "Http-статус: " + response.HttpStatusCode + "\r\n" +
             "Строка ответа: " + (string.IsNullOrEmpty(response.ResponseString) ? "Отсуствует" : response.ResponseString);

            var patch = GetPatch(request.SendDateTime.Value) + "/" + request.SendDateTime.Value.Hour + "/";

            var fileName = request.SendDateTime.Value.ToString("HH_mm_ss")
               + (request.TargetId == null ? "" : ("_" + request.TargetId)) + "_" + request.Guid
               + ".txt";

            try
            {
                Directory.CreateDirectory(patch);

                File.WriteAllText(patch + fileName, message);
            }
            catch (Exception e)
            {
                logger.WriteError(e);
            }
        }
    }
}
