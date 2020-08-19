using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
namespace FAD3.Database.Classes.merge
{
    public class CatchDetailViewModel
    {
        public bool AddSucceeded { get; set; }
        public ObservableCollection<CatchDetail> CatchDetailCollection { get; set; }
        private CatchDetailRepository CatchDetails { get; set; }

        public double TotalWtOfCactchComposition(string samplingGUID)
        {
            double wt = 0;
            foreach(var item in CatchDetailCollection.Where(t => t.CatchComposition.SamplingID == samplingGUID))
            {
                wt += item.Weight;
            }
            return wt;
        }

        public double WeightFromTotal(string samplingGuid)
        {
            double wt = 0;
            foreach (var item in CatchDetailCollection
                .Where(t => t.CatchComposition.SamplingID == samplingGuid)
                .Where(t=>t.FromTotal))
            {
                wt += item.Weight;
            }
            return wt;
        }
        public List<CatchDetailFlattened>GetFlattened(List<int> years, string aoiGUID)
        {

            return CatchDetails.getCatchDetailsFlattened(years,aoiGUID);
        }

        public List<CatchDetail> GetCatchDetails(string samplingGuid)
        {
            return CatchDetailCollection.Where(t => t.CatchComposition.SamplingID == samplingGuid).ToList();
        }
        public CatchDetailViewModel(FADEntities fadEntities)
        {
            CatchDetails = new CatchDetailRepository(fadEntities);
            CatchDetailCollection = new ObservableCollection<CatchDetail>(CatchDetails.CatchDetails);
            CatchDetailCollection.CollectionChanged += CatchDetails_CollectionChanged;
        }
        public List<CatchDetail> GetAllCatchDetails()
        {
            return CatchDetailCollection.ToList();
        }

        public CatchDetail getCatchDetail(string id)
        {
            return CatchDetailCollection.FirstOrDefault(n => n.RowGUID == id);

        }
        private void CatchDetails_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        int newIndex = e.NewStartingIndex;
                        AddSucceeded =  CatchDetails.Add(CatchDetailCollection[newIndex]);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    {
                        List<CatchDetail> tempListOfRemovedItems = e.OldItems.OfType<CatchDetail>().ToList();
                        CatchDetails.Delete(tempListOfRemovedItems[0].RowGUID);
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    {
                        List<CatchDetail> tempList = e.NewItems.OfType<CatchDetail>().ToList();
                        CatchDetails.Update(tempList[0]);      // As the IDs are unique, only one row will be effected hence first index only
                    }
                    break;
            }
        }

        public int Count
        {
            get { return CatchDetailCollection.Count; }
        }

        public bool  AddRecordToRepo(CatchDetail item)
        {
            if (item == null)
                throw new ArgumentNullException("Error: The argument is Null");
            CatchDetailCollection.Add(item);
            return AddSucceeded;
        }

        public void UpdateRecordInRepo(CatchDetail item)
        {
            if (item.RowGUID == null)
                throw new Exception("Error: ID cannot be null");

            int index = 0;
            while (index < CatchDetailCollection.Count)
            {
                if (CatchDetailCollection[index].RowGUID == item.RowGUID)
                {
                    CatchDetailCollection[index] = item;
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
            while (index < CatchDetailCollection.Count)
            {
                if (CatchDetailCollection[index].RowGUID == id)
                {
                    CatchDetailCollection.RemoveAt(index);
                    break;
                }
                index++;
            }
        }
    }
}
