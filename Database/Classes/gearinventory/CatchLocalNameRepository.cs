using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
namespace FAD3.Database.Classes.gearinventory
{
    public class CatchLocalNameRepository
    {
        public List<CatchLocalName> CatchLocalNames { get; set; }

        public CatchLocalNameRepository()
        {
            CatchLocalNames = getCatchLocalNames();
        }

        private List<CatchLocalName> getCatchLocalNames()
        {
            List<CatchLocalName> theList = new List<CatchLocalName>();
            var dt = new DataTable();
            using (var conection = new OleDbConnection(global.ConnectionString))
            {
                try
                {
                    conection.Open();
                    string query = $"Select * from tblBaseLocalNames";


                    var adapter = new OleDbDataAdapter(query, conection);
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        theList.Clear();
                        foreach (DataRow dr in dt.Rows)
                        {
                            CatchLocalName cln = new CatchLocalName();
                            cln.GUID = dr["NameNo"].ToString();
                            cln.LocalName = dr["Name"].ToString();
                            theList.Add(cln);
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
