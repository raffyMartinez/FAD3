using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAD3.Database.Classes.gearinventory
{
    public class Gear
    {
        public string Name { get; set; }
        public string VariationGuid { get; set; }
        public string ClassName { get; set; }

        public override string ToString()
        {
            return $"{Name}, {ClassName}";
        }
    }
}
