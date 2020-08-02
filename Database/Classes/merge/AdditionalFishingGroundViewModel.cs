using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
namespace FAD3.Database.Classes.merge
{
    public class AdditionalFishingGroundViewModel
    {

        public bool AddSucceeded { get; set; }
        public ObservableCollection<AdditionalFishingGround> AdditionalFishingGroundCollection { get; set; }
        private AdditionalFishingGroundRepository AdditionalFishingGrounds { get; set; }

        public AdditionalFishingGroundViewModel(FADEntities fadEntitites)
        {
            AdditionalFishingGrounds = new AdditionalFishingGroundRepository(fadEntitites);
            AdditionalFishingGroundCollection = new ObservableCollection<AdditionalFishingGround>(AdditionalFishingGrounds.AdditionalFishingGrounds);
            AdditionalFishingGroundCollection.CollectionChanged += AdditionalFishingGrounds_CollectionChanged;
        }

        private void AdditionalFishingGrounds_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    int newIndex = e.NewStartingIndex;
                    AddSucceeded= AdditionalFishingGrounds.Add(AdditionalFishingGroundCollection[newIndex]);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    List<AdditionalFishingGround> tempListOfRemovedItems = e.OldItems.OfType<AdditionalFishingGround>().ToList();
                    AdditionalFishingGrounds.Delete(tempListOfRemovedItems[0].RowGUID);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    List<AdditionalFishingGround> tempList = e.NewItems.OfType<AdditionalFishingGround>().ToList();
                    AdditionalFishingGrounds.Update(tempList[0]);      // As the IDs are unique, only one row will be effected hence first index only
                    break;
            }
        }
        public List<AdditionalFishingGround> GetAllAdditionalFishingGround()
        {
            return AdditionalFishingGroundCollection.ToList();
        }


        public int Count
        {
            get { return AdditionalFishingGroundCollection.Count; }
        }

        public AdditionalFishingGround GetAdditionalFishingGround(string id)
        {
            return AdditionalFishingGroundCollection.FirstOrDefault(n => n.RowGUID == id);
        }

        public bool AddRecordToRepo(AdditionalFishingGround afg)
        {
            if (afg == null)
                throw new ArgumentNullException("Error: The argument is Null");
            AdditionalFishingGroundCollection.Add(afg);
            return AddSucceeded;
        }

        public void UpdateRecordInRepo(AdditionalFishingGround afg)
        {
            if (afg.RowGUID == null)
                throw new Exception("Error: ID cannot be null");

            int index = 0;
            while (index < AdditionalFishingGroundCollection.Count)
            {
                if (AdditionalFishingGroundCollection[index].RowGUID == afg.RowGUID)
                {
                    AdditionalFishingGroundCollection[index] = afg;
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
            while (index < AdditionalFishingGroundCollection.Count)
            {
                if (AdditionalFishingGroundCollection[index].RowGUID == id)
                {
                    AdditionalFishingGroundCollection.RemoveAt(index);
                    break;
                }
                index++;
            }
        }
    }
}
