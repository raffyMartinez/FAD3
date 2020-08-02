using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAD3.Database.Classes.merge
{
   public class AdditionalExtent
    {
        private AOI _aoi;
        public string AOIGuid { get; set; }

        public FADEntities FADEntities { get; set; }
        public AOI AOI
        {
            get
            {
                if (_aoi == null)
                {
                    _aoi = FADEntities.AOIViewModel.GetAOI(AOIGuid);
                }
                return _aoi;
            }
            set { _aoi = value; }
        }

        public string UpperLeft { get; set; }
        public string LowerRight { get; set; }
        public string Description { get; set; }
        public string RowID { get; set; }

        public override string ToString()
        {
            return $"{AOI.AOIName}: {UpperLeft}-{LowerRight}";
        }
    }
}
