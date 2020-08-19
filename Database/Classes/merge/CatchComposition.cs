using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAD3.Database.Classes.merge
{

   public class CatchComposition
    {
        private Sampling _sampling;
        public FADEntities FADEntities { get; set; }

        public Sampling Sampling
        {
            get
            {
                if (_sampling == null)
                {
                    _sampling = FADEntities.SamplingViewModel.GetSampling(SamplingID);
                }
                return _sampling;
            }

            set
            {
                _sampling = value;
            }
        }
        public string SamplingID { get; set; }
        public CatchName CatchName { get; set; }

        public string CatchNameString
        {
            get
            {
                return CatchName.ToString();
            }
        }
        public string NameGUID { get; set; }
        
        public int? Sequence { get; set; }

        public Identification NameType { get; set; }


        public string RowGUID { get; set; }
        public override string ToString()
        {

            return $"{Sampling.ReferenceNumber}- {CatchName.ToString()}";
        }
    }
}
