using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using FAD3.GUI.Classes;
using System.Diagnostics;
using System.Data.SqlClient;

namespace FAD3.Database.Classes.merge
{
    public class SamplingRepository
    {
        private FADEntities _fadEntities;
        public List<Sampling> Samplings { get; set; }

        public SamplingRepository(FADEntities fadEntities)
        {
            _fadEntities = fadEntities;
            Samplings = getSamplings();
        }

        private List<Sampling> getSamplings()
        {
            int counter = 0;
            List<Sampling> listSamplings = new List<Sampling>();
            var dt = new DataTable();
            using (var conection = new OleDbConnection(_fadEntities.ConnectionString))
            {
                try
                {
                    conection.Open();
                    string query = @"SELECT tblSampling.*
                                    FROM tblSampling INNER JOIN tblAOI ON tblSampling.AOI = tblAOI.AOIGuid
                                    ORDER BY tblAOI.AOIName, tblSampling.SamplingDate";

                    var adapter = new OleDbDataAdapter(query, conection);
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        listSamplings.Clear();

                        foreach (DataRow dr in dt.Rows)
                        {
                            //if (counter == 62) Debugger.Break();
                            Sampling s = new Sampling
                            {
                                AOI = _fadEntities.AOIViewModel.GetAOI(dr["AOI"].ToString()),
                                RowID = dr["SamplingGUID"].ToString(),
                                Gear = _fadEntities.GearViewModel.GetGear(dr["GearVarGUID"].ToString()),
                                LandingSite = _fadEntities.LandingSiteViewModel.GetLandingSite(dr["LSGUID"].ToString()),
                                DateTimeSampled = ((DateTime)dr["SamplingDate"]).AddHours(((DateTime)dr["SamplingTime"]).ToOADate() * 24)
                            };
                            s.ReferenceNumber = new SamplingReferenceNumber(dr["RefNo"].ToString(), s);
                            if (!string.IsNullOrEmpty(dr["TimeSet"].ToString()) && !string.IsNullOrEmpty(dr["DateSet"].ToString()))
                            {
                                s.DateTimeGearSet = ((DateTime)dr["DateSet"]).AddHours(((DateTime)dr["TimeSet"]).ToOADate() * 24);
                            }
                            if (!string.IsNullOrEmpty(dr["TimeHauled"].ToString()) && !string.IsNullOrEmpty(dr["DateHauled"].ToString()))
                            {
                                s.DateTimeGearHaul = ((DateTime)dr["DateHauled"]).AddHours(((DateTime)dr["TimeHauled"]).ToOADate() * 24);
                            }
                            if (!string.IsNullOrEmpty(dr["DateEncoded"].ToString()))
                            {
                                s.DateAdded = (DateTime)dr["DateEncoded"];
                            }
                            if (!string.IsNullOrEmpty(dr["NoHauls"].ToString()))
                            {
                                s.NumberOfHauls = Convert.ToInt32(dr["NoHauls"].ToString());
                            }
                            if (!string.IsNullOrEmpty(dr["NoFishers"].ToString()))
                            {
                                s.NumberOfFishers = Convert.ToInt32(dr["NoFishers"].ToString());
                            }
                            if (!string.IsNullOrEmpty(dr["WtCatch"].ToString()))
                            {
                                s.WeightOfCatch = Convert.ToDouble(dr["WtCatch"].ToString());
                            }
                            if (!string.IsNullOrEmpty(dr["WtSample"].ToString()))
                            {
                                s.WeightOfSample = Convert.ToDouble(dr["WtSample"].ToString());
                            }
                            string fishingGround = dr["FishingGround"].ToString();

                            if (fishingGround.Length > 0)
                            {
                                if (s.AOI.IsGrid25 && s.AOI.UTMZone != null)
                                {
                                    Grid25GridCell gc = new Grid25GridCell(s.AOI.UTMZone, fishingGround);
                                    if (byte.TryParse(dr["SubGrid"].ToString(), out byte v))
                                    {
                                        gc.SubGrids.Add(new Grid25SubGrid(gc, v));
                                    }
                                    s.FishingGround = new FishingGround(gc, s);

                                }
                                else
                                {
                                    s.FishingGround = new FishingGround(fishingGround, s);
                                }
                            }
                            s.Notes = dr["Notes"].ToString();
                            FishingVessel fv = new FishingVessel
                            {
                                Depth = string.IsNullOrEmpty(dr["hgt"].ToString()) ? null : (double?)dr["hgt"],
                                Breadth = string.IsNullOrEmpty(dr["wdt"].ToString()) ? null : (double?)dr["wdt"],
                                Length = string.IsNullOrEmpty(dr["len"].ToString()) ? null : (double?)dr["len"],
                                SamplingGUID = s.RowID,
                                EngineHorsePower= string.IsNullOrEmpty(dr["hp"].ToString()) ? null : (double?)dr["hp"],
                                Engine = dr["Engine"].ToString()
                            };
                            if (string.IsNullOrEmpty(dr["VesType"].ToString()))
                            {
                                fv.VesselType = VesselType.NotDetermined;
                            }
                            else
                            {
                                switch ((int)dr["VesType"])
                                {
                                    case 1:
                                        fv.VesselType = VesselType.Motorized;
                                        break;
                                    case 2:
                                        fv.VesselType = VesselType.NonMotorized;
                                        break;
                                    case 3:
                                        fv.VesselType = VesselType.NoVesselUsed;
                                        break;
                                    case 4:
                                        fv.VesselType = VesselType.NotDetermined;
                                        break;
                                }
                            }

                            s.FishingVessel = fv;

                            //if (!string.IsNullOrEmpty(fishingGround))
                            //{
                            //    s.FishingGround = new FishingGround(fishingGround, s);
                            //    //if (s.AOI.IsGrid25)
                            //    //{
                            //    //    s.FishingGround = new FishingGround(fishingGround, s);
                            //    //}   
                            //    //else
                            //    //{
                            //    //    s.FishingGround = new FishingGround(fishingGround, s);
                            //    //}

                            //    if (byte.TryParse( dr["SubGrid"].ToString(),out byte v))
                            //    {
                            //        //s.FishingGround.SubGrid = v;
                            //        //s.FishingGround.Grid25FishingGrounds[0].Add
                            //    }

                            //}
                            if (!string.IsNullOrEmpty(dr["Enumerator"].ToString()))
                            {
                                s.SamplingEnumerator = _fadEntities.SamplingEnumeratorViewModel.GetSamplingEnumerator(dr["Enumerator"].ToString());
                            }
                            //else
                            //{
                            //    s.SamplingEnumerator = new SamplingEnumerator { Name = "" };
                            //}

                            listSamplings.Add(s);
                            counter++;
                            Console.WriteLine($"counter is {counter} - {_fadEntities.ConnectionString}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex);

                }
            }

            return listSamplings;
        }
        public bool Add(Sampling s)
        {
            int vesType = 4;
            if (s.FishingVessel.VesselType != VesselType.NotDetermined)
            {
                vesType = (int)s.FishingVessel.VesselType;
            }
            bool success = false;
            s.DateAdded = DateTime.Now;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();

                var sql = $@"Insert into tblSampling ( SamplingGUID, GearVarGUID, AOI, RefNo,
                                SamplingDate, SamplingTime, FishingGround, DateSet, TimeSet,
                                DateHauled, TimeHauled, NoHauls, NoFishers,WtCatch, WtSample,
                                LSGUID, HasLiveFish, Enumerator, DateEncoded,wdt,len,hgt,VesType,hp,Engine,Notes)
                               Values (
                               {{{s.RowID}}},
                               {{{s.Gear.GearID}}},
                               {{{s.AOI.AOIGuid}}},
                               '{s.ReferenceNumber.ReferenceNumber}',
                                '{s.DateTimeSampled.ToString("MMM-dd-yyyy")}',
                                '{s.DateTimeSampled.ToString("HH: mm")}',
                                '{(s.FishingGround == null ? "" : s.FishingGround.ToString())}',
                                '{(s.DateTimeGearSet == null ? "null" : ((DateTime)s.DateTimeGearSet).ToString("MMM - dd - yyyy"))}',
                                '{(s.DateTimeGearSet == null ? "null" : ((DateTime)s.DateTimeGearSet).ToString("HH:mm"))}',
                                '{(s.DateTimeGearHaul == null ? "null" : ((DateTime)s.DateTimeGearHaul).ToString("MMM - dd - yyyy"))}',
                                '{(s.DateTimeGearHaul == null ? "null" : ((DateTime)s.DateTimeGearHaul).ToString("HH:mm"))}',
                                {(s.NumberOfHauls == null ? "null" : s.NumberOfHauls.ToString())},
                                {(s.NumberOfFishers == null ? "null" : s.NumberOfFishers.ToString())},
                                {(s.WeightOfCatch == null ? "null" : s.WeightOfCatch.ToString())},
                                {(s.WeightOfSample == null ? "null" : s.WeightOfSample.ToString())},
                                {{{s.LandingSite.LandingSiteGuid}}},
                                {s.HasLiveFish},
                                {{{s.SamplingEnumerator.EnumeratorID}}},
                                '{DateTime.Now}',
                                {(s.FishingVessel.Breadth == null ? "null" : s.FishingVessel.Breadth.ToString())},
                                {(s.FishingVessel.Length == null ? "null" : s.FishingVessel.Length.ToString())},
                                {(s.FishingVessel.Depth == null ? "null" : s.FishingVessel.Depth.ToString())},
                                {vesType},
                                {(s.FishingVessel.EngineHorsePower == null ? "null" : s.FishingVessel.EngineHorsePower.ToString())},
                                '{s.FishingVessel.Engine}',
                                '{s.Notes}'
                               )";
                using (OleDbCommand update = new OleDbCommand(sql, conn))
                {
                    try
                    {
                        success = update.ExecuteNonQuery() > 0;
                    }
                    catch (OleDbException dbex)
                    {
                        Logger.LogMerge(dbex.Message,true,s);
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex);
                    }
                }
            }
            return success;
        }

        public bool ModifyRefNumber(Sampling s)
        {
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $@"Update tblSampling set
                                RefNo = '{s.ReferenceNumber.ReferenceNumber}_1'
                            WHERE RefNo = '{s.ReferenceNumber.ReferenceNumber}'";
                using (OleDbCommand update = new OleDbCommand(sql, conn))
                {
                    success = update.ExecuteNonQuery() > 0;
                }
            }
            return success;
        }

        public bool Update(Sampling s)
        {
            int vesType = 4;
            if (s.FishingVessel.VesselType != VesselType.NotDetermined)
            {
                vesType = (int)s.FishingVessel.VesselType;
            }
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $@"Update tblSampling set
                              GearVarGUID = {{{s.Gear.GearID}}},
                               AOI = {{{s.AOI.AOIGuid}}},
                               RefNo = '{s.ReferenceNumber.ReferenceNumber}',
                               SamplingDate =  '{s.DateTimeSampled.ToString("MMM-dd-yyyy")}',
                               SamplingTime = '{s.DateTimeSampled.ToString("HH: mm")}',
                               FishingGround = '{(s.FishingGround == null ? "" : s.FishingGround.ToString())}',
                               DateSet = '{(s.DateTimeGearSet == null ? "null" : ((DateTime)s.DateTimeGearSet).ToString("MMM - dd - yyyy"))}',
                               TimeSet = '{(s.DateTimeGearSet == null ? "null" : ((DateTime)s.DateTimeGearSet).ToString("HH:mm"))}',
                               DateHauled =  '{(s.DateTimeGearHaul == null ? "null" : ((DateTime)s.DateTimeGearHaul).ToString("MMM - dd - yyyy"))}',
                               TimeHauled= '{(s.DateTimeGearHaul == null ? "null" : ((DateTime)s.DateTimeGearHaul).ToString("HH:mm"))}',
                               NoHauls= {(s.NumberOfHauls == null ? "null" : s.NumberOfHauls.ToString())},
                               NoFishers= {(s.NumberOfFishers == null ? "null" : s.NumberOfFishers.ToString())},
                               WtCatch= {(s.WeightOfCatch == null ? "null" : s.WeightOfCatch.ToString())},
                               WtSample= {(s.WeightOfSample == null ? "null" : s.WeightOfSample.ToString())},
                               LSGUID= {{{s.LandingSite.LandingSiteGuid}}},
                               HasLiveFish= {s.HasLiveFish},
                               Enumerator= {{{s.SamplingEnumerator.EnumeratorID}}},
                               DateEncoded= '{DateTime.Now}',
                                wdt = {(s.FishingVessel.Breadth == null ? "null" : s.FishingVessel.Breadth.ToString())},
                                len = {(s.FishingVessel.Length == null ? "null" : s.FishingVessel.Length.ToString())},
                                hgt= {(s.FishingVessel.Depth == null ? "null" : s.FishingVessel.Depth.ToString())},
                                VesType = {vesType},
                                hp= {(s.FishingVessel.EngineHorsePower == null ? "null" : s.FishingVessel.EngineHorsePower.ToString())},
                                Engine = '{s.FishingVessel.Engine}',
                                Notes = '{s.Notes}'
                            WHERE RowID={{{s.RowID}}}";
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
                var sql = $"Delete * from tblSampling where RowID={{{id}}}";
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
