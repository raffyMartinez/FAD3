using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace FAD3.Database.Classes.gearinventory
{
    public class EnumeratorViewModel
    {
        public ObservableCollection<Enumerator> EnumeratorCollection { get; set; }
        private EnumeratorRepository Enumerators{ get; set; }



        public EnumeratorViewModel()
        {
            Enumerators = new EnumeratorRepository();
            EnumeratorCollection = new ObservableCollection<Enumerator>(Enumerators.Enumerators);
            //ProvinceCollection.CollectionChanged += Provinces_CollectionChanged;
        }
        public List<Enumerator> GetAllEnumerators()
        {
            return EnumeratorCollection.ToList();
        }
        public Enumerator GetEnumerator(string id)
        {
            return EnumeratorCollection.FirstOrDefault(n => n.GUID == id);

        }
    }
}
