using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
namespace FAD3.Database.Classes.merge
{
   public class SampledGearSpecViewModel
    {
        public bool AddSucceeded { get; set; }
        public ObservableCollection<SampledGearSpec> SampledGearSpecCollection { get; set; }
        private SampledGearSpecRepository SampledGearSpecs { get; set; }



        public SampledGearSpecViewModel(FADEntities fadEntities)
        {
            SampledGearSpecs = new SampledGearSpecRepository(fadEntities);
            SampledGearSpecCollection = new ObservableCollection<SampledGearSpec>(SampledGearSpecs.SampledGearSpecs);
            SampledGearSpecCollection.CollectionChanged += CatchLocalNamess_CollectionChanged;
        }

        public SampledGearSpec getSampledGearSpec(string guid)
        {
            return SampledGearSpecCollection.FirstOrDefault(n => n.RowID == guid);

        }
        private void CatchLocalNamess_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        int newIndex = e.NewStartingIndex;
                        AddSucceeded= SampledGearSpecs.Add(SampledGearSpecCollection[newIndex]);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    {
                        List<SampledGearSpec> tempListOfRemovedItems = e.OldItems.OfType<SampledGearSpec>().ToList();
                        SampledGearSpecs.Delete(tempListOfRemovedItems[0].RowID);
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    {
                        List<SampledGearSpec> tempList = e.NewItems.OfType<SampledGearSpec>().ToList();
                        SampledGearSpecs.Update(tempList[0]);      // As the IDs are unique, only one row will be effected hence first index only
                    }
                    break;
            }
        }

        public int Count
        {
            get { return SampledGearSpecCollection.Count; }
        }

        public bool AddRecordToRepo(SampledGearSpec sgc)
        {
            if (sgc == null)
                throw new ArgumentNullException("Error: The argument is Null");
            SampledGearSpecCollection.Add(sgc);
            return AddSucceeded;
        }

        public void UpdateRecordInRepo(SampledGearSpec sgc)
        {
            if (sgc.RowID == null)
                throw new Exception("Error: ID cannot be null");

            int index = 0;
            while (index < SampledGearSpecCollection.Count)
            {
                if (SampledGearSpecCollection[index].RowID == sgc.RowID)
                {
                    SampledGearSpecCollection[index] = sgc;
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
            while (index < SampledGearSpecCollection.Count)
            {
                if (SampledGearSpecCollection[index].RowID == id)
                {
                    SampledGearSpecCollection.RemoveAt(index);
                    break;
                }
                index++;
            }
        }
    }
}
