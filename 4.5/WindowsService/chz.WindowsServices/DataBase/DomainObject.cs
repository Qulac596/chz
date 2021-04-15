using System;
using System.Net;

namespace chz.WindowsServices.DataBase
{
    /*
     * Базовый класс для объектом получаемых-отправляемых в МДЛП
     */
    public abstract class DomainObject
    {
        /*
         * Id в бд
         */
        public int Id { get; set; }

        /*
         * Уникальный индификатор объекта в службе
         */
        public string Guid { get; set; }

        /*
         * Дата выполнения
         */
        public DateTime? CompleteDateTime { get; set; }

        /*
        * Ошибка при отправке
        */
        public Error Error { get; set; }

        public void SetError(string message, ServiceErrorType serviceErrorType)
        {
            Error = new ServiceError(message, serviceErrorType);
        }

        public void SetError(string message, string response, HttpStatusCode httpStatusCode)
        {
            Error = new MDLPError(message, response, httpStatusCode);
        }
    }
}
