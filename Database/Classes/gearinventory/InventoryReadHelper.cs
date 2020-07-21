using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAD3.Database.Classes.gearinventory
{
    public class InventoryReadHelper
    {
        public EventHandler<InventoryReadEventArg> OnInventoryRecordRead;
        public void RecordReading(int totalRecords, int currentRecord, string location)
        {
            OnInventoryRecordRead?.Invoke(null, new InventoryReadEventArg { CurrentRecord = currentRecord, Records = totalRecords, CurrentLocation = location });
        }
    }
}
