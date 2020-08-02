using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace FAD3.Database.Classes.merge
{
    public class GearLocalNameRepository
    {
        private FADEntities _fadEntities;
        public List<GearLocalName> GearLocalNames { get; set; }

        public GearLocalNameRepository(FADEntities fadEntities)
        {
            _fadEntities = fadEntities;
            GearLocalNames = getGearLocalNames();
        }

        private List<GearLocalName> getGearLocalNames()
        {
            List<GearLocalName> thisList = new List<GearLocalName>();
            var dt = new DataTable();
            using (var conection = new OleDbConnection(_fadEntities.ConnectionString))
            {
                try
                {
                    conection.Open();
                    string query = $"Select * from tblGearLocalNames";


                    var adapter = new OleDbDataAdapter(query, conection);
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        thisList.Clear();
                        foreach (DataRow dr in dt.Rows)
                        {
                            GearLocalName gln = new GearLocalName();
                            gln.Guid = dr["LocalNameGUID"].ToString();
                            gln.LocalName= dr["LocalName"].ToString();


                            thisList.Add(gln);
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

        public bool Add(GearLocalName gln)
        {
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $@"Insert into tblGearLocalNames (LocalNameGUID, LocalName)
                           Values 
                           ({{{gln.Guid}}},'{gln.LocalName}')";
                using (OleDbCommand update = new OleDbCommand(sql, conn))
                {
                    success = update.ExecuteNonQuery() > 0;
                }
            }
            return success;
        }

        public bool Update(GearLocalName gln)
        {
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $@"Update tblGearLocalNames set
                                LocalName = '{gln.LocalName}'
                            WHERE LocalNameGUID = {{{gln.Guid}}}";
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
                var sql = $"Delete * from tblGearLocalNames where LocalNameGUID={{{id}}}";
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
