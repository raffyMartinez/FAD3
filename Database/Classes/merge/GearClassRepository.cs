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
   public class GearClassRepository
    {
        private FADEntities _fadEntities;
        public List<GearClass> GearClasses{ get; set; }

        public GearClassRepository(FADEntities fadEntities)
        {
            _fadEntities = fadEntities;
            GearClasses = getGearClasses();
        }

        private List<GearClass> getGearClasses()
        {
            List<GearClass> listGearClasses = new List<GearClass>();
            var dt = new DataTable();
            using (var conection = new OleDbConnection(_fadEntities.ConnectionString))
            {
                try
                {
                    conection.Open();
                    string query = $"Select * from tblGearClass";


                    var adapter = new OleDbDataAdapter(query, conection);
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        listGearClasses.Clear();
                        foreach (DataRow dr in dt.Rows)
                        {
                            GearClass gc = new GearClass();
                            gc.GearClassGuid = dr["GearClass"].ToString();
                            gc.GearClassName= dr["GearClassName"].ToString();
                            gc.GearCode = dr["GearLetter"].ToString();


                            listGearClasses.Add(gc);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex);

                }
                return listGearClasses;
            }
        }

        public bool Add(GearClass gc)
        {
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $@"Insert into tblGearClass (GearClass,GearClassName, GearLetter)
                           Values 
                           ({{{gc.GearClassGuid}}},'{gc.GearClassName}','{gc.GearCode}')";
                using (OleDbCommand update = new OleDbCommand(sql, conn))
                {
                    success = update.ExecuteNonQuery() > 0;
                }
            }
            return success;
        }

        public bool Update(GearClass gc)
        {
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $@"Update tblGearClass set
                                GearClassName = '{gc.GearClassName}',
                                GearLetter = '{gc.GearCode}'
                            WHERE GearClass = {{{gc.GearClassGuid}}}";
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
                var sql = $"Delete * from tblGearClass where GearClass={{{id}}}";
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
