using System.Collections.Generic;
using Tools.SqlTransact;

namespace WebService.DataBase.Address
{
    public class AddressDataBase : DataBase
    {
        public AddressDataBase(string connectionString, string login, string password) : base(connectionString, login, password) { }

        public IEnumerable<Address> GetUserAddresses(int userId)
        {
            const string commandText = @"Select chz_address_id, chz_addresses.sys_id,text
            From chz_addresses Inner Join chz_companies On chz_addresses.chz_company_id=chz_companies.chz_company_id
            Inner Join doctors On doctors.chz_company_id=chz_companies.chz_company_id
            Where doctor_id=@doctor_id";

            var parameter = new Doctor.IdParameter() { Id = userId };
            var transaction = Transact<Address>.Create(ConnectionString, commandText, parameter: parameter);
            return transaction.ExecuteReader();
        }

        public IEnumerable<Address> GetCompanyAddresses(int companyId)
        {
            const string commandText = @"Select chz_address_id,text
            From chz_companies Inner Join chz_addresses On chz_companies.chz_company_id=chz_addresses.chz_company_id
            Where chz_companies.chz_company_id=@chz_company_id";

            var parameter = new Companies.CompanyIdParameter() { Id = companyId };
            var transaction = Transact<Address>.Create(ConnectionString, commandText, parameter: parameter);
            return transaction.ExecuteReader();
        }
    }
}
