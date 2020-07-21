using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace FAD3.Database.Classes.gearinventory
{
    public class GearLocalNameViewModel
    {
        public ObservableCollection<GearLocalName> GearLocalNameCollection { get; set; }
        private GearLocalNameRepository GearLocalNames { get; set; }



        public GearLocalNameViewModel()
        {
            GearLocalNames = new GearLocalNameRepository();
            GearLocalNameCollection = new ObservableCollection<GearLocalName>(GearLocalNames.GearLocalNames);

        }
        public List<GearLocalName> GetAllGearLocalNames()
        {
            return GearLocalNameCollection.ToList();
        }


        public GearLocalName GetGearLocalName(string guid)
        {
            return GearLocalNameCollection.FirstOrDefault(n => n.GUID == guid);

        }


        public int Count
        {
            get { return GearLocalNameCollection.Count; }
        }

    }
}
