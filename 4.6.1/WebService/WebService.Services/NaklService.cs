using System;
using System.Collections.Generic;
using System.Linq;
using WebService.DataBase.Address;
using WebService.DataBase.Nakl;
using WebService.ViewModel;

namespace WebService.Services
{
    public class NaklService : ServiceBase
    {
        public NaklService(string connectionStringPattern, string login, string password) : base(connectionStringPattern, login, password) { }

        public Result<IEnumerable<NaklGridViewModel>> Filtr(int? company_id = null, int? year = null, int? month = null, int? status_id = null)
        {
            try
            {
                var naklDataBase = new NaklDataBase(ConnectionStringPattern, Login, Password);
                return new Result<IEnumerable<NaklGridViewModel>>(naklDataBase.Get(company_id, year, month, status_id)
                    .Select((x) => new NaklGridViewModel()
                    {
                        nakl_id = x.Id,
                        doc_num = x.DocNum,
                        doc_date = x.DateTime,
                        contract_type = x.ContractType,
                        acceptance_type = x.AcceptanceType,
                        provider = x.Povider,
                        status = x.Status,
                        status_style = x.StatusStule,
                        sum = x.Sum
                    }).ToList());
            }
            catch (Exception e)
            {
                return new Result<IEnumerable<NaklGridViewModel>>(e.Message);
            }
        }

        public Result<NaklViewModel> GetNakl(int id)
        {
            try
            {
                var naklDataBase = new NaklDataBase(ConnectionStringPattern, Login, Password);
                var nakl = naklDataBase.GetById(id);
                if (nakl != null)
                {
                    return new Result<NaklViewModel>(new NaklViewModel()
                    {
                        nakl_id = nakl.nakl_id,
                        doc_num = nakl.doc_num,
                        doc_date = nakl.date,
                        nakl_status_id = nakl.nakl_status_id,
                        provider_inn = nakl.provider_inn,
                        provider_name = nakl.provider_name,
                        provider_address = nakl.provider_address,
                        receiver_inn = nakl.receiver_inn,
                        receiver_name = nakl.receiver_name,
                        receiver_address = nakl.receiver_address,
                        acceptance_type = nakl.acceptance_type,
                        form_caption = "Поясняющий текст",
                        provider_tooltip = nakl.provider_name + " " + nakl.provider_inn + " " + nakl.provider_address,
                        receiver_tooltip = nakl.receiver_name + " " + nakl.receiver_inn + " " + nakl.receiver_address
                    });
                }
                return null;
            }
            catch (Exception e)
            {
                return new Result<NaklViewModel>(e.Message);
            }
        }
        public Result<object> SignAndSend(int id)
        {
            try
            {
                var naklDataBase = new NaklDataBase(ConnectionStringPattern, Login, Password);
                naklDataBase.SetNaklStatus(id, 3);
                return new Result<object>();
            }
            catch (Exception e)
            {
                return new Result<object>(e.Message);
            }
        }

        public Result<NaklFullViewModel> Full(int id)
        {
            try
            {
                var naklDataBase = new NaklDataBase(ConnectionStringPattern, Login, Password);
                var nakl = naklDataBase.GetFullNaklById(id);
                if (nakl != null)
                {
                    return new Result<NaklFullViewModel>(new NaklFullViewModel()
                    {
                        nakl_id = nakl.nakl_id,
                        company_id= nakl.company_id,
                        contract_num = nakl.contract_num,
                        doc_num = nakl.doc_num,
                        doc_date = nakl.doc_date,
                        operation_date = nakl.operation_date,
                        contract_type_id = nakl.contract_type_id,
                        provider_id = nakl.provider_id,
                        receive_type_id = nakl.receive_type_id,
                        source_type_id = nakl.source_type_id,
                        turnover_type_id = nakl.turnover_type_id
                    });
                }
                else
                {
                    return new Result<NaklFullViewModel>("Накладная с таким id не найдена.");
                }
            }
            catch (Exception e)
            {
                return new Result<NaklFullViewModel>(e.Message);
            }
        }

        public Result<object> Update(NaklFullViewModel naklFullViewModel, int userId)
        {
            try
            {
                var addressDataBase = new AddressDataBase(ConnectionStringPattern, Login, Password);
                var naklDataBase = new NaklDataBase(ConnectionStringPattern, Login, Password);
                var addressId = addressDataBase.GetCompanyAddresses(userId).First().Id;

                var nakl = new NaklFull()
                {
                    nakl_id = naklFullViewModel.nakl_id,
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

                naklDataBase.Update(nakl);

                return new Result<object>();
            }
            catch (Exception e)
            {
                return new Result<object>(e.Message);
            }
        }
    }
}
