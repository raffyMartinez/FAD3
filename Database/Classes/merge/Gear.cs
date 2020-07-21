using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAD3.Database.Classes.merge
{
    public class Gear
    {
        public string GearName { get; set; }

        public Gear(){}

        public string GearID { get; set; }
        public Gear(string gearName, string code, string gearID)
        {
            GearName = gearName;
            Code = code;
            GearID = gearID;
        }

        public override string ToString()
        {
            return GearName;
        }

        public GearClass GearClass { get; set; }

        public string Code { get; set; }


    }
}
