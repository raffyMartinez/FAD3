using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAD3.Database.Classes.merge
{
    public class RefGearCode
    {
        private Gear _gear;
        public FADEntities FADEntities { get; set; }
        public string GearCode { get; set; }
        public Gear Gear
        {
            set { _gear = value; }
            get
            {
                if(_gear==null)
                {
                    _gear = FADEntities.GearViewModel.GetGear(GearID);
                }
                return _gear;
            }
        }
        public string GearID { get; set; }
        public bool IsSubVariation { get; set; }
        public override string ToString()
        {
            return $"{GearCode}: {Gear.ToString()}";
        }
    }
}
