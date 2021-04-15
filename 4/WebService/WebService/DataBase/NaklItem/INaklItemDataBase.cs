using System.Collections.Generic;
using WebService.Models;

namespace WebService.DataBase.NaklItem
{
    public interface INaklItemDataBase
    {
        IEnumerable<NaklItemModel> Get(int naklId);

        void SetAllStatus(int naklId, int statusId);

        void SetOneStatus(int gtinId, int statusId);

        void Add(CreateNaklItemModel createNaklItemModel);

        int GetMaxId();

        void SaveChanges(CreateNaklItemModel createNaklItemModel);

        void Delete(int naklItemId);
    }
}