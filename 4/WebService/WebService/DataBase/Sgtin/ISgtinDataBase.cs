using WebService.Models;
using System.Collections.Generic;


namespace WebService.DataBase.Sgtin
{
    public interface ISgtinDataBase
    {
        IEnumerable<SgtinModel> Get(int gtinId);

        void AddScannedSgtins(int gtinId, string[] sgtins);

        void Reset(int gtinId);

        bool MatchCheck(int gtin);
    }
}