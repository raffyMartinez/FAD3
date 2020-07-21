using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace FAD3.Database.Classes.gearinventory
{
    public class CatchLocalNameViewModel
    {
        public ObservableCollection<CatchLocalName> CatchLocalNameCollection { get; set; }
        private CatchLocalNameRepository CatchLocalNames { get; set; }



        public CatchLocalNameViewModel()
        {
            CatchLocalNames = new CatchLocalNameRepository();
            CatchLocalNameCollection = new ObservableCollection<CatchLocalName>(CatchLocalNames.CatchLocalNames);

        }
        public List<CatchLocalName> GetAllCatchLocalNames()
        {
            return CatchLocalNameCollection.ToList();
        }


        public CatchLocalName GetCatchLocalName(string guid)
        {
            return CatchLocalNameCollection.FirstOrDefault(n => n.GUID == guid);

        }


        public int Count
        {
            get { return CatchLocalNameCollection.Count; }
        }
    }
}
