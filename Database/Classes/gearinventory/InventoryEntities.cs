using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAD3.Database.Classes.gearinventory
{
    public static class InventoryEntities
    {
        public static CatchLocalNameViewModel CatchLocalNameViemModel { get; set; }
        public static ProvinceViewModel ProvinceViewModel { get; set; }
        public static MunicipalityViewModel MunicipalityViewModel{ get; set; }

        public static GearLocalNameViewModel GearLocalNameViewModel { get; set; }
        public static GearViewModel GearViewModel { get; set; }

        public static EnumeratorViewModel EnumeratorViewModel { get; set; }

        public static InventoryViewModel InventoryViewModel { get; set; }
    }
}
