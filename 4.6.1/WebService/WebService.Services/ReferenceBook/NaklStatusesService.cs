using System;
using System.Collections.Generic;
using System.Linq;
using WebService.DataBase.NaklStatus;
using WebService.ViewModel;

namespace WebService.Services.ReferenceBook
{
    public class NaklStatusesService : ServiceBase
    {
        public NaklStatusesService(string connectionStringPattern, string login, string password) : base(connectionStringPattern, login, password) { }

        public Result<IEnumerable<NaklStatusViewModel>> Get()
        {
            try
            {
                var naklStatusDataBase = new NaklStatusDataBase(ConnectionStringPattern, Login, Password);
                return new Result<IEnumerable<NaklStatusViewModel>>(naklStatusDataBase.GetAll()
                .Select((x) => new NaklStatusViewModel() { nakl_status_id = x.Id, value = x.Value, class_name = x.ClassName })
                .ToList());
            }
            catch (Exception e)
            {
                return new Result<IEnumerable<NaklStatusViewModel>>(e.Message);
            }
        }
    }
}
