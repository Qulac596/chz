using System.Collections.Generic;
using System.Linq;
using Tools.SqlTransact;

namespace WebService.DataBase.Companies
{
    public class CompaniesDataBase : DataBase
    {
        public CompaniesDataBase(string connectionString, string login, string password) : base(connectionString, login, password) { }

        public IEnumerable<Company> GetAll()
        {
            const string commandText = @"Select chz_company_id,inn,name 
            From chz_companies";

            var transaction = Transact<Company>.Create(ConnectionString, commandText);
            return transaction.ExecuteReader();
        }

        public Company GetUserCompany(int userId)
        {
            const string commandText = @"Select chz_companies.chz_company_id, chz_companies.inn,name
            From doctors Inner Join chz_companies On doctors.chz_company_id=chz_companies.chz_company_id
			where doctor_id = @doctor_id";

            var parameter = new Doctor.IdParameter() { Id = userId };
            var transaction = Transact<Company>.Create(ConnectionString, commandText, parameter: parameter);
            return transaction.ExecuteReader().FirstOrDefault();
        }
    }
}
