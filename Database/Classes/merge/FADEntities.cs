namespace FAD3.Database.Classes.merge
{
    public class FADEntities
    {
        private string _mdbPath;
        public AOIViewModel AOIViewModel { get; set; }

        public AdditionalExtentViewModel AdditionalExtentViewModel { get; set; }
        public ProvinceViewModel ProvinceViewModel { get; set; }
        public MunicipalityViewModel MunicipalityViewModel { get; set; }
        public LandingSiteViewModel LandingSiteViewModel { get; set; }

        public CatchLocalNameViewModel CatchLocalNameViewModel { get; set; }
        public GearLocalNameViewModel GearLocalNameViewModel { get; set; }
        public GearClassViewModel GearClassViewModel { get; set; }
        public GearViewModel GearViewModel { get; set; }

        public GearSpecViewModel GearSpecViewModel { get; set; }

        public RefGearCodeViewModel RefGearCodeViewModel { get; set; }

        public RefGearCodeUsageViewModel RefGearCodeUsageViewModel { get; set; }

        public RefGearCodeUsageLocalNameViewModel RefGearCodeUsageLocalNameViewModel { get; set; }
        public SamplingEnumeratorViewModel SamplingEnumeratorViewModel { get; set; }

        public FishingExpenseViewModel FishingExpenseViewModel { get; set; }

        public FishingExpenseItemViewModel FishingExpenseItemViewModel { get; set; }
        public SamplingViewModel SamplingViewModel { get; set; }

        public AdditionalFishingGroundViewModel AdditionalFishingGroundViewModel { get; set; }
        public TaxaViewModel TaxaViewModel { get; set; }
        public SpeciesViewModel SpeciesViewModel { get; set; }

        public CatchNameViewModel CatchNameViewModel { get; set; }

        public CatchCompositionViewModel CatchCompositionViewModel { get; set; }

        public CatchDetailViewModel CatchDetailViewModel { get; set; }

        public LenFreqViewModel LenFreqViewModel { get; set; }

        public GonadMaturityStageViewModel GonadMaturityStageViewModel { get; set; }

        public SampledGearSpecViewModel SampledGearSpecViewModel { get; set; }
        public string ConnectionString
        {
            get { return $"Provider=Microsoft.JET.OLEDB.4.0;data source= {_mdbPath}"; }
        }

        public string MDBPath
        {
            get { return _mdbPath; }
        }
        public FADEntities(string mdbPath)
        {
            _mdbPath = mdbPath;
        }
    }
}