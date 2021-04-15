using System;
using System.Linq;
using System.Security.Principal;
using System.Web.Http;

namespace WebService.Controllers
{
    public abstract class BaseController : ApiController
    {
        private readonly string connectionStringPattern;

        protected BaseController(string connectionStringPattern)
        {
            this.connectionStringPattern = connectionStringPattern;
        }

        protected TService Create<TService>()
        {
            var genericIdentity = (GenericIdentity)User.Identity;
            var login = genericIdentity.Claims.First((x) => x.Type == "Login").Value;
            var password = genericIdentity.Claims.First((x) => x.Type == "Password").Value;

            return (TService)Activator.CreateInstance(typeof(TService), connectionStringPattern, login, password);
        }

        protected int GetUserId()
        {
            var genericIdentity = (GenericIdentity)User.Identity;
            var id = genericIdentity.Claims.First((x) => x.Type == "Id").Value;
            return int.Parse(id);
        }
    }
}