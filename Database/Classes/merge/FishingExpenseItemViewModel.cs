using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
namespace FAD3.Database.Classes.merge
{
    public class FishingExpenseItemViewModel
    {
        public bool AddSucceeded { get; set; }
        public ObservableCollection<FishingExpenseItem> FishingExpenseItemCollection { get; set; }
        private FishingExpenseItemRepository FishingExpenseItems { get; set; }



        public FishingExpenseItemViewModel(FADEntities fadEntities)
        {
            FishingExpenseItems = new FishingExpenseItemRepository(fadEntities);
            FishingExpenseItemCollection = new ObservableCollection<FishingExpenseItem>(FishingExpenseItems.FishingExpenseItems);
            FishingExpenseItemCollection.CollectionChanged += FishingExpenseItem_CollectionChanged;
        }
        public List<FishingExpenseItem> GetAllFishingExpenseItems()
        {
            return FishingExpenseItemCollection.ToList();
        }

        public FishingExpenseItem getFishingExpenseItem(string id)
        {
            return FishingExpenseItemCollection.FirstOrDefault(n => n.ExpenseRowID == id);

        }
        private void FishingExpenseItem_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        int newIndex = e.NewStartingIndex;
                        AddSucceeded = FishingExpenseItems.Add(FishingExpenseItemCollection[newIndex]);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    {
                        List<FishingExpenseItem> tempListOfRemovedItems = e.OldItems.OfType<FishingExpenseItem>().ToList();
                        FishingExpenseItems.Delete(tempListOfRemovedItems[0].ExpenseRowID);
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    {
                        List<FishingExpenseItem> tempList = e.NewItems.OfType<FishingExpenseItem>().ToList();
                        FishingExpenseItems.Update(tempList[0]);      // As the IDs are unique, only one row will be effected hence first index only
                    }
                    break;
            }
        }

        public int Count
        {
            get { return FishingExpenseItemCollection.Count; }
        }

        public bool AddRecordToRepo(FishingExpenseItem item)
        {
            if (item == null)
                throw new ArgumentNullException("Error: The argument is Null");
            FishingExpenseItemCollection.Add(item);
            return AddSucceeded;
        }

        public void UpdateRecordInRepo(FishingExpenseItem item)
        {
            if (item.ExpenseRowID == null)
                throw new Exception("Error: ID cannot be null");

            int index = 0;
            while (index < FishingExpenseItemCollection.Count)
            {
                if (FishingExpenseItemCollection[index].ExpenseRowID == item.ExpenseRowID)
                {
                    FishingExpenseItemCollection[index] = item;
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
            while (index < FishingExpenseItemCollection.Count)
            {
                if (FishingExpenseItemCollection[index].ExpenseRowID == id)
                {
                    FishingExpenseItemCollection.RemoveAt(index);
                    break;
                }
                index++;
            }
        }
    }
}
