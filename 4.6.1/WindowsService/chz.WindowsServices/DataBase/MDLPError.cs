using System.Net;

namespace chz.WindowsServices.DataBase
{
    /*
     * Ошибка возвращаемая МДЛП
     */
    public class MDLPError : Error
    {
        public MDLPError(string message, string response, HttpStatusCode httpStatusCode) : base(message)
        {
            Response = response;
            HttpStatusCode = httpStatusCode;
        }

        /*
         * Json описание ошибки которое вернул МДЛП
         */
        public string Response { get; set; }

        /*
         * HttpStatus возвращаемый при ошибке
         */
        public HttpStatusCode HttpStatusCode { get; set; }

    }
}
