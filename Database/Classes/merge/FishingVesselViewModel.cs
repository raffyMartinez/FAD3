using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
namespace FAD3.Database.Classes.merge
{
    public class FishingVesselViewModel
    {
        public bool AddSucceeded { get; set; }
        public ObservableCollection<FishingVessel> FishingVesselCollection { get; set; }
        public FishingVesselRepository FishingVessels { get; set; }



        public FishingVesselViewModel(FADEntities fadEntities)
        {
            FishingVessels = new FishingVesselRepository(fadEntities);
            FishingVesselCollection = new ObservableCollection<FishingVessel>(FishingVessels.FishingVessels);
            FishingVesselCollection.CollectionChanged +=FishingVessels_CollectionChanged;
        }


        public FishingVessel GetFishingVessel(string guid)
        {
            return FishingVesselCollection.FirstOrDefault(n => n.SamplingGUID == guid);

        }
        private void FishingVessels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        int newIndex = e.NewStartingIndex;
                        AddSucceeded = FishingVessels.Add(FishingVesselCollection[newIndex]);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    {
                        List<FishingVessel> tempListOfRemovedItems = e.OldItems.OfType<FishingVessel>().ToList();
                        FishingVessels.Delete(tempListOfRemovedItems[0].SamplingGUID);
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    {
                        List<FishingVessel> tempList = e.NewItems.OfType<FishingVessel>().ToList();
                        FishingVessels.Update(tempList[0]);      // As the IDs are unique, only one row will be effected hence first index only
                    }
                    break;
            }
        }

        public int Count
        {
            get { return FishingVesselCollection.Count; }
        }

        public bool AddRecordToRepo(FishingVessel fv)
        {
            if (fv == null)
                throw new ArgumentNullException("Error: The argument is Null");
            FishingVesselCollection.Add(fv);
            return AddSucceeded;
        }

        public void UpdateRecordInRepo(FishingVessel fv)
        {
            if (fv.SamplingGUID == null)
                throw new Exception("Error: ID cannot be null");

            int index = 0;
            while (index < FishingVesselCollection.Count)
            {
                if (FishingVesselCollection[index].SamplingGUID == fv.SamplingGUID)
                {
                    FishingVesselCollection[index] = fv;
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
            while (index < FishingVesselCollection.Count)
            {
                if (FishingVesselCollection[index].SamplingGUID == id)
                {
                    FishingVesselCollection.RemoveAt(index);
                    break;
                }
                index++;
            }
        }
    }
}
