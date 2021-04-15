using chz.Common;
using chz.WindowsServices.DataBase;
using System;

namespace chz.WindowsServices.UnloadDocument.DataBase
{
    /*
     * Исходящий документ для отправки в МДЛП
     */
    public class OutcomeDocument : DomainObject
    {
        /*
         * Флаг отмены загружки в МДЛП
         */
        public bool IsCansel { get; set; }

        /*
         * Id документа в МДЛП
         */
        public string DocumentId { get; set; }

        /*
         * Id запроса на отправку в МДЛП
         */
        public string RequestId { get; set; }

        /*
         * Содержимое документа
         */
        public string Content { get; set; }

        /*
         * Содержимое документа в кодировке base64
         */
        public string Base64Content { get; set; }

        /*
         * Цифровая подпись документа
         */
        public string Signature { get; set; }

        /*
         * Статус документа в МДЛП
         */
        public MDLPDocumentStatus? MDLPDocumentStatus { get; set; }

        /*
         * Количество попыток проверки статуса документа в МДЛП
         */
        public int AttemptCount { get; set; }

        /*
        * Ссылка на квинатцию
        */
        public string TicketLink { get; set; }

        /*
         * Квитанция о приеме документа в МДЛП
         */
        public string Ticket { get; set; }

        /*
        * Статус документа в службе
        */
        public DocumentServiceStatus DocumentServiceStatus { get; set; }

        /*
         * Возвращает документ в начальное состояние
         */
        public void Cleaning()
        {
            DocumentId = null;
            RequestId = null;
            Base64Content = null;
            Signature = null;
            MDLPDocumentStatus = null;
            AttemptCount = 0;
            Ticket = null;
            DocumentServiceStatus = DocumentServiceStatus.New;
            TicketLink = null;
            Error = null;
        }
    }
}
