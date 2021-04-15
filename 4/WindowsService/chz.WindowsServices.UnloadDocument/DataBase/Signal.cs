using System.Collections.Generic;

namespace chz.WindowsServices.UnloadDocument.DataBase
{
    /*
     * Данные переодически считываемые из БД
     */
    public class Signal
    {
        public Signal(IEnumerable<OutcomeDocument> newDocuments, IEnumerable<OutcomeDocument> canceledDocuments)
        {
            NewDocuments = newDocuments;
            CanceledDocuments = canceledDocuments;
        }

        /*
         * Новые документы
         */
        public IEnumerable<OutcomeDocument> NewDocuments { get; private set; }

        /*
         * Отмененные документы
         */
        public IEnumerable<OutcomeDocument> CanceledDocuments { get; private set; }
    }
}
