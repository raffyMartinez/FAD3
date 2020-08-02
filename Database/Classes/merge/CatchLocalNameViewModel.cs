using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace FAD3.Database.Classes.merge
{
    public class CatchLocalNameViewModel
    {
        public bool AddSucceeded { get; set; }
        public ObservableCollection<CatchLocalName> CatchLocalNameCollection { get; set; }
        private CatchLocalNameRepository CatchLocalNames{ get; set; }



        public CatchLocalNameViewModel(FADEntities fadEntities)
        {
            CatchLocalNames = new CatchLocalNameRepository(fadEntities);
            CatchLocalNameCollection = new ObservableCollection<CatchLocalName>(CatchLocalNames.CatchLocalNames);
            CatchLocalNameCollection.CollectionChanged += CatchLocalNamess_CollectionChanged;
        }
        public bool NameExists(string localName)
        {
            foreach (CatchLocalName cln in CatchLocalNameCollection)
            {
                if (cln.LocalName == localName)
                {
                    return true;
                }
            }
            return false;
        }

        public CatchLocalName GetCatchLocalName(string guid)
        {
            return CatchLocalNameCollection.FirstOrDefault(n => n.Guid == guid);

        }
        private void CatchLocalNamess_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        int newIndex = e.NewStartingIndex;
                         AddSucceeded= CatchLocalNames.Add(CatchLocalNameCollection[newIndex]);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    {
                        List<CatchLocalName> tempListOfRemovedItems = e.OldItems.OfType<CatchLocalName>().ToList();
                        CatchLocalNames.Delete(tempListOfRemovedItems[0].Guid);
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    {
                        List<CatchLocalName> tempList = e.NewItems.OfType<CatchLocalName>().ToList();
                        CatchLocalNames.Update(tempList[0]);      // As the IDs are unique, only one row will be effected hence first index only
                    }
                    break;
            }
        }

        public int Count
        {
            get { return CatchLocalNameCollection.Count; }
        }

        public bool AddRecordToRepo(CatchLocalName cln)
        {
            if (cln == null)
                throw new ArgumentNullException("Error: The argument is Null");
            CatchLocalNameCollection.Add(cln);
            return AddSucceeded;
        }

        public void UpdateRecordInRepo(CatchLocalName cln)
        {
            if (cln.Guid == null)
                throw new Exception("Error: ID cannot be null");

            int index = 0;
            while (index < CatchLocalNameCollection.Count)
            {
                if (CatchLocalNameCollection[index].Guid == cln.Guid)
                {
                    CatchLocalNameCollection[index] = cln;
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
            while (index < CatchLocalNameCollection.Count)
            {
                if (CatchLocalNameCollection[index].Guid == id)
                {
                    CatchLocalNameCollection.RemoveAt(index);
                    break;
                }
                index++;
            }
        }
    }
}
