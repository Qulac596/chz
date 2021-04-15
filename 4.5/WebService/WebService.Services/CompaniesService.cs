using System;
using System.Collections.Generic;
using System.Linq;
using WebService.DataBase.Address;
using WebService.DataBase.Companies;
using WebService.ViewModel;

namespace WebService.Services
{
    public class CompaniesService : ServiceBase
    {
        public CompaniesService(string connectionStringPattern, string login, string password) : base(connectionStringPattern, login, password)
        {

        }

        public Result<IEnumerable<CompanyViewModel>> Get()
        {
            try
            {
                var companiesDataBase = new CompaniesDataBase(ConnectionStringPattern, Login, Password);
                return new Result<IEnumerable<CompanyViewModel>>(companiesDataBase.GetAll()
               .Select((x) => new CompanyViewModel() { company_id = x.Id, name = x.Name, inn = x.Inn, short_name = x.ShortName })
                .ToList());
            }
            catch (Exception e)
            {
                return new Result<IEnumerable<CompanyViewModel>>(e.Message);
            }
        }

        public Result<IEnumerable<AddressViewModel>> Get(int id)
        {
            try
            {
                var addressDataBase = new AddressDataBase(ConnectionStringPattern, Login, Password);
                return new Result<IEnumerable<AddressViewModel>>(addressDataBase.GetCompanyAddresses(id)
                    .Select((x) => new AddressViewModel() { address_id = x.Id, text = x.Text }).ToList());
            }
            catch (Exception e)
            {
                return new Result<IEnumerable<AddressViewModel>>(e.Message);
            }
        }

        public Result<CompanyResultViewModel> Get(string inn)
        {
            var cr = inn[inn.Length - 1];
            if (cr == '0')
            {
                return new Result<CompanyResultViewModel>(new CompanyResultViewModel()
                { status = "not_found", message = "Поставщик с таким ИНН не найден." });
            }
            else if (cr == '1')
            {
                return new Result<CompanyResultViewModel>(new CompanyResultViewModel() { status = "search", message = "Идет поиск данных." });
            }
            else
            {
                return new Result<CompanyResultViewModel>(new CompanyResultViewModel()
                {
                    status = "ok",
                    company = new CompanyViewModel()
                    {
                        company_id = 666,
                        inn = inn,
                        name = "Банк Плакали ваши денюшки"
                    }
                });
            }
        }

        public Result<int> Add(CompanyViewModel companyViewModel)
        {
            try
            {
                var companiesDataBase = new CompaniesDataBase(ConnectionStringPattern, Login, Password);
                return new Result<int>(
                    companiesDataBase.Add(companyViewModel.inn, companyViewModel.name, companyViewModel.short_name)
                    );
            }
            catch (Exception e)
            {
                return new Result<int>(e.Message);
            }
        }

        public Result<CompanyViewModel> GetById(int id)
        {
            try
            {
                var companiesDataBase = new CompaniesDataBase(ConnectionStringPattern, Login, Password);
                var company = companiesDataBase.GetById(id);

                if (company != null)
                {
                   return new Result<CompanyViewModel>(new CompanyViewModel()
                    {
                        company_id = company.Id,
                        inn = company.Inn,
                        name = company.Name,
                        short_name = company.ShortName
                    });
                }
                return null;
            }
            catch (Exception e)
            {
                return new Result<CompanyViewModel>(e.Message);
            }
        }

        public Result<object> Update(CompanyViewModel companyViewModel)
        {
            try
            {
                var companiesDataBase = new CompaniesDataBase(ConnectionStringPattern, Login, Password);
               
                companiesDataBase.Update(companyViewModel.company_id,companyViewModel.short_name);

                return new Result<object>();
            }
            catch (Exception e)
            {
                return new Result<object>(e.Message);
            }
        }
    }
}
