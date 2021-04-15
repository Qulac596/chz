using System.Collections.Generic;
using Tools.SqlTransact;
using WebService.Models;

namespace WebService.DataBase.Company
{
    public class CompanyDataBase : DataBase, ICompanyDataBase
    {
        public CompanyDataBase(string connectionSgring) : base(connectionSgring)
        {

        }

        public IEnumerable<CompanyResult> Get()
        {
            const string commandText = @"Select chz_companys.chz_company_id,org_name,inn,chz_address_id,text
            From chz_companys Inner Join chz_addresses On chz_companys.chz_company_id = chz_addresses.chz_company_id";

            var transaction = Transact<CompanyResult>.Create(ConnectionString, commandText);
            return transaction.ExecuteReader();
        }
    }
}