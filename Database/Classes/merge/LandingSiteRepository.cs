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
    public class LandingSiteRepository
    {
        public List<LandingSite> LandingSites { get; set; }

        public LandingSiteRepository()
        {
            LandingSites = getLandingSites();
        }

        private List<LandingSite> getLandingSites()
        {
            float lat;
            float lon;
            List<LandingSite> listLandingSites = new List<LandingSite>();
            var dt = new DataTable();
            using (var conection = new OleDbConnection(global.ConnectionString))
            {
                try
                {
                    conection.Open();
                    string query = $"Select * from tblLandingSites";


                    var adapter = new OleDbDataAdapter(query, conection);
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        listLandingSites.Clear();
                        foreach (DataRow dr in dt.Rows)
                        {
                            LandingSite  ls = new LandingSite();
                            ls.LandingSiteGuid = dr["LSGUID"].ToString();
                            ls.LandingSiteName = dr["LSName"].ToString();
                            ls.Municipality = FADEntities.MunicipalityViewModel.GetMunicipality(Convert.ToInt32( dr["MunNo"]));
                            if(!string.IsNullOrEmpty(dr["cx"].ToString()) && !string.IsNullOrEmpty(dr["cy"].ToString()))
                            {
                                lat = Convert.ToSingle(dr["cy"]);
                                lon = Convert.ToSingle(dr["cx"]);
                                ls.Coordinate = new ISO_Classes.Coordinate(lat, lon);
                            }
                            listLandingSites.Add(ls);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex);

                }
                return listLandingSites;
            }
        }

        public bool Add(LandingSite ls)
        {
            string sql;
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(global.ConnectionString))
            {
                conn.Open();
                if (ls.Coordinate != null)
                {
                    sql = $@"Insert into tblLandingSites (AOIGuid, LSName, MunNo, cx,cy,LSGUID)
                           Values 
                           ({{{ls.AOI.AOIGuid}}}, '{ls.LandingSiteName}', {ls.Municipality.MunicipalityID}, {ls.Coordinate.Longitude},{ls.Coordinate.Latitude},{{{ls.LandingSiteGuid}}})";
                }
                else

                {
                    sql = $@"Insert into tblLandingSites (AOIGuid, LSName, MunNo, LSGUID)
                           Values 
                           ({{{ls.AOI.AOIGuid}}}, '{ls.LandingSiteName}', {ls.Municipality.MunicipalityID}, {{{ls.LandingSiteGuid}}})";
                }
                using (OleDbCommand update = new OleDbCommand(sql, conn))
                {
                    success = update.ExecuteNonQuery() > 0;
                }
            }
            return success;
        }

        public bool Update(LandingSite ls)
        {
            string sql;
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(global.ConnectionString))
            {
                conn.Open();
                if (ls.Coordinate == null)
                {
                    sql = $@"Update tblLandingSites set
                                LSName= '{ls.LandingSiteName}',
                                MunNo = {ls.Municipality.MunicipalityID}
                                cx = null,
                                cy=null,
                            WHERE LSGUID = {ls.LandingSiteGuid}";
                }
                else
                {
                    sql = $@"Update tblLandingSites set
                                LSName= '{ls.LandingSiteName}',
                                MunNo = {ls.Municipality.MunicipalityID}
                                cx = {ls.Coordinate.Longitude},
                                cy={ls.Coordinate.Latitude},
                            WHERE LSGUID = {ls.LandingSiteGuid}";
                }
                using (OleDbCommand update = new OleDbCommand(sql, conn))
                {
                    success = update.ExecuteNonQuery() > 0;
                }
            }
            return success;
        }

        public bool Delete(string ID)
        {
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(global.ConnectionString))
            {
                conn.Open();
                var sql = $"Delete * from tblLandingSites where LSGUID={ID}";
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
