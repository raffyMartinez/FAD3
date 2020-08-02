using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAD3.Database.Classes.merge

{
    public class LandingSiteViewModel
    {
        public bool AddSucceeded { get; set; }
        public ObservableCollection<LandingSite> LandingSiteCollection { get; set; }
        private LandingSiteRepository LandingSites { get; set; }

        public event EventHandler<EntityChangedEventArgs> EntityChanged;

        public LandingSiteViewModel(FADEntities fadEntities)
        {

            LandingSites = new LandingSiteRepository(fadEntities);
            LandingSiteCollection = new ObservableCollection<LandingSite>(LandingSites.LandingSites);
            LandingSiteCollection.CollectionChanged += LandingSiteCollection_CollectionChanged;
        }

        public int Count

        {
            get { return LandingSiteCollection.Count; }
        }

        public int CountByAOI(AOI aoi)
        {
            return LandingSiteCollection.Count(t => t.AOI.AOIGuid == aoi.AOIGuid);
        }
        public LandingSite GetEqual(LandingSite ls)
        {
            return LandingSiteCollection.FirstOrDefault(n => n.Equals(ls));
        }
        public bool NameExists(string landingSiteName)
        {
            foreach (LandingSite ls in LandingSiteCollection)
            {
                if (ls.LandingSiteName == landingSiteName)
                {
                    return true;
                }
            }
            return false;
        }

        public LandingSite GetLandingSite(string landingSiteID)
        {
            return LandingSiteCollection.FirstOrDefault(n => n.LandingSiteGuid == landingSiteID);

        }

        public List<LandingSite> GetAllLandingSites()
        {
            return LandingSiteCollection.ToList();
        }

        private void LandingSiteCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            LandingSite editedLandingSite=new LandingSite();
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        int newIndex = e.NewStartingIndex;
                        editedLandingSite = LandingSiteCollection[newIndex];
                        AddSucceeded= LandingSites.Add(editedLandingSite);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    {
                        List<LandingSite> tempListOfRemovedItems = e.OldItems.OfType<LandingSite>().ToList();
                        editedLandingSite = tempListOfRemovedItems[0];
                        LandingSites.Delete(editedLandingSite.LandingSiteGuid);
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    {
                        List<LandingSite> tempListOfLandingSites = e.NewItems.OfType<LandingSite>().ToList();
                        editedLandingSite = tempListOfLandingSites[0];
                        LandingSites.Update(editedLandingSite);      // As the IDs are unique, only one row will be effected hence first index only
                    }
                    break;
            }
            EntityChangedEventArgs args = new EntityChangedEventArgs(editedLandingSite.GetType().Name,editedLandingSite);
            EntityChanged?.Invoke(this, args);
        }

        public bool AddRecordToRepo(LandingSite ls)
        {
            if (ls == null)
                throw new ArgumentNullException("Error: The argument is Null");

            LandingSiteCollection.Add(ls);
            return AddSucceeded;
        }

        public void UpdateRecordInRepo(LandingSite ls)
        {
            if (ls.LandingSiteGuid == null)
                throw new Exception("Error: ID cannot be null");

            int index = 0;
            while (index < LandingSiteCollection.Count)
            {
                if (LandingSiteCollection[index].LandingSiteGuid == ls.LandingSiteGuid)
                {
                    LandingSiteCollection[index] = ls;
                    break;
                }
                index++;
            }
        }

        public void DeleteRecordFromRepo(string id)
        {
            if (id == null)
                throw new Exception("Record ID cannot be null");

            int index = 0;
            while (index < LandingSiteCollection.Count)
            {
                if (LandingSiteCollection[index].LandingSiteGuid == id)
                {
                    LandingSiteCollection.RemoveAt(index);
                    break;
                }
                index++;
            }
        }

        public bool EntityValidated(LandingSite landingSite, out List<string> messages, bool isNew = false)
        {

            messages = new List<string>();

            if (landingSite.LandingSiteName.Length < 5)
                messages.Add("Landing site's name must be at least 5 characters long");


            if (landingSite.Municipality == null)
                messages.Add("Municipality cannot be empty");

            return messages.Count == 0;
        }
    }
}
