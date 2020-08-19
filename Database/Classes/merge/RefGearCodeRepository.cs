using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace FAD3.Database.Classes.merge
{
   public class RefGearCodeRepository
    {
        private FADEntities _fadEntities;
        public List<RefGearCode> RefGearCodes { get; set; }

        public RefGearCodeRepository(FADEntities fadEntities)
        {
            _fadEntities = fadEntities;
            RefGearCodes = getRefGearCodes();
        }

        private List<RefGearCode> getRefGearCodes()
        {
            List<RefGearCode> thisList = new List<RefGearCode>();
            var dt = new DataTable();
            using (var conection = new OleDbConnection(_fadEntities.ConnectionString))
            {
                try
                {
                    conection.Open();
                    string query = $"Select * from tblRefGearCodes";


                    var adapter = new OleDbDataAdapter(query, conection);
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        thisList.Clear();
                        foreach (DataRow dr in dt.Rows)
                        {
                            RefGearCode rgc = new RefGearCode();
                            rgc.FADEntities = _fadEntities;
                            rgc.GearID = dr["GearVar"].ToString();
                            rgc.IsSubVariation = (bool)dr["SubVariation"];
                            rgc.GearCode = dr["RefGearCode"].ToString();
                            thisList.Add(rgc);
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

        public bool Add(RefGearCode rgc)
        {
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $@"Insert into tblRefGearCodes (RefGearCode, GearVar,SubVariation)
                           Values 
                           ('{rgc.GearCode}',{{{rgc.Gear.GearID}}},{rgc.IsSubVariation})";
                using (OleDbCommand update = new OleDbCommand(sql, conn))
                {
                    try
                    {
                        success = update.ExecuteNonQuery() > 0;
                    }
                    catch (OleDbException dbex)
                    {
                        Logger.LogMerge(dbex.Message,true,rgc);
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex);
                    }
                }
            }
            return success;
        }

        public bool Update(RefGearCode rgc)
        {
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $@"Update tblRefGearCodes set
                                GearVar = {{{rgc.Gear.GearID}}},
                                SubVariation={rgc.IsSubVariation}
                            WHERE RefGearCode = '{rgc.GearCode}'";
                using (OleDbCommand update = new OleDbCommand(sql, conn))
                {
                    success = update.ExecuteNonQuery() > 0;
                }
            }
            return success;
        }

        public bool Delete(string code)
        {
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $"Delete * from tblRefGearCodes where RefGearCode='{code}'";
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
