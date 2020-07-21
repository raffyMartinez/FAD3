using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace FAD3.Database.Classes.gearinventory
{
    public class GearRepository
    {
        public List<Gear> Gears { get; set; }

        public GearRepository()
        {
            Gears = getGears();
        }

        private List<Gear> getGears()
        {
            List<Gear> thisList = new List<Gear>();
            var dt = new DataTable();
            using (var conection = new OleDbConnection(global.ConnectionString))
            {
                try
                {
                    conection.Open();
                    string query = @"SELECT GearVarGUID, Variation, GearClassName FROM tblGearClass 
                                    INNER JOIN tblGearVariations ON tblGearClass.GearClass = tblGearVariations.GearClass;";


                    var adapter = new OleDbDataAdapter(query, conection);
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        thisList.Clear();
                        foreach (DataRow dr in dt.Rows)
                        {
                            Gear g = new Gear();
                            g.Name = dr["Variation"].ToString();
                            g.VariationGuid = dr["GearVarGUID"].ToString();
                            g.ClassName = dr["GearClassName"].ToString();

                            thisList.Add(g);
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
    }
}
