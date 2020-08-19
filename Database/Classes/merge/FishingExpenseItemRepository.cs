using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;


namespace FAD3.Database.Classes.merge
{
    class FishingExpenseItemRepository
    {
        private FADEntities _fadEntities;
        public List<FishingExpenseItem> FishingExpenseItems { get; set; }

        public FishingExpenseItemRepository(FADEntities fadEntities)
        {
            _fadEntities = fadEntities;
            FishingExpenseItems = getFishingExpenseItems();
        }

        private List<FishingExpenseItem> getFishingExpenseItems()
        {
            int counter = 0;
            List<FishingExpenseItem> thisList = new List<FishingExpenseItem>();
            var dt = new DataTable();
            using (var conection = new OleDbConnection(_fadEntities.ConnectionString))
            {
                try
                {
                    conection.Open();
                    string query = $"Select * from tblFishingExpenseItems";


                    var adapter = new OleDbDataAdapter(query, conection);
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        thisList.Clear();
                        
                        foreach (DataRow dr in dt.Rows)
                        {
                            FishingExpenseItem item = new FishingExpenseItem();
                            item.ParentExpenseID = dr["SamplingGuid"].ToString();
                            //item.ParentFishingExpense = _fadEntities.FishingExpenseViewModel.getFishingExpense(dr["SamplingGuid"].ToString());
                            item.ExpenseRowID = dr["ExpenseRow"].ToString();
                            item.ExpenseItem = dr["ExpenseItem"].ToString();
                            item.Unit = dr["Unit"].ToString();
                            item.UnitQuantity = string.IsNullOrEmpty(dr["UnitQuantity"].ToString()) ? null : (double?)dr["UnitQuantity"];
                            item.Cost = string.IsNullOrEmpty(dr["Cost"].ToString()) ? null : (double?)dr["Cost"];
                            item.FADEntities = _fadEntities;
                            thisList.Add(item);
                            counter++;
                            //if(counter==10)
                            //{

                            //}
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex);

                }
                return thisList;
            }
        }

        public bool Add(FishingExpenseItem item)
        {
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $@"Insert into tblFishingExpenseItems (SamplingGuid, ExpenseRow, ExpenseItem, Cost, Unit, UnitQuantity)
                           Values 
                           ({{{item.ParentFishingExpense.Sampling.RowID}}},{{{item.ExpenseRowID}}},'{item.ExpenseItem}',{(item.Cost==null?"null":item.Cost.ToString())}, '{item.Unit}', {(item.UnitQuantity==null?"null":item.UnitQuantity.ToString())})";
                using (OleDbCommand update = new OleDbCommand(sql, conn))
                {
                    try
                    {
                        success = update.ExecuteNonQuery() > 0;
                    }
                    catch (OleDbException dbex)
                    {
                        Logger.LogMerge(dbex.Message, true, item);
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex);
                    }
                }
            }
            return success;
        }

        public bool Update(FishingExpenseItem item)
        {
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $@"Update tblFishingExpenseItems set
                                ExpenseItem = '{item.ExpenseItem}',
                                Cost = {item.Cost},
                                Sampling = {{{item.ParentFishingExpense.Sampling.RowID}}},
                                Unit = {item.Unit},
                                UnitQuantity = {item.UnitQuantity}
                            WHERE ExpenseRow = {{{item.ExpenseRowID}}}";
                using (OleDbCommand update = new OleDbCommand(sql, conn))
                {
                    success = update.ExecuteNonQuery() > 0;
                }
            }
            return success;
        }

        public bool Delete(string id)
        {
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $"Delete * from tblFishingExpenseItems where ExpenseRow={{{id}}}";
                using (OleDbCommand update = new OleDbCommand(sql, conn))
                {
                    try
                    {
                        success = update.ExecuteNonQuery() > 0;
                    }
                    catch (OleDbException)
                    {
                        success = false;
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex);
                        success = false;
                    }
                }
            }
            return success;
        }
    }
}
