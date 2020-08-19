using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
namespace FAD3.Database.Classes.merge
{
    public class LenFreqViewModel
    {

        public bool AddSucceeded { get; set; }
        public ObservableCollection<LenFreq> LenFreqCollection{ get; set; }
        private LenFreqRepository LenFreqs{ get; set; }




        public LenFreqViewModel(FADEntities fadEntities)
        {
            LenFreqs = new LenFreqRepository(fadEntities);
            LenFreqCollection = new ObservableCollection<LenFreq>(LenFreqs.LenFreqs);
            LenFreqCollection.CollectionChanged += LenFreqs_CollectionChanged;
        }
        public List<LenFreq> GetAllLenFreq()
        {
            return LenFreqCollection.ToList();
        }
        public List<LenFreqFlattened> GetFlattened(List<int> years, string aoiGUID)
        {

            return LenFreqs.getFlattened(years, aoiGUID);
        }
        public LenFreq getLenFreq(string id)
        {
            return LenFreqCollection.FirstOrDefault(n => n.RowGUID == id);

        }
        private void LenFreqs_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        int newIndex = e.NewStartingIndex;
                       AddSucceeded= LenFreqs.Add(LenFreqCollection[newIndex]);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    {
                        List<LenFreq> tempListOfRemovedItems = e.OldItems.OfType<LenFreq>().ToList();
                        LenFreqs.Delete(tempListOfRemovedItems[0].RowGUID);
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    {
                        List<LenFreq> tempList = e.NewItems.OfType<LenFreq>().ToList();
                        LenFreqs.Update(tempList[0]);      // As the IDs are unique, only one row will be effected hence first index only
                    }
                    break;
            }
        }

        public int Count
        {
            get { return LenFreqCollection.Count; }
        }

        public bool AddRecordToRepo(LenFreq item)
        {
            if (item == null)
                throw new ArgumentNullException("Error: The argument is Null");
            LenFreqCollection.Add(item);
            return AddSucceeded;
        }

        public void UpdateRecordInRepo(LenFreq item)
        {
            if (item.RowGUID == null)
                throw new Exception("Error: ID cannot be null");

            int index = 0;
            while (index < LenFreqCollection.Count)
            {
                if (LenFreqCollection[index].RowGUID == item.RowGUID)
                {
                    LenFreqCollection[index] = item;
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
            while (index < LenFreqCollection.Count)
            {
                if (LenFreqCollection[index].RowGUID == id)
                {
                    LenFreqCollection.RemoveAt(index);
                    break;
                }
                index++;
            }
        }
    }
}
