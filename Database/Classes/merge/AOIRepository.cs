using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.Threading.Tasks;
using FAD3.GUI.Classes;

namespace FAD3.Database.Classes.merge
{
    class AOIRepository
    {
        private FADEntities _fadEntities;
        public List<AOI> AOIs{ get; set; }

        public AOIRepository(FADEntities fadEntities)
        {
            _fadEntities = fadEntities;
            AOIs = getAOIs();
        }

        private List<AOI> getAOIs()
        {
            List<AOI> listAOIs = new List<AOI>();
            var dt = new DataTable();
            using (var conection = new OleDbConnection(_fadEntities.ConnectionString))
            {
                try
                {
                    conection.Open();
                    string query = $@"SELECT * from tblAOI";

                    var adapter = new OleDbDataAdapter(query, conection);
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        listAOIs.Clear();
                        foreach (DataRow dr in dt.Rows)
                        {
                            try
                            {
                                AOI a = new AOI();
                                a.IsGrid25 = (bool)dr["UseGrid25"];
                                a.AOIGuid = dr["AOIGuid"].ToString();
                                a.AOIName = dr["AOIName"].ToString();
                                a.Code = dr["Letter"].ToString();
                                a.SetSubGridStyleFromString(dr["SubgridStyle"].ToString());
                                string utmZone = dr["UTMZone"].ToString();
                                if (utmZone.Length > 0)
                                {
                                    a.UTMZone = new UTMZone(dr["UTMZone"].ToString());
                                    a.AddMBR(new MBR(new Grid25GridCell(a.UTMZone, dr["UpperLeftGrid"].ToString()), new Grid25GridCell(a.UTMZone, dr["LowerRightGrid"].ToString())));
                                }

                                listAOIs.Add(a);
                            }
                            catch(Exception ex)
                            {
                                Logger.Log(ex);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex);

                }
            }

            return listAOIs;
        }

        public bool Add(AOI aoi)
        {
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $@"Insert into tblAOI (AOIGuid, AOIName,Letter, UTMZone, UpperLeftGrid, LowerRightGrid, UseGrid25)
                           Values 
                           ({{{aoi.AOIGuid}}},'{aoi.AOIName}','{aoi.Code}', '{aoi.UTMZone.ToString()}','{aoi.UpperLeftGrid.ToString()}','{aoi.LowerRightGrid.ToString()}', {aoi.IsGrid25})";
                using (OleDbCommand update = new OleDbCommand(sql, conn))
                {
                    try
                    {
                        success = update.ExecuteNonQuery() > 0;
                    }
                    catch (OleDbException dbex)
                    {
                        Logger.LogMerge(dbex.Message);
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex);
                    }
                }
            }
            return success;
        }

        public bool Update(AOI aoi)
        {
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $@"Update tblAOI set
                                AOIName = '{aoi.AOIName}',
                                Letter = '{aoi.Code}',
                                UTMZone = '{aoi.UTMZone.ToString()}',
                                UpperLeftGrid='{aoi.UpperLeftGrid.ToString()}',
                                LowerRightGrid ='{aoi.LowerRightGrid.ToString()}',
                                UseGrid25 = {aoi.IsGrid25}
                            WHERE AOIGuid={{{aoi.AOIGuid}}}";
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
                var sql = $"Delete * from tblAOI where AOIGuid={{{id}}}";
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
