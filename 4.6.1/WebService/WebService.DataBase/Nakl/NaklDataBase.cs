using System.Collections.Generic;
using System.Linq;
using Tools.SqlTransact;

namespace WebService.DataBase.Nakl
{
    public class NaklDataBase : DataBase
    {
        public NaklDataBase(string connectionString, string login, string password) : base(connectionString, login, password) { }

        public IEnumerable<NaklGrid> Get(int? companyId, int? year, int? month, int? statusId)
        {
            const string commandText = @"Select chz_nakls.chz_nakl_id As chz_nakl_id,doc_num,doc_data,ProviderCompany.name As provider_name,chz_nakls__list_of_accept_types.value As accept_types_value,
            chz_contract_types.value As contract_types_value,chz_nakl_statuses.value As nakl_statuses_value,class_name,(Select Sum(cost) From chz_nakl_items Where chz_nakl_items.chz_nakl_id =chz_nakls.chz_nakl_id) As sum

            From chz_nakls Inner Join  chz_addresses ProviderAddresses On chz_nakls.provider_chz_address_id = ProviderAddresses.chz_address_id
            Inner Join chz_addresses ReceiverAddresses On chz_nakls.receiver_chz_address_id=ReceiverAddresses.chz_address_id
            Inner Join chz_companies ProviderCompany On ProviderAddresses.chz_company_id=ProviderCompany.chz_company_id
            Inner Join chz_companies ReceiverCompany On ReceiverAddresses.chz_company_id=ReceiverCompany.chz_company_id
            Inner Join chz_nakls__list_of_accept_types On chz_nakls.chz_nakls__list_of_accept_type_id=chz_nakls__list_of_accept_types.chz_nakls__list_of_accept_type_id
            Inner Join chz_contract_types On chz_nakls.chz_contract_type_id=chz_contract_types.chz_contract_type_id
            Inner Join chz_nakl_statuses On chz_nakls.chz_nakl_status_id = chz_nakl_statuses.chz_nakl_status_id

             Where (@company_id is Null or ProviderCompany.chz_company_id=@company_id) And
            (@year is Null Or YEAR(chz_nakls.doc_data)=@year) And
            (@month is Null Or MONTH(chz_nakls.doc_data)=@month) And
            (@statusId is Null Or chz_nakls.chz_nakl_status_id = @statusId)";

            var parameter = new NaklFiltrParameter() { CompanyId = companyId, Year = year, Month = month, StatusId = statusId };
            var transaction = Transact<NaklGrid>.Create(ConnectionString, commandText, parameter: parameter);
            return transaction.ExecuteReader();
        }

        public Nak GetById(int id)
        {
            const string commandText = @"Select chz_nakls.chz_nakl_id As chz_nakl_id,doc_num,doc_data,ProviderCompany.inn As provider_inn,ProviderCompany.name As provider_name,
			ProviderAddresses.text As provider_address,ReceiverCompany.inn As receiver_inn,ReceiverCompany.name As receiver_name,ReceiverAddresses.text As receiver_address,
			chz_nakls.chz_nakl_status_id As nakl_status_id,chz_nakls__list_of_accept_types.value As acept_type

            From chz_nakls Inner Join  chz_addresses ProviderAddresses On chz_nakls.provider_chz_address_id = ProviderAddresses.chz_address_id
            Inner Join chz_addresses ReceiverAddresses On chz_nakls.receiver_chz_address_id=ReceiverAddresses.chz_address_id
            Inner Join chz_companies ProviderCompany On ProviderAddresses.chz_company_id=ProviderCompany.chz_company_id
            Inner Join chz_companies ReceiverCompany On ReceiverAddresses.chz_company_id=ReceiverCompany.chz_company_id
            Inner Join chz_nakls__list_of_accept_types On chz_nakls.chz_nakls__list_of_accept_type_id=chz_nakls__list_of_accept_types.chz_nakls__list_of_accept_type_id
            Inner Join chz_nakl_statuses On chz_nakls.chz_nakl_status_id = chz_nakl_statuses.chz_nakl_status_id

            Where chz_nakls.chz_nakl_id = @chz_nakl_id";

            var parameter = new NaklIdParameter() { Id = id };
            var transaction = Transact<Nak>.Create(ConnectionString, commandText, parameter: parameter);
            return transaction.ExecuteReader().FirstOrDefault();
        }

        public void SetNaklStatus(int naklId, int naklStatusId)
        {
            const string commandText = @"Update chz_nakls
            Set chz_nakl_status_id=@chz_nakl_status_id
            Where chz_nakl_id = @chz_nakl_id";

            var parameter = new SetNaklStatusParameter() { NaklId = naklId, NaklStatusId = naklStatusId };
            var transaction = Transact<object>.Create(ConnectionString, commandText, parameter: parameter);
            transaction.ExecuteNonQuery();
        }

        public void SetNaklTrust(int naklId, bool? isTrust)
        {
            const string commandText = @"Update chz_nakls
            Set is_trust=@trust
            Where chz_nakl_id=@chz_nakl_id";

            var parameter = new SetNaklTrustParameter() { Id = naklId, IsTrust = isTrust };
            var transaction = Transact<object>.Create(ConnectionString, commandText, parameter: parameter);
            transaction.ExecuteNonQuery();
        }

        public NaklFull GetFullNaklById(int naklId)
        {
            const string commandText = @"Select chz_nakl_id,
             chz_address_id As provider_id,
             chz_companies.chz_company_id As company_id,
             operation_data,
             doc_data,
             doc_num,
             chz_receive_types.chz_receive_type_id As chz_receive_type_id,

             chz_source_types.chz_source_type_id As chz_source_type_id,chz_contract_types.chz_contract_type_id As chz_contract_type_id,chz_turnover_types.chz_turnover_tupe_id As chz_turnover_tupe_id,
            contract_num

            From chz_nakls Inner Join chz_addresses On chz_nakls.provider_chz_address_id = chz_addresses.chz_address_id
			Inner Join chz_companies On chz_companies.chz_company_id = chz_addresses.chz_company_id
            Inner Join chz_receive_types On chz_nakls.chz_receive_type_id = chz_receive_types.chz_receive_type_id
            Inner Join chz_source_types On chz_nakls.chz_source_type_id = chz_source_types.chz_source_type_id
            Inner Join chz_contract_types On chz_nakls.chz_contract_type_id = chz_contract_types.chz_contract_type_id
            Inner Join chz_turnover_types On chz_nakls.chz_turnover_tupe_id = chz_turnover_types.chz_turnover_tupe_id

            Where chz_nakls.chz_nakl_id = @chz_nakl_id";

            var parameter = new NaklIdParameter() { Id = naklId };
            var transaction = Transact<NaklFull>.Create(ConnectionString, commandText, parameter: parameter);
            return transaction.ExecuteReader().FirstOrDefault();
        }

        public int Add(NaklFull naklFull)
        {
            const string addCommandText = @"Insert Into chz_nakls (operation_data,doc_num,doc_data,contract_num,chz_nakls__list_of_accept_type_id,chz_contract_type_id,chz_nakl_status_id
           ,provider_chz_address_id,receiver_chz_address_id,chz_source_type_id,chz_turnover_tupe_id,chz_receive_type_id,is_trust)

		   Values (@operation_data,@doc_num,@doc_data,@contract_num,2,@chz_contract_type_id,1,@provider_id,@receiver_id
           ,@chz_source_type_id
           ,@chz_turnover_tupe_id
           ,@chz_receive_type_id
           ,NULL)";

            const string getIdCommandTex = @"Select Max(chz_nakl_id) 
            From chz_nakls";

            var addTransaction = Transact<object>.Create(ConnectionString, addCommandText, parameter: naklFull);
            addTransaction.ExecuteNonQuery();
            var getIdTransaction = Transact<object>.Create(ConnectionString, getIdCommandTex);
            return getIdTransaction.ExecuteScalar<int>();
        }

        public void Update(NaklFull naklFull)
        {
            const string commandText = @"Update chz_nakls 
           Set operation_data=@operation_data,
           doc_num=@doc_num,
           doc_data=@doc_data,
           contract_num=@contract_num,
           chz_nakls__list_of_accept_type_id=2,
           chz_contract_type_id=@chz_contract_type_id,
           chz_nakl_status_id=1,
           provider_chz_address_id=@provider_id,
           receiver_chz_address_id=@receiver_id,
           chz_source_type_id=@chz_source_type_id,
           chz_turnover_tupe_id=@chz_turnover_tupe_id,
           chz_receive_type_id=@chz_receive_type_id,
           is_trust=NULL
           Where chz_nakl_id=@chz_nakl_id";

            var transaction = Transact<object>.Create(ConnectionString, commandText, parameter: naklFull);
            transaction.ExecuteNonQuery();
        }

        public void Delete(int naklId)
        {
            const string commandText = @"Delete From chz_nakls
            Where chz_nakl_id = @chz_nakl_id";

            var parameter = new NaklIdParameter() { Id = naklId };
            var transaction = Transact<object>.Create(ConnectionString, commandText, parameter: parameter);
            transaction.ExecuteNonQuery();
        }

        public int GetAccepTypeId(int nakId)
        {
            const string commandText = @"Select chz_nakls__list_of_accept_type_id From chz_nakls
            Where chz_nakls.chz_nakl_id=@chz_nakl_id";

            var parameter = new NaklIdParameter() { Id = nakId };
            var transaction = Transact<object>.Create(ConnectionString, commandText, parameter: parameter);
            return transaction.ExecuteScalar<int>();
        }
    }
}
