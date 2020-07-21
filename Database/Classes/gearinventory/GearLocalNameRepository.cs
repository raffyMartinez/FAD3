using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace FAD3.Database.Classes.gearinventory
{
    class GearLocalNameRepository
    {
        public List<GearLocalName> GearLocalNames { get; set; }

        public GearLocalNameRepository()
        {
            GearLocalNames = getGearLocalNames();
        }

        private List<GearLocalName> getGearLocalNames()
        {
            List<GearLocalName> theList = new List<GearLocalName>();
            var dt = new DataTable();
            using (var conection = new OleDbConnection(global.ConnectionString))
            {
                try
                {
                    conection.Open();
                    string query = $"Select * from tblGearLocalNames";


                    var adapter = new OleDbDataAdapter(query, conection);
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        theList.Clear();
                        foreach (DataRow dr in dt.Rows)
                        {
                            GearLocalName gln = new GearLocalName();
                            gln.GUID= dr["LocalNameGUID"].ToString();
                            gln.LocalName= dr["LocalName"].ToString();
                            theList.Add(gln);
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
