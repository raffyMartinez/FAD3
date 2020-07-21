using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAD3.Database.Classes.merge
{
    public class SamplingQuickView
    {
        public string SamplingID { get; private set; }
        public AOI AOI { get; set; }
        public SamplingReferenceNumber RefNo { get; private set; }
        public DateTime SamplingDate { get; private set; }
        public double? CatchWeight { get; private set; }
        public Gear Gear { get; private set; }
        public LandingSite LandingSite { get; private set; }
        public FishingGround FishingGround {get;private set;}
        public SamplingQuickView(string samplingID, AOI aoi, SamplingReferenceNumber refNo, DateTime samplingDate, 
            double? catchWeight, Gear gear, LandingSite landingSite, FishingGround fishingGround)
        {
            SamplingID = samplingID;
            AOI = aoi;
            RefNo = refNo;
            SamplingDate = samplingDate;
            CatchWeight = catchWeight;
            Gear = gear;
            LandingSite = landingSite;
            FishingGround = fishingGround;
        }
        public override string ToString()
        {
            return RefNo.ToString();
        }
    }
}
