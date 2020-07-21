using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
namespace FAD3.Database.Classes.gearinventory
{
    public class InventoryViewModel
    {

        public ObservableCollection<Inventory> InventoryCollection { get; set; }

        public InventoryProject InventoryProject { get; set; }

        public InventoryViewModel(string inventoryProjectGUID, InventoryReadHelper readHelper)
        {
            if (InventoryEntities.CatchLocalNameViemModel == null)
            {
                InventoryEntities.CatchLocalNameViemModel = new CatchLocalNameViewModel();
            }
            if (InventoryEntities.GearLocalNameViewModel == null)
            {
                InventoryEntities.GearLocalNameViewModel = new GearLocalNameViewModel();
            }
            if (InventoryEntities.EnumeratorViewModel == null)
            {
                InventoryEntities.EnumeratorViewModel = new EnumeratorViewModel();
            }
            if (InventoryEntities.GearViewModel == null)
            {
                InventoryEntities.GearViewModel = new GearViewModel();
            }
            if (InventoryEntities.ProvinceViewModel == null)
            {
                InventoryEntities.ProvinceViewModel = new ProvinceViewModel();
            }
            if (InventoryEntities.MunicipalityViewModel == null)
            {
                InventoryEntities.MunicipalityViewModel = new MunicipalityViewModel();
            }
            InventoryRepository Inventories = new InventoryRepository(inventoryProjectGUID, readHelper);
            InventoryProject = Inventories.InventoryProject;
            InventoryCollection = new ObservableCollection<Inventory>(Inventories.Inventories);
            InventoryEntities.InventoryViewModel = this;

        }

        public List<Inventory> GetAllInventories()
        {
            return InventoryCollection.ToList();
        }
        public Inventory GetInventory(string inventoryGUID)
        {
            return InventoryCollection.FirstOrDefault(n => n.InventoryGuid == inventoryGUID);

        }


    }
}
