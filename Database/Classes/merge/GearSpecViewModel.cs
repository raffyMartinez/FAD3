using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace FAD3.Database.Classes.merge
{
    public class GearSpecViewModel
    {
        public bool AddSucceeded { get; set; }
        public ObservableCollection<GearSpec> GearSpecCollection { get; set; }
        private GearSpecRepository GearSpecs { get; set; }




        public GearSpecViewModel(FADEntities fadEntities)
        {
            GearSpecs = new GearSpecRepository(fadEntities);
            GearSpecCollection = new ObservableCollection<GearSpec>(GearSpecs.GearSpecs);
            GearSpecCollection.CollectionChanged += GearSpecs_CollectionChanged;
        }
        public List<GearSpec> GetAllGearSpecs()
        {
            return GearSpecCollection.ToList();
        }

        public GearSpec getGearSpec(string id)
        {
            return GearSpecCollection.FirstOrDefault(n => n.RowGUID == id);

        }
        private void GearSpecs_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        int newIndex = e.NewStartingIndex;
                        AddSucceeded= GearSpecs.Add(GearSpecCollection[newIndex]);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    {
                        List<GearSpec> tempListOfRemovedItems = e.OldItems.OfType<GearSpec>().ToList();
                        GearSpecs.Delete(tempListOfRemovedItems[0].RowGUID);
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    {
                        List<GearSpec> tempList = e.NewItems.OfType<GearSpec>().ToList();
                        GearSpecs.Update(tempList[0]);      // As the IDs are unique, only one row will be effected hence first index only
                    }
                    break;
            }
        }

        public int Count
        {
            get { return GearSpecCollection.Count; }
        }

        public bool AddRecordToRepo(GearSpec item)
        {
            if (item == null)
                throw new ArgumentNullException("Error: The argument is Null");
            GearSpecCollection.Add(item);
            return AddSucceeded;
        }

        public void UpdateRecordInRepo(GearSpec item)
        {
            if (item.RowGUID == null)
                throw new Exception("Error: ID cannot be null");

            int index = 0;
            while (index < GearSpecCollection.Count)
            {
                if (GearSpecCollection[index].RowGUID == item.RowGUID)
                {
                    GearSpecCollection[index] = item;
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
            while (index < GearSpecCollection.Count)
            {
                if (GearSpecCollection[index].RowGUID == id)
                {
                    GearSpecCollection.RemoveAt(index);
                    break;
                }
                index++;
            }
        }
    }
}
