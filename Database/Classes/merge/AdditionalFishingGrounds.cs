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
    public static class AdditionalFishingGrounds
    {
        public static int Count { get; private set; }

        static AdditionalFishingGrounds()
        {
            Count = -1;
        }

        public static void GetAdditionalFishingGrounds()
        {
            if (Count==-1)
            {
                var dt = new DataTable();
                using (var conection = new OleDbConnection(global.ConnectionString))
                {
                    try
                    {
                        conection.Open();
                        string query = $@"SELECT * from tblGrid";

                        var adapter = new OleDbDataAdapter(query, conection);
                        adapter.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                Sampling s = FADEntities.SamplingViewModel.GetSampling(dr["SamplingGUID"].ToString());
                                if (s.AOI.IsGrid25 )
                                {
                                    Grid25GridCell gc = new Grid25GridCell(s.AOI.UTMZone, dr["GridName"].ToString());
                                    Grid25GridCell samplingGridCell= s.FishingGround.GetGridCell(gc);
                                    if (samplingGridCell == null)
                                    {
                                        s.FishingGround.Grid25FishingGrounds.Add(gc);
                                    }



                                    if (byte.TryParse(dr["SubGrid"].ToString(), out byte v))
                                    {
  

                                    }
                                    s.FishingGround.Grid25FishingGrounds.Add(gc);
                                    
                                }
                                Count++;
                            }
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex);

                    }
                    Count = 0;
                }
            }
        }

    }
}
