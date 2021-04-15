using System.Collections.Generic;
using Tools.SqlTransact;
using WebService.Models;

namespace WebService.DataBase.Address
{
    public class AddressDataBase : DataBase, IAddressDataBase
    {
        public AddressDataBase(string connectionString) : base(connectionString)
        { }

        public IEnumerable<AddressModel> Get(string accessToken)
        {
            const string commandText = @"Select chz_address_id,text
            From chz_users Inner Join chz_companys On chz_users.chz_company_id = chz_companys.chz_company_id
            Inner Join chz_addresses On chz_addresses.chz_company_id = chz_companys.chz_company_id
            Where chz_users.access_token = @access_token";

            var parameter = new UserAccessTokenParameter() { AccessToken = accessToken };
            var transaction = Transact<AddressModel>.Create(ConnectionString, commandText, parameter: parameter);
            return transaction.ExecuteReader();
        }
    }
}