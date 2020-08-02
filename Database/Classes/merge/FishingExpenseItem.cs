using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAD3.Database.Classes.merge
{
    public class FishingExpenseItem
    {
        public FADEntities FADEntities { get; set; }

        private FishingExpense _parentExpense;
        public string ParentExpenseID { get; set; }
        public FishingExpense ParentFishingExpense 
        {
            get
            {
                if(_parentExpense==null)
                {
                    _parentExpense = FADEntities.FishingExpenseViewModel.getFishingExpense(ParentExpenseID);
                }
                return _parentExpense;
            }
            set {_parentExpense = value; } 
        }
        public string ExpenseRowID { get; set; }
        public string ExpenseItem { get; set; }
        public double? Cost { get; set; }
        public string Unit { get; set; }
        public double? UnitQuantity { get; set; }

        public override string ToString()
        {
            return $"{ParentFishingExpense.Sampling.ReferenceNumber.ToString()} - {ExpenseItem}";
        }
    }
}
