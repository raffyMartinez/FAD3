using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAD3.Database.Classes.merge
{
   public class CatchDetail
    {
        private CatchComposition _catchComposition;
        public FADEntities FADEntities { get; set; }

        public CatchComposition CatchComposition
        {
            get
            {
                if (_catchComposition == null)
                {
                    _catchComposition = FADEntities.CatchCompositionViewModel.GetCatchComposition(CatchCompositionID);
                }
                return _catchComposition;
            }

            set
            {
                _catchComposition = value;
            }
        }
        public string CatchCompositionID { get; set; }
        public double Weight { get; set; }
        public int? Count { get; set; }
        public double? SampleWeight { get; set; }
        public int? SampleCount { get; set; }
        public string RowGUID { get; set; }
        public bool FromTotal { get; set; }
        public bool LiveFish { get; set; }

        public override string ToString()
        {
            return $"Catch detail {CatchComposition.Sampling.ReferenceNumber.ReferenceNumber} {CatchComposition.CatchName.ToString()}";
        }
    }
}
