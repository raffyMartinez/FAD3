using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAD3.Database.Classes.merge
{
    public class Taxa
    {
        public int TaxaID { get; set; }
        public string TaxaName { get; set; }
        public override string ToString()
        {
            return TaxaName;
        }
    }
}
