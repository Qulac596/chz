using System.Collections.Generic;
using Tools.SqlTransact;

namespace WebService.DataBase.NaklItem
{
    public class NaklItemDataBase : DataBase
    {
        public NaklItemDataBase(string connectionString, string login, string password) : base(connectionString, login, password) { }


        public IEnumerable<NaklItem> GetDirectAcceptance(int naklId)
        {
            const string commandText = @"Select chz_nakl_item_id,
            name,
            (Select Count(*) From chz_sgtins Where chz_sgtins.chz_nakl_item_id=chz_nakl_items.chz_nakl_item_id) As count,
            (vat_value * (Select Count(*) From chz_sgtins Where chz_sgtins.chz_nakl_item_id=chz_nakl_items.chz_nakl_item_id)) As nds,
            ((cost - vat_value)*(Select Count(*) From chz_sgtins Where chz_sgtins.chz_nakl_item_id=chz_nakl_items.chz_nakl_item_id)) As price,
            (cost * (Select Count(*) From chz_sgtins Where chz_sgtins.chz_nakl_item_id=chz_nakl_items.chz_nakl_item_id)) As sum,
            (Select Count(*) From chz_scanned_sgtins Where chz_scanned_sgtins.chz_nakl_item_id = chz_nakl_items.chz_nakl_item_id) As scaned_count,
            (Select Count(*) From chz_sgtins Inner Join chz_scanned_sgtins On chz_sgtins.sgtin=chz_scanned_sgtins.value 
            Where chz_scanned_sgtins.chz_nakl_item_id = chz_nakl_items.chz_nakl_item_id And
            chz_sgtins.chz_nakl_item_id = chz_nakl_items.chz_nakl_item_id
            )  As count_matched

            From chz_nakl_items

            Where chz_nakl_items.chz_nakl_id = @chz_nakl_id";

            var parameter = new NaklIdParameter() { Id = naklId };
            var transaction = Transact<NaklItem>.Create(ConnectionString, commandText, parameter: parameter);
            return transaction.ExecuteReader();
        }

        public IEnumerable<NaklItem> GetReverseAcceptance(int naklId)
        {
            const string commandText = @"Select chz_nakl_item_id,
            name,
            (Select Count(*) From chz_scanned_sgtins Where chz_scanned_sgtins.chz_nakl_item_id=chz_nakl_items.chz_nakl_item_id) As count,
            (vat_value * (Select Count(*) From chz_scanned_sgtins Where chz_scanned_sgtins.chz_nakl_item_id=chz_nakl_items.chz_nakl_item_id)) As nds,
            ((cost - vat_value)*(Select Count(*) From chz_scanned_sgtins Where chz_scanned_sgtins.chz_nakl_item_id=chz_nakl_items.chz_nakl_item_id)) As price,
            (cost * (Select Count(*) From chz_scanned_sgtins Where chz_scanned_sgtins.chz_nakl_item_id=chz_nakl_items.chz_nakl_item_id)) As sum
            From chz_nakl_items
             Where chz_nakl_items.chz_nakl_id = @chz_nakl_id";

            var parameter = new NaklIdParameter() { Id = naklId };
            var transaction = Transact<NaklItem>.Create(ConnectionString, commandText, parameter: parameter);
            return transaction.ExecuteReader();
        }
    }
}
