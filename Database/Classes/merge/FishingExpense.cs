using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAD3.Database.Classes.merge
{
    public class FishingExpense
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
        public double? CostOfFishing { get; set; }
        public double? ReturnOnInvestment { get; set; }
        public double? IncomeFromFishSold { get; set; }
        public double? FishWeightForConsumption { get; set; }


        public override string ToString()
        {
            return $"{Sampling.ReferenceNumber.ToString()} expense";
        }
    }
}
