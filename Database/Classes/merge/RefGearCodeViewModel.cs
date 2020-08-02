using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace FAD3.Database.Classes.merge
{
   public class RefGearCodeViewModel
    {
        public bool AddSucceeded { get; set; }
        public ObservableCollection<RefGearCode> RefGearCodeCollection { get; set; }
        private RefGearCodeRepository RefGearCodes { get; set; }




        public RefGearCodeViewModel(FADEntities fadEntities)
        {
            RefGearCodes = new RefGearCodeRepository(fadEntities);
            RefGearCodeCollection = new ObservableCollection<RefGearCode>(RefGearCodes.RefGearCodes);
            RefGearCodeCollection.CollectionChanged += RefGearCodes_CollectionChanged;
        }
        public List<RefGearCode> GetAllRefGearCodes()
        {
            return RefGearCodeCollection.ToList();
        }

        public RefGearCode GetRefGearCode(string code)
        {
            return RefGearCodeCollection.FirstOrDefault(n => n.GearCode == code);

        }
        private void RefGearCodes_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        int newIndex = e.NewStartingIndex;
                        AddSucceeded = RefGearCodes.Add(RefGearCodeCollection[newIndex]);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    {
                        List<RefGearCode> tempListOfRemovedItems = e.OldItems.OfType<RefGearCode>().ToList();
                        RefGearCodes.Delete(tempListOfRemovedItems[0].GearCode);
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    {
                        List<RefGearCode> tempList = e.NewItems.OfType<RefGearCode>().ToList();
                        RefGearCodes.Update(tempList[0]);      // As the IDs are unique, only one row will be effected hence first index only
                    }
                    break;
            }
        }

        public int Count
        {
            get { return RefGearCodeCollection.Count; }
        }

        public bool AddRecordToRepo(RefGearCode item)
        {
            if (item == null)
                throw new ArgumentNullException("Error: The argument is Null");
            RefGearCodeCollection.Add(item);
            return AddSucceeded;
        }

        public void UpdateRecordInRepo(RefGearCode item)
        {
            if (item.GearCode == null)
                throw new Exception("Error: ID cannot be null");

            int index = 0;
            while (index < RefGearCodeCollection.Count)
            {
                if (RefGearCodeCollection[index].GearCode == item.GearCode)
                {
                    RefGearCodeCollection[index] = item;
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
            while (index < RefGearCodeCollection.Count)
            {
                if (RefGearCodeCollection[index].GearCode == id)
                {
                    RefGearCodeCollection.RemoveAt(index);
                    break;
                }
                index++;
            }
        }
    }
}
