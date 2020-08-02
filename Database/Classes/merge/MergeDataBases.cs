﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace FAD3.Database.Classes.merge
{
    public static class MergeDataBases
    {
        public static AOI SourceAOI { get; set; }
        public static AOI DestinationAOI { get; set; }

        private static string _sourceDatabaseFileName;
        private static string _desitantionDatabaseFileName;
        public static string OtherDatabaseFileName
        {
            get
            {
                return _sourceDatabaseFileName;
            }
            set
            {
                if (DBCheck.CheckDB(value))
                {
                    _sourceDatabaseFileName = value;
                    SetSourceAndDestination();
                }
                else
                {
                    MergeResultMessage = "Selected database could not be merged";
                }
            }
        }

        public static void SetSourceAndDestination()
        {
            _desitantionDatabaseFileName = global.MDBPath;
            Destination = new FADEntities(_desitantionDatabaseFileName);
            Source = new FADEntities(_sourceDatabaseFileName);
        }
        public static FADEntities Destination { get; private set; }
        public static FADEntities Source { get; private set; }

        public static async Task Setup(MergeDBHelper mergedbHelper)
        {
            await Task.Run(() => SetUpEntities(mergedbHelper));
        }

        private static void SetUpEntities(MergeDBHelper mergeDBHelper)
        {
            mergeDBHelper.TableLocation = "Destination";
            Destination.AOIViewModel = new AOIViewModel(Destination);
            mergeDBHelper.MergingTable("Target areas");
            Destination.AdditionalExtentViewModel = new AdditionalExtentViewModel(Destination);
            mergeDBHelper.MergingTable("Addtional extents");
            Destination.ProvinceViewModel = new ProvinceViewModel(Destination);
            mergeDBHelper.MergingTable("Provinces");
            Destination.MunicipalityViewModel = new MunicipalityViewModel(Destination);
            mergeDBHelper.MergingTable("Municipalities");
            Destination.LandingSiteViewModel = new LandingSiteViewModel(Destination);
            mergeDBHelper.MergingTable("Landing sites");
            Destination.CatchLocalNameViewModel = new CatchLocalNameViewModel(Destination);
            mergeDBHelper.MergingTable("Local name of catch");
            Destination.GearLocalNameViewModel = new GearLocalNameViewModel(Destination);
            mergeDBHelper.MergingTable("Local name of gears");
            Destination.GearClassViewModel = new GearClassViewModel(Destination);
            mergeDBHelper.MergingTable("Gear classes");
            Destination.GearViewModel = new GearViewModel(Destination);
            mergeDBHelper.MergingTable("Fishing gears");
            Destination.GearSpecViewModel = new GearSpecViewModel(Destination);
            mergeDBHelper.MergingTable("Gear specs");
            Destination.RefGearCodeViewModel = new RefGearCodeViewModel(Destination);
            mergeDBHelper.MergingTable("Reference gear codes");
            Destination.RefGearCodeUsageViewModel = new RefGearCodeUsageViewModel(Destination);
            mergeDBHelper.MergingTable("Reference gear code usage");
            Destination.RefGearCodeUsageLocalNameViewModel = new RefGearCodeUsageLocalNameViewModel(Destination);
            mergeDBHelper.MergingTable("Reference gear code usage local names");
            Destination.SamplingEnumeratorViewModel = new SamplingEnumeratorViewModel(Destination);
            mergeDBHelper.MergingTable("Enumerators");
            Destination.TaxaViewModel = new TaxaViewModel(Destination);
            mergeDBHelper.MergingTable("Taxa");
            Destination.SpeciesViewModel = new SpeciesViewModel(Destination);
            mergeDBHelper.MergingTable("Species");
            Destination.CatchNameViewModel = new CatchNameViewModel(Destination);
            mergeDBHelper.MergingTable("Catch names");
            Destination.SamplingViewModel = new SamplingViewModel(Destination);
            mergeDBHelper.MergingTable("Sampling");
            Destination.FishingExpenseViewModel = new FishingExpenseViewModel(Destination);
            mergeDBHelper.MergingTable("Fishing expense");
            Destination.FishingExpenseItemViewModel = new FishingExpenseItemViewModel(Destination);
            mergeDBHelper.MergingTable("Fishing expense items");
            Destination.AdditionalFishingGroundViewModel = new AdditionalFishingGroundViewModel(Destination);
            mergeDBHelper.MergingTable("Additional fishing grounds");
            Destination.CatchCompositionViewModel = new CatchCompositionViewModel(Destination);
            mergeDBHelper.MergingTable("Catch composition");
            Destination.CatchDetailViewModel = new CatchDetailViewModel(Destination);
            mergeDBHelper.MergingTable("Catch detail");
            Destination.LenFreqViewModel = new LenFreqViewModel(Destination);
            mergeDBHelper.MergingTable("Length frequency");
            Destination.GonadMaturityStageViewModel = new GonadMaturityStageViewModel(Destination);
            mergeDBHelper.MergingTable("Gonad maturity");
            Destination.SampledGearSpecViewModel = new SampledGearSpecViewModel(Destination);
            mergeDBHelper.MergingTable("Gear spec of sampled gears");

            mergeDBHelper.TableLocation = "Source";
            Source.AOIViewModel = new AOIViewModel(Source);
            mergeDBHelper.MergingTable("Target areas");
            Source.AdditionalExtentViewModel = new AdditionalExtentViewModel(Source);
            mergeDBHelper.MergingTable("Addtional extents");
            Source.ProvinceViewModel = new ProvinceViewModel(Source);
            mergeDBHelper.MergingTable("Provinces");
            Source.MunicipalityViewModel = new MunicipalityViewModel(Source);
            mergeDBHelper.MergingTable("Municipalities");
            Source.LandingSiteViewModel = new LandingSiteViewModel(Source);
            mergeDBHelper.MergingTable("Landing sites");
            Source.CatchLocalNameViewModel = new CatchLocalNameViewModel(Source);
            mergeDBHelper.MergingTable("Local name of catch");
            Source.GearLocalNameViewModel = new GearLocalNameViewModel(Source);
            mergeDBHelper.MergingTable("Local name of gears");
            Source.GearClassViewModel = new GearClassViewModel(Source);
            mergeDBHelper.MergingTable("Gear classes");
            Source.GearViewModel = new GearViewModel(Source);
            mergeDBHelper.MergingTable("Fishing gears");
            Source.GearSpecViewModel = new GearSpecViewModel(Source);
            mergeDBHelper.MergingTable("Gear specs");
            Source.RefGearCodeViewModel = new RefGearCodeViewModel(Source);
            mergeDBHelper.MergingTable("Reference gear codes");
            Source.RefGearCodeUsageViewModel = new RefGearCodeUsageViewModel(Source);
            mergeDBHelper.MergingTable("Reference gear code usage");
            Source.RefGearCodeUsageLocalNameViewModel = new RefGearCodeUsageLocalNameViewModel(Source);
            mergeDBHelper.MergingTable("Reference gear code usage local names");
            Source.SamplingEnumeratorViewModel = new SamplingEnumeratorViewModel(Source);
            mergeDBHelper.MergingTable("Enumerators");
            Source.TaxaViewModel = new TaxaViewModel(Source);
            mergeDBHelper.MergingTable("Taxa");
            Source.SpeciesViewModel = new SpeciesViewModel(Source);
            mergeDBHelper.MergingTable("Species");
            Source.CatchNameViewModel = new CatchNameViewModel(Source);
            mergeDBHelper.MergingTable("Catch names");
            Source.SamplingViewModel = new SamplingViewModel(Source);
            mergeDBHelper.MergingTable("Sampling");
            Source.FishingExpenseViewModel = new FishingExpenseViewModel(Source);
            mergeDBHelper.MergingTable("Fishing expense");
            Source.FishingExpenseItemViewModel = new FishingExpenseItemViewModel(Source);
            mergeDBHelper.MergingTable("Fishing expense items");
            Source.AdditionalFishingGroundViewModel = new AdditionalFishingGroundViewModel(Source);
            mergeDBHelper.MergingTable("Additional fishing grounds");
            Source.CatchCompositionViewModel = new CatchCompositionViewModel(Source);
            mergeDBHelper.MergingTable("Catch composition");
            Source.CatchDetailViewModel = new CatchDetailViewModel(Source);
            mergeDBHelper.MergingTable("Catch detail");
            Source.LenFreqViewModel = new LenFreqViewModel(Source);
            mergeDBHelper.MergingTable("Length frequency");
            Source.GonadMaturityStageViewModel = new GonadMaturityStageViewModel(Source);
            mergeDBHelper.MergingTable("Gonad maturity");
            Source.SampledGearSpecViewModel = new SampledGearSpecViewModel(Source);
            mergeDBHelper.MergingTable("Gear spec of sampled gears");
            mergeDBHelper.IsDone();

        }
        public static string MergeResultMessage { get; private set; }

        public static async Task<bool> Merge(MergeDBHelper mergedbHelper, bool log)
        {
            return await Task.Run(() => MergeTables(log, mergedbHelper));
        }
        private static bool MergeTables(bool log, MergeDBHelper mergedbHelper, bool trial = false)
        {
            Logger.LogMerge(Source, Destination);


            Logger.LogMerge("merging AOIs");
            if (Destination.AOIViewModel.GetEqual(SourceAOI) == null)
            {
                if (Destination.AOIViewModel.AddRecordToRepo(SourceAOI) && log)
                {

                    Logger.LogMerge($"merged {SourceAOI.ToString()}");
                }
            }
            mergedbHelper.MergingTable("Target areas");

            Logger.LogMerge("merging additional extents");
            foreach (var item in Source.AdditionalExtentViewModel.AdditionalExtentCollection.Where(t=>t.AOI.AOIGuid==SourceAOI.AOIGuid))
            {
                if (Destination.AdditionalExtentViewModel.GetAdditionalExtent(item.RowID) == null)
                {
                    if (Destination.AdditionalExtentViewModel.AddRecordToRepo(item) && log)
                    {
                        Logger.LogMerge($"merged {item.ToString()}");
                    }
                }
            }
            mergedbHelper.MergingTable("Additional AOI extents");


            Logger.LogMerge("merging landing sites");
            foreach (LandingSite landingSiteToMerge in Source.LandingSiteViewModel.LandingSiteCollection.Where(t => t.AOI.AOIGuid == SourceAOI.AOIGuid))
            {
                if (Destination.LandingSiteViewModel.GetEqual(landingSiteToMerge) == null)
                {
                    if (Destination.LandingSiteViewModel.AddRecordToRepo(landingSiteToMerge) && log)
                    {
                        Logger.LogMerge($"merged {landingSiteToMerge.ToString()}");
                    }
                }
            }
            mergedbHelper.MergingTable("Landing sites");

            Logger.LogMerge("merging gear local names");
            foreach (GearLocalName gln in Source.GearLocalNameViewModel.GearLocalNameCollection)
            {
                if (Destination.GearLocalNameViewModel.GetGearLocalName(gln.Guid) == null)
                {
                    if (Destination.GearLocalNameViewModel.AddRecordToRepo(gln) && log)
                    {
                        Logger.LogMerge($"merged {gln.ToString()}");
                    }
                }
            }
            mergedbHelper.MergingTable("Gear local names");

            Logger.LogMerge("merging gears");
            foreach (Gear gear in Destination.GearViewModel.GearCollection)
            {
                var sourceGear = Source.GearViewModel.GetGearFromName(gear.GearName);
                if (sourceGear != null && sourceGear.GearID != gear.GearID && Source.GearViewModel.GetGear(gear.GearID) != null && Source.GearViewModel.ModifyGearName(gear))
                {
                    Logger.LogMerge($"modified name of {gear.GearName} from source");
                }
            }

            foreach (Gear gear in Source.GearViewModel.GearCollection)
            {
                if (Destination.GearViewModel.GetGear(gear.GearID) == null)
                {
                    if (Destination.GearViewModel.AddRecordToRepo(gear) && log)
                    {
                        Logger.LogMerge($"merged {gear.ToString()}");
                    }
                }
            }
            mergedbHelper.MergingTable("Fishing gears");

            Logger.LogMerge("merging gear specs");
            foreach (GearSpec gs in Source.GearSpecViewModel.GearSpecCollection)
            {
                if (Destination.GearSpecViewModel.getGearSpec(gs.RowGUID) == null)
                {
                    if (Destination.GearSpecViewModel.AddRecordToRepo(gs) && log)
                    {
                        Logger.LogMerge($"merged {gs.ToString()}");
                    }
                }
            }
            mergedbHelper.MergingTable("Fishing gear specifications");

            Logger.LogMerge("reference gear codes");
            foreach (var rgc in Source.RefGearCodeViewModel.RefGearCodeCollection)
            {
                if (Destination.RefGearCodeViewModel.GetRefGearCode(rgc.GearCode) == null)
                {
                    if (Destination.RefGearCodeViewModel.AddRecordToRepo(rgc) && log)
                    {
                        Logger.LogMerge($"merged {rgc.ToString()}");
                    }
                }
            }
            mergedbHelper.MergingTable("Reference gear codes");

            Logger.LogMerge("reference gear code usage");
            foreach (var item in Source.RefGearCodeUsageViewModel.RefGearCodeUsageCollection.Where(t=>t.AOI.AOIGuid==SourceAOI.AOIGuid))
            {
                if (Destination.RefGearCodeUsageViewModel.GetRefGearCodeUsage(item.RowNumber) == null)
                {
                    if (Destination.RefGearCodeUsageViewModel.AddRecordToRepo(item) && log)
                    {
                        Logger.LogMerge($"merged {item.ToString()}");
                    }
                }
            }
            mergedbHelper.MergingTable("Reference gear codes usage");

            Logger.LogMerge("reference gear code usage local name");
            foreach (var item in Source.RefGearCodeUsageLocalNameViewModel.RefGearCodeUsageLocalNameCollection.Where(t=>t.RefGearCodeUsage.AOI.AOIGuid==SourceAOI.AOIGuid))
            {
                if (Destination.RefGearCodeUsageLocalNameViewModel.GetUsageLocalName(item.RowID) == null)
                {
                    if (Destination.RefGearCodeUsageLocalNameViewModel.AddRecordToRepo(item) && log)
                    {
                        Logger.LogMerge($"merged {item.ToString()}");
                    }
                }
            }
            mergedbHelper.MergingTable("Reference gear codes usage local names");


            Logger.LogMerge("merging catch local names");
            foreach (var ln in Source.CatchLocalNameViewModel.CatchLocalNameCollection)
            {
                if (Destination.CatchLocalNameViewModel.GetCatchLocalName(ln.Guid) == null)
                {
                    if (Destination.CatchLocalNameViewModel.AddRecordToRepo(ln) && log)
                    {
                        Logger.LogMerge($"merged {ln.ToString()}");
                    }
                }
            }
            mergedbHelper.MergingTable("Catch local names");

            Logger.LogMerge("merging species");
            foreach (Species sp in Source.SpeciesViewModel.SpeciesCollection)
            {
                if (Destination.SpeciesViewModel.GetSpecies(sp.SpeciesID) == null)
                {
                    if (Destination.SpeciesViewModel.AddRecordToRepo(sp) && log)
                    {
                        Logger.LogMerge($"merged {sp.ToString()}");
                    }
                }
            }
            mergedbHelper.MergingTable("Species");


            Logger.LogMerge("merging enumerators");
            foreach (SamplingEnumerator se in Source.SamplingEnumeratorViewModel.SamplingEnumeratorCollection)
            {
                if (Destination.SamplingEnumeratorViewModel.GetSamplingEnumerator(se.EnumeratorID) == null)
                {
                    if (Destination.SamplingEnumeratorViewModel.AddRecordToRepo(se) && log)
                    {
                        Logger.LogMerge($"merged {se.ToString()}");
                    }
                }
            }
            mergedbHelper.MergingTable("Enumerators");

            Logger.LogMerge("merging samplings");
            foreach (Sampling s in Source.SamplingViewModel.SamplingCollection.Where(t => t.AOI.AOIGuid == SourceAOI.AOIGuid))
            {
                if (Destination.SamplingViewModel.GetSampling(s.RowID) == null)
                {
                    if (Destination.SamplingViewModel.AddRecordToRepo(s) && log)
                    {
                        Logger.LogMerge($"merged {s.ToString()}");
                    }
                }
            }
            mergedbHelper.MergingTable("Samplings");

            Logger.LogMerge("merging additional fishing grounds");
            foreach (var afg in Source.AdditionalFishingGroundViewModel.AdditionalFishingGroundCollection.Where(t => t.Sampling.AOI.AOIGuid == SourceAOI.AOIGuid))
            {
                if (Destination.AdditionalFishingGroundViewModel.GetAdditionalFishingGround(afg.RowGUID) == null)
                {
                    if (Destination.AdditionalFishingGroundViewModel.AddRecordToRepo(afg) && log)
                    {
                        Logger.LogMerge($"merged {afg.ToString()}");
                    }
                }
            }
            mergedbHelper.MergingTable("Additional fishing grounds");


            Logger.LogMerge("merging sampled gear specs");
            foreach (SampledGearSpec sgs in Source.SampledGearSpecViewModel.SampledGearSpecCollection.Where(t => t.Sampling.AOI.AOIGuid == SourceAOI.AOIGuid))
            {
                if (Destination.SampledGearSpecViewModel.getSampledGearSpec(sgs.RowID) == null)
                {
                    if (Destination.SampledGearSpecViewModel.AddRecordToRepo(sgs) && log)
                    {
                        Logger.LogMerge($"merged {sgs.ToString()}");
                    }
                }
            }
            mergedbHelper.MergingTable("Sampled gear specs");

            Logger.LogMerge("merging expense");
            foreach (var exp in Source.FishingExpenseViewModel.FishingExpenseCollection.Where(t => t.Sampling.AOI.AOIGuid == SourceAOI.AOIGuid))
            {
                if (Destination.FishingExpenseViewModel.getFishingExpense(exp.SamplingID) == null)
                {
                    if (Destination.FishingExpenseViewModel.AddRecordToRepo(exp) && log)
                    {
                        Logger.LogMerge($"merged {exp.ToString()}");
                    }
                }
            }
            mergedbHelper.MergingTable("Fishing expense");

            Logger.LogMerge("merging expense items");
            foreach (var exi in Source.FishingExpenseItemViewModel.FishingExpenseItemCollection.Where(t => t.ParentFishingExpense.Sampling.AOI.AOIGuid == SourceAOI.AOIGuid))
            {
                if (Destination.FishingExpenseItemViewModel.getFishingExpenseItem(exi.ExpenseRowID) == null)
                {
                    if (Destination.FishingExpenseItemViewModel.AddRecordToRepo(exi) && log)
                    {
                        Logger.LogMerge($"merged {exi.ToString()}");
                    }
                }
            }
            mergedbHelper.MergingTable("Fishing expense items");

            Logger.LogMerge("merging catch composition");
            foreach (var cc in Source.CatchCompositionViewModel.CatchCompositionCollection.Where(t => t.Sampling.AOI.AOIGuid == SourceAOI.AOIGuid))
            {
                if (Destination.CatchCompositionViewModel.GetCatchComposition(cc.RowGUID) == null)
                {
                    if (Destination.CatchCompositionViewModel.AddRecordToRepo(cc) && log)
                    {
                        Logger.LogMerge($"merged {cc.ToString()}");
                    }
                }
            }
            mergedbHelper.MergingTable("Catch composition");

            Logger.LogMerge("merging catch detail");
            foreach (var cd in Source.CatchDetailViewModel.CatchDetailCollection.Where(t => t.CatchComposition.Sampling.AOI.AOIGuid == SourceAOI.AOIGuid))
            {
                if (Destination.CatchDetailViewModel.getCatchDetail(cd.RowGUID) == null)
                {
                    if (Destination.CatchDetailViewModel.AddRecordToRepo(cd) && log)
                    {
                        Logger.LogMerge($"merged {cd.ToString()}");
                    }
                }
            }
            mergedbHelper.MergingTable("Catch details");

            Logger.LogMerge("merging len freq");
            foreach (var lf in Source.LenFreqViewModel.LenFreqCollection.Where(t => t.CatchComposition.Sampling.AOI.AOIGuid == SourceAOI.AOIGuid))
            {
                if (Destination.LenFreqViewModel.getLenFreq(lf.RowGUID) == null)
                {
                    if (Destination.LenFreqViewModel.AddRecordToRepo(lf) && log)
                    {
                        Logger.LogMerge($"merged {lf.ToString()}");
                    }
                }
            }
            mergedbHelper.MergingTable("Length frequency");

            Logger.LogMerge("merging GMS");
            foreach (var gms in Source.GonadMaturityStageViewModel.GonadalMaturiryStageCollection.Where(t => t.CatchComposition.Sampling.AOI.AOIGuid == SourceAOI.AOIGuid))
            {
                if (Destination.GonadMaturityStageViewModel.GetGonadalMaturityState(gms.RowGUID) == null)
                {
                    if (Destination.GonadMaturityStageViewModel.AddRecordToRepo(gms) && log)
                    {
                        Logger.LogMerge($"merged {gms.ToString()}");
                    }
                }
            }
            mergedbHelper.MergingTable("Gonadal maturity stage");

            mergedbHelper.IsDone();

            return true;
        }


    }
}