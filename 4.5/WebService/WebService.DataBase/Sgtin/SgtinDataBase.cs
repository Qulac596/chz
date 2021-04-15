using System.Collections.Generic;
using System.Linq;
using Tools.SqlTransact;

namespace WebService.DataBase.Sgtin
{
    public class SgtinDataBase : DataBase
    {
        public SgtinDataBase(string connectionString, string login, string password) : base(connectionString, login, password) { }

        public int GetNaklAcceptTypeId(int naklItemId)
        {
            const string commandText = @"Select chz_nakls__list_of_accept_type_id
            From chz_nakl_items Inner Join chz_nakls On chz_nakl_items.chz_nakl_id=chz_nakls.chz_nakl_id
            Where chz_nakl_items.chz_nakl_item_id = @chz_nakl_item_id";

            var parameter = new NaklItemIdParameter() { Id = naklItemId };
            var transaction = Transact<object>.Create(ConnectionString, commandText, parameter: parameter);
            return transaction.ExecuteScalar<int>();
        }

        public IEnumerable<AddSgtinResult> AddNewSgtin(int? naklId, int? naklItemId, string[] sgtins)
        {
            const string procName = "chz_scanned_sgtins__add";

            var str = "";
            for (var i = 0; i < sgtins.Length; i++)
            {
                if (i < sgtins.Length - 1)
                {
                    str += sgtins[i] + ",";
                }
                else
                {
                    str += sgtins[i];
                }
            }

            var parameter = new AddSgtinParameter() { NaklId = naklId, NaklItemId = naklItemId, Sgtins = sgtins };
            var storageProcedure = StorageProcedure<AddSgtinResult>.Create(ConnectionString, procName, parameter: parameter);
            return storageProcedure.ExecuteReader();
        }

        public void AddScaned(int naklItemId, string[] sgtins)
        {
            /*
            var commandText = @"Insert Into chz_scanned_sgtins(chz_nakl_item_id,value)
            Values";

            for (var i = 0; i < sgtins.Length; i++)
            {
                commandText += "(" + naklItemId + ",'" + sgtins[i] + "')";
                if (i < sgtins.Length - 1)
                {
                    commandText += ",";
                }
            }

            var transaction = Transact<object>.Create(ConnectionString, commandText);
            transaction.ExecuteNonQuery();
            */
        }

        public void AddScaned(int naklId, IEnumerable<NaklItem> naklItems)
        {
            const string addNaklItemCommandText = @"Insert Into chz_nakl_items(chz_nakl_id,gtin,name)
             Values(@chz_nakl_id,@gtin,@name)";
            const string getMaxNaklItemIdCommandText = @"Select Max(chz_nakl_item_id) From chz_nakl_items";


            foreach (var item in naklItems)
            {
                var addNaklItemParameter = new NaklItemGtinParameter() { Gtin = item.Gtin, NaklId = naklId, Name = item.Name };
                var addNaklItemTransaction = Transact<object>.Create(ConnectionString, addNaklItemCommandText, parameter: addNaklItemParameter);
                addNaklItemTransaction.ExecuteNonQuery();

                var getNaklItemIdTranseaction = Transact<object>.Create(ConnectionString, getMaxNaklItemIdCommandText);
                var nakItemId = getNaklItemIdTranseaction.ExecuteScalar<int>();

                AddScaned(nakItemId, item.Sgtins.ToArray());
            }
        }

        public IEnumerable<Sgtin> Get(int naklItemId)
        {
            const string commandText = @"Select value From chz_scanned_sgtins
             Where chz_nakl_item_id=@chz_nakl_item_id";

            var parameter = new NaklItemIdParameter() { Id = naklItemId };
            var transaction = Transact<Sgtin>.Create(ConnectionString, commandText, parameter: parameter);
            return transaction.ExecuteReader();
        }

        public IEnumerable<SgtinResult> GetStinResult(int naklItemId)
        {
            const string commandText = @"Select chz_sgtins.sgtin As provider_sgtin, chz_scanned_sgtins.value As recesiver_sgtin 
            From chz_sgtins FULL OUTER JOIN chz_scanned_sgtins On chz_sgtins.sgtin=chz_scanned_sgtins.value
            Where chz_sgtins.chz_nakl_item_id = @chz_nakl_item_id Or chz_scanned_sgtins.chz_nakl_item_id = @chz_nakl_item_id";

            var parameter = new NaklItemIdParameter() { Id = naklItemId };
            var transaction = Transact<SgtinResult>.Create(ConnectionString, commandText, parameter: parameter);
            return transaction.ExecuteReader();
        }
    }
}
