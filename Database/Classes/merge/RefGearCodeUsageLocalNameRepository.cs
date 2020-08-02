using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace FAD3.Database.Classes.merge
{
    public class RefGearCodeUsageLocalNameRepository
    {
        private FADEntities _fadEntities;
        public List<RefGearCodeUsageLocalName> RefGearCodeUsageLocalNames { get; set; }

        public RefGearCodeUsageLocalNameRepository(FADEntities fadEntities)
        {
            _fadEntities = fadEntities;
            RefGearCodeUsageLocalNames = getRefGearCodeUsageLocalNames();
        }

        private List<RefGearCodeUsageLocalName> getRefGearCodeUsageLocalNames()
        {
            List<RefGearCodeUsageLocalName> thisList = new List<RefGearCodeUsageLocalName>();
            var dt = new DataTable();
            using (var conection = new OleDbConnection(_fadEntities.ConnectionString))
            {
                try
                {
                    conection.Open();
                    string query = $"Select * from tblRefGearUsage_LocalName";


                    var adapter = new OleDbDataAdapter(query, conection);
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        thisList.Clear();
                        foreach (DataRow dr in dt.Rows)
                        {
                            RefGearCodeUsageLocalName item = new RefGearCodeUsageLocalName();
                            item.FADEntities = _fadEntities;
                            item.GearLocalNameID = dr["GearLocalName"].ToString();
                            item.RefGearCodeUsageID = dr["GearUsageRow"].ToString();
                            item.RowID = dr["RowNo"].ToString();
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

        public bool Add(RefGearCodeUsageLocalName item)
        {
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $@"Insert into tblRefGearUsage_LocalName (RowNo, GearLocalName,GearUsageRow)
                           Values 
                           ({{{item.RowID}}},{{{item.GearLocalName.Guid}}},{{{item.RefGearCodeUsage.RowNumber}}})";
                using (OleDbCommand update = new OleDbCommand(sql, conn))
                {
                    success = update.ExecuteNonQuery() > 0;
                }
            }
            return success;
        }

        public bool Update(RefGearCodeUsageLocalName item)
        {
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $@"Update tblRefGearUsage_LocalName set
                                GearLocalName = {{{item.GearLocalName.Guid}}},
                                GearUsageRow = {{{item.RefGearCodeUsage.RowNumber}}}
                            WHERE RowNo = {{{item.RowID}}}";
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
                var sql = $"Delete * from tblRefGearUsage_LocalName where RowNo={{{id}}}";
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
