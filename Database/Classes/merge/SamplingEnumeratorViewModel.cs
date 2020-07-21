using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace FAD3.Database.Classes.merge
{
    public class SamplingEnumeratorViewModel
    {
        public ObservableCollection<SamplingEnumerator> SamplingEnumeratorCollection { get; set; }
        private SamplingEnumeratorRepository SamplingEnumerators{ get; set; }



        public SamplingEnumeratorViewModel()
        {
            SamplingEnumerators = new SamplingEnumeratorRepository();
            SamplingEnumeratorCollection = new ObservableCollection<SamplingEnumerator>(SamplingEnumerators.SamplingEnumerators);
            SamplingEnumeratorCollection.CollectionChanged += SamplingEnumeratorCollection_CollectionChanged;
        }
        public List<SamplingEnumerator> GetAllSamplingENumerators()
        {
            return SamplingEnumeratorCollection.ToList();
        }
        public bool CanDeleteEntity(SamplingEnumerator se)
        {
            return FADEntities.SamplingViewModel.SamplingCollection
                .Where(t => t.SamplingEnumerator.EnumeratorID == se.EnumeratorID).ToList().Count == 0;
        }

        public SamplingEnumerator GetSamplingEnumerator(string id)
        {
            return SamplingEnumeratorCollection.FirstOrDefault(n => n.EnumeratorID== id);

        }
        private void SamplingEnumeratorCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        int newIndex = e.NewStartingIndex;
                        SamplingEnumerators.Add(SamplingEnumeratorCollection[newIndex]);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    {
                        List<SamplingEnumerator> tempListOfRemovedItems = e.OldItems.OfType<SamplingEnumerator>().ToList();
                        SamplingEnumerators.Delete(tempListOfRemovedItems[0].EnumeratorID);
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    {
                        List<SamplingEnumerator> tempList = e.NewItems.OfType<SamplingEnumerator>().ToList();
                        SamplingEnumerators.Update(tempList[0]);      // As the IDs are unique, only one row will be effected hence first index only
                    }
                    break;
            }
        }

        public int Count
        {
            get { return SamplingEnumeratorCollection.Count; }
        }

        public void AddRecordToRepo(SamplingEnumerator se)
        {
            if (se == null)
                throw new ArgumentNullException("Error: The argument is Null");
            SamplingEnumeratorCollection.Add(se);
        }

        public void UpdateRecordInRepo(SamplingEnumerator se)
        {
            if (se.EnumeratorID == null)
                throw new Exception("Error: ID cannot be null");

            int index = 0;
            while (index < SamplingEnumeratorCollection.Count)
            {
                if (SamplingEnumeratorCollection[index].EnumeratorID == se.EnumeratorID)
                {
                    SamplingEnumeratorCollection[index] = se;
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
            while (index < SamplingEnumeratorCollection.Count)
            {
                if (SamplingEnumeratorCollection[index].EnumeratorID == id)
                {
                    SamplingEnumeratorCollection.RemoveAt(index);
                    break;
                }
                index++;
            }
        }
    }
}
