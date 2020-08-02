using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace FAD3.Database.Classes.merge
{
    class GearSpecRepository
    {
        private FADEntities _fadEntities;
        public List<GearSpec> GearSpecs {get; set; }

        public GearSpecRepository(FADEntities fadEntities)
        {
            _fadEntities = fadEntities;
            GearSpecs = getGearSpecs();
        }

        private List<GearSpec> getGearSpecs()
        {
            List<GearSpec> thisList = new List<GearSpec>();
            var dt = new DataTable();
            using (var conection = new OleDbConnection(_fadEntities.ConnectionString))
            {
                try
                {
                    conection.Open();
                    string query = "Select * from tblGearSpecs Where Version='2'";


                    var adapter = new OleDbDataAdapter(query, conection);
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        thisList.Clear();
                        foreach (DataRow dr in dt.Rows)
                        {
                            GearSpec gs = new GearSpec();
                            gs.Gear = _fadEntities.GearViewModel.GetGear(dr["GearVarGuid"].ToString());
                            gs.Notes = dr["Description"].ToString();
                            gs.Property = dr["ElementName"].ToString();
                            gs.RowGUID = dr["RowID"].ToString();
                            gs.Sequence = string.IsNullOrEmpty(dr["sequence"].ToString()) ? null : (int?)(int)dr["sequence"];
                            gs.Version = 2;
                            gs.Type = dr["ElementType"].ToString();
                            thisList.Add(gs);
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

        public bool Add(GearSpec gs)
        {
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $@"Insert into tblGearSpecs (ElementName, ElementType, Description, Sequence, Version, RowId, GearVarGuid)
                           Values 
                           ('{gs.Property}','{gs.Type}','{gs.Notes}',{gs.Sequence},{gs.Version},{{{gs.RowGUID}}}, {{{gs.Gear.GearID}}})";
                using (OleDbCommand update = new OleDbCommand(sql, conn))
                {
                    success = update.ExecuteNonQuery() > 0;
                }
            }
            return success;
        }

        public bool Update(GearSpec gs)
        {
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $@"Update tblGearSpecs set
                              ElementName ='{gs.Property}',
                              ElementType = '{gs.Type}',
                              Description = '{gs.Notes}',
                              Sequence =  {gs.Sequence},
                              GearVarGuid = {{{gs.Gear.GearID}}},
                              Version = '2' where RowID = {{{gs.RowGUID}}}";
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
                var sql = $"Delete * from tblGearSpecs where RowID={{{id}}}";
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
