using System.Collections.Generic;
using WebService.Models;

namespace WebService.DataBase.Address
{
    public interface IAddressDataBase
    {
        IEnumerable<AddressModel> Get(string accessToken);
    }
}