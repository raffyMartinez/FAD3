using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace FAD3.Database.Classes.merge
{
    public class GearLocalNameViewModel
    {
        public bool AddSucceeded { get; set; }
        public ObservableCollection<GearLocalName> GearLocalNameCollection { get; set; }
        private GearLocalNameRepository GearLocalNames{ get; set; }



        public GearLocalNameViewModel(FADEntities fadEntities)
        {
            GearLocalNames = new GearLocalNameRepository(fadEntities);
            GearLocalNameCollection = new ObservableCollection<GearLocalName>(GearLocalNames.GearLocalNames);
            GearLocalNameCollection.CollectionChanged += GearLocalNamess_CollectionChanged;
        }
        public List<GearLocalName> GetAllGearClasses()
        {
            return GearLocalNameCollection.ToList();
        }
        public bool NameExists(string localName)
        {
            foreach (GearLocalName gc in GearLocalNameCollection)
            {
                if (gc.LocalName == localName)
                {
                    return true;
                }
            }
            return false;
        }

        public GearLocalName GetGearLocalNameEx(string localName)
        {
            return GearLocalNameCollection.FirstOrDefault(n => n.LocalName == localName);

        }
        public GearLocalName GetGearLocalName(string guid)
        {
            return GearLocalNameCollection.FirstOrDefault(n => n.Guid == guid);

        }
        private void GearLocalNamess_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        int newIndex = e.NewStartingIndex;
                       AddSucceeded= GearLocalNames.Add(GearLocalNameCollection[newIndex]);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    {
                        List<GearLocalName> tempListOfRemovedItems = e.OldItems.OfType<GearLocalName>().ToList();
                        GearLocalNames.Delete(tempListOfRemovedItems[0].Guid);
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    {
                        List<GearLocalName> tempList = e.NewItems.OfType<GearLocalName>().ToList();
                        GearLocalNames.Update(tempList[0]);      // As the IDs are unique, only one row will be effected hence first index only
                    }
                    break;
            }
        }

        public int Count
        {
            get { return GearLocalNameCollection.Count; }
        }

        public bool AddRecordToRepo(GearLocalName gc)
        {
            if (gc == null)
                throw new ArgumentNullException("Error: The argument is Null");
            
            GearLocalNameCollection.Add(gc);
            return AddSucceeded;
        }

        public void UpdateRecordInRepo(GearLocalName gc)
        {
            if (gc.Guid == null)
                throw new Exception("Error: ID cannot be null");

            int index = 0;
            while (index < GearLocalNameCollection.Count)
            {
                if (GearLocalNameCollection[index].Guid == gc.Guid)
                {
                    GearLocalNameCollection[index] = gc;
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
            while (index < GearLocalNameCollection.Count)
            {
                if (GearLocalNameCollection[index].Guid == id)
                {
                    GearLocalNameCollection.RemoveAt(index);
                    break;
                }
                index++;
            }
        }
    }
}
