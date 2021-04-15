using System;
using System.Net;
using System.Net.Http;

namespace chz.Client.WebApiClient
{
    /*
     * Запрос с сервису мдлп
     */
    public class Request
    {
        public Request(HttpMethod method, Uri uri, string json = null, string targetId = null, string accessToken = null)
        {
            Guid = Guid.NewGuid();
            Metchod = method;
            Uri = uri;
            Json = json;
            TargetId = targetId;
            AccessToken = accessToken;
        }

        /*
         * Ункальный идификатор объекта службы с которым связан запрос
         */
        public string TargetId { get; set; }

        /*
         * Уникаьный идификатор
         */
        public Guid Guid { get; set; }

        /*
         * Токен доступа
         */
        public string AccessToken { get; set; }

        /*
         * Дата отправки
         */
        public DateTime? SendDateTime { get; set; }

        /*
         * Метод
         */
        public HttpMethod Metchod { get; set; }

        /*
         * Url запроса
         */
        public Uri Uri { get; set; }

        /*
         * Отправляемые данные
         */
        public string Json { get; set; }

        /*
         * Ответ
         */
        public Response Response { get; private set; }

        /*
         * Добавляет ответ
         */
        public void AddResponse(DateTime dateTime, HttpStatusCode httpStatusCode, string responseString)
        {
            Response = new Response(dateTime, httpStatusCode, responseString);
        }
    }
}
