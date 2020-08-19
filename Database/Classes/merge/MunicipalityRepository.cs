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
    public class MunicipalityRepository
    {
        private FADEntities _fadEntities;
        public List<Municipality> Municipalities{ get; set; }

        public MunicipalityRepository(FADEntities fadEntities)
        {
            _fadEntities = fadEntities;
            Municipalities = getMunicipalities();
        }

        private List<Municipality> getMunicipalities()
        {
            List<Municipality> listMunicipalities = new List<Municipality>();
            var dt = new DataTable();
            using (var conection = new OleDbConnection(_fadEntities.ConnectionString))
            {
                try
                {
                    conection.Open();
                    string query = $"Select * from Municipalities";


                    var adapter = new OleDbDataAdapter(query, conection);
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        listMunicipalities.Clear();
                        foreach (DataRow dr in dt.Rows)
                        {
                            Municipality m = new Municipality();
                            m.Province = _fadEntities.ProvinceViewModel.GetProvince(Convert.ToInt32( dr["ProvNo"]));
                            m.MunicipalityID = (int)dr["MunNo"];
                            m.MunicipalityName = dr["Municipality"].ToString();
                            if (dr["yCoord"].ToString().Length > 0 && dr["xCoord"].ToString().Length > 0)
                            {
                                m.Coordinate = new ISO_Classes.Coordinate(Convert.ToSingle(dr["yCoord"]), Convert.ToSingle(dr["xCoord"]));
                            }
                            m.IsCoastal = (bool)dr["IsCoastal"];
                            listMunicipalities.Add(m);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex);

                }
                return listMunicipalities;
            }
        }

        public bool Add(Municipality m)
        {
            bool success = false;
            string sql;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                if (m.Coordinate != null)
                {
                    sql = $@"Insert into Municipalities(ProvNo, MunNo, MunicipalityName, xCoord, yCoord, IsCoastal)
                           Values
                           ({m.Province.ProvinceID}, {m.MunicipalityID}, '{m.MunicipalityName}', {m.Coordinate.Longitude}, {m.Coordinate.Latitude}, {m.IsCoastal})";
                }
                else

                {
                    sql = $@"Insert into Municipalities(ProvNo, MunNo, MunicipalityName, IsCoastal)
                           Values
                           ({m.Province.ProvinceID}, {m.MunicipalityID}, '{m.MunicipalityName}', {m.IsCoastal})";
                }
                using (OleDbCommand update = new OleDbCommand(sql, conn))
                {
                    try
                    {
                        success = update.ExecuteNonQuery() > 0;
                    }
                    catch (OleDbException dbex)
                    {
                        Logger.LogMerge(dbex.Message,true,m);
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex);
                    }
                }
            }
            return success;
        }

        public bool Update(Municipality m)
        {
            string sql;
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                if (m.Coordinate != null)
                {
                    sql = $@"Update Municipalities set
                                ProvNo = {m.Province.ProvinceID},
                                MunicipalityName='{m.MunicipalityName}',
                                xCoord = {m.Coordinate.Longitude},
                                yCoord = {m.Coordinate.Latitude},
                                IsCoastal = {m.IsCoastal}
                            WHERE MunNo = {m.MunicipalityID}";
                }
                else
                {
                    sql = $@"Update Municipalities set
                                ProvNo = {m.Province.ProvinceID},
                                MunicipalityName='{m.MunicipalityName}',
                                xCoord = null,
                                yCoord = null,
                                IsCoastal = {m.IsCoastal}
                            WHERE MunNo = {m.MunicipalityID}";
                }
                using (OleDbCommand update = new OleDbCommand(sql, conn))
                {
                    success = update.ExecuteNonQuery() > 0;
                }
            }
            return success;
        }

        public bool Delete(int id)
        {
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $"Delete * from Municipalities where MunNo={id}";
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
