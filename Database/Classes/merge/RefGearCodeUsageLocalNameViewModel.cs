using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace FAD3.Database.Classes.merge
{
   public class RefGearCodeUsageLocalNameViewModel
    {
        public bool AddSucceeded { get; set; }
        public ObservableCollection<RefGearCodeUsageLocalName> RefGearCodeUsageLocalNameCollection { get; set; }
        private RefGearCodeUsageLocalNameRepository RefGearCodeUsageLocalNames { get; set; }



        public RefGearCodeUsageLocalNameViewModel(FADEntities fadEntities)
        {
            RefGearCodeUsageLocalNames = new RefGearCodeUsageLocalNameRepository(fadEntities);
            RefGearCodeUsageLocalNameCollection = new ObservableCollection<RefGearCodeUsageLocalName>(RefGearCodeUsageLocalNames.RefGearCodeUsageLocalNames);
            RefGearCodeUsageLocalNameCollection.CollectionChanged += RefGearCodeUsageLocalNames_CollectionChanged;
        }

        public RefGearCodeUsageLocalName GetUsageLocalName(string rowID)
        {
            return RefGearCodeUsageLocalNameCollection.FirstOrDefault(n => n.RowID == rowID);

        }
        private void RefGearCodeUsageLocalNames_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        int newIndex = e.NewStartingIndex;
                        AddSucceeded = RefGearCodeUsageLocalNames.Add(RefGearCodeUsageLocalNameCollection[newIndex]);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    {
                        List<RefGearCodeUsageLocalName> tempListOfRemovedItems = e.OldItems.OfType<RefGearCodeUsageLocalName>().ToList();
                        RefGearCodeUsageLocalNames.Delete(tempListOfRemovedItems[0].RowID);
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    {
                        List<RefGearCodeUsageLocalName> tempList = e.NewItems.OfType<RefGearCodeUsageLocalName>().ToList();
                        RefGearCodeUsageLocalNames.Update(tempList[0]);      // As the IDs are unique, only one row will be effected hence first index only
                    }
                    break;
            }
        }

        public int Count
        {
            get { return RefGearCodeUsageLocalNameCollection.Count; }
        }

        public bool AddRecordToRepo(RefGearCodeUsageLocalName item)
        {
            if (item == null)
                throw new ArgumentNullException("Error: The argument is Null");
            RefGearCodeUsageLocalNameCollection.Add(item);
            return AddSucceeded;
        }

        public void UpdateRecordInRepo(RefGearCodeUsageLocalName item)
        {
            if (item.RowID == null)
                throw new Exception("Error: ID cannot be null");

            int index = 0;
            while (index < RefGearCodeUsageLocalNameCollection.Count)
            {
                if (RefGearCodeUsageLocalNameCollection[index].RowID == item.RowID)
                {
                    RefGearCodeUsageLocalNameCollection[index] = item;
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
            while (index < RefGearCodeUsageLocalNameCollection.Count)
            {
                if (RefGearCodeUsageLocalNameCollection[index].RowID == id)
                {
                    RefGearCodeUsageLocalNameCollection.RemoveAt(index);
                    break;
                }
                index++;
            }
        }
    }
}
