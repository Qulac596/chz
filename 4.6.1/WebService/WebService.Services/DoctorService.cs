using System;
using System.Collections.Generic;
using System.Linq;
using WebService.DataBase.Address;
using WebService.DataBase.Companies;
using WebService.DataBase.Doctor;
using WebService.MemoryDataBase;
using WebService.ViewModel;

namespace WebService.Services
{
    public class DoctorService : ServiceBase
    {
        public DoctorService() { }

        public DoctorService(string conString, string login, string password) : base(conString, login, password)
        {
        }

        public Result<ViewModel.Doctor> Enter(string login, string password)
        {
            try
            {
                var doctorDataBase = new DoctorDataBase(ConnectionStringPattern, Login, Password);
                var doctor = doctorDataBase.GetUser(login);
                var accessToken = Guid.NewGuid().ToString();
                var user = new User() { Id = doctor.Id, AccessToken = accessToken, DateTime = DateTime.Now, Login = login, Password = password };
                UsersStorage.Add(user);
                return new Result<ViewModel.Doctor>(new ViewModel.Doctor()
                {
                    access_token = accessToken,
                    fio = doctor.Fio
                });
            }
            catch (Exception e)
            {
                return new Result<ViewModel.Doctor>(e.Message);
            }
        }

        public Result<object> Exit(string login)
        {
            try
            {
                UsersStorage.Remove(login);
                return new Result<object>();
            }
            catch (Exception e)
            {
                return new Result<object>(e.Message);
            }
        }

        public Result<IEnumerable<AddressViewModel>> GetUserAddresses(int userId)
        {
            try
            {
                var addressDataBase = new AddressDataBase(ConnectionStringPattern, Login, Password);
                return new Result<IEnumerable<AddressViewModel>>(addressDataBase.GetUserAddresses(userId)
                .Select((x) => new AddressViewModel() { address_id = x.Id, text = x.Text })
                .ToList());
            }
            catch (Exception e)
            {
                return new Result<IEnumerable<AddressViewModel>>(e.Message);
            }
        }


        public Result<CompanyViewModel> GetUserCompany(int userId)
        {
            try
            {
                var companiesDataBase = new CompaniesDataBase(ConnectionStringPattern, Login, Password);
                var company = companiesDataBase.GetUserCompany(userId);
                return new Result<CompanyViewModel>(new CompanyViewModel()
                {
                    company_id = company.Id,
                    inn = company.Inn,
                    name = company.Name
                });
            }
            catch (Exception e)
            {
                return new Result<CompanyViewModel>(e.Message);
            }
        }
    }
}
