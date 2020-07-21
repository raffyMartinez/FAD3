using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace FAD3.Database.Classes.merge
{
    public class SpeciesViewModel
    {
        public ObservableCollection<Species> SpeciesCollection { get; set; }
        private SpeciesRepository Specieses { get; set; }

        public event EventHandler<EntityChangedEventArgs> EntityChanged;

        public SpeciesViewModel()
        {

            Specieses = new SpeciesRepository();
            SpeciesCollection = new ObservableCollection<Species>(Specieses.SpeciesList);
            SpeciesCollection.CollectionChanged += SpeciesCollectionn_CollectionChanged;
        }

        public int Count

        {
            get { return SpeciesCollection.Count; }
        }

        public bool NameExists(string genus, string species)
        {
            foreach (Species sp in SpeciesCollection)
            {
                if (sp.Generic == genus && sp.Specific==species)
                {
                    return true;
                }
            }
            return false;
        }

        public Species GetSpecies(string speciesID)
        {
            return SpeciesCollection.FirstOrDefault(n => n.SpeciesID== speciesID);

        }

        //public bool CanDeleteEntity(Species sp)
        //{
        //    return FAD4Entities.SamplingViewModel.SamplingCollection
        //        .Where(t => t.LandingSite.LandingSiteGuid == ls.LandingSiteGuid).ToList().Count == 0;
        //}

        public List<Species> GetAllSpecies()
        {
            return SpeciesCollection.ToList();
        }

        private void SpeciesCollectionn_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Species editedSpecies = new Species();
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        int newIndex = e.NewStartingIndex;
                        editedSpecies = SpeciesCollection[newIndex];
                        Specieses.Add(editedSpecies);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    {
                        List<Species> tempListOfRemovedItems = e.OldItems.OfType<Species>().ToList();
                        editedSpecies = tempListOfRemovedItems[0];
                        Specieses.Delete(editedSpecies.SpeciesID);
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    {
                        List<Species> tempList = e.NewItems.OfType<Species>().ToList();
                        editedSpecies = tempList[0];
                        Specieses.Update(editedSpecies);      // As the IDs are unique, only one row will be effected hence first index only
                    }
                    break;
            }
            EntityChangedEventArgs args = new EntityChangedEventArgs(editedSpecies.GetType().Name, editedSpecies);
            EntityChanged?.Invoke(this, args);
        }

        public void AddRecordToRepo(Species sp)
        {
            if (sp == null)
                throw new ArgumentNullException("Error: The argument is Null");
            SpeciesCollection.Add(sp);
        }

        public void UpdateRecordInRepo(Species sp)
        {
            if (sp.SpeciesID== null)
                throw new Exception("Error: ID cannot be null");

            int index = 0;
            while (index < SpeciesCollection.Count)
            {
                if (SpeciesCollection[index].SpeciesID == sp.SpeciesID)
                {
                    SpeciesCollection[index] = sp;
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
            while (index < SpeciesCollection.Count)
            {
                if (SpeciesCollection[index].SpeciesID == id)
                {
                    SpeciesCollection.RemoveAt(index);
                    break;
                }
                index++;
            }
        }
    }
}
