using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAD3.Database.Classes.merge
{
    public class SampledGearSpec
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
        public GearSpec GearSpec { get; set; }
        public string Value { get; set; }
        public string RowID { get; set; }

        public override string ToString()
        {
            return $"{_sampling.ReferenceNumber.ToString()} - {GearSpec.ToString()} - {Value}";
        }
    }
}
