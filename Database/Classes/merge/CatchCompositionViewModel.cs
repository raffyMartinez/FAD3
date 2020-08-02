using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
namespace FAD3.Database.Classes.merge
{
   public class CatchCompositionViewModel
    {
        public bool AddSucceeded { get; set; }
        public ObservableCollection<CatchComposition> CatchCompositionCollection { get; set; }
        private CatchCompositionRepository CatchCompositions { get; set; }



        public CatchCompositionViewModel(FADEntities fadEntities)
        {
            CatchCompositions = new CatchCompositionRepository(fadEntities);
            CatchCompositionCollection = new ObservableCollection<CatchComposition>(CatchCompositions.CatchCompositions);
            CatchCompositionCollection.CollectionChanged += CatchComposition_CollectionChanged;
        }

        public CatchComposition GetCatchComposition(string guid)
        {
            return CatchCompositionCollection.FirstOrDefault(n => n.RowGUID == guid);

        }
        private void CatchComposition_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        int newIndex = e.NewStartingIndex;
                        AddSucceeded= CatchCompositions.Add(CatchCompositionCollection[newIndex]);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    {
                        List<CatchComposition> tempListOfRemovedItems = e.OldItems.OfType<CatchComposition>().ToList();
                        CatchCompositions.Delete(tempListOfRemovedItems[0].RowGUID);
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    {
                        List<CatchComposition> tempList = e.NewItems.OfType<CatchComposition>().ToList();
                        CatchCompositions.Update(tempList[0]);      // As the IDs are unique, only one row will be effected hence first index only
                    }
                    break;
            }
        }

        public int Count
        {
            get { return CatchCompositionCollection.Count; }
        }

        public bool AddRecordToRepo(CatchComposition cc)
        {
            if (cc == null)
                throw new ArgumentNullException("Error: The argument is Null");
            CatchCompositionCollection.Add(cc);
            return AddSucceeded;
        }

        public void UpdateRecordInRepo(CatchComposition cc)
        {
            if (cc.RowGUID == null)
                throw new Exception("Error: ID cannot be null");

            int index = 0;
            while (index < CatchCompositionCollection.Count)
            {
                if (CatchCompositionCollection[index].RowGUID == cc.RowGUID)
                {
                    CatchCompositionCollection[index] = cc;
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
            while (index < CatchCompositionCollection.Count)
            {
                if (CatchCompositionCollection[index].RowGUID == id)
                {
                    CatchCompositionCollection.RemoveAt(index);
                    break;
                }
                index++;
            }
        }
    }
}
