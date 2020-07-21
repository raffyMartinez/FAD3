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
    public class GearRepository
    {
        public List<Gear> Gears{ get; set; }

        public GearRepository()
        {
            Gears = getGears();
        }

        private List<Gear>getGears()
        {
            List<Gear> listGears = new List<Gear>();
            var dt = new DataTable();
            using (var conection = new OleDbConnection(global.ConnectionString))
            {
                try
                {
                    conection.Open();
                    string query = $"Select * from tblGearVariations";


                    var adapter = new OleDbDataAdapter(query, conection);
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        listGears.Clear();
                        foreach (DataRow dr in dt.Rows)
                        {
                            Gear g = new Gear();
                            g.GearID = dr["GearVarGUID"].ToString();
                            g.GearName = dr["Variation"].ToString();
                            g.Code = dr["GearCode"].ToString();
                            g.GearClass = FADEntities.GearClassViewModel.GetGearClass(dr["GearClass"].ToString());

                            listGears.Add(g);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex);

                }
                return listGears;
            }
        }

        public bool Add(Gear g)
        {
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(global.ConnectionString))
            {
                conn.Open();
                var sql = $@"Insert into tblGearVariations (GearName,GearCode,GearID,GearClass)
                           Values 
                           ('{g.GearName}','{g.Code}',{{{g.GearID}}}, {{{g.GearClass.GearClassGuid}}})";
                using (OleDbCommand update = new OleDbCommand(sql, conn))
                {
                    success = update.ExecuteNonQuery() > 0;
                }
            }
            return success;
        }

        public bool Update(Gear g)
        {
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(global.ConnectionString))
            {
                conn.Open();
                var sql = $@"Update tblGearVariations set
                                GearName = '{g.GearName}',
                                GearCode = '{g.Code}',
                                GearClass={{{g.GearClass.GearClassGuid}}}
                            WHERE GearID = {{{g.GearID}}}";
                using (OleDbCommand update = new OleDbCommand(sql, conn))
                {
                    success = update.ExecuteNonQuery() > 0;
                }
            }
            return success;
        }

        public bool Delete(string gearID)
        {
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(global.ConnectionString))
            {
                conn.Open();
                var sql = $"Delete * from tblGearVariations where GearID='{gearID}'";
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
