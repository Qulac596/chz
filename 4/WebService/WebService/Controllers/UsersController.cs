using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebService.DataBase.User;
using WebService.Models;

namespace WebService.Controllers
{
    /*
     * Контролер для авторизации пользователя
     */
    public class UsersController : ApiController
    {
        private readonly IUserDataBase dataBase;

        public UsersController(IUserDataBase dataBase)
        {
            this.dataBase = dataBase;
        }

        /*
         *Вход в службу. Возвращает токен доступа
         */
        [HttpPut]
        public AccessTokenModel Enter(string login, string password)
        {
            UserModel user;
            try
            {
                user = dataBase.Get(login, password);
            }
            catch (Exception e)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.InternalServerError);
            }

            if (user == null)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            }
            var accessToken = Guid.NewGuid().ToString();
            user.AccessToken = accessToken;
            user.DateTime = DateTime.Now;

            try
            {
                dataBase.SaveChanges(user);
            }
            catch (Exception e)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.InternalServerError);
            }

            return new AccessTokenModel() { AccessToken = accessToken };
        }

        /*
         * Выход из службы
         */
        [HttpPut]
        public void Exit()
        {
            IEnumerable<string> headers;
            if (Request.Headers.TryGetValues("accessToken", out headers))
            {
                var accessToken = headers.First();
                try
                {
                    dataBase.Exit(accessToken);
                }
                catch (Exception e)
                {
                    throw new HttpResponseException(System.Net.HttpStatusCode.InternalServerError);
                }
            }
        }
    }
}