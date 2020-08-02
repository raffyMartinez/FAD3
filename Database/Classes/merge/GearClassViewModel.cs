using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using ISO_Classes;
namespace FAD3.Database.Classes.merge
{
    public class GearClassViewModel
    {
        public ObservableCollection<GearClass> GearClassCollection { get; set; }
        private GearClassRepository GearClasses { get; set; }



        public GearClassViewModel(FADEntities fadEntities)
        {
            GearClasses = new GearClassRepository(fadEntities);
            GearClassCollection = new ObservableCollection<GearClass>(GearClasses.GearClasses);
            GearClassCollection.CollectionChanged += GearClasses_CollectionChanged;
        }
        public List<GearClass> GetAllGearClasses()
        {
            return GearClassCollection.ToList();
        }
        public bool NameExists(string gearClassName)
        {
            foreach (GearClass gc in GearClassCollection)
            {
                if (gc.GearClassName == gearClassName)
                {
                    return true;
                }
            }
            return false;
        }

        public bool CodeExists(string gearClassCode)
        {
            foreach (GearClass gc in GearClassCollection)
            {
                if (gc.GearCode== gearClassCode)
                {
                    return true;
                }
            }
            return false;
        }
        public GearClass GetGearClass(string gearClassID)
        {
            return GearClassCollection.FirstOrDefault(n => n.GearClassGuid == gearClassID);

        }
        private void GearClasses_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        int newIndex = e.NewStartingIndex;
                        GearClasses.Add(GearClassCollection[newIndex]);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    {
                        List<GearClass> tempListOfRemovedItems = e.OldItems.OfType<GearClass>().ToList();
                        GearClasses.Delete(tempListOfRemovedItems[0].GearClassGuid);
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    {
                        List<GearClass> tempList = e.NewItems.OfType<GearClass>().ToList();
                        GearClasses.Update(tempList[0]);      // As the IDs are unique, only one row will be effected hence first index only
                    }
                    break;
            }
        }

        public int Count
        {
            get { return GearClassCollection.Count; }
        }

        public void AddRecordToRepo(GearClass gc)
        {
            if (gc == null)
                throw new ArgumentNullException("Error: The argument is Null");
            GearClassCollection.Add(gc);
        }

        public void UpdateRecordInRepo(GearClass gc)
        {
            if (gc.GearClassGuid == null)
                throw new Exception("Error: ID cannot be null");

            int index = 0;
            while (index < GearClassCollection.Count)
            {
                if (GearClassCollection[index].GearClassGuid == gc.GearClassGuid)
                {
                    GearClassCollection[index] = gc;
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
            while (index < GearClassCollection.Count)
            {
                if (GearClassCollection[index].GearClassGuid == id)
                {
                    GearClassCollection.RemoveAt(index);
                    break;
                }
                index++;
            }
        }

    }
}
