using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace FAD3.Database.Classes.gearinventory
{
    public class EnumeratorRepository
    {
        public List<Enumerator> Enumerators { get; set; }

        public EnumeratorRepository()
        {
            Enumerators = getEnumerators();
        }

        private List<Enumerator> getEnumerators()
        {
            List<Enumerator> theList = new List<Enumerator>();
            var dt = new DataTable();
            using (var conection = new OleDbConnection(global.ConnectionString))
            {
                try
                {
                    conection.Open();
                    string query = $"Select * from tblEnumerators";


                    var adapter = new OleDbDataAdapter(query, conection);
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        theList.Clear();
                        foreach (DataRow dr in dt.Rows)
                        {
                            Enumerator en = new Enumerator();
                            en.GUID = dr["EnumeratorID"].ToString();
                            en.Name = dr["EnumeratorName"].ToString();
                            theList.Add(en);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex);

                }
                return theList;
            }
        }
    }
}
