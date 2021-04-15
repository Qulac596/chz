using System.Collections.Generic;

namespace WebService.DataBase.Sgtin
{
    public class NaklItem
    {
        public string Name { get; set; }

        public string Gtin { get; set; }

        public IEnumerable<string> Sgtins { get; set; }
    }
}
