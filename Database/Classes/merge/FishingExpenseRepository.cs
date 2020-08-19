using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace FAD3.Database.Classes.merge
{
    public class FishingExpenseRepository
    {
        private FADEntities _fadEntities;
        public List<FishingExpense> FishingExpenses { get; set; }

        public FishingExpenseRepository(FADEntities fadEntities)
        {
            _fadEntities = fadEntities;
            FishingExpenses = getFishingExpenses();
        }

        private List<FishingExpense> getFishingExpenses()
        {
            List<FishingExpense> thisList = new List<FishingExpense>();
            var dt = new DataTable();
            using (var conection = new OleDbConnection(_fadEntities.ConnectionString))
            {
                try
                {
                    conection.Open();
                    string query = $"Select * from tblFishingExpense";


                    var adapter = new OleDbDataAdapter(query, conection);
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        thisList.Clear();
                        foreach (DataRow dr in dt.Rows)
                        {
                            FishingExpense item = new FishingExpense();
                            item.SamplingID = dr["SamplingGUID"].ToString();
                            //item.Sampling = _fadEntities.SamplingViewModel.GetSampling(dr["SamplingGUID"].ToString());
                            item.ReturnOnInvestment = string.IsNullOrEmpty(dr["ReturnOfInvestment"].ToString()) ? null : (double?)Convert.ToDouble(dr["ReturnOfInvestment"]);
                            item.CostOfFishing = string.IsNullOrEmpty(dr["CostOfFishing"].ToString()) ? null : (double?)Convert.ToDouble(dr["CostOfFishing"]);
                            item.IncomeFromFishSold = string.IsNullOrEmpty(dr["IncomeFromFishSold"].ToString()) ? null : (double?)Convert.ToDouble(dr["IncomeFromFishSold"]);
                            item.FishWeightForConsumption = string.IsNullOrEmpty(dr["FishWeightForConsumption"].ToString()) ? null : (double?)Convert.ToDouble(dr["FishWeightForConsumption"]);
                            item.FADEntities = _fadEntities;
                            thisList.Add(item);
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

        public bool Add(FishingExpense item)
        {
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $@"Insert into tblFishingExpense (SamplingGUID, ReturnOfInvestment, CostOfFishing, IncomeFromFishSold, FishWeightForConsumption)
                           Values 
                           ({{{item.Sampling.RowID}}}, 
                                {(item.ReturnOnInvestment!=null?item.ReturnOnInvestment.ToString():"null")},
                                {(item.CostOfFishing != null ? item.CostOfFishing.ToString() : "null")},
                                {(item.IncomeFromFishSold != null ? item.IncomeFromFishSold.ToString() : "null")},
                                {(item.FishWeightForConsumption != null ? item.FishWeightForConsumption.ToString() : "null")})";
                using (OleDbCommand update = new OleDbCommand(sql, conn))
                {
                    try
                    {
                        success = update.ExecuteNonQuery() > 0;
                    }
                    catch(OleDbException dbex)
                    {
                        Logger.LogMerge(dbex.Message,true,item);
                    }
                    catch(Exception ex)
                    {
                        Logger.Log(ex);
                    }
                }
            }
            return success;
        }

        public bool Update(FishingExpense item)
        {
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $@"Update tblFishingExpense set
                                ReturnOfInvestment={(item.ReturnOnInvestment != null ? item.ReturnOnInvestment : null)},
                                CostOfFishing={(item.CostOfFishing != null ? item.CostOfFishing : null)},
                                IncomeFromFishSold={(item.IncomeFromFishSold != null ? item.IncomeFromFishSold : null)},
                                FishWeightForConsumption ={(item.FishWeightForConsumption != null ? item.FishWeightForConsumption : null)}
                                WHERE SamplingGUID = {{{item.Sampling.RowID}}}";
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
                var sql = $"Delete * from tblFishingExpense where SamplingGUID={{{id}}}";
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
