using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAD3.Database.Classes.merge
{
    public class GearClass
    {
        public string GearClassGuid { get; set; }

        public string GearClassName { get; set; }

        public string GearCode { get; set; }

        public override string ToString()
        {
            return GearClassName;
        }
    }
}
