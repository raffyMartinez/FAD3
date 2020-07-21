using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAD3.Database.Classes.gearinventory
{
    public class InventoryReadEventArg:EventArgs
    {
        public int Records { get; set; }
        public int CurrentRecord { get; set; }

        public string CurrentLocation { get; set; }
    }
}
