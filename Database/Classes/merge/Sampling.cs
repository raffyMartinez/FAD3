using System;
using System.ComponentModel;

namespace FAD3.Database.Classes.merge
{
    public class Sampling
    {
        public string RowID { get; set; }

        public string Notes { get; set; }
        public SamplingReferenceNumber ReferenceNumber { get; set; }


        public SamplingEnumerator SamplingEnumerator { get; set; }


        public DateTime DateTimeSampled { get; set; }


        public double? WeightOfSample { get; set; }




        public AOI AOI { get; set; }


        public LandingSite LandingSite { get; set; }


        public DateTime? DateAdded { get; set; }


        public FishingGround FishingGround { get; set; }


        public Gear Gear { get; set; }


        public DateTime? DateTimeGearSet { get; set; }


        public DateTime? DateTimeGearHaul { get; set; }


        public int? NumberOfFishers { get; set; }


        public int? NumberOfHauls { get; set; }


        public double? WeightOfCatch { get; set; }


        public bool HasLiveFish { get; set; }

        public Sampling() { }
       
        public FishingVessel FishingVessel { get; set; }

        public string Name { get { return ReferenceNumber.ToString(); } }
        
        public override string ToString()
        {
            return $"{AOI.AOIName} - {ReferenceNumber.ToString()}";
        }
        
    }
}
