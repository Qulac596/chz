using System.Collections.Generic;
using Tools.SqlTransact;
using WebService.DataBase.Nakl;
using WebService.Models;

namespace WebService.DataBase.NaklItem
{
    public class NaklItemDataBase : DataBase, INaklItemDataBase
    {
        public NaklItemDataBase(string connectionString) : base(connectionString) { }

        public void Add(CreateNaklItemModel createNaklItemModel)
        {
            const string commandText = @"Insert Into chz_gtins(chz_nakl_id,count,const,vat_value,prod_sell_name)
            Values(@chz_nakl_id,@count,@const,@vat_value,@prod_sell_name)";

            var transaction = Transact<object>.Create(ConnectionString, commandText, parameter: createNaklItemModel);
            transaction.ExecuteNonQuery();
        }

        public void Delete(int naklItemId)
        {
            var commandText = @"Delete chz_gtins
            Where chz_gtin_id =@chz_nakl_id";

            var parameter = new NaklIdParameter() { NaklId = naklItemId };
            var transaction = Transact<object>.Create(ConnectionString, commandText, parameter: parameter);
            transaction.ExecuteNonQuery();
        }

        public IEnumerable<NaklItemModel> Get(int naklId)
        {
            const string commandText = @"Select chz_gtins.chz_gtin_id,
            chz_gtins.prod_sell_name As sell_name,
            (Select Count(*) From chz_sgtins Where chz_gtins.chz_gtin_id = chz_sgtins.chz_gtin_id) As sgtin_count,
            (Select Sum(chz_sgtins.cost) From chz_sgtins Where chz_gtins.chz_gtin_id = chz_sgtins.chz_gtin_id) As sum,
            (Select Sum(chz_sgtins.vat_value) From chz_sgtins Where chz_gtins.chz_gtin_id = chz_sgtins.chz_gtin_id) As nds,
            (Select Sum(chz_sgtins.cost - chz_sgtins.vat_value) From chz_sgtins Where chz_gtins.chz_gtin_id = chz_sgtins.chz_gtin_id) As price,
            (Select  chz_gtin_statuses.name From chz_gtin_statuses Where chz_gtins.gtin_status_id = chz_gtin_statuses.gtin_status_id) as status,
			(Select  chz_gtin_statuses.color From chz_gtin_statuses Where chz_gtins.gtin_status_id = chz_gtin_statuses.gtin_status_id) as color
            From chz_gtins 
            Where chz_gtins.chz_nakl_id =@chz_nakl_id";

            var parametr = new NaklIdParameter() { NaklId = naklId };
            var transact = Transact<NaklItemModel>.Create(ConnectionString, commandText, parameter: parametr);
            return transact.ExecuteReader();
        }

        public int GetMaxId()
        {
            const string commandText = @"Select Max(chz_gtin_id) From chz_gtins";

            var transact = Transact<object>.Create(ConnectionString, commandText);
            return transact.ExecuteScalar<int>();
        }

        public void SaveChanges(CreateNaklItemModel createNaklItemModel)
        {
            const string commandText = @"Update chz_gtins
            Set chz_nakl_id = @chz_nakl_id, const=@const, vat_value=@vat_value,count=@count,prod_sell_name=@prod_sell_name
            Where chz_gtin_id=@chz_gtin_id";

            var transaction = Transact<object>.Create(ConnectionString, commandText, parameter: createNaklItemModel);
            transaction.ExecuteNonQuery();
        }

        public void SetAllStatus(int naklId, int statusId)
        {
            const string commandText = @"Update chz_gtins
            Set gtin_status_id = @gtin_status_id
            Where chz_gtins.chz_nakl_id = @chz_nakl_id";

            var parameter = new NaklItemSetAllStatusParametr() { NaklId = naklId, Status = statusId };
            var transact = Transact<NaklItemModel>.Create(ConnectionString, commandText, parameter: parameter);
            transact.ExecuteNonQuery();
        }

        public void SetOneStatus(int gtinId, int statusId)
        {
            const string commandText = @"Update chz_gtins
            Set gtin_status_id = @gtin_status_id
            Where chz_gtins.chz_gtin_id = @chz_gtin_id";

            var parameter = new NaklItemSenOneParameter() { Id = gtinId, Status = statusId };
            var transact = Transact<NaklItemModel>.Create(ConnectionString, commandText, parameter: parameter);
            transact.ExecuteNonQuery();
        }
    }
}