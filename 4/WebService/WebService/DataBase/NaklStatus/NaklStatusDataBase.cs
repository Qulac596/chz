using System.Collections.Generic;
using Tools.SqlTransact;
using WebService.Models;

namespace WebService.DataBase.NaklStatus
{
    public class NaklStatusDataBase : DataBase, INaklStatusDataBase
    {
        public NaklStatusDataBase(string connectionString):base(connectionString)
        {
           
        }

        public IEnumerable<NaklStatusModel> GetNaklStatusModels()
        {
            const string commandText = "Select chz_nakl_status_id,chz_nakl_status_name From chz_nakl__list_of_status";

            var transact = Transact<NaklStatusModel>.Create(ConnectionString, commandText);
            return transact.ExecuteReader();
        }
    }
}