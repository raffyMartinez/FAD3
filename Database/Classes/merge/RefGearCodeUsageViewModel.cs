using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
namespace FAD3.Database.Classes.merge
{
   public class RefGearCodeUsageViewModel
    {
        public bool AddSucceeded { get; set; }
        public ObservableCollection<RefGearCodeUsage> RefGearCodeUsageCollection { get; set; }
        private RefGearCodeUsageRepository RefGearCodeUsages { get; set; }



        public RefGearCodeUsageViewModel(FADEntities fadEntities)
        {
            RefGearCodeUsages = new RefGearCodeUsageRepository(fadEntities);
            RefGearCodeUsageCollection = new ObservableCollection<RefGearCodeUsage>(RefGearCodeUsages.RefGearCodeUsages);
            RefGearCodeUsageCollection.CollectionChanged += CatchComposition_CollectionChanged;
        }

        public RefGearCodeUsage GetRefGearCodeUsage(string guid)
        {
            return RefGearCodeUsageCollection.FirstOrDefault(n => n.RowNumber == guid);

        }
        private void CatchComposition_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        int newIndex = e.NewStartingIndex;
                        AddSucceeded = RefGearCodeUsages.Add(RefGearCodeUsageCollection[newIndex]);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    {
                        List<RefGearCodeUsage> tempListOfRemovedItems = e.OldItems.OfType<RefGearCodeUsage>().ToList();
                        RefGearCodeUsages.Delete(tempListOfRemovedItems[0].RowNumber);
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    {
                        List<RefGearCodeUsage> tempList = e.NewItems.OfType<RefGearCodeUsage>().ToList();
                        RefGearCodeUsages.Update(tempList[0]);      // As the IDs are unique, only one row will be effected hence first index only
                    }
                    break;
            }
        }

        public int Count
        {
            get { return RefGearCodeUsageCollection.Count; }
        }

        public bool AddRecordToRepo(RefGearCodeUsage usage)
        {
            if (usage == null)
                throw new ArgumentNullException("Error: The argument is Null");
            RefGearCodeUsageCollection.Add(usage);
            return AddSucceeded;
        }

        public void UpdateRecordInRepo(RefGearCodeUsage usage)
        {
            if (usage.RowNumber == null)
                throw new Exception("Error: ID cannot be null");

            int index = 0;
            while (index < RefGearCodeUsageCollection.Count)
            {
                if (RefGearCodeUsageCollection[index].RowNumber == usage.RowNumber)
                {
                    RefGearCodeUsageCollection[index] = usage;
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
            while (index < RefGearCodeUsageCollection.Count)
            {
                if (RefGearCodeUsageCollection[index].RowNumber == id)
                {
                    RefGearCodeUsageCollection.RemoveAt(index);
                    break;
                }
                index++;
            }
        }
    }
}
