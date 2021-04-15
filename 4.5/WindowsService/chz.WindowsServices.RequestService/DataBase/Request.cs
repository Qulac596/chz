using chz.WindowsServices.DataBase;
using System;
using System.Net.Http;

namespace chz.WindowsServices.DirectoryLoader.DataBase
{
    /*
     * Запрос к сервису МДЛП
     */
    public class Request : DomainObject
    {
        /*
         * Метод
         */
        public HttpMethod Method { get; set; }

        /*
         * Url
         */
        public string Url { get; set; }

        /*
         * Содержимое запроса
         */
        public string RequestJson { get; set; }

        /*
         * Время отправки
         */
        public DateTime? StartAfte { get; set; }

        /*
         * Статус в службе
         */
        public ServiceStatus ServiceStatus { get; set; }

        /*
         * Время завершения запроса
         */
        public DateTime? ProcessedDatetime { get; set; }

        /*
         * Полученный ответ
         */
        public string ResponseJson { get; set; }

    }
}
