using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
namespace FAD3.Database.Classes.merge
{
    class LenFreqRepository
    {
        private FADEntities _fadEntities;

        public List<LenFreq> LenFreqs{ get; set; }
        public LenFreqRepository(FADEntities fadEntities)
        {
            _fadEntities = fadEntities;
            LenFreqs = getLenFreqs();
        }

        public List<LenFreqFlattened> getFlattened(List<int>years, string aoiGUID)
        {
            string inYears = "";
            foreach (var item in years)
            {
                inYears += $"{item.ToString()},";
            }
            inYears = inYears.Trim(' ', ',');
            List<LenFreqFlattened> thisList = new List<LenFreqFlattened>();
            var dt = new DataTable();
            using (var conection = new OleDbConnection(_fadEntities.ConnectionString))
            {
                try
                {
                    conection.Open();
                    string query = $@"SELECT tblSampling.RefNo, tblSampling.SamplingDate,
                                    [LSName] & ', ' & [Municipalities.Municipality] & ',  ' & [ProvinceName] AS LandingSiteName,
                                    tblGearVariations.Variation, [Name1] & ' ' & [temp_AllNames.Name2] AS CatchName, 
                                    tblLF.LenClass, tblLF.Freq
                                    FROM Provinces 
                                        INNER JOIN (Municipalities 
                                        INNER JOIN (tblLandingSites 
                                        INNER JOIN ((tblGearVariations 
                                        INNER JOIN (tblSampling 
                                        INNER JOIN (tblCatchComp 
                                        INNER JOIN tblLF 
                                            ON tblCatchComp.RowGUID = tblLF.CatchCompRow) 
                                            ON tblSampling.SamplingGUID = tblCatchComp.SamplingGUID) 
                                            ON tblGearVariations.GearVarGUID = tblSampling.GearVarGUID) 
                                        INNER JOIN temp_AllNames ON tblCatchComp.NameGUID = temp_AllNames.NameNo) 
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
                            LenFreqFlattened lff = new LenFreqFlattened
                            {
                                RefNo = dr["RefNo"].ToString(),
                                LandingSite = dr["LandingSiteName"].ToString(),
                                Gear = dr["Variation"].ToString(),
                                DateSampled = (DateTime)dr["SamplingDate"],
                                CatchName = dr["CatchName"].ToString(),
                                Length = (double)dr["LenClass"],
                                Frequency = (int)dr["Freq"]
                            };
                            thisList.Add(lff);
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
        private List<LenFreq> getLenFreqs()
        {
            List<LenFreq> thisList = new List<LenFreq>();
            var dt = new DataTable();
            using (var conection = new OleDbConnection(_fadEntities.ConnectionString))
            {
                try
                {
                    conection.Open();
                    string query = $"Select * from tblLF";


                    var adapter = new OleDbDataAdapter(query, conection);
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        thisList.Clear();
                        foreach (DataRow dr in dt.Rows)
                        {
                            LenFreq lf = new LenFreq();
                            lf.LenClass = Convert.ToDouble(dr["LenClass"]);
                            lf.Freq = Convert.ToInt32(dr["Freq"]);
                            lf.Sequence = string.IsNullOrEmpty(dr["Sequence"].ToString()) ? null : (int?)Convert.ToInt32(dr["Sequence"]);
                            lf.CatchCompositionID = dr["CatchCompRow"].ToString();
                            lf.RowGUID = dr["RowGUID"].ToString();
                            lf.FADEntities = _fadEntities;
                            thisList.Add(lf);
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

        public bool Add(LenFreq lf)
        {
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $@"Insert into tblLF (LenClass,Freq,CatchCompRow,RowGUID,Sequence)
                           Values 
                           ({lf.LenClass},{lf.Freq},{{{lf.CatchComposition.RowGUID}}},{{{lf.RowGUID}}},{(lf.Sequence==null?"null":lf.Sequence.ToString())})";
                using (OleDbCommand update = new OleDbCommand(sql, conn))
                {
                    try
                    {
                        success = update.ExecuteNonQuery() > 0;
                    }
                    catch (OleDbException dbex)
                    {
                        Logger.LogMerge(dbex.Message,true,lf);
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex);
                    }
                }
            }
            return success;
        }

        public bool Update(LenFreq lf)
        {
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $@"Update tblLF set
                                LenClass = {lf.LenClass},
                                Freq = {lf.Freq},
                                CatchCompRow = {{{lf.CatchComposition.RowGUID}}}
                            WHERE RowGUID = {{{lf.RowGUID}}}";
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
                var sql = $"Delete * from tblLF where RowGUID={{{id}}}";
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
