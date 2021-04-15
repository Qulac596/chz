using System;
using System.Collections.Generic;
using System.Linq;
using Tools.SqlTransact;
using WebService.Models;

namespace WebService.DataBase.Nakl
{
    public class NaklDataBase : DataBase, INaklDataBase
    {
        public NaklDataBase(string connectionString) : base(connectionString)
        {
        }

        public void Cancel(int naklId)
        {
            const string commandText = @"Update chz_sgtins
            Set is_scened=0
            Where chz_sgtin_id In 
           ( Select chz_sgtin_id
           From chz_sgtins Inner Join chz_gtins On chz_sgtins.chz_gtin_id = chz_gtins.chz_gtin_id
           Inner Join chz_nakls On chz_gtins.chz_nakl_id = chz_nakls.chz_nakl_id Where chz_nakls.chz_nakl_id = @chz_nakl_id)";

            var parameter = new NaklIdParameter() { NaklId = naklId };
            var transact = Transact<NaklGridModel>.Create(ConnectionString, commandText, parameter: parameter);
            transact.ExecuteNonQuery();
        }

        public IEnumerable<NaklGridModel> Get(DateTime startDateTime, DateTime endDateTime, int addressId)
        {
            const string commandText = @"Select chz_nakl_id,doc_num,doc_date,is_direct,chz_nakl_statuses.name As status_name,pic_url,chz_contract_types.name As contract_type_name,
            org_name,
           (Select Sum(chz_sgtins.cost) From chz_gtins Inner Join chz_sgtins On chz_gtins.chz_gtin_id=chz_sgtins.chz_gtin_id Where chz_gtins.chz_nakl_id = chz_nakl_id) As sum
           From chz_nakls Inner Join chz_nakl_statuses On chz_nakls.chz_nakl_status_id=chz_nakl_statuses.chz_nakl_status_id
           Inner Join chz_contract_types On chz_nakls.contract_type = chz_contract_types.chz_contract_type_id
           Inner Join chz_companys On chz_nakls.subject_id = chz_companys.chz_company_id
           Inner Join chz_addresses On chz_addresses.chz_company_id = chz_companys.chz_company_id
           Where chz_addresses.chz_address_id = @chz_address_id And chz_nakls.doc_date >= @startDateTime And chz_nakls.doc_date < @endDateTime";

            var parametr = new SelectAllNaklParametr() { StartDateTime = startDateTime, EndDateTime = endDateTime, AddressId = addressId };
            var transact = Transact<NaklGridModel>.Create(ConnectionString, commandText, parameter: parametr);
            return transact.ExecuteReader();
        }

        public IEnumerable<NaklGridModel> Get(DateTime startDateTime, DateTime endDateTime, int statusId, int addressId)
        {
            const string commandText = @"Select chz_nakl_id,doc_num,doc_date,is_direct,chz_nakl_statuses.name As status_name,pic_url,chz_contract_types.name As contract_type_name,
            org_name,
           (Select Sum(chz_sgtins.cost) From chz_gtins Inner Join chz_sgtins On chz_gtins.chz_gtin_id=chz_sgtins.chz_gtin_id Where chz_gtins.chz_nakl_id = chz_nakl_id) As sum
           From chz_nakls Inner Join chz_nakl_statuses On chz_nakls.chz_nakl_status_id=chz_nakl_statuses.chz_nakl_status_id
           Inner Join chz_contract_types On chz_nakls.contract_type = chz_contract_types.chz_contract_type_id
           Inner Join chz_companys On chz_nakls.subject_id = chz_companys.chz_company_id
           Inner Join chz_addresses On chz_addresses.chz_company_id = chz_companys.chz_company_id
           Where chz_nakl_statuses.chz_nakl_status_id = @chz_nakl_status_id And chz_addresses.chz_address_id = @chz_address_id And chz_nakls.doc_date >= @startDateTime And chz_nakls.doc_date < @endDateTime";

            var parametr = new SelectStatusNaklParameter() { StartDateTime = startDateTime, EndDateTime = endDateTime, AddressId = addressId, StatudId = statusId };
            var transact = Transact<NaklGridModel>.Create(ConnectionString, commandText, parameter: parametr);
            return transact.ExecuteReader();
        }

        public NaklModel Get(int naklId)
        {
            const string commandText = @"Select chz_nakl_id,
            Sc.org_name As provider_name,Sc.inn As provider_inn,Sa.text As provider_address,Rc.org_name As recesiver_name,Rc.inn As recesiver_inn, Ra.text As recesiver_address
            From chz_nakls Inner Join chz_addresses As Sa On chz_nakls.subject_id = Sa.chz_address_id
            Inner Join chz_addresses As Ra On chz_nakls.receiver_id = Ra.chz_address_id
            Inner Join chz_companys As Sc On Sa.chz_company_id = Sc.chz_company_id
            Inner Join chz_companys As Rc On Ra.chz_company_id = Rc.chz_company_id
            Where chz_nakls.chz_nakl_id =@chz_nakl_id";

            var parametr = new NaklIdParameter() { NaklId = naklId };
            var transact = Transact<NaklModel>.Create(ConnectionString, commandText, parameter: parametr);
            return transact.ExecuteReader().First();
        }

        public void Finish(int naklId, DateTime dateTime)
        {
            const string commandText = @"Update chz_nakls
            Set chz_nakl_status_id = 3,date_time_finish =@date_time_finish
            Where chz_nakl_id =@chz_nakl_id";

            var parameter = new NaklFinishParameter() { NaklId = naklId, DateTime = dateTime };
            var transact = Transact<NaklModel>.Create(ConnectionString, commandText, parameter: parameter);
            transact.ExecuteNonQuery();
        }

        public void TrustAccept(int naklId, DateTime dateTime)
        {
            const string commandText = @"Update chz_nakls
            Set chz_nakl_status_id = 3,date_time_finish =@date_time_finish,is_trust_accept=1
            Where chz_nakl_id =@chz_nakl_id";

            var parameter = new NaklFinishParameter() { NaklId = naklId, DateTime = dateTime };
            var transact = Transact<NaklModel>.Create(ConnectionString, commandText, parameter: parameter);
            transact.ExecuteNonQuery();
        }

        public bool GetIsDirect(int gtinId)
        {
            const string commandText = @"Select is_direct
            From chz_nakls Inner Join chz_gtins On chz_nakls.chz_nakl_id = chz_gtins.chz_gtin_id
            Where chz_gtins.chz_gtin_id =@chz_gtin_id";

            var parameter = new NaklItemIdParameter() { Id = gtinId };
            var transact = Transact<NaklModel>.Create(ConnectionString, commandText, parameter: parameter);
            return transact.ExecuteScalar<bool>();
        }

        public void Add(CreateNaklModel createNaklModel)
        {
            const string commandText = @"Insert Into chz_nakls(subject_id,shipper_id,operation_date,doc_num,doc_date,receive_type,source,contract_type,contract_num,is_direct,chz_nakl_status_id)
            Values(@subject_id,@shipper_id,@operation_date,@doc_num,@doc_date,@receive_type,@source,@contract_type,@contract_num,0,1)";

            var transaction = Transact<object>.Create(ConnectionString, commandText, parameter: createNaklModel);
            transaction.ExecuteNonQuery();
        }

        public int GetMaxId()
        {
            const string commandText = @"Select Max(chz_nakl_id) From chz_nakls";

            var transaction = Transact<object>.Create(ConnectionString, commandText);
            return transaction.ExecuteScalar<int>();
        }

        public void SaveChanges(CreateNaklModel createNaklModel)
        {
            const string commandText = @"Update chz_nakls
            Set subject_id= @subject_id,shipper_id= @shipper_id,operation_date= @operation_date,doc_num= @doc_num,doc_date= @doc_date,receive_type= @receive_type,
            source= @source,contract_type= @contract_type,contract_num= @contract_num,is_direct= 0,chz_nakl_status_id=1
            Where chz_nakl_id=@chz_nakl_id";

            var parameter = new NaklIdParameter() { NaklId = createNaklModel.Id };
            var transaction = Transact<object>.Create(ConnectionString, commandText, parameter: createNaklModel);
            transaction.ExecuteNonQuery();
        }

        public void Delete(int naklId)
        {
            const string commandText = @"Delete chz_nakls
            Where chz_nakl_id = @chz_nakl_id";

            var parameter = new NaklIdParameter() { NaklId = naklId };
            var transaction = Transact<object>.Create(ConnectionString, commandText, parameter: parameter);
            transaction.ExecuteNonQuery();
        }
    }
}