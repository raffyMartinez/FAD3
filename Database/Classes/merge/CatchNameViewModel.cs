using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
namespace FAD3.Database.Classes.merge
{
    public class CatchNameViewModel
    {
        public ObservableCollection<CatchName> CatchNameCollection { get; set; }

        private List<CatchName> _catchNames;
        public CatchNameViewModel(FADEntities fadEntities)
        {
            _catchNames = new List<CatchName>();
            foreach(var ln in fadEntities.CatchLocalNameViewModel.CatchLocalNameCollection)
            {
                _catchNames.Add( new CatchName { CatchLocalName = ln, CatchNameGuid = ln.Guid,CatchNameType = Identification.LocalName });
            }
            foreach (var sp in fadEntities.SpeciesViewModel.SpeciesCollection)
            {
                _catchNames.Add(new CatchName { Species = sp, CatchNameGuid = sp.SpeciesID, CatchNameType=Identification.Scientific });
            }

            CatchNameCollection = new ObservableCollection<CatchName>(_catchNames);
            //CatchNameCollection.CollectionChanged += CatchNames_CollectionChanged;
        }

        public CatchName GetCatchName(string guid)
        {
            return CatchNameCollection.FirstOrDefault(n => n.CatchNameGuid == guid);

        }

        public int Count
        {
            get { return CatchNameCollection.Count; }
        }
    }
}
