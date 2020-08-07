using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAD3.Database.Classes.merge
{
   public class CatchDetail
    {
        private double? _wtCatch;
        private double? _wtSample;
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

        public double? ComputedWeight
        {
            get
            {
                _wtCatch = CatchComposition.Sampling.WeightOfCatch;
                _wtSample = CatchComposition.Sampling.WeightOfSample;
                double? computedWt = Weight;
                if(!FromTotal)
                {
                    if (_wtSample != null && _wtSample >0 )
                    {
                        computedWt = Weight * (double)(_wtCatch / _wtSample);
                    }
                    else
                    {
                        computedWt = null;
                    }
                }
                return computedWt;
            }
        }

        public int? ComputedCount
        {
            
            get
            {
                int? computedCt=null;
                {
                    if(FromTotal)
                    {
                        if (Count != null)
                        {
                            computedCt = (int)Count;
                        }
                        else
                        {
                            if (SampleWeight != null && SampleWeight>0)
                            {
                                computedCt = (int)((Weight / SampleWeight) * SampleCount);
                            }
                            else
                            {
                                computedCt = null;
                            }
                        }
                    }
                    else if(Count==null)
                    {
                        computedCt = SampleWeight > 0 ? (int?)((Weight / SampleWeight) * SampleCount) : null;
                    }
                    else
                    {
                        if (_wtSample != null && _wtSample > 0)
                        {
                            computedCt = (int)(Count * (_wtCatch / _wtSample));
                        }
                    }

                }
                return computedCt;
            }
        }
        public override string ToString()
        {
            return $"Catch detail {CatchComposition.Sampling.ReferenceNumber.ReferenceNumber} {CatchComposition.CatchName.ToString()}";
        }
    }
}
