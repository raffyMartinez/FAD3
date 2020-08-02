using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace FAD3.Database.Classes.merge
{
    public class FishingExpenseViewModel
    {
        public bool AddSucceeded { get; set; }
        public ObservableCollection<FishingExpense> FishingExpenseCollection { get; set; }
        private FishingExpenseRepository FishingExpenses { get; set; }



        public FishingExpenseViewModel(FADEntities fadEntities)
        {
            FishingExpenses = new FishingExpenseRepository(fadEntities);
            FishingExpenseCollection = new ObservableCollection<FishingExpense>(FishingExpenses.FishingExpenses);
            FishingExpenseCollection.CollectionChanged += FishingExpenses_CollectionChanged;
        }
        public List<FishingExpense> GetAllFishingExpenseItems()
        {
            return FishingExpenseCollection.ToList();
        }

        public FishingExpense getFishingExpense(string id)
        {
            return FishingExpenseCollection.FirstOrDefault(n => n.SamplingID == id);

        }
        private void FishingExpenses_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        int newIndex = e.NewStartingIndex;
                        AddSucceeded = FishingExpenses.Add(FishingExpenseCollection[newIndex]);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    {
                        List<FishingExpense> tempListOfRemovedItems = e.OldItems.OfType<FishingExpense>().ToList();
                        FishingExpenses.Delete(tempListOfRemovedItems[0].SamplingID);
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    {
                        List<FishingExpense> tempList = e.NewItems.OfType<FishingExpense>().ToList();
                        FishingExpenses.Update(tempList[0]);      // As the IDs are unique, only one row will be effected hence first index only
                    }
                    break;
            }
        }

        public int Count
        {
            get { return FishingExpenseCollection.Count; }
        }

        public bool AddRecordToRepo(FishingExpense item)
        {
            if (item == null)
                throw new ArgumentNullException("Error: The argument is Null");
            FishingExpenseCollection.Add(item);
            return AddSucceeded;
        }

        public void UpdateRecordInRepo(FishingExpense item)
        {
            if (item.SamplingID == null)
                throw new Exception("Error: ID cannot be null");

            int index = 0;
            while (index < FishingExpenseCollection.Count)
            {
                if (FishingExpenseCollection[index].SamplingID == item.SamplingID)
                {
                    FishingExpenseCollection[index] = item;
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
            while (index < FishingExpenseCollection.Count)
            {
                if (FishingExpenseCollection[index].SamplingID == id)
                {
                    FishingExpenseCollection.RemoveAt(index);
                    break;
                }
                index++;
            }
        }
    }
}
