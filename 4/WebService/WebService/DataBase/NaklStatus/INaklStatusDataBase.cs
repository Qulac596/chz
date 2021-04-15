using System.Collections.Generic;
using WebService.Models;

namespace WebService.DataBase.NaklStatus
{
    public interface INaklStatusDataBase
    {
        IEnumerable<NaklStatusModel> GetNaklStatusModels();
    }
}