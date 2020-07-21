using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace FAD3.Database.Classes.merge
{

    public class SamplingForEdit
    {
        public Sampling Sampling { get; private set; }

        public bool IsNew { get; private set; }

        public AOI AOI { get; set; }

        public string RowID { get; set; }

        public GearClass GearClass { get; set; }


        public Gear Gear { get; set; }


        public SamplingReferenceNumber ReferenceNumber { get; set; }


        public DateTime DateTimeSampled { get; set; }


        public DateTime? DateTimeGearSet { get; set; }


        public DateTime? DateTimeGearHauled { get; set; }


        public SamplingEnumerator SamplingEnumerator { get; set; }


        public double? WeightOfCatch { get; set; }

        public double? WeightOfSample { get; set; }


        public int? NumberOfFishers { get; set; }


        public int? NumberOfHauls { get; set; }


        public bool HasLiveFish { get; set; }

        public LandingSite LandingSite { get; set; }

        public FishingGround FishingGround { get; set; }

        public SamplingForEdit(Sampling sampling)
        {
            Sampling = new Sampling();
            AOI = sampling.AOI;
            Gear = sampling.Gear;
            GearClass = sampling.Gear.GearClass;
            ReferenceNumber = sampling.ReferenceNumber;
            DateTimeSampled = sampling.DateTimeSampled;
            DateTimeGearSet = sampling.DateTimeGearSet;
            DateTimeGearHauled = sampling.DateTimeGearHaul;
            SamplingEnumerator = sampling.SamplingEnumerator;
            WeightOfCatch = sampling.WeightOfCatch;
            WeightOfSample = sampling.WeightOfSample;
            NumberOfFishers = sampling.NumberOfFishers;
            NumberOfFishers = sampling.NumberOfHauls;
            HasLiveFish = sampling.HasLiveFish;
            RowID = sampling.RowID;
            LandingSite = sampling.LandingSite;
            FishingGround = sampling.FishingGround;
            IsNew = false;

        }

        public SamplingForEdit()
        {
            Sampling = new Sampling();
            IsNew = true;
        }

        public void Save()
        {
            Sampling.AOI = AOI;
        }

    }
}
