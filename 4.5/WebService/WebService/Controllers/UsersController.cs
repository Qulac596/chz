using System.Collections.Generic;
using System.Web.Http;
using WebService.Filters;
using WebService.Services;
using WebService.ViewModel;

namespace WebService.Controllers
{
    /*
     * Контролер для авторизации пользователя
     */
    public class UsersController : BaseController
    {
        private readonly string connectionStringPattern;

        public UsersController(string connectionStringPattern) : base(connectionStringPattern)
        {
            this.connectionStringPattern = connectionStringPattern;
        }

        /*
         *Вход в службу. Возвращает токен доступа
         */
        [HttpPut]
        public Result<ViewModel.Doctor> Enter(string login, string password)
        {
            var doctorService = new DoctorService(connectionStringPattern, login, password);
            return doctorService.Enter(login, password);
        }

        /*
        * Выход из службы
        */
        [HttpPut]
        [AuthenticationFiltr()]
        public Result<object> Exit()
        {
            var doctorService = new DoctorService();
            return doctorService.Exit(User.Identity.Name);
        }

        [AuthenticationFiltr()]
        [HttpGet]
        public Result<IEnumerable<AddressViewModel>> GetAddresses()
        {
            var doctorService = Create<DoctorService>();
            return doctorService.GetUserAddresses(GetUserId());
        }

        [HttpGet]
        [AuthenticationFiltr()]
        public Result<CompanyViewModel> GetCompany()
        {
            var doctorService = Create<DoctorService>();
            return doctorService.GetUserCompany(GetUserId());
        }
    }
}
