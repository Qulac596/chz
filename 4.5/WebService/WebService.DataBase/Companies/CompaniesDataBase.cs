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
            const string commandText = @"Select chz_company_id,inn,name,short_name 
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

        public int Add( string inn,string name,string shortName)
        {
            const string procedureName = @"chz_add_company";

            var parameter = new AddCompanyParameter() { Inn = inn, Name = name, ShortName = shortName };
            var storageProcedure = StorageProcedure<CompanyIdParameter>.Create(ConnectionString, procedureName, parameter: parameter);
            return storageProcedure.ExecuteResult().Id;
        }

        public Company GetById(int id)
        {
            const string commandText = @"select chz_company_id,inn,name,short_name
            From chz_companies
            Where chz_company_id=@chz_company_id";

            var parameter = new CompanyIdParameter() { Id = id };
            var transaction = Transact<Company>.Create(ConnectionString, commandText, parameter: parameter);
            return transaction.ExecuteReader().FirstOrDefault();
        }

        public void Update(int id,string shortName)
        {
            const string commandText = @"UPDATE chz_companies
            SET short_name = @short_name
            WHERE chz_company_id = @chz_company_id";

            var parameter = new UpdateCompanyParameter() { Id = id, ShortName  = shortName };
            var transaction = Transact<object>.Create(ConnectionString, commandText, parameter: parameter);
            transaction.ExecuteNonQuery();
        }
    }
}
