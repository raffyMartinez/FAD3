using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace FAD3.Database.Classes.merge
{
   public class GonadMaturityStageViewModel
    {
        public bool AddSucceeded { get; set; }
        public ObservableCollection<GonadalMaturiryStage> GonadalMaturiryStageCollection { get; set; }
        private GonadalMaturityStageRepository GonadalMaturiryStages { get; set; }



        public GonadMaturityStageViewModel(FADEntities fadEntities)
        {
            GonadalMaturiryStages = new GonadalMaturityStageRepository(fadEntities);
            GonadalMaturiryStageCollection = new ObservableCollection<GonadalMaturiryStage>(GonadalMaturiryStages.GonadalMaturiryStages);
            GonadalMaturiryStageCollection.CollectionChanged += GonadalMaturiryStages_CollectionChanged;
        }

        public List<GonadalMaturiryStage>GetAllGonadalMaturityStages()
        {
            return GonadalMaturiryStageCollection.ToList();
        }
        public GonadalMaturiryStage GetGonadalMaturityState(string guid)
        {
            return GonadalMaturiryStageCollection.FirstOrDefault(n => n.RowGUID == guid);

        }
        private void GonadalMaturiryStages_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        int newIndex = e.NewStartingIndex;
                       AddSucceeded= GonadalMaturiryStages.Add(GonadalMaturiryStageCollection[newIndex]);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    {
                        List<GonadalMaturiryStage> tempListOfRemovedItems = e.OldItems.OfType<GonadalMaturiryStage>().ToList();
                        GonadalMaturiryStages.Delete(tempListOfRemovedItems[0].RowGUID);
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    {
                        List<GonadalMaturiryStage> tempList = e.NewItems.OfType<GonadalMaturiryStage>().ToList();
                        GonadalMaturiryStages.Update(tempList[0]);      // As the IDs are unique, only one row will be effected hence first index only
                    }
                    break;
            }
        }

        public int Count
        {
            get { return GonadalMaturiryStageCollection.Count; }
        }

        public bool AddRecordToRepo(GonadalMaturiryStage gms)
        {
            if (gms == null)
                throw new ArgumentNullException("Error: The argument is Null");
            GonadalMaturiryStageCollection.Add(gms);
            return AddSucceeded;
        }

        public void UpdateRecordInRepo(GonadalMaturiryStage gms)
        {
            if (gms.RowGUID== null)
                throw new Exception("Error: ID cannot be null");

            int index = 0;
            while (index < GonadalMaturiryStageCollection.Count)
            {
                if (GonadalMaturiryStageCollection[index].RowGUID == gms.RowGUID)
                {
                    GonadalMaturiryStageCollection[index] = gms;
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
            while (index < GonadalMaturiryStageCollection.Count)
            {
                if (GonadalMaturiryStageCollection[index].RowGUID == id)
                {
                    GonadalMaturiryStageCollection.RemoveAt(index);
                    break;
                }
                index++;
            }
        }
    }
}
