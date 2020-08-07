using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace FAD3.Database.Classes.merge
{
    public class SamplingViewModel
    {
        public bool AddSucceeded { get; set; }
        public ObservableCollection<Sampling> SamplingCollection { get; set; }
        private SamplingRepository Samplings { get; set; }

        public EditedEntity EditedEntity { get; private set; }

        public SamplingViewModel(FADEntities fadEntities)
        {
            Samplings = new SamplingRepository(fadEntities);
            SamplingCollection = new ObservableCollection<Sampling>(Samplings.Samplings);
            SamplingCollection.CollectionChanged += SamplingCollection_CollectionChanged;
            
        }

        public Sampling GetEarliestSampling(AOI aoi)
        {
            Sampling sampling = null;
            foreach (var item in SamplingCollection
                .Where(t=>t.AOI.AOIGuid==aoi.AOIGuid)
                .OrderBy(t => t.DateTimeSampled))
            {
                sampling = item;
                break;
            }
            return sampling;
        }

        public Sampling GetLatestSampling(AOI aoi)
        {
            Sampling sampling = null;
            foreach (var item in SamplingCollection
                .Where(t=>t.AOI.AOIGuid==aoi.AOIGuid)
                .OrderByDescending(t => t.DateTimeSampled))
            {
                sampling = item;
                break;
            }
            return sampling;
        }

        public int SerialNumberMinima(AOI aoi)
        {

            int minima = 0;

            foreach (var item in SamplingCollection
                .Where(t => t.AOI.AOIGuid == aoi.AOIGuid)
                .OrderBy(t => t.ReferenceNumber.SerialNumber))
            {
                minima = item.ReferenceNumber.SerialNumber;
                break;
            }

            return minima;
        }

            public int SerialNumberMaxima(AOI aoi)
            {

                int maxima = 0;

                foreach (var item in SamplingCollection
                    .Where(t => t.AOI.AOIGuid == aoi.AOIGuid)
                    .OrderByDescending(t => t.ReferenceNumber.SerialNumber))
                {
                maxima = item.ReferenceNumber.SerialNumber;
                    break;
                }

                return maxima;
            }
            public int CountByAOI(AOI aoi)
        {
            return SamplingCollection.Count(t => t.AOI.AOIGuid == aoi.AOIGuid);
        }
        public SamplingForEdit GetSamplingFoEdit(Sampling s, bool isNew = false)
        {
            if (isNew)
            {
                return new SamplingForEdit();
            }
            else
            {
                return new SamplingForEdit(s);
            }
        }
        public int Count
        {
            get { return SamplingCollection.Count; }
        }

        public List<Sampling> GetAllSamplings(LandingSite ls, Gear g, DateTime month)
        {
            return SamplingCollection
               .Where(t => t.LandingSite.LandingSiteGuid == ls.LandingSiteGuid)
               .Where(t => t.Gear.GearName == g.GearName)
               .Where(t => t.DateTimeSampled >= month)
               .Where(t => t.DateTimeSampled < month.AddMonths(1)).ToList();
        }

        /// <summary>
        /// returns a list of samplings specific to a landing site
        /// </summary>
        /// <param name="ls"></param>
        /// <returns></returns>
        public List<Sampling> GetAllSamplings(LandingSite ls)
        {
            return SamplingCollection
               .Where(t => t.LandingSite.LandingSiteGuid == ls.LandingSiteGuid).ToList();

        }


        /// <summary>
        /// returns a list of samplings specific to a gear
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        public List<Sampling> GetAllSamplings(Gear g)
        {
            return SamplingCollection
               .Where(t => t.Gear.GearID == g.GearID).ToList();

        }

        public Sampling GetSamplingFromReferenceNumber(SamplingReferenceNumber refNo)
        {
            return SamplingCollection
                .Where(t => t.ReferenceNumber.ReferenceNumber == refNo.ReferenceNumber).FirstOrDefault();
        }


        public List<SamplingQuickView> GetQuickViews(AOI aoi, LandingSite ls, Gear g, DateTime samplingMonth)
        {
            List<SamplingQuickView> listQV = new List<SamplingQuickView>();
            foreach (Sampling s in SamplingCollection
                .Where(t => t.AOI.AOIGuid == aoi.AOIGuid)
                .Where(t => t.LandingSite.LandingSiteGuid == ls.LandingSiteGuid)
                .Where(t => t.Gear.GearID == g.GearID)
                .Where(t => t.DateTimeSampled.Month == samplingMonth.Month)
                .Where(t => t.DateTimeSampled.Year == samplingMonth.Year))
            {
                listQV.Add(new SamplingQuickView(s.RowID, s.AOI, s.ReferenceNumber, s.DateTimeSampled, s.WeightOfCatch, s.Gear, s.LandingSite, s.FishingGround));
            }
            return listQV;
        }

        public Sampling GetSamplingFromReferenceNumber(string refNo)
        {
            return SamplingCollection
                .Where(t => t.ReferenceNumber.ReferenceNumber == refNo).FirstOrDefault();
        }

        public List<SamplingQuickView> GetQuickViews()
        {
            List<SamplingQuickView> listQV = new List<SamplingQuickView>();
            foreach (Sampling s in SamplingCollection)
            {
                listQV.Add(new SamplingQuickView(s.RowID, s.AOI, s.ReferenceNumber, s.DateTimeSampled, s.WeightOfCatch, s.Gear, s.LandingSite, s.FishingGround));
            }
            return listQV;
        }

        /// <summary>
        /// returns a list of samplings specific to a gear class
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        public List<Sampling> GetAllSamplings(GearClass gc)
        {
            return SamplingCollection
               .Where(t => t.Gear.GearClass.GearClassGuid == gc.GearClassGuid).ToList();

        }

        /// <summary>
        /// returns a list of samplings specific to an enumerator
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        public List<Sampling> GetAllSamplings(SamplingEnumerator se)
        {
            return SamplingCollection
               .Where(t => t.SamplingEnumerator.EnumeratorID == se.EnumeratorID).ToList();

        }


        /// <summary>
        /// returns sampling specified by its rowID identifier
        /// </summary>
        /// <param name="rowID"></param>
        /// <returns></returns>
        public Sampling GetSampling(string rowID)
        {
            return SamplingCollection.FirstOrDefault(n => n.RowID == rowID);

        }

        public int SamplingCountByAOI(AOI aoi)
        {
            return SamplingCollection.Where(t => t.AOI.AOIGuid == aoi.AOIGuid).Count();
        }

        public (int Min,int Max) SerialNumberRange(AOI aoi)
        {
            return (SerialNumberMinima(aoi), SerialNumberMaxima(aoi));
        }
        public List<Sampling> GetAllSamplings()
        {
            return SamplingCollection.ToList();
        }

        private void SamplingCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        int newIndex = e.NewStartingIndex;
                        Sampling editedEntity = SamplingCollection[newIndex];
                        if (Samplings.Add(editedEntity))
                        {
                            EditedEntity = new EditedEntity(EditAction.Add, editedEntity);
                            AddSucceeded = true;
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    {
                        List<Sampling> tempListOfRemovedItems = e.OldItems.OfType<Sampling>().ToList();
                        Sampling editedEntity = tempListOfRemovedItems[0];
                        if (Samplings.Delete(editedEntity.RowID))
                            EditedEntity = new EditedEntity(EditAction.Delete, editedEntity);
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    {
                        List<Sampling> tempListOfSamplings = e.NewItems.OfType<Sampling>().ToList();
                        Sampling editedEntity = tempListOfSamplings[0];
                        if (Samplings.Update(editedEntity))      // As the IDs are unique, only one row will be effected hence first index only
                            EditedEntity = new EditedEntity(EditAction.Update, editedEntity);
                    }
                    break;
            }
        }

        public bool AddRecordToRepo(Sampling s)
        {
            EditedEntity = null;
            if (s == null)
                throw new ArgumentNullException("Error: The argument is Null");
            SamplingCollection.Add(s);
            return AddSucceeded;
        }

        public void UpdateRecordInRepo(Sampling s)
        {
            EditedEntity = null;
            if (s.RowID == null)
                throw new Exception("Error: ID cannot be null");

            int index = 0;
            while (index < SamplingCollection.Count)
            {
                if (SamplingCollection[index].RowID == s.RowID)
                {
                    SamplingCollection[index] = s;
                    break;
                }
                index++;
            }
        }

        public void DeleteRecordFromRepo(string id)
        {
            EditedEntity = null;
            if (id == null)
                throw new Exception("Record ID cannot be null");

            int index = 0;
            while (index < SamplingCollection.Count)
            {
                if (SamplingCollection[index].RowID == id)
                {
                    SamplingCollection.RemoveAt(index);
                    break;
                }
                index++;
            }
        }

        public bool EntityValidated(Sampling sampling, out List<EntityValidationMessage> messages)//, bool isNew = false)
        {

            messages = new List<EntityValidationMessage>();



            if (sampling.LandingSite == null)
                messages.Add(new EntityValidationMessage("Landing site cannot be empty"));


            if (sampling.Gear == null)
                messages.Add(new EntityValidationMessage("Gear used cannot be empty"));

            return messages.Count == 0;
        }
    }
}
