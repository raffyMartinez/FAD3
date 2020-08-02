using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;


namespace FAD3.Database.Classes.merge
{
    public class GearViewModel
    {
        public bool AddSucceeded { get; set; }
        public ObservableCollection<Gear> GearCollection { get; set; }
        private GearRepository Gears { get; set; }



        public GearViewModel(FADEntities fadEntities)
        {
            Gears = new GearRepository(fadEntities);
            GearCollection = new ObservableCollection<Gear>(Gears.Gears);
            GearCollection.CollectionChanged += Gearss_CollectionChanged;
        }
        public List<Gear> GetAllGears()
        {
            return GearCollection.ToList();
        }

        public Gear GetGearFromName(string gearName)
        {
            return GearCollection.FirstOrDefault(n => n.GearName == gearName);
        }
        public GearClass GearClass(Gear g)
        {
            return GearCollection
                .Where(t => t.GearID == g.GearID).FirstOrDefault().GearClass;
        }
        public bool NameExists(string gearName)
        {
            foreach (Gear g in GearCollection)
            {
                if (g.GearName == gearName)
                {
                    return true;
                }
            }
            return false;
        }

        public bool CodeExists(string gearCode)
        {
            foreach (Gear g in GearCollection)
            {
                if (g.Code == gearCode)
                {
                    return true;
                }
            }
            return false;
        }

        public bool ModifyGearName (Gear gear)
        {
            var success =  Gears.ModifyGearName(gear);
            if(success)
            {
                GearCollection.Where(t => t.GearName == gear.GearName).FirstOrDefault().GearName = $"{gear.GearName}_1";
            }
            return success;
        }
        public bool ReplaceGearName(Gear gear)
        {
             return Gears.UpdateGearIDFromDestinationGearID(gear);
        }

        public List<Gear>GetAllGears(GearClass gc)
        {
            return GearCollection
                .Where(t => t.GearClass.GearClassGuid == gc.GearClassGuid).ToList();
        }

        public Gear GetGearEx(string gearName)
        {
            return GearCollection.FirstOrDefault(n => n.GearName == gearName);
        }
        public Gear GetGear(string id)
        {
            return GearCollection.FirstOrDefault(n => n.GearID == id);
        }
        private void Gearss_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        int newIndex = e.NewStartingIndex;
                       AddSucceeded= Gears.Add(GearCollection[newIndex]);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    {
                        List<Gear> tempListOfRemovedItems = e.OldItems.OfType<Gear>().ToList();
                        Gears.Delete(tempListOfRemovedItems[0].GearID);
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    {
                        List<Gear> tempList = e.NewItems.OfType<Gear>().ToList();
                        Gears.Update(tempList[0]);      // As the IDs are unique, only one row will be effected hence first index only
                    }
                    break;
            }
        }

        public int Count
        {
            get { return GearCollection.Count; }
        }

        public bool AddRecordToRepo(Gear gear)
        {
            if (gear == null)
                throw new ArgumentNullException("Error: The argument is Null");
            GearCollection.Add(gear);
            return AddSucceeded;
        }

        public void UpdateRecordInRepo(Gear gear)
        {
            if (gear.GearID == null)
                throw new Exception("Error: ID cannot be null");

            int index = 0;
            while (index < GearCollection.Count)
            {
                if (GearCollection[index].GearID == gear.GearID)
                {
                    GearCollection[index] = gear;
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
            while (index < GearCollection.Count)
            {
                if (GearCollection[index].GearID == id)
                {
                    GearCollection.RemoveAt(index);
                    break;
                }
                index++;
            }
        }

        public bool EntityValidated(Gear gear, out List<EntityValidationMessage> messages, bool isNew = false, string oldName="", string oldCode="")
        {

            messages = new List<EntityValidationMessage>();

            if (gear.GearName.Length < 3)
                messages.Add( new EntityValidationMessage( "Fishing gear's name must be at least 3 characters long"));

            if(gear.Code.Length==0)
            {
                messages.Add(new EntityValidationMessage("Gear code cannot be empty"));
            }
            else if(gear.Code.Length>3)
            {
                messages.Add(new EntityValidationMessage("Gear code cannot be more than 3 letters"));
            }

            if (isNew && gear.GearName.Length > 0 && NameExists(gear.GearName))
                messages.Add( new EntityValidationMessage("Gear name already used"));

            if (isNew && gear.Code.Length > 0 && CodeExists(gear.Code))
                messages.Add(new EntityValidationMessage("Gear code already used"));

            if (!isNew && gear.GearName.Length > 0
                 && oldName!= gear.GearName
                && NameExists(gear.GearName))
                messages.Add(new EntityValidationMessage("Gear name already used"));

            if (!isNew && gear.Code.Length > 0
                 && oldCode != gear.Code
                && CodeExists(gear.Code))
                messages.Add(new EntityValidationMessage("Gear code already used"));

            return messages.Count == 0;
        }
    }
}
