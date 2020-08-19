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
            ReferenceNumber = refNo;
            Parse();
            //Generate(refNo);        
        }
        public SamplingReferenceNumber(Sampling s)
        {
            Sampling = s;
            //Generate();


        }

        private void Parse()
        {
            try
            {
                var result = ReferenceNumber.Split('-');
                if (result.Count() == 3)
                {
                    YearCode = int.Parse(result[0].Substring(result[0].Length - 2, 2));
                    AOICode = result[0].Substring(0, result[0].Length - 2);
                    SerialNumber = int.Parse(result[2]);
                    GearCode = result[1];
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
        }
        public string GearCode { get; private set; }
        public int YearCode { get; set; }
        public string AOICode { get; private set; }
        public Sampling Sampling { get; set; }

        public void Generate(string refNo = "")
        {
            //if(Sampling.Fad4Database.DatabaseIsFad4)
            //{
            //    ReferenceNumber = $"{Sampling.LandingSite.AOI.Code}{((DateTime)(Sampling.DateTimeSampled)).ToString("YY")}-{Sampling.Gear.Code}";
            //}
            //else
            //{
            //ReferenceNumber = refNo;
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
