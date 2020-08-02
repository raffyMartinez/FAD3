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
        private FADEntities _fadEntities;
        public List<Gear> Gears{ get; set; }

        public GearRepository(FADEntities fadEntities)
        {
            _fadEntities = fadEntities;
            Gears = getGears();
        }

        private List<Gear>getGears()
        {
            List<Gear> listGears = new List<Gear>();
            var dt = new DataTable();
            using (var conection = new OleDbConnection(_fadEntities.ConnectionString))
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
                            g.GearClass = _fadEntities.GearClassViewModel.GetGearClass(dr["GearClass"].ToString());

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
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $@"Insert into tblGearVariations (Variation,GearVarGUID,GearClass,Name2)
                           Values 
                           ('{g.GearName}',{{{g.GearID}}}, {{{g.GearClass.GearClassGuid}}},'{(g.GearName.Replace(" ",""))}')";
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
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $@"Update tblGearVariations set
                                GearName = '{g.GearName}',
                                GearClass={{{g.GearClass.GearClassGuid}}}
                            WHERE GearID = {{{g.GearID}}}";
                using (OleDbCommand update = new OleDbCommand(sql, conn))
                {
                    success = update.ExecuteNonQuery() > 0;
                }
            }
            return success;
        }


        /// <summary>
        /// modify gear name by appending _1 to it.
        /// </summary>
        /// <param name="gear"></param>
        /// <returns></returns>
        public bool ModifyGearName(Gear gear)
        {
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $@"Update tblGearVariations set
                                Variation = '{gear.GearName}_1',
                                Name2 = '{gear.GearName.Replace(" ","")}_1'
                            WHERE Variation = '{gear.GearName}'";
                using (OleDbCommand update = new OleDbCommand(sql, conn))
                {
                    success = update.ExecuteNonQuery() > 0;
                }
            }
            return success;
        }

        public bool UpdateGearIDFromDestinationGearID(Gear gear)
        {
            string oldGearID = "";
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();

                var sql = $"Select GearVarGUID from tblGearVariations where Variation = '{gear.GearName}'";
                using (OleDbCommand getID = new OleDbCommand(sql,conn))
                {
                    oldGearID = getID.ExecuteScalar().ToString();
                }


                sql = $@"Update tblGearVariations set
                            Variation='{gear.GearName}_1',
                            Name2 = '{gear.GearName.Replace(" ","")}_1'
                        WHERE Variation = '{gear.GearName}'";
                using (OleDbCommand update = new OleDbCommand(sql, conn))
                {
                    success = update.ExecuteNonQuery() > 0;
                }

                if(success)
                {
                    sql = $@"INSERT INTO tblGearVariations (Variation,GearVarGUID,GearClass,Name2)
                           Values 
                           ('{gear.GearName}',{{{gear.GearID}}}, {{{gear.GearClass.GearClassGuid}}},'{(gear.GearName.Replace(" ", ""))}')";

                    using (OleDbCommand update = new OleDbCommand(sql, conn))
                    {
                        success = update.ExecuteNonQuery() > 0;
                    }

                    if(success)
                    {
                        sql = $@"Update tblSampling set GearVarGUID={{{gear.GearID}}} where GearVarGUID = {{{oldGearID}}}";
                        using (OleDbCommand update = new OleDbCommand(sql, conn))
                        {
                            success = update.ExecuteNonQuery() > 0;
                        }

                        sql = $@"Update tblGearSpecs set GearVarGuid={{{gear.GearID}}} where GearVarGUID = {{{oldGearID}}}";
                        using (OleDbCommand update = new OleDbCommand(sql, conn))
                        {
                            success = update.ExecuteNonQuery() > 0;
                        }
                    }
                }

                sql = $"Delete * from tblGearVariations where Variation = '{gear.GearName}_1'";
            }
            return success;
        }
        public bool Delete(string gearID)
        {
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(global.ConnectionString))
            {
                conn.Open();
                var sql = $"Delete * from tblGearVariations where GearID={{{gearID}}}";
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
