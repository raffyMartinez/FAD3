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
    public class AdditionalFishingGroundRepository
    {
        private FADEntities _fadEntities;
        public List<AdditionalFishingGround> AdditionalFishingGrounds { get; set; }
        public AdditionalFishingGroundRepository(FADEntities fadEntities)
        {
            _fadEntities = fadEntities;
            AdditionalFishingGrounds = GetAdditionalFishingGrounds();
        }
        public FADEntities FADEntities { get; set; }


        private List<AdditionalFishingGround> GetAdditionalFishingGrounds()
        {
            List<AdditionalFishingGround> thisList = new List<AdditionalFishingGround>();

            var dt = new DataTable();
            using (var conection = new OleDbConnection(_fadEntities.ConnectionString))
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
                            Grid25GridCell gc = null;
                            Sampling s = _fadEntities.SamplingViewModel.GetSampling(dr["SamplingGUID"].ToString());
                            string gridName = dr["GridName"].ToString();
                            if (s.AOI.IsGrid25 && gridName.Length > 0)
                            {
                                gc = new Grid25GridCell(s.AOI.UTMZone, gridName);
                                //Grid25GridCell samplingGridCell = s.FishingGround.GetGridCell(gc);
                                //if (samplingGridCell == null)
                                //{
                                //    s.FishingGround.Grid25FishingGrounds.Add(gc);
                                //}



                                //if (byte.TryParse(dr["SubGrid"].ToString(), out byte v))
                                //{


                                //}
                                //s.FishingGround.Grid25FishingGrounds.Add(gc);

                            }
                            if (gc.IsValid)
                            {
                                thisList.Add(new AdditionalFishingGround { Sampling = s, GridCell = gc, RowGUID = dr["RowGUID"].ToString(), FADEntities = _fadEntities });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex);

                }
            }
            return thisList;
        }

        public bool Add(AdditionalFishingGround afg)
        {
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $@"Insert into tblGrid (SamplingGUID, GridName,RowGUID)
                           Values 
                           ({{{afg.Sampling.RowID}}},'{afg.GridCell.ToString()}', {{{afg.RowGUID}}})";
                using (OleDbCommand update = new OleDbCommand(sql, conn))
                {
                    success = update.ExecuteNonQuery() > 0;
                }
            }
            return success;
        }

        public bool Update(AdditionalFishingGround afg)
        {
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $@"Update tblGrid set
                                SamplingGUID = {{{afg.Sampling.RowID}}},
                                GridName = '{afg.GridCell.ToString()}',
                            WHERE RowGUID = {{{afg.RowGUID}}}";
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
                var sql = $"Delete * from tblGrid where RowGUID={{{id}}}";
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
