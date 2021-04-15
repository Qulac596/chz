using chz.WindowsServices.Workes;
using System.Collections.Generic;

namespace chz.WindowsServices.DataBase
{
    /*
     * База данных
     */
    public interface IDataBase<TItem> where TItem : DomainObject
    {
        /*
         * Возвращает элементы с ошибками
         */
        IEnumerable<TItem> GetError();

        /*
         * Обновляет элемент
         */
        void Update(TItem item);
    }
}
