using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace FAD3.Database.Classes.merge
{
    class RefGearCodeUsageRepository
    {
        private FADEntities _fadEntities;
        public List<RefGearCodeUsage> RefGearCodeUsages { get; set; }

        public RefGearCodeUsageRepository(FADEntities fadEntities)
        {
            _fadEntities = fadEntities;
            RefGearCodeUsages = getRefGearCodeUsages();
        }

        private List<RefGearCodeUsage> getRefGearCodeUsages()
        {
            List<RefGearCodeUsage> thisList = new List<RefGearCodeUsage>();
            var dt = new DataTable();
            using (var conection = new OleDbConnection(_fadEntities.ConnectionString))
            {
                try
                {
                    conection.Open();
                    string query = $"Select * from tblRefGearCodes_Usage";


                    var adapter = new OleDbDataAdapter(query, conection);
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        thisList.Clear();
                        foreach (DataRow dr in dt.Rows)
                        {
                            RefGearCodeUsage usage = new RefGearCodeUsage();
                            usage.FADEntities = _fadEntities;
                            usage.GearCode = dr["RefGearCode"].ToString();
                            usage.RowNumber = dr["RowNo"].ToString();
                            usage.AOIId = dr["TargetAreaGUID"].ToString();
                            thisList.Add(usage);
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

        public bool Add(RefGearCodeUsage codeUsage)
        {
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $@"Insert into tblRefGearCodes_Usage (RefGearCode, RowNo, TargetAreaGUID)
                           Values 
                           ('{codeUsage.RefGearCode.GearCode}',{{{codeUsage.RowNumber}}},{{{codeUsage.AOI.AOIGuid}}})";
                using (OleDbCommand update = new OleDbCommand(sql, conn))
                {
                    try
                    {
                        success = update.ExecuteNonQuery() > 0;
                    }
                    catch(OleDbException dbex)
                    {
                        Logger.LogMerge(dbex.Message,true,codeUsage);
                        
                    }
                    catch(Exception ex)
                    {
                        Logger.Log(ex);
                    }
                }
            }
            return success;
        }

        public bool Update(RefGearCodeUsage codeUsage)
        {
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $@"Update tblRefGearCodes_Usage set
                                TargetAreaGUID = {{{codeUsage.AOI.AOIGuid}}},
                                RefGearCode = '{codeUsage.RefGearCode.GearCode}'
                            WHERE RowNo = {{{codeUsage.RowNumber}}}";
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
                var sql = $"Delete * from tblRefGearCodes_Usage where RowNo={{{id}}}";
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
