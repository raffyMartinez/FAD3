using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace FAD3.Database.Classes.merge
{
    public class CatchDetailRepository
    {
        private FADEntities _fadEntities;
        public List<CatchDetail> CatchDetails { get; set; }

        public CatchDetailRepository(FADEntities fadEntities)
        {
            _fadEntities = fadEntities;
            CatchDetails = getCatchDetails();
        }

        private List<CatchDetail> getCatchDetails()
        {
            List<CatchDetail> thisList = new List<CatchDetail>();
            var dt = new DataTable();
            using (var conection = new OleDbConnection(_fadEntities.ConnectionString))
            {
                try
                {
                    conection.Open();
                    string query = $"Select * from tblCatchDetail";


                    var adapter = new OleDbDataAdapter(query, conection);
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        thisList.Clear();
                        foreach (DataRow dr in dt.Rows)
                        {
                            CatchDetail cd = new CatchDetail();
                            //cd.CatchComposition = _fadEntities.CatchCompositionViewModel.GetCatchComposition(dr["CatchCompRow"].ToString());
                            cd.CatchCompositionID = dr["CatchCompRow"].ToString();
                            cd.LiveFish = (bool)dr["Live"];
                            cd.FromTotal = (bool)dr["FromTotal"];
                            cd.RowGUID = dr["RowGUID"].ToString();
                            cd.Weight = Convert.ToDouble(dr["wt"]);
                            cd.Count = string.IsNullOrEmpty(dr["ct"].ToString()) ? null : (int?)Convert.ToInt32(dr["ct"]);
                            cd.SampleCount = string.IsNullOrEmpty(dr["sct"].ToString()) ? null : (int?)Convert.ToInt32(dr["sct"]);
                            cd.SampleWeight = string.IsNullOrEmpty(dr["swt"].ToString()) ? null : (double?)Convert.ToInt32(dr["swt"]);
                            cd.FADEntities = _fadEntities;
                            thisList.Add(cd);
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

        public bool Add(CatchDetail cd)
        {
            string ct = cd.Count == null ? "null" : cd.Count.ToString();
            string sct = cd.SampleCount == null ? "null" : cd.SampleCount.ToString();
            string swt = cd.SampleWeight == null ? "null" : cd.SampleWeight.ToString();
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $@"Insert into tblCatchDetail (CatchCompRow, Live, FromTotal, RowGUID, wt, ct,sct, swt)
                           Values 
                           ({{{cd.CatchComposition.RowGUID}}},{cd.LiveFish},{cd.FromTotal},{{{cd.RowGUID}}}, {cd.Weight},{ct},{sct}, {swt})";
                using (OleDbCommand update = new OleDbCommand(sql, conn))
                {
                    success = update.ExecuteNonQuery() > 0;
                }
            }
            return success;
        }

        public bool Update(CatchDetail cd)
        {
            string ct = cd.Count == null ? "null" : cd.Weight.ToString();
            string sct = cd.Count == null ? "null" : cd.SampleCount.ToString();
            string swt = cd.Count == null ? "null" : cd.SampleWeight.ToString();
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $@"Update tblCatchDetail set
                                CatchCompRow = {{{cd.CatchComposition.RowGUID}}},
                                Live = {cd.LiveFish},
                                FromTotal = {cd.FromTotal},
                                ct = {ct},
                                wt = {cd.Weight},
                                sct = {sct},
                                swt = {swt}
                            WHERE RowGUID = {{{cd.RowGUID}}}";
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
                var sql = $"Delete * from tblCatchDetail where RowGUID={{{id}}}";
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
