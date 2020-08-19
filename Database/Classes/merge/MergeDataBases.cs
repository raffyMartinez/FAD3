using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace FAD3.Database.Classes.merge
{
    public static class MergeDataBases
    {
        public static List<string> EntityTables { get; private set; }
        public static Dictionary<string, int> DestinationBeforeCounts { get; set; }
        public static Dictionary<string, int> SourceCounts { get; set; }
        public static Dictionary<string, int> DestinationAfterCounts { get; set; }
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

        private static void SetUpEntityList()
        {
            EntityTables = new List<string>();
            EntityTables.Add("Samplings");
            EntityTables.Add("Catch composition");
            EntityTables.Add("Catch details");
            EntityTables.Add("Length frequency");
            EntityTables.Add("GMS");
            EntityTables.Add("Fishing expense");
            EntityTables.Add("Fishing expense items");
            EntityTables.Add("Enumerators");
            EntityTables.Add("Landing sites");
            EntityTables.Add("Species");
            EntityTables.Add("Gear class");
            EntityTables.Add("Gears");
            EntityTables.Add("Sampled gear specifications");
            EntityTables.Add("Catch local names");
            EntityTables.Add("Catch names");
            EntityTables.Add("Gear local names");
            EntityTables.Add("Additional extents");
            EntityTables.Add("Additional fishing grounds");
            EntityTables.Add("Gear specs");
            EntityTables.Add("Municipalities");
            EntityTables.Add("Provinces");
            EntityTables.Add("Reference gear codes");
            EntityTables.Add("Reference gear code usage");
            EntityTables.Add("Reference gear code local names");
        }
        public static void SetSourceAndDestination()
        {
            _desitantionDatabaseFileName = global.MDBPath;
            Destination = new FADEntities(_desitantionDatabaseFileName);
            Source = new FADEntities(_sourceDatabaseFileName);
            SetUpEntityList();

        }

        public static async void SetUpForReporting(MergeDBHelper mergedbHelper)
        {
            _desitantionDatabaseFileName = global.MDBPath;
            Destination = new FADEntities(_desitantionDatabaseFileName);
            await Setup(mergedbHelper, true);
        }
        public static FADEntities Destination { get; private set; }
        public static FADEntities Source { get; private set; }

        public static async Task Setup(MergeDBHelper mergedbHelper, bool destinationOnly = false)
        {
            await Task.Run(() => SetUpEntities(mergedbHelper, destinationOnly));
        }

        public static void SyncGearClasses()
        {
            foreach (var gcSource in Source.GearClassViewModel.GearClassCollection)
            {
                if (Destination.GearClassViewModel.GetGearClass(gcSource.GearCode) == null)
                {
                    Destination.GearClassViewModel.AddRecordToRepo(gcSource);
                }
            }
        }
        private static void SetUpEntities(MergeDBHelper mergeDBHelper, bool destinationOnly = false)
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

            if (destinationOnly)
            {
                mergeDBHelper.IsDone();
            }
            else
            {

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

                SetupCounts();
                PickupCounts();
            }

        }

        private static void SetupCounts()
        {
            DestinationBeforeCounts = new Dictionary<string, int>();
            SourceCounts = new Dictionary<string, int>();
            DestinationAfterCounts = new Dictionary<string, int>();

            foreach (var item in EntityTables)
            {
                DestinationBeforeCounts.Add(item, 0);
                SourceCounts.Add(item, 0);
                DestinationAfterCounts.Add(item, 0);
            }
        }
        private static void PickupCounts(bool before = true)
        {
            if (before)
            {
                DestinationBeforeCounts["Samplings"] = Destination.SamplingViewModel.Count;
                DestinationBeforeCounts["Catch composition"] = Destination.CatchCompositionViewModel.Count;
                DestinationBeforeCounts["Catch details"] = Destination.CatchDetailViewModel.Count;
                DestinationBeforeCounts["Length frequency"] = Destination.LenFreqViewModel.Count;
                DestinationBeforeCounts["GMS"] = Destination.GonadMaturityStageViewModel.Count;
                DestinationBeforeCounts["Fishing expense"] = Destination.FishingExpenseViewModel.Count;
                DestinationBeforeCounts["Fishing expense items"] = Destination.FishingExpenseItemViewModel.Count;
                DestinationBeforeCounts["Enumerators"] = Destination.SamplingEnumeratorViewModel.Count;
                DestinationBeforeCounts["Landing sites"] = Destination.LandingSiteViewModel.Count;
                DestinationBeforeCounts["Species"] = Destination.SpeciesViewModel.Count;
                DestinationBeforeCounts["Gear class"] = Destination.GearClassViewModel.Count;
                DestinationBeforeCounts["Gears"] = Destination.GearViewModel.Count;
                DestinationBeforeCounts["Sampled gear specifications"] = Destination.SampledGearSpecViewModel.Count;
                DestinationBeforeCounts["Catch local names"] = Destination.CatchLocalNameViewModel.Count;
                DestinationBeforeCounts["Catch names"] = Destination.CatchNameViewModel.Count;
                DestinationBeforeCounts["Geal local names"] = Destination.GearLocalNameViewModel.Count;
                DestinationBeforeCounts["Additional extents"] = Destination.AdditionalExtentViewModel.Count;
                DestinationBeforeCounts["Additional fishing grounds"] = Destination.AdditionalFishingGroundViewModel.Count;
                DestinationBeforeCounts["Gear specs"] = Destination.GearSpecViewModel.Count;
                DestinationBeforeCounts["Municipalities"] = Destination.MunicipalityViewModel.Count;
                DestinationBeforeCounts["Provinces"] = Destination.ProvinceViewModel.Count;
                DestinationBeforeCounts["Reference gear codes"] = Destination.RefGearCodeViewModel.Count;
                DestinationBeforeCounts["Reference gear code usage"] = Destination.RefGearCodeUsageViewModel.Count;
                DestinationBeforeCounts["Reference gear code local names"] = Destination.RefGearCodeUsageLocalNameViewModel.Count;

                SourceCounts["Samplings"] = Source.SamplingViewModel.Count;
                SourceCounts["Catch composition"] = Source.CatchCompositionViewModel.Count;
                SourceCounts["Catch details"] = Source.CatchDetailViewModel.Count;
                SourceCounts["Length frequency"] = Source.LenFreqViewModel.Count;
                SourceCounts["GMS"] = Source.GonadMaturityStageViewModel.Count;
                SourceCounts["Fishing expense"] = Source.FishingExpenseViewModel.Count;
                SourceCounts["Fishing expense items"] = Source.FishingExpenseItemViewModel.Count;
                SourceCounts["Enumerators"] = Source.SamplingEnumeratorViewModel.Count;
                SourceCounts["Landing sites"] = Source.LandingSiteViewModel.Count;
                SourceCounts["Species"] = Source.SpeciesViewModel.Count;
                SourceCounts["Gear class"] = Source.GearClassViewModel.Count;
                SourceCounts["Gears"] = Source.GearViewModel.Count;
                SourceCounts["Sampled gear specifications"] = Source.SampledGearSpecViewModel.Count;
                SourceCounts["Catch local names"] = Source.CatchLocalNameViewModel.Count;
                SourceCounts["Catch names"] = Source.CatchNameViewModel.Count;
                SourceCounts["Geal local names"] = Source.GearLocalNameViewModel.Count;
                SourceCounts["Additional extents"] = Source.AdditionalExtentViewModel.Count;
                SourceCounts["Additional fishing grounds"] = Source.AdditionalFishingGroundViewModel.Count;
                SourceCounts["Gear specs"] = Source.GearSpecViewModel.Count;
                SourceCounts["Municipalities"] = Source.MunicipalityViewModel.Count;
                SourceCounts["Provinces"] = Source.ProvinceViewModel.Count;
                SourceCounts["Reference gear codes"] = Source.RefGearCodeViewModel.Count;
                SourceCounts["Reference gear code usage"] = Source.RefGearCodeUsageViewModel.Count;
                SourceCounts["Reference gear code local names"] = Source.RefGearCodeUsageLocalNameViewModel.Count;
            }
            else
            {
                DestinationAfterCounts["Samplings"] = Destination.SamplingViewModel.Count;
                DestinationAfterCounts["Catch composition"] = Destination.CatchCompositionViewModel.Count;
                DestinationAfterCounts["Catch details"] = Destination.CatchDetailViewModel.Count;
                DestinationAfterCounts["Length frequency"] = Destination.LenFreqViewModel.Count;
                DestinationAfterCounts["GMS"] = Destination.GonadMaturityStageViewModel.Count;
                DestinationAfterCounts["Fishing expense"] = Destination.FishingExpenseViewModel.Count;
                DestinationAfterCounts["Fishing expense items"] = Destination.FishingExpenseItemViewModel.Count;
                DestinationAfterCounts["Enumerators"] = Destination.SamplingEnumeratorViewModel.Count;
                DestinationAfterCounts["Landing sites"] = Destination.LandingSiteViewModel.Count;
                DestinationAfterCounts["Species"] = Destination.SpeciesViewModel.Count;
                DestinationAfterCounts["Gear class"] = Destination.GearClassViewModel.Count;
                DestinationAfterCounts["Gears"] = Destination.GearViewModel.Count;
                DestinationAfterCounts["Sampled gear specifications"] = Destination.SampledGearSpecViewModel.Count;
                DestinationAfterCounts["Catch local names"] = Destination.CatchLocalNameViewModel.Count;
                DestinationAfterCounts["Catch names"] = Destination.CatchNameViewModel.Count;
                DestinationAfterCounts["Geal local names"] = Destination.GearLocalNameViewModel.Count;
                DestinationAfterCounts["Additional extents"] = Destination.AdditionalExtentViewModel.Count;
                DestinationAfterCounts["Additional fishing grounds"] = Destination.AdditionalFishingGroundViewModel.Count;
                DestinationAfterCounts["Gear specs"] = Destination.GearSpecViewModel.Count;
                DestinationAfterCounts["Municipalities"] = Destination.MunicipalityViewModel.Count;
                DestinationAfterCounts["Provinces"] = Destination.ProvinceViewModel.Count;
                DestinationAfterCounts["Reference gear codes"] = Destination.RefGearCodeViewModel.Count;
                DestinationAfterCounts["Reference gear code usage"] = Destination.RefGearCodeUsageViewModel.Count;
                DestinationAfterCounts["Reference gear code local names"] = Destination.RefGearCodeUsageLocalNameViewModel.Count;
            }

        }
        public static string MergeResultMessage { get; private set; }

        public static async Task<bool> Merge(MergeDBHelper mergedbHelper, bool log)
        {
            return await Task.Run(() => MergeTables(log, mergedbHelper));
        }

        private static bool MergeTables(bool log, MergeDBHelper mergedbHelper, bool trial = false)
        {
            Logger.DeleteMergeLog();
            Logger.DeleteMergeErrorLog();
            Logger.LogMerge(Source, Destination);


            Logger.LogMerge("merging AOIs");
            mergedbHelper.MergingTable("Target areas");
            if (Destination.AOIViewModel.GetEqual(SourceAOI) == null)
            {
                if (Destination.AOIViewModel.AddRecordToRepo(SourceAOI) && log)
                {

                    Logger.LogMerge($"merged {SourceAOI.ToString()}");
                }
            }


            Logger.LogMerge("merging additional extents");
            mergedbHelper.MergingTable("Additional AOI extents");
            foreach (var item in Source.AdditionalExtentViewModel.AdditionalExtentCollection.Where(t => t.AOI.AOIGuid == SourceAOI.AOIGuid))
            {
                if (Destination.AdditionalExtentViewModel.GetAdditionalExtent(item.RowID) == null)
                {
                    if (Destination.AdditionalExtentViewModel.AddRecordToRepo(item) && log)
                    {
                        Logger.LogMerge($"merged {item.ToString()}");
                    }
                }
            }



            Logger.LogMerge("merging landing sites");
            mergedbHelper.MergingTable("Landing sites");
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


            Logger.LogMerge("merging gear local names");
            mergedbHelper.MergingTable("Gear local names");
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


            Logger.LogMerge("merging gears");
            mergedbHelper.MergingTable("Fishing gears");
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
                if (gear.GearName == "Boat Cast Net") { Debugger.Break(); }
                if (Destination.GearViewModel.GetGear(gear.GearID) == null)
                {
                    if (Destination.GearViewModel.AddRecordToRepo(gear) && log)
                    {
                        Logger.LogMerge($"merged {gear.ToString()}");
                    }
                }
            }


            Logger.LogMerge("merging gear specs");
            mergedbHelper.MergingTable("Fishing gear specifications");
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


            Logger.LogMerge("reference gear codes");
            mergedbHelper.MergingTable("Reference gear codes");
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


            Logger.LogMerge("reference gear code usage");
            mergedbHelper.MergingTable("Reference gear codes usage");
            foreach (var item in Source.RefGearCodeUsageViewModel.RefGearCodeUsageCollection.Where(t => t.AOI.AOIGuid == SourceAOI.AOIGuid))
            {
                if (Destination.RefGearCodeUsageViewModel.GetRefGearCodeUsage(item.RowNumber) == null)
                {
                    if (Destination.RefGearCodeUsageViewModel.AddRecordToRepo(item) && log)
                    {
                        Logger.LogMerge($"merged {item.ToString()}");
                    }
                }
            }


            Logger.LogMerge("reference gear code usage local name");
            mergedbHelper.MergingTable("Reference gear codes usage local names");
            foreach (var item in Source.RefGearCodeUsageLocalNameViewModel.RefGearCodeUsageLocalNameCollection.Where(t => t.RefGearCodeUsage.AOI.AOIGuid == SourceAOI.AOIGuid))
            {
                if (Destination.RefGearCodeUsageLocalNameViewModel.GetUsageLocalName(item.RowID) == null)
                {
                    if (Destination.RefGearCodeUsageLocalNameViewModel.AddRecordToRepo(item) && log)
                    {
                        Logger.LogMerge($"merged {item.ToString()}");
                    }
                }
            }



            Logger.LogMerge("merging catch local names");
            mergedbHelper.MergingTable("Catch local names");
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


            Logger.LogMerge("merging species");
            mergedbHelper.MergingTable("Species");
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



            Logger.LogMerge("merging enumerators");
            mergedbHelper.MergingTable("Enumerators");
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


            Logger.LogMerge("merging samplings");
            mergedbHelper.MergingTable("Samplings");
            foreach (Sampling sampling in Destination.SamplingViewModel.SamplingCollection)
            {
                var sourceSampling = Source.SamplingViewModel.GetSamplingFromReferenceNumber(sampling.ReferenceNumber.ReferenceNumber);
                if (sourceSampling != null)
                {
                    if (sourceSampling.RowID != sampling.RowID)
                    {

                        var item = Source.SamplingViewModel.GetSampling(sampling.RowID);
                    }
                }
                if (sourceSampling != null && sourceSampling.RowID != sampling.RowID && Source.SamplingViewModel.ModifyReferenceNumber(sampling))
                {
                    Logger.LogMerge($"modified RefNo of {sampling.ReferenceNumber.ReferenceNumber} from source");
                }
            }


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


            Logger.LogMerge("merging additional fishing grounds");
            mergedbHelper.MergingTable("Additional fishing grounds");
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



            Logger.LogMerge("merging sampled gear specs");
            mergedbHelper.MergingTable("Sampled gear specs");
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


            Logger.LogMerge("merging expense");
            mergedbHelper.MergingTable("Fishing expense");
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


            Logger.LogMerge("merging expense items");
            mergedbHelper.MergingTable("Fishing expense items");
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


            Logger.LogMerge("merging catch composition");
            mergedbHelper.MergingTable("Catch composition");
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


            Logger.LogMerge("merging catch detail");
            mergedbHelper.MergingTable("Catch details");
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


            Logger.LogMerge("merging len freq");
            mergedbHelper.MergingTable("Length frequency");
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


            Logger.LogMerge("merging GMS");
            mergedbHelper.MergingTable("Gonadal maturity stage");
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


            mergedbHelper.IsDone();

            PickupCounts(false);

            return true;
        }
        private static bool MergeTables1(bool log, MergeDBHelper mergedbHelper, bool trial = false)
        {
            Logger.DeleteMergeLog();
            Logger.DeleteMergeErrorLog();
            Logger.LogMerge(Source, Destination);


            Logger.LogMerge("merging AOIs");
            mergedbHelper.MergingTable("Target areas");
            //if (Destination.AOIViewModel.GetEqual(SourceAOI) == null)
            //{
            if (Destination.AOIViewModel.AddRecordToRepo(SourceAOI) && log)
            {

                Logger.LogMerge($"merged {SourceAOI.ToString()}");
            }
            //}


            Logger.LogMerge("merging additional extents");
            mergedbHelper.MergingTable("Additional AOI extents");
            foreach (var item in Source.AdditionalExtentViewModel.AdditionalExtentCollection.Where(t => t.AOI.AOIGuid == SourceAOI.AOIGuid))
            {
                //if (Destination.AdditionalExtentViewModel.GetAdditionalExtent(item.RowID) == null)
                //{
                if (Destination.AdditionalExtentViewModel.AddRecordToRepo(item) && log)
                {
                    Logger.LogMerge($"merged {item.ToString()}");
                }
                //}
            }



            Logger.LogMerge("merging landing sites");
            mergedbHelper.MergingTable("Landing sites");
            foreach (LandingSite landingSiteToMerge in Source.LandingSiteViewModel.LandingSiteCollection.Where(t => t.AOI.AOIGuid == SourceAOI.AOIGuid))
            {
                //if (Destination.LandingSiteViewModel.GetEqual(landingSiteToMerge) == null)
                // {
                if (Destination.LandingSiteViewModel.AddRecordToRepo(landingSiteToMerge) && log)
                {
                    Logger.LogMerge($"merged {landingSiteToMerge.ToString()}");
                }
                //}
            }


            Logger.LogMerge("merging gear local names");
            mergedbHelper.MergingTable("Gear local names");
            foreach (GearLocalName gln in Source.GearLocalNameViewModel.GearLocalNameCollection)
            {
                //if (Destination.GearLocalNameViewModel.GetGearLocalName(gln.Guid) == null)
                // {
                if (Destination.GearLocalNameViewModel.AddRecordToRepo(gln) && log)
                {
                    Logger.LogMerge($"merged {gln.ToString()}");
                }
                //}
            }


            Logger.LogMerge("merging gears");
            mergedbHelper.MergingTable("Fishing gears");
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
                //if (gear.GearName == "Boat Cast Net") { Debugger.Break(); }
                //if (Destination.GearViewModel.GetGear(gear.GearID) == null)
                //{
                if (Destination.GearViewModel.AddRecordToRepo(gear) && log)
                {
                    Logger.LogMerge($"merged {gear.ToString()}");
                }
                //}
            }


            Logger.LogMerge("merging gear specs");
            mergedbHelper.MergingTable("Fishing gear specifications");
            foreach (GearSpec gs in Source.GearSpecViewModel.GearSpecCollection)
            {
                //if (Destination.GearSpecViewModel.getGearSpec(gs.RowGUID) == null)
                //{
                if (Destination.GearSpecViewModel.AddRecordToRepo(gs) && log)
                {
                    Logger.LogMerge($"merged {gs.ToString()}");
                }
                //}
            }


            Logger.LogMerge("reference gear codes");
            mergedbHelper.MergingTable("Reference gear codes");
            foreach (var rgc in Source.RefGearCodeViewModel.RefGearCodeCollection)
            {
                //if (Destination.RefGearCodeViewModel.GetRefGearCode(rgc.GearCode) == null)
                //{
                if (Destination.RefGearCodeViewModel.AddRecordToRepo(rgc) && log)
                {
                    Logger.LogMerge($"merged {rgc.ToString()}");
                }
                //}
            }


            Logger.LogMerge("reference gear code usage");
            mergedbHelper.MergingTable("Reference gear codes usage");
            foreach (var item in Source.RefGearCodeUsageViewModel.RefGearCodeUsageCollection.Where(t => t.AOI.AOIGuid == SourceAOI.AOIGuid))
            {
                //if (Destination.RefGearCodeUsageViewModel.GetRefGearCodeUsage(item.RowNumber) == null)
                //{
                if (Destination.RefGearCodeUsageViewModel.AddRecordToRepo(item) && log)
                {
                    Logger.LogMerge($"merged {item.ToString()}");
                }
                //}
            }


            Logger.LogMerge("reference gear code usage local name");
            mergedbHelper.MergingTable("Reference gear codes usage local names");
            foreach (var item in Source.RefGearCodeUsageLocalNameViewModel.RefGearCodeUsageLocalNameCollection.Where(t => t.RefGearCodeUsage.AOI.AOIGuid == SourceAOI.AOIGuid))
            {
                //if (Destination.RefGearCodeUsageLocalNameViewModel.GetUsageLocalName(item.RowID) == null)
                //{
                if (Destination.RefGearCodeUsageLocalNameViewModel.AddRecordToRepo(item) && log)
                {
                    Logger.LogMerge($"merged {item.ToString()}");
                }
                //}
            }



            Logger.LogMerge("merging catch local names");
            mergedbHelper.MergingTable("Catch local names");
            foreach (var ln in Source.CatchLocalNameViewModel.CatchLocalNameCollection)
            {
                //if (Destination.CatchLocalNameViewModel.GetCatchLocalName(ln.Guid) == null)
                //{
                if (Destination.CatchLocalNameViewModel.AddRecordToRepo(ln) && log)
                {
                    Logger.LogMerge($"merged {ln.ToString()}");
                }
                //}
            }


            Logger.LogMerge("merging species");
            mergedbHelper.MergingTable("Species");
            foreach (Species sp in Source.SpeciesViewModel.SpeciesCollection)
            {
                //if (Destination.SpeciesViewModel.GetSpecies(sp.SpeciesID) == null)
                //{
                if (Destination.SpeciesViewModel.AddRecordToRepo(sp) && log)
                {
                    Logger.LogMerge($"merged {sp.ToString()}");
                }
                //}
            }



            Logger.LogMerge("merging enumerators");
            mergedbHelper.MergingTable("Enumerators");
            foreach (SamplingEnumerator se in Source.SamplingEnumeratorViewModel.SamplingEnumeratorCollection)
            {
                //if (Destination.SamplingEnumeratorViewModel.GetSamplingEnumerator(se.EnumeratorID) == null)
                //{
                if (Destination.SamplingEnumeratorViewModel.AddRecordToRepo(se) && log)
                {
                    Logger.LogMerge($"merged {se.ToString()}");
                }
                //}
            }


            Logger.LogMerge("merging samplings");
            mergedbHelper.MergingTable("Samplings");
            foreach (Sampling sampling in Destination.SamplingViewModel.SamplingCollection)
            {
                var sourceSampling = Source.SamplingViewModel.GetSamplingFromReferenceNumber(sampling.ReferenceNumber.ReferenceNumber);
                if (sourceSampling != null)
                {
                    if (sourceSampling.RowID != sampling.RowID)
                    {

                        var item = Source.SamplingViewModel.GetSampling(sampling.RowID);
                    }
                }
                if (sourceSampling != null && sourceSampling.RowID != sampling.RowID && Source.SamplingViewModel.ModifyReferenceNumber(sampling))
                {
                    Logger.LogMerge($"modified RefNo of {sampling.ReferenceNumber.ReferenceNumber} from source");
                }
            }


            foreach (Sampling s in Source.SamplingViewModel.SamplingCollection.Where(t => t.AOI.AOIGuid == SourceAOI.AOIGuid))
            {
                //if (Destination.SamplingViewModel.GetSampling(s.RowID) == null)
                //{
                if (Destination.SamplingViewModel.AddRecordToRepo(s) && log)
                {
                    Logger.LogMerge($"merged {s.ToString()}");
                }
                //}
            }


            Logger.LogMerge("merging additional fishing grounds");
            mergedbHelper.MergingTable("Additional fishing grounds");
            foreach (var afg in Source.AdditionalFishingGroundViewModel.AdditionalFishingGroundCollection.Where(t => t.Sampling.AOI.AOIGuid == SourceAOI.AOIGuid))
            {
                //if (Destination.AdditionalFishingGroundViewModel.GetAdditionalFishingGround(afg.RowGUID) == null)
                //{
                if (Destination.AdditionalFishingGroundViewModel.AddRecordToRepo(afg) && log)
                {
                    Logger.LogMerge($"merged {afg.ToString()}");
                }
                //}
            }



            Logger.LogMerge("merging sampled gear specs");
            mergedbHelper.MergingTable("Sampled gear specs");
            foreach (SampledGearSpec sgs in Source.SampledGearSpecViewModel.SampledGearSpecCollection.Where(t => t.Sampling.AOI.AOIGuid == SourceAOI.AOIGuid))
            {
                //if (Destination.SampledGearSpecViewModel.getSampledGearSpec(sgs.RowID) == null)
                //{
                if (Destination.SampledGearSpecViewModel.AddRecordToRepo(sgs) && log)
                {
                    Logger.LogMerge($"merged {sgs.ToString()}");
                }
                //}
            }


            Logger.LogMerge("merging expense");
            mergedbHelper.MergingTable("Fishing expense");
            foreach (var exp in Source.FishingExpenseViewModel.FishingExpenseCollection.Where(t => t.Sampling.AOI.AOIGuid == SourceAOI.AOIGuid))
            {
                //if (Destination.FishingExpenseViewModel.getFishingExpense(exp.SamplingID) == null)
                //{
                if (Destination.FishingExpenseViewModel.AddRecordToRepo(exp) && log)
                {
                    Logger.LogMerge($"merged {exp.ToString()}");
                }
                //}
            }


            Logger.LogMerge("merging expense items");
            mergedbHelper.MergingTable("Fishing expense items");
            foreach (var exi in Source.FishingExpenseItemViewModel.FishingExpenseItemCollection.Where(t => t.ParentFishingExpense.Sampling.AOI.AOIGuid == SourceAOI.AOIGuid))
            {
                //if (Destination.FishingExpenseItemViewModel.getFishingExpenseItem(exi.ExpenseRowID) == null)
                //{
                if (Destination.FishingExpenseItemViewModel.AddRecordToRepo(exi) && log)
                {
                    Logger.LogMerge($"merged {exi.ToString()}");
                }
                //}
            }


            Logger.LogMerge("merging catch composition");
            mergedbHelper.MergingTable("Catch composition");
            foreach (var cc in Source.CatchCompositionViewModel.CatchCompositionCollection.Where(t => t.Sampling.AOI.AOIGuid == SourceAOI.AOIGuid))
            {
                //if (Destination.CatchCompositionViewModel.GetCatchComposition(cc.RowGUID) == null)
                //{
                if (Destination.CatchCompositionViewModel.AddRecordToRepo(cc) && log)
                {
                    Logger.LogMerge($"merged {cc.ToString()}");
                }
                //}
            }


            Logger.LogMerge("merging catch detail");
            mergedbHelper.MergingTable("Catch details");
            foreach (var cd in Source.CatchDetailViewModel.CatchDetailCollection.Where(t => t.CatchComposition.Sampling.AOI.AOIGuid == SourceAOI.AOIGuid))
            {
                //if (Destination.CatchDetailViewModel.getCatchDetail(cd.RowGUID) == null)
                //{
                if (Destination.CatchDetailViewModel.AddRecordToRepo(cd) && log)
                {
                    Logger.LogMerge($"merged {cd.ToString()}");
                }
                //}
            }


            Logger.LogMerge("merging len freq");
            mergedbHelper.MergingTable("Length frequency");
            foreach (var lf in Source.LenFreqViewModel.LenFreqCollection.Where(t => t.CatchComposition.Sampling.AOI.AOIGuid == SourceAOI.AOIGuid))
            {
                //if (Destination.LenFreqViewModel.getLenFreq(lf.RowGUID) == null)
                //{
                if (Destination.LenFreqViewModel.AddRecordToRepo(lf) && log)
                {
                    Logger.LogMerge($"merged {lf.ToString()}");
                }
                //}
            }


            Logger.LogMerge("merging GMS");
            mergedbHelper.MergingTable("Gonadal maturity stage");
            foreach (var gms in Source.GonadMaturityStageViewModel.GonadalMaturiryStageCollection.Where(t => t.CatchComposition.Sampling.AOI.AOIGuid == SourceAOI.AOIGuid))
            {
                //if (Destination.GonadMaturityStageViewModel.GetGonadalMaturityState(gms.RowGUID) == null)
                //{
                if (Destination.GonadMaturityStageViewModel.AddRecordToRepo(gms) && log)
                {
                    Logger.LogMerge($"merged {gms.ToString()}");
                }
                //}
            }


            mergedbHelper.IsDone();

            PickupCounts(false);

            return true;
        }


    }
}
