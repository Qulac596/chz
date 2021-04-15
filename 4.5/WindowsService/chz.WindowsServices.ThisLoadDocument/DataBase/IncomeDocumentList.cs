using chz.WindowsServices.DataBase;
using System.Collections.Generic;

namespace chz.WindowsServices.ThisLoadDocument.DataBase
{
    public class IncomeDocumentList : DomainObject
    {
        public IEnumerable<IncomeDocument> Documents { get; set; }

        public int Count { get; set; }
    }
}
