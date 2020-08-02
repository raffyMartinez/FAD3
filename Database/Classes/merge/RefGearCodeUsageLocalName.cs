using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAD3.Database.Classes.merge
{
   public class RefGearCodeUsageLocalName
    {

        private RefGearCodeUsage _refGearCodeUsage;
        private GearLocalName _gearLocalName;
        public FADEntities FADEntities { get; set; }

        public string GearLocalNameID { get; set; }
        public GearLocalName GearLocalName
        {
            set { _gearLocalName = value; }
            get
            {
                if(_gearLocalName==null)
                {
                    _gearLocalName = FADEntities.GearLocalNameViewModel.GetGearLocalName(GearLocalNameID);
                }
                return _gearLocalName;
            }
        }

        public string RefGearCodeUsageID { get; set; }
        public RefGearCodeUsage RefGearCodeUsage
        {
            set { _refGearCodeUsage = value; }
            get
            {
                if(_refGearCodeUsage==null)
                {
                    _refGearCodeUsage = FADEntities.RefGearCodeUsageViewModel.GetRefGearCodeUsage(RefGearCodeUsageID);
                }
                return _refGearCodeUsage;
            }
        }
        public string RowID { get; set; }
    }
}
