using chz.WindowsServices.DataBase;
using System.Collections.Generic;

namespace chz.WindowsServices.LoadDocument.DataBase
{
    public class IncomeDocumentList : DomainObject
    {
        public IEnumerable<IncomeDocument> Documents { get; set; }

        public int Count { get; set; }
    }
}
