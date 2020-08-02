using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAD3.Database.Classes.merge
{
    public class RefGearCodeUsage
    {
        private RefGearCode _refGearCode;
        private AOI _aoi;

        public string GearCode { get; set; }
        public string AOIId { get; set; }
        public RefGearCode RefGearCode
        {
            set { _refGearCode = value; }
            get 
            {
                if(_refGearCode==null)
                {
                    _refGearCode = FADEntities.RefGearCodeViewModel.GetRefGearCode(GearCode);
                }
                return _refGearCode;
            }
        }

        public string RowNumber {get;set;}

        public AOI AOI
        {
            set { _aoi = value; }
            get
            {
                if(_aoi==null)
                {
                    _aoi = FADEntities.AOIViewModel.GetAOI(AOIId);
                }
                return _aoi;
            }
        }

        public override string ToString()
        {
            return $"{RefGearCode.GearCode} - {AOI.AOIName}";
        }
        public FADEntities FADEntities { get; set; }
    }
}
