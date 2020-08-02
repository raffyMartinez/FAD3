using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAD3.Database.Classes.merge
{
    public class GearLocalName
    {
        public string LocalName { get; set; }
        public string Guid { get; set; }

        public override string ToString()
        {
            return LocalName;
        }
    }
}

