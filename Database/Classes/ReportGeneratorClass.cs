using System;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Data;
using System.Data.OleDb;
using System.Linq;
using FAD3.Database.Classes.merge;
namespace FAD3.Database.Classes
{
    public static class ReportGeneratorClass
    {
        public static TargetArea TargetArea { get; set; }
        public static string Topic { get; set; }
        public static List<int> Years { get; set; }
        private static string _years;

        public static void Generate()
        {
            DataSet = new System.Data.DataSet();
            switch (Topic)
            {
                case "len_freq":
                    var queryLF = from lf in MergeDataBases.Destination.LenFreqViewModel.LenFreqCollection
                                  where lf.CatchComposition.Sampling.AOI.AOIGuid == TargetArea.TargetAreaGuid
                                  where Years.Contains(lf.CatchComposition.Sampling.DateTimeSampled.Year)
                                  orderby lf.CatchComposition.Sampling.DateTimeSampled
                                  orderby lf.CatchComposition.Sampling.LandingSite.LandingSiteName
                                  select new
                                  {
                                      targetArea = lf.CatchComposition.Sampling.AOI.AOIName,
                                      refNo = lf.CatchComposition.Sampling.ReferenceNumber.ReferenceNumber,
                                      dateSampled = lf.CatchComposition.Sampling.DateTimeSampled.ToString("MMM-dd-yyyy HH:mm"),
                                      landingSite = lf.CatchComposition.Sampling.LandingSite.ToString(),
                                      gear = lf.CatchComposition.Sampling.Gear.ToString(),
                                      catchName = lf.CatchComposition.CatchName,
                                      identification = lf.CatchComposition.NameType.ToString(),
                                      lenClass = lf.LenClass,
                                      freq = lf.Freq
                                  };
                    System.Data.DataTable tableLF = queryLF.CopyToDataTable();
                    DataSet.Tables.Add(tableLF);
                    break;
                case "effort":
                    var queryEffort = from i in MergeDataBases.Destination.SamplingViewModel.SamplingCollection
                                where i.AOI.AOIGuid == TargetArea.TargetAreaGuid
                                where Years.Contains(i.DateTimeSampled.Year)
                                orderby i.DateTimeSampled
                                orderby i.LandingSite.LandingSiteName
                                select new
                                {
                                    targetArea = i.AOI.AOIName,
                                    refNo = i.ReferenceNumber.ReferenceNumber,
                                    dateSampled = i.DateTimeSampled.ToString("MMM-dd-yyyy HH:mm"),
                                    landingSite = i.LandingSite.ToString(),
                                    fishingGround = $"{i.FishingGround}, {new AdditionalFishingGroundsMerged(i.RowID, MergeDataBases.Destination).MergedFishingGrounds}".Trim(new char[] { ' ', ',' }),
                                    gearClass = i.Gear.GearClass.GearClassName,
                                    gearVariation = i.Gear.ToString(),
                                    enumerator = i.SamplingEnumerator == null ? "" : i.SamplingEnumerator.ToString(),
                                    dateTimeGearSet =  i.DateTimeGearSet==null?"": ((DateTime)i.DateTimeGearSet).ToString("MMM-dd-yyyy HH:mm"),
                                    dateTimeGearHaul = i.DateTimeGearHaul==null?"": ((DateTime)i.DateTimeGearHaul).ToString("MMM-dd-yyyy HH:mm"),
                                    numberFishers = i.NumberOfFishers == null ? null : i.NumberOfFishers,
                                    numberHauls = i.NumberOfHauls==null ? null : i.NumberOfHauls,
                                    catchTotalWt = i.WeightOfCatch==null? null: i.WeightOfCatch,
                                    catchSampleWt = i.WeightOfSample==null?null:i.WeightOfSample,
                                    vesselType = i.FishingVessel.Construction,
                                    vesselDimension = i.FishingVessel.Dimension,
                                    notes = i.Notes,
                                    dateAdded  =   i.DateAdded==null?"": ((DateTime)i.DateAdded).ToString("MMM-dd-yyyy HH:mm")
                                };
                    System.Data.DataTable tableEffort = queryEffort.CopyToDataTable();
                    DataSet.Tables.Add(tableEffort);
                    break;
                    
                case "fishing_expense_items":
                    var queryEpenseItems = from exi in MergeDataBases.Destination.FishingExpenseItemViewModel.FishingExpenseItemCollection
                                           where exi.ParentFishingExpense.Sampling.AOI.AOIGuid == TargetArea.TargetAreaGuid
                                           where Years.Contains(exi.ParentFishingExpense.Sampling.DateTimeSampled.Year)
                                           orderby exi.ParentFishingExpense.Sampling.DateTimeSampled
                                           orderby exi.ParentFishingExpense.Sampling.LandingSite.LandingSiteName
                                           select new
                                           {
                                               dateSampled = exi.ParentFishingExpense.Sampling.DateTimeSampled.ToString("MMM-dd-yyyy HH:mm"),
                                               refno = exi.ParentFishingExpense.Sampling.ReferenceNumber.ReferenceNumber,
                                               landingSite = exi.ParentFishingExpense.Sampling.LandingSite.ToString(),
                                               gear = exi.ParentFishingExpense.Sampling.Gear.ToString(),
                                               expenseItem = exi.ExpenseItem,
                                               cost =  exi.Cost==null?null: exi.Cost,
                                               unit = exi.Unit,
                                               unitQuantity = exi.UnitQuantity==null?null:exi.UnitQuantity
                                           };
                    System.Data.DataTable tableExpenseItems = queryEpenseItems.CopyToDataTable();
                    DataSet.Tables.Add(tableExpenseItems);
                    break;
                case "fishing_expense":
                    var queryExpense = from fe in MergeDataBases.Destination.FishingExpenseViewModel.FishingExpenseCollection
                                       where fe.Sampling.AOI.AOIGuid == TargetArea.TargetAreaGuid
                                       where Years.Contains(fe.Sampling.DateTimeSampled.Year)
                                       orderby fe.Sampling.DateTimeSampled
                                       orderby fe.Sampling.LandingSite.LandingSiteName
                                       select new
                                       {
                                           dateSampled = fe.Sampling.DateTimeSampled.ToString("MMM-dd-yyyy HH:mm"),
                                           refno = fe.Sampling.ReferenceNumber.ReferenceNumber,
                                           landingSite = fe.Sampling.LandingSite.ToString(),
                                           gear = fe.Sampling.Gear.ToString(),
                                           costFishing = fe.CostOfFishing==null?null:fe.CostOfFishing,
                                           returnOfInvestment = fe.ReturnOnInvestment==null?null:fe.ReturnOnInvestment,
                                           incomeFromSale = fe.IncomeFromFishSold==null?null:fe.IncomeFromFishSold,
                                           weightConsumned = fe.FishWeightForConsumption==null?null:fe.FishWeightForConsumption
                                       };
                    System.Data.DataTable tableExpense = queryExpense.CopyToDataTable();
                    DataSet.Tables.Add(tableExpense);
                    break;
                case "gear_specs":
                    var querySpecs = from gs in MergeDataBases.Destination.SampledGearSpecViewModel.SampledGearSpecCollection
                                     where gs.Sampling.AOI.AOIGuid == TargetArea.TargetAreaGuid
                                     where Years.Contains(gs.Sampling.DateTimeSampled.Year)
                                     orderby gs.Sampling.DateTimeSampled
                                     orderby gs.Sampling.LandingSite.LandingSiteName
                                     select new
                                     {
                                         dateSampled = gs.Sampling.DateTimeSampled.ToString("MMM-dd-yyyy HH:mm"),
                                         refno = gs.Sampling.ReferenceNumber.ReferenceNumber,
                                         landingSite = gs.Sampling.LandingSite.ToString(),
                                         gear = gs.Sampling.Gear.ToString(),
                                         gs.GearSpec.Property,
                                         gs.Value
                                     };
                    System.Data.DataTable tablegs = querySpecs.CopyToDataTable();
                    DataSet.Tables.Add(tablegs);
                    break;
                case "catch":
                    List<merge.CatchDetail> catchdtls = MergeDataBases.Destination.CatchDetailViewModel.CatchDetailCollection.ToList();

                    var queryCatch = from cd in catchdtls
                                 where cd.CatchComposition.Sampling.AOI.AOIGuid == TargetArea.TargetAreaGuid
                                 where Years.Contains(cd.CatchComposition.Sampling.DateTimeSampled.Year)
                                 orderby cd.CatchComposition.Sampling.DateTimeSampled
                                 orderby cd.CatchComposition.Sampling.LandingSite.LandingSiteName
                                 select new
                                 {
                                     dateSampled = cd.CatchComposition.Sampling.DateTimeSampled.ToString("MMM-dd-yyyy HH:mm"),
                                     refno = cd.CatchComposition.Sampling.ReferenceNumber.ReferenceNumber,
                                     landingSite = cd.CatchComposition.Sampling.LandingSite.ToString(),
                                     gear = cd.CatchComposition.Sampling.Gear.ToString(),
                                     catchName = cd.CatchComposition.CatchName,
                                     idType = cd.CatchComposition.NameType.ToString(),
                                     catchTotalWt = cd.CatchComposition.Sampling.WeightOfCatch,
                                     catchSampleWt = cd.CatchComposition.Sampling.WeightOfSample,
                                     isLiveFish = cd.LiveFish,
                                     catchWt = cd.Weight,
                                     catchCt = cd.Count,
                                     catchSubSampleWt = cd.SampleWeight,
                                     catchSubSampleCt = cd.SampleCount,
                                     fromTotal = cd.FromTotal,
                                     computedWt=cd.ComputedWeight,
                                     computedCt=cd.ComputedCount
                                 };
                    System.Data.DataTable tableCatch = queryCatch.CopyToDataTable();
                    DataSet.Tables.Add(tableCatch);
                    break;
            }
        }

        public static System.Data.DataSet DataSet { get; internal set; }
    }
}