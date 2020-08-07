using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAD3.Database.Classes.merge
{
    public enum VesselType
    {
        NotDetermined,
        Motorized,
        NonMotorized,
        NoVesselUsed
    }
   public class FishingVessel
    {
        public FADEntities FADEntities { get; set; }

        private Sampling _sampling;
        public string SamplingGUID { get; set; }
        public double? Length { get; set; }
        public double? Breadth { get; set; }
        public double? Depth { get; set; }
        public VesselType VesselType { get; set; }
        public double? EngineHorsePower { get; set; }

        public string Construction
        {
            get
            {
                
                string rv = "Not determined";
                switch(VesselType)
                {
                    case VesselType.Motorized:
                    if(EngineHorsePower!=null)
                    {
                        rv= $"Motorized {EngineHorsePower}hp";
                    }
                    else
                    {
                        rv= "Motorized";
                    }
                        break;
                    case VesselType.NonMotorized:
                        rv= "Non-motorized";
                        break;
                    case VesselType.NoVesselUsed:
                        rv= "No vessel";
                        break;
                }
                return rv;
            }
        }
        public string Dimension
        {
            get
            {
                if (Breadth == null || Depth == null || Length == null)
                {
                    return "";
                }
                else
                {
                    return $"(BxDxL) {Breadth} x {Depth} x {Length}";
                }
            }
        }
        public override string ToString()
        {
            if (Breadth == null || Depth == null || Length == null)
            {
                return $"{VesselType.ToString()}";
            }
            else
            {
                return $"{VesselType.ToString()} - (BxDxL) {Breadth} x {Depth} x {Length}";
            }
        }
        public Sampling Sampling
        {
            get
            {
                if(_sampling==null)
                {
                    _sampling = FADEntities.SamplingViewModel.GetSampling(SamplingGUID);
                }
                return _sampling;
            }
            set { _sampling = value; }
        }

        public string Engine { get; set; }
    }
}
