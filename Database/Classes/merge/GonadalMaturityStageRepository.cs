using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
namespace FAD3.Database.Classes.merge
{
    public class GonadalMaturityStageRepository
    {
        private FADEntities _fadEntities;
        public List<GonadalMaturiryStage> GonadalMaturiryStages { get; set; }

        public GonadalMaturityStageRepository(FADEntities fadEntities)
        {
            _fadEntities = fadEntities;
            GonadalMaturiryStages = getGonadalMaturiryStages();
        }

        private List<GonadalMaturiryStage> getGonadalMaturiryStages()
        {
            List<GonadalMaturiryStage> thisList = new List<GonadalMaturiryStage>();
            var dt = new DataTable();
            using (var conection = new OleDbConnection(_fadEntities.ConnectionString))
            {
                try
                {
                    conection.Open();
                    string query = $"Select * from tblGMS";


                    var adapter = new OleDbDataAdapter(query, conection);
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        thisList.Clear();
                        foreach (DataRow dr in dt.Rows)
                        {
                            GonadalMaturiryStage gms = new GonadalMaturiryStage();
                            //gms.CatchComposition = _fadEntities.CatchCompositionViewModel.GetCatchComposition(dr["CatchCompRow"].ToString());
                            gms.CatchCompositionID = dr["CatchCompRow"].ToString();
                            gms.Length = string.IsNullOrEmpty(dr["Len"].ToString()) ? null : (double?)dr["Len"];
                            gms.Weight = string.IsNullOrEmpty(dr["Wt"].ToString()) ? null : (double?)dr["Wt"];
                            gms.GonadWeight = string.IsNullOrEmpty(dr["Gonadwt"].ToString()) ? null : (double?)dr["Gonadwt"];
                            gms.Sex = string.IsNullOrEmpty(dr["Sex"].ToString()) ? Sex.NotDetermined : (Sex)Enum.Parse(typeof(Sex), dr["Sex"].ToString());
                            gms.GMS = string.IsNullOrEmpty(dr["GMS"].ToString()) ? FishCrabGMS.AllTaxaNotDetermined : (FishCrabGMS)Enum.Parse(typeof(FishCrabGMS), dr["GMS"].ToString());
                            gms.RowGUID = dr["RowGUID"].ToString();
                            thisList.Add(gms);
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

        public bool Add(GonadalMaturiryStage gms)
        {

            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $@"Insert into tblGMS (CatchCompRow,Len,Wt,Gonadwt,Sex,GMS,RowGUID)
                           Values 
                           (
                                {{{gms.CatchComposition.RowGUID}}},
                                {(gms.Length==null?"null":gms.Length.ToString())},
                                {(gms.Weight==null?"null":gms.Weight.ToString())},
                                {(gms.GonadWeight== null ? "null" : gms.GonadWeight.ToString())},
                                {(gms.Sex==Sex.NotDetermined?"null":((int)gms.Sex).ToString())},
                                {(int)gms.GMS},
                                {{{gms.RowGUID}}}
                            )";
                using (OleDbCommand update = new OleDbCommand(sql, conn))
                {
                    success = update.ExecuteNonQuery() > 0;
                }
            }
            return success;
        }

        public bool Update(GonadalMaturiryStage gms)
        {
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $@"Update tblGMS set
                                CatchCompRow = {{{gms.CatchComposition.RowGUID}}},
                                Len =  {(gms.Length == null ? "null" : gms.Length.ToString())},
                                Weight = {(gms.Weight == null ? "null" : gms.Weight.ToString())},
                                Gonadwt = {(gms.GonadWeight == null ? "null" : gms.GonadWeight.ToString())},
                                Sex = {(gms.Sex == Sex.NotDetermined ? "null" : ((int)gms.Sex).ToString())},
                                GMS = {(int)gms.GMS},
                            WHERE RowGUID = {{{gms.RowGUID}}}";
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
                var sql = $"Delete * from tblGMS where RowGUID={{{id}}}";
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
