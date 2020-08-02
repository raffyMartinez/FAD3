using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
namespace FAD3.Database.Classes.merge
{
    public class AdditionalExtentViewModel
    {
        public bool AddSucceeded { get; set; }
        public ObservableCollection<AdditionalExtent> AdditionalExtentCollection { get; set; }
        private AdditionalExtentRepository AdditionalExtents { get; set; }



        public AdditionalExtentViewModel(FADEntities fadEntities)
        {
            AdditionalExtents = new AdditionalExtentRepository(fadEntities);
            AdditionalExtentCollection = new ObservableCollection<AdditionalExtent>(AdditionalExtents.AdditionalExtents);
            AdditionalExtentCollection.CollectionChanged += AdditionalExtents_CollectionChanged;
        }

        public AdditionalExtent GetAdditionalExtent(string guid)
        {
            return AdditionalExtentCollection.FirstOrDefault(n => n.RowID == guid);

        }
        private void AdditionalExtents_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        int newIndex = e.NewStartingIndex;
                        AddSucceeded = AdditionalExtents.Add(AdditionalExtentCollection[newIndex]);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    {
                        List<AdditionalExtent> tempListOfRemovedItems = e.OldItems.OfType<AdditionalExtent>().ToList();
                        AdditionalExtents.Delete(tempListOfRemovedItems[0].RowID);
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    {
                        List<AdditionalExtent> tempList = e.NewItems.OfType<AdditionalExtent>().ToList();
                        AdditionalExtents.Update(tempList[0]);      // As the IDs are unique, only one row will be effected hence first index only
                    }
                    break;
            }
        }

        public int Count
        {
            get { return AdditionalExtentCollection.Count; }
        }

        public bool AddRecordToRepo(AdditionalExtent item)
        {
            if (item == null)
                throw new ArgumentNullException("Error: The argument is Null");
            AdditionalExtentCollection.Add(item);
            return AddSucceeded;
        }

        public void UpdateRecordInRepo(AdditionalExtent item)
        {
            if (item.RowID== null)
                throw new Exception("Error: ID cannot be null");

            int index = 0;
            while (index < AdditionalExtentCollection.Count)
            {
                if (AdditionalExtentCollection[index].RowID == item.RowID)
                {
                    AdditionalExtentCollection[index] = item;
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
            while (index < AdditionalExtentCollection.Count)
            {
                if (AdditionalExtentCollection[index].RowID == id)
                {
                    AdditionalExtentCollection.RemoveAt(index);
                    break;
                }
                index++;
            }
        }
    }
}
