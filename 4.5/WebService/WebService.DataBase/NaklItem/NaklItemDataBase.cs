using System.Collections.Generic;
using Tools.SqlTransact;
using WebService.DataBase.NdsValue;

namespace WebService.DataBase.NaklItem
{
    public class NaklItemDataBase : DataBase
    {
        public NaklItemDataBase(string connectionString, string login, string password) : base(connectionString, login, password) { }

        public IEnumerable<NaklItem> Get(int naklId)
        {
            const string procedureName = "chz_get_nakl_items";

            var parameter = new NaklIdParameter() { Id = naklId };
            var procedure = StorageProcedure<NaklItem>.Create(ConnectionString, procedureName, parameter: parameter);
            return procedure.ExecuteReader();
        }

        public void Update(int naklItemId, decimal cost, decimal vat_value,int? ndsValue)
        {
            const string commandText = @"Update chz_nakl_items
            Set cost=@cost, vat_value=@vat_value, nds=@nds
            Where chz_nakl_item_id=@chz_nakl_item_id;";

            var parameter = new NaklItemUpdateParameter() { Id = naklItemId, Cost = cost, VatValue = vat_value,Nds=ndsValue };
            var transaction = Transact<object>.Create(ConnectionString, commandText, parameter: parameter);
            transaction.ExecuteNonQuery();
        }
    }
}
