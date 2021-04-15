using System.Collections.Generic;

namespace chz.WindowsServices.ThisLoadDocument.DataBase
{
    /*
     * База данных входщих документов
     */
    public interface IDataBase : WindowsServices.DataBase.IDataBase<IncomeDocument>
    {
        /*
         * Возращает документы с самой последней датой
         */
        IEnumerable<IncomeDocument> GetLastDocument();

        /*
         * Возвращает новые документы
         */
        IEnumerable<IncomeDocument> GetNewIncomeDocument();

        /*
         * Добавляет новый документ в бд
         * Если документ был добавлен, то возвращает его Id
         */
        int Add(IncomeDocument incomeDocument);

        /*
         * Возвращает документы с загруженной ссылкой
         */
        IEnumerable<IncomeDocument> GetLoadLink();

    }
}
