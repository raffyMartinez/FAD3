﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using dao;


namespace FAD3.Database.Classes
{
    public static class ReferenceNumberManager
    {
        private static int _RefNoRangeMin = 0;
        private static int _RefNoRangeMax = 0;
        private static string _AOIGuid;
        private static string _GearVariationGuid;
        private static DateTime _SamplingDate;
        private static Dictionary<string, VariationCode> _VariationCodes = new Dictionary<string, VariationCode>();
        private static bool _HasVariationCode;
        private static string _FirstCode;
        private static string _AOI_Year_GearCode;
        private static bool _Has_AOI_Year_GearCode;
        private static int _counter;

        public static int Count
        {
            get { return _VariationCodes.Count; }
        }

        public static bool HasVariationCode
        {
            get { return _HasVariationCode; }
        }

        public static bool ResetReferenceNumbers()
        {
            var dbe = new DBEngine();
            dao.Database db = dbe.OpenDatabase(global.MDBPath);

            try
            {
                try
                {
                    db.QueryDefs.Delete("qry_tempResetRefNo1");
                }
                catch { }

                try
                {
                    db.QueryDefs.Delete("qry_tempResetRefNo2");
                }
                catch { }

                string sql = @"SELECT Left([RefNo],InStr(InStr(1,[RefNo],'-')+1,[RefNo],'-')-1) AS code,
                         CLng(Right([RefNo],Len(Left([RefNo],InStr(InStr(1,[RefNo],'-')+1,[RefNo],'-')-1))-3)) AS [counter]
                        FROM tblSampling";
                db.CreateQueryDef("qry_tempResetRefNo1", sql);

                sql = @"SELECT qry_tempResetRefNo1.code,
                    Max(qry_tempResetRefNo1.counter) AS [max]
                    From qry_tempResetRefNo1
                    GROUP BY qry_tempResetRefNo1.code";
                db.CreateQueryDef("qry_tempResetRefNo2", sql);

                sql = "Delete * from tblRefCodeCounter";
                db.Execute(sql);

                sql = @"INSERT INTO tblRefCodeCounter ( GearRefCode, [Counter] )
                    SELECT qry_tempResetRefNo2.code, [Max]+1 AS countermax
                    FROM qry_tempResetRefNo2";
                db.Execute(sql);

                db.QueryDefs.Delete("qry_tempResetRefNo1");
                db.QueryDefs.Delete("qry_tempResetRefNo2");
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }

        public static Dictionary<string, VariationCode> VariationCodes
        {
            get { return _VariationCodes; }
        }

        public static string FirstCode
        {
            get { return _FirstCode; }
        }

        /// <summary>
        /// prepares the target area and the gear variation to retrieve
        /// appropriate AOI code and gear code can b
        /// </summary>
        /// <param name="AOIGuid"></param>
        /// <param name="GearVariationGuid"></param>
        /// <param name="SamplingDate"></param>
        public static void SetAOI_GearVariation(string AOIGuid, string GearVariationGuid, DateTime SamplingDate)
        {
            _AOIGuid = AOIGuid;
            _GearVariationGuid = GearVariationGuid;
            _SamplingDate = SamplingDate;
            GetGearVariationCodes();
        }

        public static bool RefNumberIsFree(string referenceNumber)
        {
            bool isUsed = false;
            string sql = $@"SELECT tblSampling.RefNo
                            FROM tblSampling
                        WHERE tblSampling.RefNo='{referenceNumber}'";
            using (var con = new OleDbConnection(global.ConnectionString))
            {
                con.Open();
                using (var dt = new DataTable())
                {
                    var adapter = new OleDbDataAdapter(sql, con);
                    adapter.Fill(dt);
                    isUsed = dt.Rows.Count == 0;
                }
            }
            return isUsed;
        }

        private static void GetGearVariationCodes()
        {
            _VariationCodes.Clear();
            _HasVariationCode = false;
            int n = 0;
            using (var con = new OleDbConnection(global.ConnectionString))
            {
                con.Open();
                string query = $@"SELECT tblRefGearCodes_Usage.RefGearCode, RowNo, SubVariation
                                  FROM tblRefGearCodes INNER JOIN tblRefGearCodes_Usage ON
                                  tblRefGearCodes.RefGearCode = tblRefGearCodes_Usage.RefGearCode
                                  WHERE TargetAreaGUID = {{{_AOIGuid}}} AND
                                  GearVar = {{{_GearVariationGuid}}}";

                using (var dt = new DataTable())
                {
                    var adapter = new OleDbDataAdapter(query, con);
                    adapter.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        _HasVariationCode = true;
                        var vc = new VariationCode();
                        vc.GearCode = row["RefGearCode"].ToString();
                        vc.LocalNames = GearLocalNames(vc.GearCode);
                        vc.RowGuid = row["RowNo"].ToString();
                        vc.IsSubVariation = bool.Parse(row["SubVariation"].ToString());
                        _VariationCodes.Add(vc.GearCode, vc);
                        if (n == 0) _FirstCode = vc.GearCode;
                        n++;
                    }
                }
                con.Close();
            }
        }

        public static string GearLocalNames(string RefCode)
        {
            var dt = new DataTable();
            var LocalNames = "";
            using (var conection = new OleDbConnection(global.ConnectionString))
            {
                try
                {
                    conection.Open();
                    var query = $@"SELECT LocalName FROM tblGearLocalNames INNER JOIN
                         (tblRefGearCodes_Usage INNER JOIN tblRefGearUsage_LocalName ON
                         tblRefGearCodes_Usage.RowNo = tblRefGearUsage_LocalName.GearUsageRow)
                         ON tblGearLocalNames.LocalNameGUID = tblRefGearUsage_LocalName.GearLocalName
                         WHERE tblRefGearCodes_Usage.RefGearCode= '{RefCode}' AND
                         tblRefGearCodes_Usage.TargetAreaGUID= {{{_AOIGuid}}}";

                    var adapter = new OleDbDataAdapter(query, conection);
                    adapter.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        LocalNames += dr["LocalName"].ToString() + ", ";
                    }
                    if (LocalNames.Length > 0) LocalNames = LocalNames.Substring(0, LocalNames.Length - 2);
                }
                catch
                {
                }
            }

            return LocalNames;
        }

        public static string GetNextReferenceNumber(string GearCode)
        {
            var AOIcode = TargetArea.TargetAreaCodeFromGuid(_AOIGuid);
            var Year = _SamplingDate.Year.ToString().Substring(2, 2);
            var NextCode = "";
            _AOI_Year_GearCode = AOIcode + Year + "-" + GearCode;
            using (var con = new OleDbConnection(global.ConnectionString))
            {
                con.Open();
                var sql = $"Select Counter from tblRefCodeCounter where GearRefCode = '{_AOI_Year_GearCode}'";

                using (var dt = new DataTable())
                {
                    var adapter = new OleDbDataAdapter(sql, con);
                    try
                    {
                        adapter.Fill(dt);
                    }
                    catch (Exception ex)
                    {
                    }
                    if (dt.Rows.Count > 0)
                    {
                        _counter = int.Parse(dt.Rows[0]["Counter"].ToString());
                        _Has_AOI_Year_GearCode = true;
                    }
                    else
                    {
                        _counter = _RefNoRangeMin;
                        _Has_AOI_Year_GearCode = false;
                    }
                    NextCode = _AOI_Year_GearCode + "-" + (++_counter).ToString("000000");
                }

                return NextCode;
            }
        }

        /// <summary>
        /// Reads from the registry the range of reference numbers
        /// that is assigned to a computer
        /// </summary>
        public static void ReadRefNoRange()
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\FAD3");
            if (rk != null)
            {
                string ReturnValue = rk.GetValue("RNRange", "NULL").ToString();
                if (ReturnValue.Length > 0 && ReturnValue != "NULL")
                {
                    string[] arr = ReturnValue.Split('|');
                    _RefNoRangeMin = int.Parse(arr[0]);
                    _RefNoRangeMax = int.Parse(arr[1]);
                }
            }
        }

        public static bool UpdateRefCodeCounter()
        {
            var sql = "";
            var Success = false;
            if (_AOI_Year_GearCode.Length > 0 && _counter > 0)
            {
                using (var con = new OleDbConnection(global.ConnectionString))
                {
                    if (_Has_AOI_Year_GearCode)
                        sql = $"Update tblRefCodeCounter set [Counter] = {_counter} where GearRefCode = '{_AOI_Year_GearCode}'";
                    else
                        sql = $"Insert into tblRefCodeCounter (GearRefCode, [Counter]) values ('{_AOI_Year_GearCode}', {_counter})";

                    if (sql.Length > 0)
                        con.Open();
                    using (OleDbCommand update = new OleDbCommand(sql, con))
                    {
                        Success = (update.ExecuteNonQuery() > 0);
                        //TODO: what to do if Success=false?
                    }
                }
            }
            return Success;
        }

        public static void SetRefNoRange(bool Reset = false, long min = 0, long max = 0)
        {
            RegistryKey rk = Registry.CurrentUser.CreateSubKey("SOFTWARE\\FAD3");

            if (Reset)
            {
                rk.DeleteValue("RNRange");
                _RefNoRangeMin = _RefNoRangeMax = 0;
            }
            else
                rk.SetValue("RNRange", min.ToString() + "|" + max.ToString(), RegistryValueKind.String);

            rk.Close();
        }

        public static void GetRefNoRange(out long min, out long max)
        {
            min = _RefNoRangeMin;
            max = _RefNoRangeMax;
        }

        public struct VariationCode
        {
            public string GearCode { get; set; }
            public bool IsSubVariation { get; set; }
            public string RowGuid { get; set; }
            public string AOIGuid { get; set; }
            public string LocalNames { get; set; }
        }
    }
}