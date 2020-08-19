using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;

namespace FAD3.Database.Classes.merge
{
    public class CatchDetailRepository
    {
        private FADEntities _fadEntities;
        public List<CatchDetail> CatchDetails { get; set; }

        public CatchDetailRepository(FADEntities fadEntities)
        {
            _fadEntities = fadEntities;
            CatchDetails = getCatchDetails();
        }

        public List<CatchDetailFlattened> getCatchDetailsFlattened(List<int>years,string aoiGUID )
        {
            string inYears = "";
            foreach (var item in years)
            {
                inYears += $"{item.ToString()},";
            }
            inYears = inYears.Trim(' ', ',');
            List<CatchDetailFlattened> thisList = new List<CatchDetailFlattened>();
            var dt = new DataTable();

            using (var conection = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conection.Open();
                string query = $@"SELECT tblSampling.SamplingDate AS DateSampled, tblSampling.RefNo, tblSampling.SamplingGUID,
                            [LSName] & ', ' & [Municipalities.Municipality] & ', ' & [ProvinceName] AS LandingSite, 
                            tblGearVariations.Variation AS Gear, [Name1] & ' ' & [temp_allNames.Name2] AS CatchName, 
                            tblCatchDetail.Live, temp_AllNames.Identification, tblSampling.WtCatch AS TotalCatchWeight,
                            tblSampling.WtSample AS TotalCatchSampleWeight, tblCatchDetail.wt AS CatchWeight, tblCatchDetail.swt AS CatchSampleWeight,
                            tblCatchDetail.ct AS [Count], tblCatchDetail.sct AS SampleCount, tblCatchDetail.FromTotal
                            FROM Provinces 
                                INNER JOIN (Municipalities 
                                INNER JOIN (tblLandingSites 
                                INNER JOIN (tblGearVariations 
                                INNER JOIN ((tblSampling 
                                INNER JOIN (tblCatchComp 
                                INNER JOIN temp_AllNames 
                                    ON tblCatchComp.NameGUID = temp_AllNames.NameNo) 
                                    ON tblSampling.SamplingGUID = tblCatchComp.SamplingGUID) 
                                INNER JOIN tblCatchDetail 
                                    ON tblCatchComp.RowGUID = tblCatchDetail.CatchCompRow) 
                                    ON tblGearVariations.GearVarGUID = tblSampling.GearVarGUID) 
                                    ON tblLandingSites.LSGUID = tblSampling.LSGUID) 
                                    ON Municipalities.MunNo = tblLandingSites.MunNo) 
                                    ON Provinces.ProvNo = Municipalities.ProvNo
                            WHERE Year([SamplingDate]) In ({inYears}) AND tblSampling.AOI={{{aoiGUID}}}
                            ORDER BY Year([SamplingDate]), 
                                    [LSName] & ', ' & [Municipalities.Municipality] & ', ' & [ProvinceName], 
                                    tblGearVariations.Variation,
                                    tblSampling.RefNo";

                var adapter = new OleDbDataAdapter(query, conection);
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    thisList.Clear();
                    foreach (DataRow dr in dt.Rows)
                    {
                        var csw = dr["CatchSampleWeight"] == DBNull.Value ? null : (double?)dr["CatchSampleWeight"];
                        //if (csw > 0 && csw < 0.3) 
                        //{
                        //    Console.WriteLine($"csw is {csw.ToString()}"); 
                        //}
                        CatchDetailFlattened cdf = new CatchDetailFlattened(dr["SamplingGUID"].ToString())
                        {
                            DateSampled = (DateTime)dr["DateSampled"],
                            RefNo = dr["RefNo"].ToString(),
                            LandingSite = dr["LandingSite"].ToString(),
                            Gear = dr["Gear"].ToString(),
                            CatchName = dr["CatchName"].ToString(),
                            IsLiveFIsh = (bool)dr["Live"],
                            IDType = dr["Identification"].ToString(),
                            TotalCatchWeight = dr["TotalCatchWeight"]==DBNull.Value ? null : (double?)dr["TotalCatchWeight"],
                            TotalCatchSampleWeight = dr["TotalCatchSampleWeight"]==DBNull.Value ? null : (double?)dr["TotalCatchSampleWeight"],
                            CatchWeight = (double)dr["CatchWeight"],
                            CatchSampleWeight = dr["CatchSampleWeight"]==DBNull.Value?null:(double?)dr["CatchSampleWeight"],
                            Count = dr["Count"]==DBNull.Value ? null : (int?)dr["Count"],
                            SampleCount = dr["SampleCount"]==DBNull.Value ? null : (int?)dr["SampleCount"],
                            FromTotal = (bool)dr["FromTotal"]
                        };
                        thisList.Add(cdf);
                    }
                }
            }
                return thisList;
        }
        private List<CatchDetail> getCatchDetails()
        {
            List<CatchDetail> thisList = new List<CatchDetail>();
            var dt = new DataTable();
            using (var conection = new OleDbConnection(_fadEntities.ConnectionString))
            {
                try
                {
                    conection.Open();
                    string query = $"Select * from tblCatchDetail";


                    var adapter = new OleDbDataAdapter(query, conection);
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        thisList.Clear();
                        foreach (DataRow dr in dt.Rows)
                        {
                            CatchDetail cd = new CatchDetail();
                            cd.CatchCompositionID = dr["CatchCompRow"].ToString();
                            cd.LiveFish = (bool)dr["Live"];
                            cd.FromTotal = (bool)dr["FromTotal"];
                            cd.RowGUID = dr["RowGUID"].ToString();
                            cd.Weight = Convert.ToDouble(dr["wt"]);
                            cd.Count = dr["ct"]==DBNull.Value ? null : (int?)dr["ct"];
                            cd.SampleCount = dr["sct"]==DBNull.Value ? null : (int?)dr["sct"];
                            cd.SampleWeight = dr["swt"]==DBNull.Value ? null : (double?)dr["swt"];
                            cd.FADEntities = _fadEntities;
                            thisList.Add(cd);
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

        public bool Add(CatchDetail cd)
        {
            string ct = cd.Count == null ? "null" : cd.Count.ToString();
            string sct = cd.SampleCount == null ? "null" : cd.SampleCount.ToString();
            string swt = cd.SampleWeight == null ? "null" : cd.SampleWeight.ToString();
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $@"Insert into tblCatchDetail (CatchCompRow, Live, FromTotal, RowGUID, wt, ct,sct, swt)
                           Values 
                           (
                                {{{cd.CatchComposition.RowGUID}}},
                                {cd.LiveFish},{cd.FromTotal},
                                {{{cd.RowGUID}}}, 
                                {cd.Weight},
                                {ct},
                                {sct}, 
                                {swt}
                            )";
                using (OleDbCommand update = new OleDbCommand(sql, conn))
                {
                    try
                    {
                        success = update.ExecuteNonQuery() > 0;
                    }
                    catch (OleDbException dbex)
                    {
                        Logger.LogMerge(dbex.Message,true,cd);
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex);
                    }
                }
            }
            return success;
        }

        public bool Update(CatchDetail cd)
        {
            string ct = cd.Count == null ? "null" : cd.Weight.ToString();
            string sct = cd.Count == null ? "null" : cd.SampleCount.ToString();
            string swt = cd.Count == null ? "null" : cd.SampleWeight.ToString();
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $@"Update tblCatchDetail set
                                CatchCompRow = {{{cd.CatchComposition.RowGUID}}},
                                Live = {cd.LiveFish},
                                FromTotal = {cd.FromTotal},
                                ct = {ct},
                                wt = {cd.Weight},
                                sct = {sct},
                                swt = {swt}
                            WHERE RowGUID = {{{cd.RowGUID}}}";
                using (OleDbCommand update = new OleDbCommand(sql, conn))
                {
                    try
                    {
                        success = update.ExecuteNonQuery() > 0;
                    }
                    catch (OleDbException dbex)
                    {
                        Logger.LogMerge(dbex.Message);
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex);
                    }
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
                var sql = $"Delete * from tblCatchDetail where RowGUID={{{id}}}";
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
