using System.Collections.Generic;

namespace chz.WindowsServices.UnloadDocument.DataBase
{
    /*
     * База данных
     */
    public interface IDataBase : WindowsServices.DataBase.IDataBase<OutcomeDocument>
    {
        /*
         * Возвращает объект-сигнал
         */
        Signal GetSignal();

        /*
         * Возвращает документы с загруженной ссылкой на тикет
         */
        IEnumerable<OutcomeDocument> GetLinkLoadDocument();

        /*
         * Возвращает документ обработанные МДЛП
         */
        IEnumerable<OutcomeDocument> GetProccessedDocuments();

        /*
         * Возвращает загруженные в МДЛП документы
         */
        IEnumerable<OutcomeDocument> GetUnloadDocuments();

        /*
         * Возращает новые документы
         */
        IEnumerable<OutcomeDocument> GetNewDocuments();

        /*
         * Возвращает отмененные документы
         */
        IEnumerable<OutcomeDocument> GetCanceledDocuments();
    }
}
