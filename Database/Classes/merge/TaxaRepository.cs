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
    public class TaxaRepository
    {
        private FADEntities _fadEntities;
        public List<Taxa> Taxas { get; set; }

        public TaxaRepository(FADEntities fadEntities)
        {
            _fadEntities = fadEntities;
            Taxas = getTaxa();
        }

        private List<Taxa> getTaxa()
        {
            List<Taxa> listTaxa = new List<Taxa>();
            var dt = new DataTable();
            using (var conection = new OleDbConnection(_fadEntities.ConnectionString))
            {
                try
                {
                    conection.Open();
                    string query = $"Select * from tblTaxa";


                    var adapter = new OleDbDataAdapter(query, conection);
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        listTaxa.Clear();
                        foreach (DataRow dr in dt.Rows)
                        {
                            Taxa t = new Taxa();
                            t.TaxaID = Convert.ToInt32(dr["TaxaNo"]);
                            t.TaxaName = dr["Taxa"].ToString();
                            listTaxa.Add(t);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex);

                }
                return listTaxa;
            }
        }

        public bool Add(Taxa t)
        {
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $@"Insert into tblTaxa (TaxaNo,Taxa)
                           Values 
                           ({t.TaxaID},'{t.TaxaName}')";
                using (OleDbCommand update = new OleDbCommand(sql, conn))
                {
                    success = update.ExecuteNonQuery() > 0;
                }
            }
            return success;
        }

        public bool Update(Taxa t)
        {
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $"Update tblTaxa set Taxa = '{t.TaxaName}' WHERE TaxaNo = {t.TaxaID}";
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
                var sql = $"Delete * from tblTaxa where TaxaNo={id}";
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
