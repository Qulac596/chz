using System;
using System.Linq;
using WebService.DataBase.Address;
using WebService.DataBase.Nakl;
using WebService.ViewModel;

namespace WebService.Services.ReverseAcceptance
{
    public class ReverseAcceptanceNaklService : ServiceBase
    {
        public ReverseAcceptanceNaklService(string connectionStringPattern, string login, string password) : base(connectionStringPattern, login, password)
        {
        }

        public Result<int> Add(NaklFullViewModel naklFullViewModel, int userId)
        {
            try
            {
                var addressDataBase = new AddressDataBase(ConnectionStringPattern, Login, Password);
                var naklDataBase = new NaklDataBase(ConnectionStringPattern, Login, Password);
                var addressId = addressDataBase.GetCompanyAddresses(userId).First().Id;

                var nakl = new NaklFull()
                {
                    contract_num = naklFullViewModel.contract_num,
                    contract_type_id = naklFullViewModel.contract_type_id,
                    doc_date = naklFullViewModel.doc_date,
                    operation_date = naklFullViewModel.operation_date,
                    provider_id = naklFullViewModel.provider_id,
                    receiver_id = addressId,
                    doc_num = naklFullViewModel.doc_num,
                    receive_type_id = naklFullViewModel.receive_type_id,
                    source_type_id = naklFullViewModel.source_type_id,
                    turnover_type_id = naklFullViewModel.turnover_type_id
                };
                return new Result<int>(naklDataBase.Add(nakl));
            }
            catch (Exception e)
            {
                return new Result<int>(e.Message);
            }
        }

        public Result<object> Delete(int id)
        {
            try
            {
                var naklDataBase = new NaklDataBase(ConnectionStringPattern, Login, Password);
                naklDataBase.Delete(id);
                return new Result<object>();
            }
            catch (Exception e)
            {
                return new Result<object>(e.Message);
            }
        }
    }
}
