using System;
using WebService.DataBase.Nakl;
using WebService.ViewModel;

namespace WebService.Services.DirectAcceptance
{
    public class DirectAcceptanceNaklService : ServiceBase
    {
        public DirectAcceptanceNaklService(string connectionStringPattern, string login, string password) : base(connectionStringPattern, login, password) { }

        public Result<object> TrustAccept(int id)
        {
            try
            {
                var naklDataBase = new NaklDataBase(ConnectionStringPattern, Login, Password);
                naklDataBase.SetNaklTrust(id, true);
                return new Result<object>();
            }
            catch (Exception e)
            {
                return new Result<object>(e.Message);
            }
        }
    }
}
