using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using FAD3.GUI.Classes;
namespace FAD3.Database.Classes.merge
{
    public class SamplingEnumeratorRepository
    {
        private FADEntities _fadEntities;
        public List<SamplingEnumerator> SamplingEnumerators{ get; set; }

        public SamplingEnumeratorRepository(FADEntities fadEntities)
        {
            _fadEntities = fadEntities;
            SamplingEnumerators = getSamplingEnumerators();
        }

        private List<SamplingEnumerator> getSamplingEnumerators()
        {
            List<SamplingEnumerator> listSamplingEnumerator = new List<SamplingEnumerator>();
            var dt = new DataTable();
            using (var conection = new OleDbConnection(_fadEntities.ConnectionString))
            {
                try
                {
                    conection.Open();
                    string query = $"Select * from tblEnumerators";

                    var adapter = new OleDbDataAdapter(query, conection);
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        listSamplingEnumerator.Clear();
                        foreach (DataRow dr in dt.Rows)
                        {
                            SamplingEnumerator se = new SamplingEnumerator();
                            se.EnumeratorID = dr["EnumeratorID"].ToString();
                            se.Name = dr["EnumeratorName"].ToString();
                            se.IsActive = (bool)dr["Active"];
                            se.HireDate = (DateTime)dr["HireDate"];
                            se.AOI = _fadEntities.AOIViewModel.GetAOI(dr["TargetArea"].ToString());
                            listSamplingEnumerator.Add(se);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex);

                }
                return listSamplingEnumerator;
            }
        }

        public bool Add(SamplingEnumerator se)
        {
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $@"Insert into tblEnumerators (EnumeratorID,EnumeratorName, Active, HireDate, TargetArea)
                           Values 
                           ({{{se.EnumeratorID}}},'{se.Name}',{se.IsActive}, '{se.HireDate}', {{{se.AOI.AOIGuid}}})";
                using (OleDbCommand update = new OleDbCommand(sql, conn))
                {
                    try
                    {
                        success = update.ExecuteNonQuery() > 0;
                    }
                    catch (OleDbException dbex)
                    {
                        Logger.LogMerge(dbex.Message,true,se);
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex);
                    }
                }
            }
            return success;
        }

        public bool Update(SamplingEnumerator se)
        {
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $@"Update tblEnumerators set
                                EnumeratorName = '{se.Name}',
                                Active = {se.IsActive},
                                HireDate = '{se.HireDate}',
                                TargetArea = {{{se.AOI.AOIGuid}}}
                            WHERE EnumeratorID = {{{se.EnumeratorID}}}";
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
                var sql = $"Delete * from tblEnumerators where EnumeratorID={{{id}}}";
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
