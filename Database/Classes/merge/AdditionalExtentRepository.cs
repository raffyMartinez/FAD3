using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
namespace FAD3.Database.Classes.merge
{
    public class AdditionalExtentRepository
    {

        private FADEntities _fadEntities;
        public List<AdditionalExtent> AdditionalExtents { get; set; }

        public AdditionalExtentRepository(FADEntities fadEntities)
        {
            _fadEntities = fadEntities;
            AdditionalExtents = getAdditionalExtents();
        }

        private List<AdditionalExtent> getAdditionalExtents()
        {
            List<AdditionalExtent> thisList = new List<AdditionalExtent>();
            var dt = new DataTable();
            using (var conection = new OleDbConnection(_fadEntities.ConnectionString))
            {
                try
                {
                    conection.Open();
                    string query = $"Select * from tblAdditionalAOIExtent";


                    var adapter = new OleDbDataAdapter(query, conection);
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        thisList.Clear();
                        foreach (DataRow dr in dt.Rows)
                        {
                            AdditionalExtent item = new AdditionalExtent();
                            item.FADEntities = _fadEntities;
                            item.AOIGuid = dr["AOIGuid"].ToString();
                            item.LowerRight = dr["LowerRight"].ToString();
                            item.UpperLeft = dr["UpperLeft"].ToString();
                            item.RowID = dr["RowNumber"].ToString();
                            item.Description = dr["GridDescription"].ToString();
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

        public bool Add(AdditionalExtent item)
        {
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $@"Insert into tblAdditionalAOIExtent (AOIGuid, LowerRight, UpperLeft, RowNumber, GridDescription)
                           Values 
                           ({{{item.AOI.AOIGuid}}},'{item.LowerRight}','{item.UpperLeft}', {{{item.RowID}}}, '{item.Description}')";
                using (OleDbCommand update = new OleDbCommand(sql, conn))
                {
                    try
                    {
                        success = update.ExecuteNonQuery() > 0;
                    }
                    catch(OleDbException dbex)
                    {
                        Logger.LogMerge(dbex.Message);
                    }
                    catch(Exception ex)
                    {
                        Logger.Log(ex);
                    }
                }
            }
            return success;
        }

        public bool Update(AdditionalExtent item)
        {
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $@"Update tblAdditionalAOIExtent set
                                LowerRight = '{item.LowerRight}',
                                UpperLeft = '{item.UpperLeft}'
                                AOIGuid = {{{item.AOI.AOIGuid}}}.
                                GridDescription = '{item.Description}'
                            WHERE RowNumber = {{{item.RowID}}}";
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
                var sql = $"Delete * from tblAdditionalAOIExtent where RowNumber={{{id}}}";
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
