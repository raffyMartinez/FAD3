using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAD3.Database.Classes.merge
{
    public class SamplingReferenceNumber
    {
        public SamplingReferenceNumber(string refNo, Sampling s)
        {
            Sampling = s;
            Generate(refNo);        
        }
        public SamplingReferenceNumber(Sampling s)
        {
            Sampling = s;
             Generate();
            
        }

        public Sampling Sampling { get; set; }

        public void Generate(string refNo="")
        {
            //if(Sampling.Fad4Database.DatabaseIsFad4)
            //{
            //    ReferenceNumber = $"{Sampling.LandingSite.AOI.Code}{((DateTime)(Sampling.DateTimeSampled)).ToString("YY")}-{Sampling.Gear.Code}";
            //}
            //else
            //{
            //    ReferenceNumber = refNo;
            //}
        }
        public int SerialNumber { get; set; }


        public string ReferenceNumber { get; set; }


        public override string ToString()
        {
            return ReferenceNumber;
        }
    }
}
