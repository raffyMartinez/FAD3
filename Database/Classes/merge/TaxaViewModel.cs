using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
namespace FAD3.Database.Classes.merge

{
    public class TaxaViewModel
    {
        public ObservableCollection<Taxa> TaxaCollection { get; set; }
        private TaxaRepository Taxas { get; set; }



        public TaxaViewModel(FADEntities fadEntities)
        {
            Taxas = new TaxaRepository(fadEntities);
            TaxaCollection = new ObservableCollection<Taxa>(Taxas.Taxas);
            TaxaCollection.CollectionChanged += Taxa_CollectionChanged;
        }
        public List<Taxa> GetAllTaxa()
        {
            return TaxaCollection.ToList();
        }
        //public bool CanDeleteEntity(Taxa t)
        //{
        //    return FAD4Entities.SamplingViewModel.SamplingCollection
        //        .Where(t => t.Gear.GearName == g.GearName).ToList().Count == 0;
        //}
        public bool NameExists(string taxaName)
        {
            foreach (Taxa t in TaxaCollection)
            {
                if (t.TaxaName == taxaName)
                {
                    return true;
                }
            }
            return false;
        }

        public int GetNextID()
        {
            if (TaxaCollection.Count == 0)
            {
                return 0;
            }
            else
            {
                return TaxaCollection.Count ;
                    
            }
        }
        public bool TaxaIdExist(int id)
        {
            foreach (Taxa t in TaxaCollection)
            {
                if (t.TaxaID == id)
                {
                    return true;
                }
            }
            return false;
        }

        public Taxa GetTaxa(string taxa)
        {
            return TaxaCollection.FirstOrDefault(n => n.TaxaName == taxa);

        }

        public Taxa GetTaxa(int id)
        {
            return TaxaCollection.FirstOrDefault(n => n.TaxaID == id);

        }
        private void Taxa_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        int newIndex = e.NewStartingIndex;
                        Taxas.Add(TaxaCollection[newIndex]);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    {
                        List<Taxa> tempListOfRemovedItems = e.OldItems.OfType<Taxa>().ToList();
                        Taxas.Delete(tempListOfRemovedItems[0].TaxaID);
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    {
                        List<Taxa> tempList = e.NewItems.OfType<Taxa>().ToList();
                        Taxas.Update(tempList[0]);      // As the IDs are unique, only one row will be effected hence first index only
                    }
                    break;
            }
        }

        public int Count
        {
            get { return TaxaCollection.Count; }
        }

        public void AddRecordToRepo(Taxa t)
        {
            if (t == null)
                throw new ArgumentNullException("Error: The argument is Null");
            TaxaCollection.Add(t);
        }

        public void UpdateRecordInRepo(Taxa t)
        {

            int index = 0;
            while (index < TaxaCollection.Count)
            {
                if (TaxaCollection[index].TaxaID == t.TaxaID)
                {
                    TaxaCollection[index] = t;
                    break;
                }
                index++;
            }
        }

        public void DeleteRecordFromRepo(int id)
        {

            int index = 0;
            while (index < TaxaCollection.Count)
            {
                if (TaxaCollection[index].TaxaID == id)
                {
                    TaxaCollection.RemoveAt(index);
                    break;
                }
                index++;
            }
        }
    }
}
