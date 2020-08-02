using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;


namespace FAD3.Database.Classes.merge
{
    public class AOIViewModel
    {
        public bool AddSucceeded { get; set; }
        public ObservableCollection<AOI> AOICollection { get; set; }
        private AOIRepository AOIs { get; set; }

        public AOIViewModel(FADEntities fadEntitites)
        {
            AOIs = new AOIRepository(fadEntitites);
            AOICollection = new ObservableCollection<AOI>(AOIs.AOIs);
            AOICollection.CollectionChanged += AOIs_CollectionChanged;
        }

        private void AOIs_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    int newIndex = e.NewStartingIndex;
                    AddSucceeded= AOIs.Add(AOICollection[newIndex]);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    List<AOI> tempListOfRemovedItems = e.OldItems.OfType<AOI>().ToList();
                    AOIs.Delete(tempListOfRemovedItems[0].AOIGuid);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    List<AOI> tempListOfAOIs = e.NewItems.OfType<AOI>().ToList();
                    AOIs.Update(tempListOfAOIs[0]);      // As the IDs are unique, only one row will be effected hence first index only
                    break;
            }
        }
        public List<AOI> GetAllAOIs()
        {
            return AOICollection.ToList();
        }

        public bool AOIExists(AOI aoi)
        {
            return AOICollection.FirstOrDefault(n => n.Equals(aoi)) != null;
        }

        public AOI GetEqual(AOI aoi)
        {
            return AOICollection.FirstOrDefault(n => n.Equals(aoi));
        }

        public bool NameExists(string name)
        {
            foreach (var aoi in AOICollection)
            {
                if (aoi.AOIName == name)
                    return true;
            }

            return false;
        }
        public int Count
        {
            get { return AOICollection.Count; }
        }

        public AOI GetAOI(string id)
        {
            return AOICollection.FirstOrDefault(n => n.AOIGuid == id);
        }

        public bool EntityValidated(AOI aoi, EntityValidationMessage evm)
        {
            return false;
        }
        public bool AddRecordToRepo(AOI aoi)
        {
            if (aoi == null)
                throw new ArgumentNullException("Error: The argument is Null");
            
            AOICollection.Add(aoi);
            return AddSucceeded;
        }

        public void UpdateRecordInRepo(AOI aoi)
        {
            if (aoi.AOIGuid == null)
                throw new Exception("Error: ID cannot be null");

            int index = 0;
            while (index < AOICollection.Count)
            {
                if (AOICollection[index].AOIGuid == aoi.AOIGuid)
                {
                    AOICollection[index] = aoi;
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
            while (index < AOICollection.Count)
            {
                if (AOICollection[index].AOIGuid == id)
                {
                    AOICollection.RemoveAt(index);
                    break;
                }
                index++;
            }
        }

    }
}
