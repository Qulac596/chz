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
               .Select((x) => new CompanyViewModel() { company_id = x.Id, name = x.Name, inn = x.Inn })
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
    }
}
