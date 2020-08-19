using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace FAD3.Database.Classes.merge
{
    public class CatchDetailFlattened
    {
        private double? _wtCatch;
        private double? _wtSample;
        private static double? _wtFromTotal;
        private string _samplingGUID;
        private double? _catchWeightLessFromTotal;
        public CatchDetailFlattened(string samplingGUID) 
        {
            if (_samplingGUID != samplingGUID)
            {
                _samplingGUID = samplingGUID;
                Console.WriteLine($"Weight from total is {_wtFromTotal}");
            }
        }
        public CatchDetailFlattened(CatchDetail cd)
        {
            DateSampled = cd.CatchComposition.Sampling.DateTimeSampled;
            RefNo = cd.CatchComposition.Sampling.ReferenceNumber.ToString();
            LandingSite = cd.CatchComposition.Sampling.LandingSite.ToString();
            Gear = cd.CatchComposition.Sampling.Gear.ToString();
            CatchName = cd.CatchComposition.CatchNameString;
            IDType = cd.CatchComposition.NameType.ToString();
            TotalCatchWeight = cd.CatchComposition.Sampling.WeightOfCatch;
            TotalCatchSampleWeight = cd.CatchComposition.Sampling.WeightOfSample;
            CatchWeight = cd.Weight;
            CatchSampleWeight = cd.SampleWeight;
            Count = cd.Count;
            SampleCount = cd.SampleCount;
            FromTotal = cd.FromTotal;
        }
        public DateTime DateSampled { get; set; }
        public string RefNo { get; set; }
        public string LandingSite { get; set; }
        public string Gear { get; set; }

        public string CatchName { get; set; }

        public string IDType { get; set; }

        public double? TotalCatchWeight { get; set; }
        public double? TotalCatchSampleWeight { get; set; }


        public bool IsLiveFIsh { get; set; }
        public double CatchWeight { get; set; }

        public double? CatchSampleWeight { get; set; }


        public int? Count { get; set; }
        public int? SampleCount { get; set; }
        public bool FromTotal { get; set; }
        public double? ComputedWt
        {
            get
            {

                _wtCatch = TotalCatchWeight;
                _wtSample = TotalCatchSampleWeight;
                _wtFromTotal = MergeDataBases.Destination.CatchDetailViewModel.WeightFromTotal(_samplingGUID);
                _catchWeightLessFromTotal = (double)_wtCatch - _wtFromTotal;
                if (FromTotal)
                {
                    return (double)CatchWeight;

                }
                else
                {
                    if(_wtSample==null)
                    {
                        return (double)CatchWeight;
                    }
                    else
                    {
                        return (CatchWeight / (double)_wtSample) * _catchWeightLessFromTotal;
                    }
                }
            }
        }
        public int? ComputedCount
        {
            get
            {

                _wtCatch = TotalCatchWeight;
                _wtSample = TotalCatchSampleWeight;                
                _wtFromTotal = MergeDataBases.Destination.CatchDetailViewModel.WeightFromTotal(_samplingGUID);
                _catchWeightLessFromTotal = (double)_wtCatch - _wtFromTotal;
                if(FromTotal || _wtSample==null)
                {
                    if(_wtSample==null)
                    {
                        if(Count==null)
                        {
                            return (int)((CatchWeight / CatchSampleWeight) * SampleCount);
                        }
                        else
                        {
                            return (int)Count;
                        }
                    }
                    else
                    {
                        if (Count != null)
                        {
                            return  (int)Count;
                        }
                        else
                        {
                            if (SampleCount != null && CatchSampleWeight != null)
                            {
                                return  (int)((CatchWeight / CatchSampleWeight) * SampleCount);
                            }
                        }
                    }
                }
                else
                {
                    if (Count == null)
                    {
                        return (int)(_catchWeightLessFromTotal / (double)_wtSample * (CatchWeight / CatchSampleWeight * SampleCount));
                    }
                    else
                    {
                        return  (int)(_catchWeightLessFromTotal / (double)_wtSample * Count);
                    }
                }
                return null;
            }
        }
        
    }
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

        public override string ToString()
        {
            return $"Catch detail {CatchComposition.Sampling.ReferenceNumber.ReferenceNumber} {CatchComposition.CatchName.ToString()}";
        }
    }
}
