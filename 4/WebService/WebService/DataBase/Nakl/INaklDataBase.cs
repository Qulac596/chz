using System;
using System.Collections.Generic;
using WebService.Models;

namespace WebService.DataBase.Nakl
{
    public interface INaklDataBase
    {
        IEnumerable<NaklGridModel> Get(DateTime startDateTime, DateTime endDateTime, int addressId);

        IEnumerable<NaklGridModel> Get(DateTime startDateTime, DateTime endDateTime, int statusId, int addressId);

        NaklModel Get(int naklId);

        void Cancel(int naklId);

        void Finish(int naklId, DateTime dateTime);

        void TrustAccept(int naklId, DateTime dateTime);

        bool GetIsDirect(int gtinId);

        void Add(CreateNaklModel createNaklModel);

        int GetMaxId();

        void SaveChanges(CreateNaklModel createNaklModel);

        void Delete(int naklId);
    }
}