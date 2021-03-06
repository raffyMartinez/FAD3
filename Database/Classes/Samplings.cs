﻿/*
 * Created by SharpDevelop.
 * User: Raffy
 * Date: 8/12/2016
 * Time: 9:20 AM
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */


using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Reflection;
using System.Xml;

namespace FAD3.Database.Classes
{
    /// <summary>
    /// Description of Sampling.
    /// </summary>
    public class Samplings
    {
        private static Dictionary<string, UserInterfaceStructure> _uis = new Dictionary<string, UserInterfaceStructure>();
        private long _catchAndEffortPropertyCount = 0;
        private string _rererenceNo = "";
        private string _samplingGUID = "";
        private static List<string> _engines = new List<string>();
        private static bool _engineReadDone = false;
        private string _targetAreaGuid = "";
        private List<int> _sampledYears = new List<int>();
        public List<string> SamplingGuids { get; internal set; } = new List<string>();
        public static EventHandler<SamplingEventArgs> OnDeleteSamplingStatus;
        //public Dictionary<string, Sampling> FishCatchMonitoringSamplings { get; internal set; } = new Dictionary<string, Sampling>();

        private static Dictionary<string, (string RefNo, DateTime SamplingDate, string FishingGround, string SubGrid,
                               string EnumeratorName, string Notes, double? WtCatch, bool IsGrid25FG,
                               string HasSpecs, int CatchRows)> _effortMonth = new Dictionary<string, (string RefNo, DateTime SamplingDate, string FishingGround,
                               string SubGrid, string EnumeratorName, string Notes, double? WtCatch, bool IsGrid25FG,
                               string HasSpecs, int CatchRows)>();

        public static Dictionary<string, (string RefNo, DateTime SamplingDate, string FishingGround, string SubGrid,
                               string EnumeratorName, string Notes, double? WtCatch, bool IsGrid25FG,
                               string HasSpecs, int CatchRows)> EffortMonth
        {
            get { return _effortMonth; }
        }

        public Dictionary<string, Sampling> SamplingsForMonth { get; internal set; } = new Dictionary<string, Sampling>();

        public static List<string> Engines
        {
            get { return _engines; }
        }

        static Samplings()
        {
        }

        public void GetSamplingGuids(string targetAreaGuid)
        {
            SamplingGuids.Clear();
            string sql = $"Select SamplingGUID from tblSampling where AOI = {{{targetAreaGuid}}}";
            using (var conn = new OleDbConnection(global.ConnectionString))
            {
                conn.Open();
                var dt = new DataTable();
                var adapter = new OleDbDataAdapter(sql, conn);
                adapter.Fill(dt);
                for (int n = 0; n < dt.Rows.Count; n++)
                {
                    SamplingGuids.Add(dt.Rows[n]["SamplingGUID"].ToString());
                }
            }
        }

        public Samplings(string targetAreaGuid, List<int> sampledYears = null)
        {
            _targetAreaGuid = targetAreaGuid;
            if (sampledYears != null)
            {
                _sampledYears = sampledYears;
            }
        }

        public Samplings(string SamplingGUID)
        {
            _samplingGUID = SamplingGUID;
        }

        public Samplings()
        {
            if (!_engineReadDone)
            {
                GetEngines();
                _engineReadDone = true;
            }
        }

        public delegate void ReadUIElement(Samplings s, UIRowFromXML e);
        public event ReadUIElement OnUIRowRead;

        public delegate void EffortUpdateHandler(Samplings s, EffortEventArg e);
        public event EffortUpdateHandler OnEffortUpdated;

        static public Dictionary<string, UserInterfaceStructure> uis
        {
            get { return _uis; }
        }

        public long CatchAndEffortPropertyCount
        {
            get { return _catchAndEffortPropertyCount; }
        }

        public string ReferenceNo
        {
            get { return _rererenceNo; }
        }

        public string SamplingGUID
        {
            get { return _samplingGUID; }
            set { _samplingGUID = value; }
        }

        private static bool HasEnumerators(string landingSiteGuid)
        {
            int samplingCount = 0;
            using (var conn = new OleDbConnection(global.ConnectionString))
            {
                conn.Open();
                var sql = $@"SELECT Count(tblEnumerators.EnumeratorID) AS n
                        FROM tblEnumerators
                            INNER JOIN tblLandingSites ON
                            tblEnumerators.TargetArea = tblLandingSites.AOIGuid
                        WHERE tblLandingSites.LSGUID ={{{landingSiteGuid}}}";

                using (OleDbCommand getCount = new OleDbCommand(sql, conn))
                {
                    samplingCount = (int)getCount.ExecuteScalar();
                }
            }
            return samplingCount > 0;
        }

        public void SamplingSummaryForMonth(string LSGUID, string GearGUID, string SamplingMonth)
        {
            //List<Sampling> samplingsForTheMonth = new List<Sampling>();
            SamplingsForMonth = new Dictionary<string, Sampling>();

            _effortMonth.Clear();
            var CompleteGrid25 = FishingGrid.IsCompleteGrid25;
            bool landingSiteHasEnumerator = false;
            string[] arr = SamplingMonth.Split('-');
            string MonthNumber = "1";
            switch (arr[0])
            {
                case "Jan":
                    MonthNumber = "1";
                    break;

                case "Feb":
                    MonthNumber = "2";
                    break;

                case "Mar":
                    MonthNumber = "3";
                    break;

                case "Apr":
                    MonthNumber = "4";
                    break;

                case "May":
                    MonthNumber = "5";
                    break;

                case "Jun":
                    MonthNumber = "6";
                    break;

                case "Jul":
                    MonthNumber = "7";
                    break;

                case "Aug":
                    MonthNumber = "8";
                    break;

                case "Sep":
                    MonthNumber = "9";
                    break;

                case "Oct":
                    MonthNumber = "10";
                    break;

                case "Nov":
                    MonthNumber = "11";
                    break;

                case "Dec":
                    MonthNumber = "12";
                    break;
            }

            string StartDate = MonthNumber + "/1/" + arr[1];
            string EndDate = (Convert.ToInt32(MonthNumber) + 1).ToString();
            if (arr[0] == "Dec")
            {
                string newYear = (Convert.ToInt32(arr[1]) + 1).ToString();
                EndDate = "1/1/" + newYear;
            }
            else
            {
                EndDate += "/1/" + arr[1];
            }

            using (var myDT = new DataTable())
            {
                try
                {
                    using (var conection = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;data source=" + global.MDBPath))
                    {
                        conection.Open();
                        string query = "";
                        landingSiteHasEnumerator = HasEnumerators(LSGUID);
                        if (landingSiteHasEnumerator)
                        {
                            //query = $@"SELECT tblSampling.*, tblEnumerators.EnumeratorName,
                            //            (SELECT TOP 1 'x' AS HasSpec
                            //              FROM tblGearSpecs
                            //              INNER JOIN tblSampledGearSpec ON
                            //                tblGearSpecs.RowID = tblSampledGearSpec.SpecID
                            //              WHERE tblGearSpecs.Version='2' AND
                            //                tblSampledGearSpec.SamplingGUID=[tblSampling.SamplingGUID]) AS Specs,
                            //            (SELECT Count(SamplingGUID) AS n
                            //              FROM tblCatchComp
                            //              GROUP BY tblCatchComp.SamplingGUID
                            //              HAVING tblCatchComp.SamplingGUID=[tblSampling.SamplingGUID]) AS [rows]
                            //            FROM tblEnumerators RIGHT JOIN
                            //              tblSampling ON
                            //              tblEnumerators.EnumeratorID = tblSampling.Enumerator
                            //            WHERE tblSampling.SamplingDate >=#{StartDate}# AND
                            //              tblSampling.SamplingDate <#{EndDate}# AND
                            //              tblSampling.LSGUID={{{LSGUID}}} AND
                            //              tblSampling.GearVarGUID={{{GearGUID}}}
                            //            ORDER BY tblSampling.DateEncoded";

                            query = $@"SELECT tblSampling.*, tblEnumerators.EnumeratorName,
                                        (SELECT TOP 1 'x' AS HasSpec FROM tblGearSpecs INNER JOIN tblSampledGearSpec ON
                                          tblGearSpecs.RowID = tblSampledGearSpec.SpecID
                                          WHERE tblGearSpecs.Version='2' AND
                                          tblSampledGearSpec.SamplingGUID=[tblSampling.SamplingGUID]) AS Specs,
                                        (SELECT Count(SamplingGUID) AS n FROM tblCatchComp
                                          GROUP BY tblCatchComp.SamplingGUID
                                          HAVING tblCatchComp.SamplingGUID=[tblSampling.SamplingGUID]) AS [rows],
                                        (Select distinct 'x' as HasExpense from tblFishingExpense where
                                          tblFishingExpense.SamplingGuid = [tblSampling.SamplingGUID] ) AS HasExpense
                                        FROM tblEnumerators RIGHT JOIN
                                          tblSampling ON
                                          tblEnumerators.EnumeratorID = tblSampling.Enumerator
                                        WHERE tblSampling.SamplingDate >=#{StartDate}# AND
                                          tblSampling.SamplingDate <#{EndDate}# AND
                                          tblSampling.LSGUID={{{LSGUID}}} AND
                                          tblSampling.GearVarGUID={{{GearGUID}}}
                                        ORDER BY tblSampling.DateEncoded";
                        }
                        else
                        {
                            query = $@"SELECT tblSampling.*,
                                        (SELECT TOP 1 'x' AS  HasSpec
                                            FROM tblGearSpecs INNER JOIN
                                              tblSampledGearSpec ON
                                              tblGearSpecs.RowID = tblSampledGearSpec.SpecID
                                            WHERE tblGearSpecs.Version='2' AND
                                              tblSampledGearSpec.SamplingGUID=[tblSampling.SamplingGUID]) AS Specs,
                                        (SELECT Count(SamplingGUID) AS n
                                            FROM tblCatchComp
                                            GROUP BY tblCatchComp.SamplingGUID
                                            HAVING tblCatchComp.SamplingGUID=[tblSampling.SamplingGUID]) AS [rows],
                                        (Select distinct 'x' as HasExpense from tblFishingExpense where
                                          tblFishingExpense.SamplingGuid = [tblSampling.SamplingGUID] ) AS HasExpense
                                        FROM tblSampling WHERE tblSampling.SamplingDate >= #{StartDate}# AND
                                          tblSampling.SamplingDate < #{EndDate}# AND
                                          tblSampling.LSGUID={{{LSGUID}}} AND
                                          tblSampling.GearVarGUID={{{GearGUID}}}
                                        ORDER BY tblSampling.DateEncoded";
                        }

                        using (var adapter = new OleDbDataAdapter(query, conection))
                        {
                            adapter.Fill(myDT);
                            foreach (DataRow dr in myDT.Rows)
                            {
                                string samplingGuid = dr["SamplingGUID"].ToString();
                                DateTime date = DateTime.Parse(dr["SamplingDate"].ToString());
                                DateTime time = DateTime.Parse(dr["SamplingTime"].ToString());

                                //Sampling s = new Sampling(dr["AOI"].ToString(), samplingGuid, DateTime.Parse(dr["SamplingDate"].ToString()), dr["LSGUID"].ToString(), dr["RefNo"].ToString());
                                Sampling s = new Sampling(dr["AOI"].ToString(), samplingGuid, date.Add(new TimeSpan(time.Hour, time.Minute, 0)), dr["LSGUID"].ToString(), dr["RefNo"].ToString());
                                s.DataStatus = fad3DataStatus.statusFromDB;
                                int rows = 0;
                                if (dr["rows"].ToString().Length > 0)
                                {
                                    rows = int.Parse(dr["rows"].ToString());
                                }

                                s.SamplingSummary = new SamplingSummary(s, rows, dr["FishingGround"].ToString());

                                if (landingSiteHasEnumerator)
                                {
                                    s.SamplingSummary.EnumeratorName = dr["EnumeratorName"].ToString();
                                    s.EnumeratorGuid = dr["Enumerator"].ToString();
                                }

                                s.SamplingSummary.GearSpecsIndicator = "x";
                                if (dr["Specs"].ToString().Length == 0)
                                {
                                    s.SamplingSummary.GearSpecsIndicator = "";
                                }

                                s.SamplingSummary.OperatingExpenseIndicator = "x";
                                if (dr["HasExpense"].ToString().Length == 0)
                                {
                                    s.SamplingSummary.OperatingExpenseIndicator = "";
                                }

                                if (int.TryParse(dr["SubGrid"].ToString(), out int sg))
                                {
                                    s.SamplingSummary.SubGrid = sg;
                                }
                                double? wt = null;
                                if (double.TryParse(dr["wtCatch"].ToString(), out double w))
                                {
                                    wt = w;
                                }
                                s.CatchWeight = wt;

                                DateTime? dateEncoded = null;
                                if (DateTime.TryParse(dr["DateEncoded"].ToString(), out DateTime dt))
                                {
                                    dateEncoded = dt;
                                }
                                s.DateEncoded = dateEncoded;

                                SamplingsForMonth.Add(samplingGuid, s);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex.Message, ex.StackTrace);
                }
            }
        }

        private static List<FishingGround> FishingGroundFromSampling(string samplingGUID)
        {
            List<FishingGround> fgs = new List<FishingGround>();
            string sql = $@"SELECT tblSampling.FishingGround, tblSampling.SubGrid FROM tblSampling WHERE tblSampling.SamplingGUID={{{samplingGUID}}}
                         union all
                        SELECT tblGrid.GridName, tblGrid.SubGrid FROM tblGrid WHERE tblGrid.SamplingGUID ={{{samplingGUID}}}";
            using (var dt = new DataTable())
            {
                try
                {
                    using (var conection = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;data source=" + global.MDBPath))
                    {
                        var adapter = new OleDbDataAdapter(sql, conection);
                        adapter.Fill(dt);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataRow dr = dt.Rows[i];
                            int? subGrid = null;
                            if (int.TryParse(dr["SubGrid"].ToString(), out int sg))
                            {
                                subGrid = sg;
                            }
                            fgs.Add(new FishingGround(dr["FishingGround"].ToString(), subGrid));
                        }
                        //conection.Open();
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex.Message, ex.StackTrace);
                }
            }
            return fgs;
        }

        /// <summary>
        /// returns a dictionary of 9 element tuples that will fill a list view with a sampling summary
        /// </summary>
        /// <param name="LSGUID"></param>
        /// <param name="GearGUID"></param>
        /// <param name="SamplingMonth"></param>
        /// <returns></returns>
        public static Dictionary<string, (string RefNo, DateTime SamplingDate, string FishingGround, string SubGrid,
                                string EnumeratorName, string Notes, double? WtCatch, bool IsGrid25FG,
                                string HasSpecs, int CatchRows)> SamplingSummaryForMonth1(string LSGUID, string GearGUID, string SamplingMonth)
        {
            _effortMonth.Clear();
            var CompleteGrid25 = FishingGrid.IsCompleteGrid25;
            string[] arr = SamplingMonth.Split('-');
            string MonthNumber = "1";
            switch (arr[0])
            {
                case "Jan":
                    MonthNumber = "1";
                    break;

                case "Feb":
                    MonthNumber = "2";
                    break;

                case "Mar":
                    MonthNumber = "3";
                    break;

                case "Apr":
                    MonthNumber = "4";
                    break;

                case "May":
                    MonthNumber = "5";
                    break;

                case "Jun":
                    MonthNumber = "6";
                    break;

                case "Jul":
                    MonthNumber = "7";
                    break;

                case "Aug":
                    MonthNumber = "8";
                    break;

                case "Sep":
                    MonthNumber = "9";
                    break;

                case "Oct":
                    MonthNumber = "10";
                    break;

                case "Nov":
                    MonthNumber = "11";
                    break;

                case "Dec":
                    MonthNumber = "12";
                    break;
            }

            string StartDate = MonthNumber + "/1/" + arr[1];
            string EndDate = (Convert.ToInt32(MonthNumber) + 1).ToString();
            if (arr[0] == "Dec")
            {
                string newYear = (Convert.ToInt32(arr[1]) + 1).ToString();
                EndDate = "1/1/" + newYear;
            }
            else
            {
                EndDate += "/1/" + arr[1];
            }

            using (var myDT = new DataTable())
            {
                try
                {
                    using (var conection = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;data source=" + global.MDBPath))
                    {
                        conection.Open();
                        string query = "";
                        if (HasEnumerators(LSGUID))
                        {
                            query = $@"SELECT tblSampling.RefNo,
                                        tblSampling.SamplingDate,
                                        tblSampling.FishingGround,
                                        tblSampling.SubGrid,
                                        tblEnumerators.EnumeratorName,
                                        tblSampling.Notes,
                                        tblSampling.WtCatch,
                                        tblSampling.SamplingGUID,
                                        tblSampling.IsGrid25FG,
                                        (SELECT TOP 1 'x' AS HasSpec
                                          FROM tblGearSpecs
                                          INNER JOIN tblSampledGearSpec ON
                                            tblGearSpecs.RowID = tblSampledGearSpec.SpecID
                                          WHERE tblGearSpecs.Version='2' AND
                                            tblSampledGearSpec.SamplingGUID=[tblSampling.SamplingGUID]) AS Specs,
                                        (SELECT Count(SamplingGUID) AS n
                                          FROM tblCatchComp
                                          GROUP BY tblCatchComp.SamplingGUID
                                          HAVING tblCatchComp.SamplingGUID=[tblSampling.SamplingGUID]) AS [rows]
                                        FROM tblEnumerators RIGHT JOIN
                                          tblSampling ON
                                          tblEnumerators.EnumeratorID = tblSampling.Enumerator
                                        WHERE tblSampling.SamplingDate >=#{StartDate}# AND
                                          tblSampling.SamplingDate <#{EndDate}# AND
                                          tblSampling.LSGUID={{{LSGUID}}} AND
                                          tblSampling.GearVarGUID={{{GearGUID}}}
                                        ORDER BY tblSampling.DateEncoded";
                        }
                        else
                        {
                            query = $@"SELECT tblSampling.RefNo,
                                        tblSampling.SamplingDate,
                                        tblSampling.FishingGround,
                                        tblSampling.SubGrid,
                                        """" AS EnumeratorName,
                                        tblSampling.Notes,
                                        tblSampling.WtCatch,
                                        tblSampling.SamplingGUID,
                                        tblSampling.IsGrid25FG,
                                        (SELECT TOP 1 'x' AS  HasSpec
                                            FROM tblGearSpecs INNER JOIN
                                              tblSampledGearSpec ON
                                              tblGearSpecs.RowID = tblSampledGearSpec.SpecID
                                            WHERE tblGearSpecs.Version='2' AND
                                              tblSampledGearSpec.SamplingGUID=[tblSampling.SamplingGUID]) AS Specs,
                                        (SELECT Count(SamplingGUID) AS n
                                            FROM tblCatchComp
                                            GROUP BY tblCatchComp.SamplingGUID
                                            HAVING tblCatchComp.SamplingGUID=[tblSampling.SamplingGUID]) AS [rows]
                                        FROM tblSampling WHERE tblSampling.SamplingDate >= #{StartDate}# AND
                                          tblSampling.SamplingDate < #{EndDate}# AND
                                          tblSampling.LSGUID={{{LSGUID}}} AND
                                          tblSampling.GearVarGUID={{{GearGUID}}}
                                        ORDER BY tblSampling.DateEncoded";
                        }

                        using (var adapter = new OleDbDataAdapter(query, conection))
                        {
                            adapter.Fill(myDT);
                            foreach (DataRow dr in myDT.Rows)
                            {
                                double? wt = null;
                                if (double.TryParse(dr["wtCatch"].ToString(), out double w))
                                {
                                    wt = w;
                                }
                                string specs = "x";
                                if (dr["Specs"].ToString().Length == 0)
                                {
                                    specs = "";
                                }
                                int rows = 0;
                                if (dr["rows"].ToString().Length > 0)
                                {
                                    rows = int.Parse(dr["rows"].ToString());
                                }
                                _effortMonth.Add(dr["SamplingGUID"].ToString(), (dr["RefNo"].ToString(), DateTime.Parse(dr["SamplingDate"].ToString()),
                                dr["FishingGround"].ToString(), dr["SubGrid"].ToString(), dr["EnumeratorName"].ToString(), dr["Notes"].ToString(), wt,
                                bool.Parse(dr["IsGrid25FG"].ToString()), specs, rows));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex.Message, "Sampling.cs", "Sampling.SamplingSummaryForMonth");
                }
            }
            return _effortMonth;
        }

        public static List<int> GetSamplingYears(string targetAreaGuid = "")
        {
            List<int> sampledYears = new List<int>();
            var sql = "";
            if (targetAreaGuid.Length == 0)
            {
                sql = @"SELECT Year([SamplingDate]) AS samplingYear,
                         Count(tblSampling.SamplingGUID) AS n
                       FROM tblSampling
                       GROUP BY Year([SamplingDate])";
            }
            else
            {
                sql = $@"SELECT Year([SamplingDate]) AS samplingYear,
                            Count(tblSampling.SamplingGUID) AS n
                         FROM tblSampling
                         WHERE tblSampling.AOI = {{{targetAreaGuid}}}
                         GROUP BY Year([SamplingDate])";
            }
            var dt = new DataTable();
            try
            {
                using (var conection = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;data source=" + global.MDBPath))
                {
                    conection.Open();
                    var adapter = new OleDbDataAdapter(sql, conection);
                    adapter.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        sampledYears.Add(int.Parse(dr["samplingYear"].ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "Sampling", "GetSamplingYears");
            }
            return sampledYears;
        }

        public static void GetEngines()
        {
            var dt = new DataTable();
            using (var conection = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;data source=" + global.MDBPath))
            {
                try
                {
                    conection.Open();
                    const string query = "SELECT DISTINCT tblSampling.Engine FROM tblSampling WHERE tblSampling.Engine<>''";

                    var adapter = new OleDbDataAdapter(query, conection);
                    adapter.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        _engines.Add(dr["Engine"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex.Message, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
                }
            }
        }

        public static bool DeleteCatchLine(string CatchLineGUID)
        {
            bool Success = false;
            string query = "";
            using (OleDbConnection conn = new OleDbConnection(global.ConnectionString))
            {
                try
                {
                    query = $"Delete * from tblCatchDetail where CatchCompRow ={{{CatchLineGUID}}}";
                    OleDbCommand update = new OleDbCommand(query, conn);
                    conn.Open();
                    Success = (update.ExecuteNonQuery() > 0);
                    conn.Close();

                    query = $"Delete * from tblGMS where CatchCompRow ={{{CatchLineGUID}}}";
                    update = new OleDbCommand(query, conn);
                    conn.Open();
                    update.ExecuteNonQuery();
                    conn.Close();

                    query = $"Delete * from tblLF where CatchCompRow ={{{CatchLineGUID}}}";
                    update = new OleDbCommand(query, conn);
                    conn.Open();
                    update.ExecuteNonQuery();
                    conn.Close();

                    if (Success)
                    {
                        query = $"Delete * from tblCatchComp where RowGUID ={{{CatchLineGUID}}}";
                        update = new OleDbCommand(query, conn);
                        conn.Open();
                        Success = (update.ExecuteNonQuery() > 0);
                        conn.Close();
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex.Message, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
                }
            }
            return Success;
        }

        public static bool DeleteLFLine(string RowGUID)
        {
            bool Success = false;
            using (OleDbConnection conn = new OleDbConnection(global.ConnectionString))
            {
                try
                {
                    string query = $"Delete * from tblLF where RowGUID = {{{RowGUID}}}";
                    OleDbCommand update = new OleDbCommand(query, conn);
                    conn.Open();
                    Success = (update.ExecuteNonQuery() > 0);
                    conn.Close();
                }
                catch (Exception ex)
                {
                    Logger.Log(ex.Message, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
                }
            }
            return Success;
        }

        public static bool DeleteSamplingsInTargetArea(string targetAreaGuid)
        {
            int rowCount = 0;
            int deletedCount = 0;
            string query = $"SELECT tblSampling.SamplingGUID, RefNo FROM tblSampling WHERE tblSampling.AOI={{{targetAreaGuid}}}";
            var dt = new DataTable();
            SamplingEventArgs sve = new SamplingEventArgs(SamplingRecordStatus.BeginDeleteSampling);
            using (var conection = new OleDbConnection(global.ConnectionString))
            {
                try
                {
                    conection.Open();

                    var adapter = new OleDbDataAdapter(query, conection);
                    adapter.Fill(dt);
                    rowCount = dt.Rows.Count;

                    sve.RecordCount = rowCount;
                    OnDeleteSamplingStatus?.Invoke(null, sve);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        if (DeleteSampling(dr["SamplingGUID"].ToString()))
                        {
                            deletedCount++;
                            sve = new SamplingEventArgs(SamplingRecordStatus.DeleteSampling);
                            sve.ReferenceNumber = dr["RefNo"].ToString();
                            OnDeleteSamplingStatus?.Invoke(null, sve);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex.Message, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
                }
            }

            sve = new SamplingEventArgs(SamplingRecordStatus.EndDeleteSampling);
            OnDeleteSamplingStatus?.Invoke(null, sve);
            return deletedCount == rowCount;
        }

        public static bool DeleteSampling(string samplingGUID)
        {
            bool Success = false;
            List<string> myList = new List<string>();
            string query = "";
            //var dt = new DataTable();
            using (var conection = new OleDbConnection(global.ConnectionString))
            {
                conection.Open();
                //Delete GMS data
                query = $@"DELETE tblGMS.* FROM tblCatchComp INNER JOIN tblGMS ON
                        tblCatchComp.RowGUID = tblGMS.CatchCompRow WHERE SamplingGUID= {{{samplingGUID}}}";
                using (OleDbCommand update = new OleDbCommand(query, conection))
                {
                    update.ExecuteNonQuery();
                }

                //Delete LF data
                query = $@"DELETE tblLF.* FROM tblCatchComp INNER JOIN tblLF ON
                        tblCatchComp.RowGUID = tblLF.CatchCompRow
                        WHERE tblCatchComp.SamplingGUID = {{{samplingGUID}}}";
                using (OleDbCommand update = new OleDbCommand(query, conection))
                {
                    update.ExecuteNonQuery();
                }

                //Delete CatchDetail data
                query = $@"DELETE tblCatchDetail.* FROM tblCatchComp INNER JOIN tblCatchDetail
                        ON tblCatchComp.RowGUID = tblCatchDetail.CatchCompRow
                        WHERE tblCatchComp.SamplingGUID = {{{samplingGUID}}}";
                using (OleDbCommand update = new OleDbCommand(query, conection))
                {
                    update.ExecuteNonQuery();
                }

                //we delete the children catch composition data
                query = $"Delete * from tblCatchComp WHERE tblCatchComp.SamplingGUID = {{{samplingGUID}}}";
                using (OleDbCommand update = new OleDbCommand(query, conection))
                {
                    update.ExecuteNonQuery();
                }

                //we delete the gear specs of the sampling
                query = $"Delete * from tblSampledGearSpec where SamplingGUID = {{{samplingGUID}}}";
                using (OleDbCommand update = new OleDbCommand(query, conection))
                {
                    update.ExecuteNonQuery();
                }

                //we delete operating expenses
                query = $"Delete * from tblFishingExpenseItems where SamplingGuid =  {{{samplingGUID}}}";
                using (OleDbCommand update = new OleDbCommand(query, conection))
                {
                    Success = (update.ExecuteNonQuery() > 0);
                }

                query = $"Delete * from tblFishingExpense where SamplingGUID =  {{{samplingGUID}}}";
                using (OleDbCommand update = new OleDbCommand(query, conection))
                {
                    Success = (update.ExecuteNonQuery() > 0);
                }

                //delete additional fishing ground
                query = $"Delete * from tblGrid where SamplingGUID = {{{samplingGUID}}}";
                using (OleDbCommand update = new OleDbCommand(query, conection))
                {
                    Success = (update.ExecuteNonQuery() > 0);
                }

                //now we delete the parent catch and effort data
                query = $"Delete * from tblSampling where SamplingGUID = {{{samplingGUID}}}";
                using (OleDbCommand update = new OleDbCommand(query, conection))
                {
                    Success = (update.ExecuteNonQuery() > 0);
                }
            }

            return Success;
        }

        /// <summary>
        /// setup a struct that contain elements of fish landing sampling
        /// to be used later for validation and error checking the sampling form
        /// </summary>
        static public void SetUpUIElement()
        {
            string xmlFile = global.AppPath + "\\UITable.xml";
            var doc = new XmlDocument();
            doc.Load(xmlFile);
            XmlElement root = doc.DocumentElement;
            XmlNodeList nodeList = root.SelectNodes("//UIRow");
            foreach (XmlNode n in nodeList)
            {
                UserInterfaceStructure uistruct = new UserInterfaceStructure();
                foreach (XmlNode c in n)
                {
                    string val = c.Name;
                    switch (val)
                    {
                        case "Required":
                            uistruct.Required = bool.Parse(c.InnerText);
                            break;

                        case "ToolTip":
                            uistruct.ToolTip = c.InnerText;
                            break;

                        case "ReadOnly":
                            uistruct.ReadOnly = bool.Parse(c.InnerText);
                            break;

                        case "DataType":
                            uistruct.DataType = c.InnerText;
                            break;

                        case "Key":
                            uistruct.Key = c.InnerText;
                            break;

                        case "Label":
                            uistruct.Label = c.InnerText;
                            break;

                        case "Height":
                            uistruct.Height = int.Parse(c.InnerText);
                            break;

                        case "ButtonText":
                            uistruct.ButtonText = c.InnerText;
                            break;

                        case "control":
                            switch (c.InnerText)
                            {
                                case "TextBox":
                                    uistruct.Control = UIControlType.TextBox;
                                    break;

                                case "ComboBox":
                                    uistruct.Control = UIControlType.ComboBox;
                                    break;

                                case "CheckBox":
                                    uistruct.Control = UIControlType.Check;
                                    break;

                                case "MaskDate":
                                    uistruct.Control = UIControlType.DateMask;
                                    break;

                                case "MaskTime":
                                    uistruct.Control = UIControlType.TimeMask;
                                    break;

                                default:
                                    uistruct.Control = UIControlType.Spacer;
                                    break;
                            }
                            break;
                    }
                }

                if (uistruct.Key != "spacer")
                {
                    try
                    {
                        _uis.Add(uistruct.Key, uistruct);
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError(ex);
                    }
                }
            }
        }

        /// <summary>
        /// reads the catch and effort data of a sampled fish landing and returns a
        /// Dictionary object of keys and values
        /// </summary>
        /// <returns></returns>
        ///

        public void CatchAndEffortOfSampling(string samplingGuid)
        {
            //if (SamplingsForMonth.ContainsKey(samplingGuid))
            //{
            var myDT = new DataTable();
            var sampling = SamplingsForMonth[samplingGuid];
            try
            {
                using (var conection = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;data source=" + global.MDBPath))
                {
                    conection.Open();

                    string query = $@"SELECT tblSampling.*, tblAOI.AOIName, tblLandingSites.LSName, tblGearVariations.Variation, tblGearClass.GearClassName,
                                tblGearClass.GearClass, tblEnumerators.EnumeratorName,
                                (Select distinct 'x' as HasExpense from tblFishingExpense where tblFishingExpense.SamplingGuid=[tblSampling.SamplingGuid]) AS HasExpense
                                FROM(tblAOI
                                RIGHT JOIN tblLandingSites ON tblAOI.AOIGuid = tblLandingSites.AOIGuid)
                                RIGHT JOIN((tblGearClass
                                RIGHT JOIN tblGearVariations ON tblGearClass.GearClass = tblGearVariations.GearClass)
                                RIGHT JOIN(tblEnumerators
                                RIGHT JOIN tblSampling ON tblEnumerators.EnumeratorID = tblSampling.Enumerator) ON tblGearVariations.GearVarGUID = tblSampling.GearVarGUID)
                                ON tblLandingSites.LSGUID = tblSampling.LSGUID
                                WHERE tblSampling.SamplingGUID= {{{samplingGuid}}}";

                    var adapter = new OleDbDataAdapter(query, conection);
                    adapter.Fill(myDT);
                    if (myDT.Rows.Count > 0)
                    {
                        DataRow dr = myDT.Rows[0];
                        sampling.SamplingSummary.TargetAreaName = dr["AOIName"].ToString();
                        sampling.SamplingSummary.LandingSiteName = dr["LSName"].ToString();
                        sampling.SamplingSummary.GearClassGuid = dr["GearClass"].ToString();
                        sampling.SamplingSummary.GearClassName = dr["GearClassName"].ToString();
                        sampling.SamplingSummary.OperatingExpenseIndicator = dr["HasExpense"].ToString();
                        sampling.GearVariationGuid = dr["GearVarGUID"].ToString();
                        sampling.SamplingSummary.GearVariationName = dr["Variation"].ToString();
                        sampling.FishingGroundList = FishingGroundFromSampling(samplingGuid);
                        sampling.SamplingSummary.EnumeratorName = dr["EnumeratorName"].ToString();

                        sampling.EnumeratorGuid = dr["Enumerator"].ToString();
                        DateTime samplingDateTime = DateTime.Parse(dr["SamplingDate"].ToString());
                        DateTime samplingTime = DateTime.Parse(dr["SamplingTime"].ToString());
                        sampling.SamplingDateTime = samplingDateTime.Add(new TimeSpan(samplingTime.Hour, samplingTime.Minute, 0));

                        if (DateTime.TryParse(dr["DateSet"].ToString(), out DateTime ds)
                            && DateTime.TryParse(dr["TimeSet"].ToString(), out DateTime ts))
                        {
                            sampling.GearSettingDateTime = ds.AddHours((double)ts.Hour + ((Double)ts.Minute) / 60);
                        }

                        if (DateTime.TryParse(dr["DateHauled"].ToString(), out DateTime dh)
                            && DateTime.TryParse(dr["TimeHauled"].ToString(), out DateTime th))
                        {
                            sampling.GearHaulingDateTime = dh.AddHours((double)th.Hour + ((Double)th.Minute) / 60);
                        }

                        if (double.TryParse(dr["WtCatch"].ToString(), out double wc))
                        {
                            sampling.CatchWeight = wc;
                        }
                        else
                        {
                            sampling.CatchWeight = null;
                        }

                        if (double.TryParse(dr["WtSample"].ToString(), out double ws))
                        {
                            sampling.SampleWeight = ws;
                        }
                        else
                        {
                            sampling.SampleWeight = null;
                        }

                        if (int.TryParse(dr["NoHauls"].ToString(), out int nh))
                        {
                            sampling.NumberOfHauls = nh;
                        }
                        else
                        {
                            sampling.NumberOfHauls = null;
                        }

                        if (int.TryParse(dr["NoFishers"].ToString(), out int nf))
                        {
                            sampling.NumberOfFishers = nf;
                        }
                        else
                        {
                            sampling.NumberOfFishers = null;
                        }
                        sampling.HasLiveFish = Convert.ToBoolean(dr["HasLiveFish"]);

                        double? breadth = null;
                        double? length = null;
                        double? depth = null;

                        if (double.TryParse(dr["len"].ToString(), out double len))
                        {
                            length = len;
                        }

                        if (double.TryParse(dr["wdt"].ToString(), out double wdt))
                        {
                            breadth = wdt;
                        }

                        if (double.TryParse(dr["hgt"].ToString(), out double hgt))
                        {
                            depth = hgt;
                        }

                        FishingVessel fv = new FishingVessel((VesselType)(int)dr["VesType"], breadth, depth, length);
                        fv.Engine = dr["Engine"].ToString();

                        //fv.VesselID = dr["VesselID"].ToString();

                        fv.EngineHorsepower = null;
                        if (double.TryParse(dr["hp"].ToString(), out double hp))
                        {
                            fv.EngineHorsepower = hp;
                        }
                        sampling.FishingVessel = fv;

                        sampling.Notes = dr["Notes"].ToString();

                        if (DateTime.TryParse(dr["DateEncoded"].ToString(), out DateTime de))
                        {
                            sampling.DateEncoded = de;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex.StackTrace);
            }
            //}
        }

        public Dictionary<string, string> GearsFromLandingSite(string lsguid)
        {
            Dictionary<string, string> myList = new Dictionary<string, string>();
            var dt = new DataTable();
            using (var conection = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;data source=" + global.MDBPath))
            {
                try
                {
                    conection.Open();
                    string query = $@"SELECT DISTINCT tblSampling.GearVarGUID, Variation FROM tblGearVariations
                                   INNER JOIN tblSampling ON tblGearVariations.GearVarGUID = tblSampling.GearVarGUID
                                   WHERE tblSampling.LSGUID ={{{lsguid}}}";

                    var adapter = new OleDbDataAdapter(query, conection);
                    adapter.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        myList.Add(dr["GearVarGUID"].ToString(), dr["Variation"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex.Message, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
                }
                return myList;
            }
        }

        public List<string> MonthsFromLSandGear(string landingsiteguid, string gearguid)
        {
            List<string> myList = new List<string>();
            var dt = new DataTable();
            using (var conection = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;data source=" + global.MDBPath))
            {
                try
                {
                    conection.Open();
                    string query = $@"SELECT DISTINCT Format([SamplingDate],'mmm - yyyy') AS sMonthYear
                                   FROM tblSampling WHERE GearVarGUID='{" + gearguid + "}' AND
                                   LSGUID={{{landingsiteguid}}} ORDER BY
                                   Format([SamplingDate],'mmm - yyyy')";

                    var adapter = new OleDbDataAdapter(query, conection);
                    adapter.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        myList.Add(dr["sMonthYear"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex.Message, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
                }
            }
            return myList;
        }

        public Sampling ReadSamplings(string samplingGuid = "")
        {
            var dt = new DataTable();
            using (var conection = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;data source=" + global.MDBPath))
            {
                try
                {
                    conection.Open();
                    string query = "";
                    if (samplingGuid.Length > 0)
                    {
                        query = $"SELECT * FROM tblSampling WHERE SamplingGUID={{{samplingGuid}}}";
                    }
                    else
                    {
                        query = $"SELECT * FROM tblSampling WHERE tblSampling.AOI={{{_targetAreaGuid}}}";
                    }

                    var adapter = new OleDbDataAdapter(query, conection);
                    adapter.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        DateTime samplingDateTime = ((DateTime)dr["SamplingDate"]).Date.Add(((DateTime)dr["SamplingTime"]).TimeOfDay);
                        string samplingGUID = dr["SamplingGUID"].ToString();
                        Sampling s = new Sampling(dr["AOI"].ToString(),
                                                  samplingGUID,
                                                  samplingDateTime,
                                                  dr["LSGUID"].ToString(),
                                                  dr["RefNo"].ToString());
                        s.GearVariationGuid = dr["GearVarGUID"].ToString();
                        s.EnumeratorGuid = dr["Enumerator"].ToString();

                        //fishing vessel
                        double? breadth = dr["wdt"].ToString().Length > 0 ?
                            (double?)dr["wdt"] : null;
                        double? depth = dr["hgt"].ToString().Length > 0 ?
                            (double?)dr["hgt"] : null;
                        double? length = dr["len"].ToString().Length > 0 ?
                            (double?)dr["len"] : null;

                        if (dr["DateSet"].ToString().Length > 0)
                        {
                            s.GearSettingDateTime = GetDateTime(dr["DateSet"].ToString(), dr["TimeSet"].ToString());
                        }
                        if (dr["DateHauled"].ToString().Length > 0)
                        {
                            s.GearHaulingDateTime = GetDateTime(dr["DateHauled"].ToString(), dr["TimeHauled"].ToString());
                        }
                        if (dr["DateEncoded"].ToString().Length > 0)
                        {
                            s.DateEncoded = DateTime.Parse(dr["DateEncoded"].ToString());
                        }
                        string wtCatch = dr["WtCatch"].ToString();
                        if (wtCatch.Length > 0)
                        {
                            s.CatchWeight = double.Parse(wtCatch);
                            string wtSample = dr["WtSample"].ToString();
                            if (wtSample.Length > 0)
                            {
                                s.SampleWeight = double.Parse(wtSample);
                            }
                        }
                        s.HasLiveFish = (bool)dr["HasLiveFish"];
                        s.Notes = dr["Notes"].ToString();
                        if (dr["NoFishers"].ToString().Length > 0)
                        {
                            s.NumberOfFishers = (int)dr["NoFishers"];
                        }
                        if (dr["NoHauls"].ToString().Length > 0)
                        {
                            s.NumberOfHauls = (int)dr["NoHauls"];
                        }

                        VesselType vt = VesselType.NotDetermined;
                        if (dr["VesType"].ToString().Length > 0)
                        {
                            vt = (VesselType)(int)dr["VesType"];
                        }
                        FishingVessel fv = new FishingVessel(vt, breadth, depth, length);
                        fv.Engine = dr["Engine"].ToString();
                        fv.EngineHorsepower = dr["hp"].ToString().Length == 0 ? null : (double?)dr["hp"];
                        s.FishingVessel = fv;

                        s.CatchComposition = CatchComposition.RetrieveCatchComposition(samplingGUID);
                        //s.FishingGrounds = GetFishingGrounds(samplingGUID);
                        s.FishingGroundList = GetFishingGroundsEx(samplingGUID);

                        if (samplingGuid.Length > 0)
                        {
                            return s;
                        }
                        //else
                        //{
                        //FishCatchMonitoringSamplings.Add(samplingGUID, s);
                        //}
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex.Message, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
                }
            }
            return null;
        }

        private List<FishingGround> GetFishingGroundsEx(string samplingGuid)
        {
            List<FishingGround> fgs = new List<FishingGround>();
            string sql = $@"SELECT FishingGround,Subgrid
                            FROM tblSampling
                            WHERE SamplingGUID={{{samplingGuid}}} AND FishingGround <> """"
                            union all

                            SELECT GridName,Subgrid
                            FROM tblGrid
                            WHERE SamplingGUID={{{samplingGuid}}}";
            var dt = new DataTable();
            using (var conection = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;data source=" + global.MDBPath))
            {
                try
                {
                    conection.Open();
                    var adapter = new OleDbDataAdapter(sql, conection);
                    adapter.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        string fg = dr["FishingGround"].ToString();
                        int? sg = null;
                        if (int.TryParse(dr["SubGrid"].ToString(), out int subg))
                        {
                            sg = subg;
                        }
                        fgs.Add(new FishingGround(fg, sg));
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex.Message, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
                }
            }
            return fgs;
        }

        private List<(string FishingGround, string SubGrid)> GetFishingGrounds(string samplingGuid)
        {
            List<(string FishingGround, string SubGrid)> fgs = new List<(string FishingGround, string SubGrid)>();
            string sql = $@"SELECT FishingGround,Subgrid
                            FROM tblSampling
                            WHERE SamplingGUID={{{samplingGuid}}} AND FishingGround <> """"
                            union all

                            SELECT GridName,Subgrid
                            FROM tblGrid
                            WHERE SamplingGUID={{{samplingGuid}}}";
            var dt = new DataTable();
            using (var conection = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;data source=" + global.MDBPath))
            {
                try
                {
                    conection.Open();
                    var adapter = new OleDbDataAdapter(sql, conection);
                    adapter.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        string fg = dr["FishingGround"].ToString();
                        string sg = dr["SubGrid"].ToString();
                        fgs.Add((fg, sg));
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex.Message, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
                }
            }
            return fgs;
        }

        private DateTime GetDateTime(string date, string time)
        {
            return DateTime.Parse(date).Date.Add((DateTime.Parse(time)).TimeOfDay);
        }

        public List<string> OtherFishingGround()
        {
            List<string> myList = new List<string>();
            var dt = new DataTable();
            using (var conection = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;data source=" + global.MDBPath))
            {
                try
                {
                    conection.Open();
                    string query = $"Select GridName from tblGrid where SamplingGUID = {{{_samplingGUID}}}";
                    var adapter = new OleDbDataAdapter(query, conection);
                    adapter.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        myList.Add(dr["GridName"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex.Message, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
                }
            }
            return myList;
        }

        public void ReadUIFromXML()
        {
            var innerText = "";
            string xmlFile = global.AppPath + "\\UITable.xml";
            if (System.IO.File.Exists(xmlFile))
            {
                var doc = new XmlDocument();
                doc.Load(xmlFile);
                XmlElement root = doc.DocumentElement;
                XmlNodeList nodeList = root.SelectNodes("//UIRow");
                foreach (XmlNode n in nodeList)
                {
                    UIRowFromXML row = new UIRowFromXML();
                    foreach (XmlNode c in n)
                    {
                        string val = c.Name;
                        switch (val)
                        {
                            case "Required":
                                row.Required = bool.Parse(c.InnerText);
                                break;

                            case "ToolTip":
                                row.ToolTip = c.InnerText;
                                break;

                            case "ReadOnly":
                                row.ReadOnly = bool.Parse(c.InnerText);
                                break;

                            case "DataType":
                                row.DataType = c.InnerText;
                                break;

                            case "Key":
                                row.Key = c.InnerText;
                                innerText = row.Key;
                                break;

                            case "Label":
                                row.RowLabel = c.InnerText;
                                break;

                            case "Height":
                                row.Height = int.Parse(c.InnerText);
                                break;

                            case "ButtonText":
                                row.ButtonText = c.InnerText;
                                break;

                            case "control":
                                switch (c.InnerText)
                                {
                                    case "TextBox":
                                        row.Control = UIControlType.TextBox;
                                        break;

                                    case "ComboBox":
                                        row.Control = UIControlType.ComboBox;
                                        break;

                                    case "CheckBox":
                                        row.Control = UIControlType.Check;
                                        break;

                                    case "MaskDate":
                                        row.Control = UIControlType.DateMask;
                                        break;

                                    case "MaskTime":
                                        row.Control = UIControlType.TimeMask;
                                        break;

                                    default:
                                        row.Control = UIControlType.Spacer;
                                        break;
                                }
                                break;
                        }
                    }

                    OnUIRowRead(this, row);
                }
            }
        }

        public bool UpdateEffort(bool isNew, Sampling sampling)
        {
            bool success = false;
            string updateQuery = "";
            string dateSet = "null";
            string timeSet = "null";
            string dateHauled = "null";
            string timeHauled = "null";
            string subGrid = "null";
            string fishingGrid = "null";
            string wtCatch = sampling.CatchWeight.ToString();
            string wtSample = sampling.SampleWeight.ToString();
            string noHauls = sampling.NumberOfHauls.ToString();
            string noFishers = sampling.NumberOfFishers.ToString();
            string engineHp = sampling.FishingVessel.EngineHorsepower.ToString();
            string breadth = sampling.FishingVessel.Breadth.ToString();
            string depth = sampling.FishingVessel.Depth.ToString();
            string length = sampling.FishingVessel.Length.ToString();
            string enumeratorGuid = sampling.EnumeratorGuid;
            string dateEncoded = sampling.DateEncoded.ToString();
            string timeSampled = string.Format("{0:HH:mm}", sampling.SamplingDateTime);
            string dateSampled = sampling.SamplingDateTime.Date.ToString("MMM-yyy-dd");

            //msaccess sql requires date time values to be formatted as strings so we just
            //enclose the values with single quotes
            if (sampling.GearSettingDateTime != null)
            {
                timeSet = $"'{string.Format("{0:HH:mm}", sampling.GearSettingDateTime)}'";
                dateSet = $"'{sampling.GearSettingDateTime.Value.Date.ToString("MMM-yyy-dd")}'";
            }
            if (sampling.GearHaulingDateTime != null)
            {
                timeHauled = $"'{string.Format("{0:HH:mm}", sampling.GearHaulingDateTime)}'";
                dateHauled = $"'{sampling.GearHaulingDateTime.Value.Date.ToString("MMM-yyy-dd")}'";
            }
            if (sampling.FishingGroundList.Count > 0)
            {
                fishingGrid = sampling.FishingGroundList[0].GridName;
                if (sampling.FishingGroundList[0].SubGrid != null)
                {
                    subGrid = sampling.FishingGroundList[0].SubGrid.ToString();
                }
            }
            if (wtCatch.Length == 0)
            {
                wtCatch = "null";
            }
            if (wtSample.Length == 0)
            {
                wtSample = "null";
            }
            if (noFishers.Length == 0)
            {
                noFishers = "null";
            }
            if (noHauls.Length == 0)
            {
                noHauls = "null";
            }
            if (engineHp.Length == 0)
            {
                engineHp = "null";
            }
            if (breadth.Length == 0)
            {
                breadth = "null";
            }
            if (depth.Length == 0)
            {
                depth = "null";
            }
            if (length.Length == 0)
            {
                length = "null";
            }
            if (enumeratorGuid.Length == 0 || enumeratorGuid == "{}")
            {
                enumeratorGuid = "null";
            }
            else
            {
                enumeratorGuid = $"{{{enumeratorGuid}}}";
            }
            if (dateEncoded.Length == 0)
            {
                dateEncoded = "null";
            }
            else
            {
                dateEncoded = $"'{dateEncoded}'";
            }
            using (OleDbConnection conn = new OleDbConnection(global.ConnectionString))
            {
                if (isNew)
                {
                    updateQuery = $@"Insert into tblSampling (SamplingGUID, GearVarGUID, AOI, RefNo, SamplingDate, SamplingTime,
                            FishingGround, SubGrid, TimeSet, DateSet, TimeHauled, DateHauled, NoHauls, NoFishers, Engine, hp,
                            WtCatch, WtSample, len, wdt, hgt, LSGUID,  Notes, VesType, SamplingType, HasLiveFish, Enumerator,
                            DateEncoded) values (
                            {{{sampling.SamplingGUID}}},
                            {{{sampling.GearVariationGuid}}},
                            {{{sampling.TargetAreaGuid}}},
                            '{sampling.ReferenceNumber}',
                            '{dateSampled}',
                            '{timeSampled}',
                            '{fishingGrid}',
                            {subGrid},
                            {timeSet},
                            {dateSet},
                            {timeHauled},
                            {dateHauled},
                            {noHauls},
                            {noFishers},
                            '{sampling.FishingVessel.Engine}',
                            {engineHp},
                            {wtCatch},
                            {wtSample},
                            {length},
                            {breadth},
                            {depth},
                            {{{sampling.LandingSiteGuid}}},
                            '{sampling.Notes}',
                            {(int)sampling.FishingVessel.VesselType},
                            {(int)sampling.SamplingType},
                            {sampling.HasLiveFish.ToString()},
                            {enumeratorGuid},
                            {dateEncoded})";
                }
                else
                {
                    updateQuery = $@"Update tblSampling set
                            GearVarGUID ={{{sampling.GearVariationGuid}}},
                            AOI ={{{sampling.TargetAreaGuid}}},
                            RefNo ='{sampling.ReferenceNumber}',
                            SamplingDate ='{sampling.SamplingDateTime.Date}',
                            SamplingTime ='{sampling.SamplingDateTime.TimeOfDay}',
                            FishingGround = '{sampling.FishingGroundList[0].GridName}',
                            SubGrid = {sampling.FishingGroundList[0].SubGrid.ToString()},
                            TimeSet ={timeSet},
                            DateSet = {dateSet},
                            TimeHauled = {timeHauled},
                            DateHauled = {dateHauled},
                            NoHauls = {sampling.NumberOfHauls},
                            NoFishers = {sampling.NumberOfFishers},
                            Engine ='{sampling.FishingVessel.Engine}',
                            hp = {sampling.FishingVessel.EngineHorsepower.ToString()},
                            WtCatch ={sampling.CatchWeight.ToString()},
                            WtSample ={sampling.SampleWeight.ToString()},
                            len ={sampling.FishingVessel.Length.ToString()},
                            wdt ={sampling.FishingVessel.Breadth.ToString()},
                            hgt ={sampling.FishingVessel.Depth.ToString()},
                            LSGUID ={{{sampling.LandingSiteGuid}}},
                            Notes = '{sampling.Notes}',
                            VesType ={(int)sampling.FishingVessel.VesselType},
                            SamplingType ={(int)sampling.SamplingType},
                            HasLiveFish = {sampling.HasLiveFish},
                            Enumerator = {{{sampling.EnumeratorGuid}}}
                            Where SamplingGUID = {{{sampling.SamplingGUID}}}";
                }
                using (OleDbCommand update = new OleDbCommand(updateQuery, conn))
                {
                    conn.Open();
                    try
                    {
                        success = (update.ExecuteNonQuery() > 0);
                    }
                    //catch (OleDbException)
                    //{
                    //    success = false;
                    //}
                    catch (Exception ex)
                    {
                        Logger.LogError(ex.Message, ex.StackTrace);
                        //Logger.Log(updateQuery);
                        success = false;
                    }
                    conn.Close();
                }
                if (success)
                {
                    if (sampling.FishingGroundList.Count > 1)
                    {
                        SaveAdditionalFishingGroundsEx(sampling.FishingGroundList, sampling.SamplingGUID);
                    }
                    if (OnEffortUpdated != null)
                    {
                        EffortEventArg e = new EffortEventArg(sampling.SamplingDateTime.Date, sampling.GearVariationGuid, sampling.LandingSiteGuid);
                        e.CatchWeight = sampling.CatchWeight;
                        e.SampleWeight = sampling.SampleWeight;
                        OnEffortUpdated(this, e);
                    }
                }
            }

            return success;
        }

        public bool UpdateEffort(Sampling sampling)
        {
            bool success = false;
            string updateQuery = "";
            string engine = "";
            //sampling.ClearFishingGroundList();
            using (OleDbConnection conn = new OleDbConnection(global.ConnectionString))
            {
                try
                {
                    if (sampling.IsNew)
                    {
                        if (sampling.FishingVessel != null)
                        {
                            updateQuery = $@"Insert into tblSampling (SamplingGUID, GearVarGUID, AOI, RefNo, SamplingDate, SamplingTime,
                            FishingGround, SubGrid, TimeSet, DateSet, TimeHauled, DateHauled, NoHauls, NoFishers, Engine, hp,
                            WtCatch, WtSample, len, wdt, hgt, LSGUID,  Notes, VesType, SamplingType, HasLiveFish, Enumerator, DateEncoded)
                            values (
                            {{{sampling.SamplingGUID}}},
                            {{{sampling.GearVariationGuid}}},
                            {{{sampling.TargetAreaGuid}}},
                            '{sampling.ReferenceNumber}',
                            '{sampling.SamplingDateTime.Date}',
                            '{sampling.SamplingDateTime.TimeOfDay}',
                            '{sampling.FirstFishingGround}',
                            {(sampling.FirstSubGrid == null ? "Null" : sampling.FirstSubGrid.Value.ToString())},
                            {(sampling.GearSettingDateTime == null ? "Null" : "'" + sampling.GearSettingDateTime.Value.TimeOfDay.ToString() + "'")},
                            {(sampling.GearSettingDateTime == null ? "Null" : "'" + sampling.GearSettingDateTime.Value.Date.ToString() + "'")},
                            {(sampling.GearHaulingDateTime == null ? "Null" : "'" + sampling.GearHaulingDateTime.Value.TimeOfDay.ToString() + "'")},
                            {(sampling.GearHaulingDateTime == null ? "Null" : "'" + sampling.GearHaulingDateTime.Value.Date.ToString() + "'")},
                            {(sampling.NumberOfHauls == null ? "Null" : sampling.NumberOfHauls.ToString())},
                            {(sampling.NumberOfFishers == null ? "Null" : sampling.NumberOfFishers.ToString())},
                            '{(sampling.FishingVessel.Engine == null ? "" : sampling.FishingVessel.Engine)}',
                            {(sampling.FishingVessel.EngineHorsepower == null ? "Null" : sampling.FishingVessel.EngineHorsepower.ToString())},
                            {sampling.CatchWeight},
                            {(sampling.SampleWeight == null ? "Null" : sampling.SampleWeight.ToString())},
                            {(sampling.FishingVessel.Length == null ? "Null" : sampling.FishingVessel.Length.ToString())},
                            {(sampling.FishingVessel.Breadth == null ? "Null" : sampling.FishingVessel.Breadth.ToString())},
                            {(sampling.FishingVessel.Depth == null ? "Null" : sampling.FishingVessel.Depth.ToString())},
                            {{{sampling.LandingSiteGuid}}},
                            '{sampling.Notes}',
                            {(int)sampling.FishingVessel.VesselType},
                            {(int)sampling.SamplingType},
                            {sampling.HasLiveFish.ToString()},
                            {{{sampling.EnumeratorGuid}}},
                            '{sampling.DateEncoded.ToString()}')";
                            // removed '{EffortData["VesselID"]}',
                        }
                        else
                        {
                            //update query if there is no fishing vessel used
                            updateQuery = $@"Insert into tblSampling (SamplingGUID, GearVarGUID, AOI, RefNo, SamplingDate, SamplingTime,
                            FishingGround, SubGrid, TimeSet, DateSet, TimeHauled, DateHauled, NoHauls, NoFishers, WtCatch, WtSample,
                            LSGUID,  Notes, VesType, SamplingType, HasLiveFish, Enumerator, DateEncoded, len, wdt, hgt, Engine, hp)
                            values (
                            {{{sampling.SamplingGUID}}},
                            {{{sampling.GearVariationGuid}}},
                            {{{sampling.TargetAreaGuid}}},
                            '{sampling.ReferenceNumber}',
                            '{sampling.SamplingDateTime.Date}',
                            '{sampling.SamplingDateTime.TimeOfDay}',
                            '{sampling.FirstFishingGround}',
                            {(sampling.FirstSubGrid == null ? "Null" : sampling.FirstSubGrid.Value.ToString())},
                            {(sampling.GearSettingDateTime == null ? "Null" : "'" + sampling.GearSettingDateTime.Value.TimeOfDay.ToString() + "'")},
                            {(sampling.GearSettingDateTime == null ? "Null" : "'" + sampling.GearSettingDateTime.Value.Date.ToString() + "'")},
                            {(sampling.GearHaulingDateTime == null ? "Null" : "'" + sampling.GearHaulingDateTime.Value.TimeOfDay.ToString() + "'")},
                            {(sampling.GearHaulingDateTime == null ? "Null" : "'" + sampling.GearHaulingDateTime.Value.Date.ToString() + "'")},
                            {(sampling.NumberOfHauls == null ? "Null" : sampling.NumberOfHauls.ToString())},
                            {(sampling.NumberOfFishers == null ? "Null" : sampling.NumberOfFishers.ToString())},
                            {sampling.CatchWeight},
                            {(sampling.SampleWeight == null ? "Null" : sampling.SampleWeight.ToString())},
                            {{{sampling.LandingSiteGuid}}},
                            '{sampling.Notes}',
                            {(int)VesselType.NoVesselUsed},
                            {(int)sampling.SamplingType},
                            {sampling.HasLiveFish.ToString()},
                            {{{sampling.EnumeratorGuid}}},
                            '{sampling.DateEncoded.ToString()}',
                            Null, Null, Null, '', Null)";
                        }
                    }
                    else
                    {
                        if (sampling.FishingVessel != null)
                        {
                            updateQuery = $@"Update tblSampling set
                            GearVarGUID ={{{sampling.GearVariationGuid}}},
                            AOI ={{{sampling.TargetAreaGuid}}},
                            RefNo ='{sampling.ReferenceNumber}',
                            SamplingDate ='{sampling.SamplingDateTime.Date}',
                            SamplingTime ='{sampling.SamplingDateTime.TimeOfDay}',
                            FishingGround = '{sampling.FirstFishingGround}',
                            SubGrid = {(sampling.FirstSubGrid == null ? "Null" : sampling.FirstSubGrid.Value.ToString())},
                            TimeSet ={(sampling.GearSettingDateTime == null ? "Null" : "'" + sampling.GearSettingDateTime.Value.TimeOfDay.ToString() + "'")},
                            DateSet = {(sampling.GearSettingDateTime == null ? "Null" : "'" + sampling.GearSettingDateTime.Value.Date.ToString() + "'")},
                            TimeHauled = {(sampling.GearHaulingDateTime == null ? "Null" : "'" + sampling.GearHaulingDateTime.Value.TimeOfDay.ToString() + "'")},
                            DateHauled = {(sampling.GearHaulingDateTime == null ? "Null" : "'" + sampling.GearHaulingDateTime.Value.Date.ToString() + "'")},
                            NoHauls = {(sampling.NumberOfHauls == null ? "Null" : sampling.NumberOfHauls.ToString())},
                            NoFishers = {(sampling.NumberOfFishers == null ? "Null" : sampling.NumberOfFishers.ToString())},
                            Engine ='{(sampling.FishingVessel == null ? "" : sampling.FishingVessel.Engine)}',
                            hp = {(sampling.FishingVessel == null ? "Null" : sampling.FishingVessel.EngineHorsepower == null ? "Null" : sampling.FishingVessel.EngineHorsepower.ToString())},
                            WtCatch ={sampling.CatchWeight.ToString()},
                            WtSample ={(sampling.SampleWeight == null ? "Null" : sampling.SampleWeight.ToString())},
                            len ={(sampling.FishingVessel.Length == null ? "Null" : sampling.FishingVessel.Length.ToString())},
                            wdt ={(sampling.FishingVessel.Breadth == null ? "Null" : sampling.FishingVessel.Breadth.ToString())},
                            hgt ={(sampling.FishingVessel.Depth == null ? "Null" : sampling.FishingVessel.Depth.ToString())},
                            LSGUID ={{{sampling.LandingSiteGuid}}},
                            Notes = '{sampling.Notes}',
                            VesType ={(int)sampling.FishingVessel.VesselType},
                            SamplingType ={(int)sampling.SamplingType},
                            HasLiveFish = {sampling.HasLiveFish.ToString()},
                            Enumerator = {{{sampling.EnumeratorGuid}}}
                            Where SamplingGUID = {{{sampling.SamplingGUID}}}";
                            //removed VesselID = '{EffortData["VesselID"]}
                        }
                        else
                        {
                            //update query if there is no fishing vessel used
                            updateQuery = $@"Update tblSampling set
                            GearVarGUID ={{{sampling.GearVariationGuid}}},
                            AOI ={{{sampling.TargetAreaGuid}}},
                            RefNo ='{sampling.ReferenceNumber}',
                            SamplingDate ='{sampling.SamplingDateTime.Date}',
                            SamplingTime ='{sampling.SamplingDateTime.TimeOfDay}',
                            FishingGround = '{sampling.FirstFishingGround}',
                            SubGrid = {(sampling.FirstSubGrid == null ? "Null" : sampling.FirstSubGrid.Value.ToString())},
                            TimeSet ={(sampling.GearSettingDateTime == null ? "Null" : "'" + sampling.GearSettingDateTime.Value.TimeOfDay.ToString() + "'")},
                            DateSet = {(sampling.GearSettingDateTime == null ? "Null" : "'" + sampling.GearSettingDateTime.Value.Date.ToString() + "'")},
                            TimeHauled = {(sampling.GearHaulingDateTime == null ? "Null" : "'" + sampling.GearHaulingDateTime.Value.TimeOfDay.ToString() + "'")},
                            DateHauled = {(sampling.GearHaulingDateTime == null ? "Null" : "'" + sampling.GearHaulingDateTime.Value.Date.ToString() + "'")},
                            NoHauls = {(sampling.NumberOfHauls == null ? "Null" : sampling.NumberOfHauls.ToString())},
                            NoFishers = {(sampling.NumberOfFishers == null ? "Null" : sampling.NumberOfFishers.ToString())},
                            WtCatch ={sampling.CatchWeight.ToString()},
                            WtSample ={(sampling.SampleWeight == null ? "Null" : sampling.SampleWeight.ToString())},
                            len = Null,
                            wdt = Null,
                            hgt = Null,
                            Engine='',
                            hp = Null,
                            LSGUID ={{{sampling.LandingSiteGuid}}},
                            Notes = '{sampling.Notes}',
                            VesType = {(int)VesselType.NoVesselUsed},
                            SamplingType ={(int)sampling.SamplingType},
                            HasLiveFish = {sampling.HasLiveFish.ToString()},
                            Enumerator = {{{sampling.EnumeratorGuid}}}
                            Where SamplingGUID = {{{sampling.SamplingGUID}}}";
                        }
                    }

                    using (OleDbCommand update = new OleDbCommand(updateQuery, conn))
                    {
                        conn.Open();
                        try
                        {
                            success = (update.ExecuteNonQuery() > 0);
                        }
                        catch (Exception ex)
                        {
                            Logger.Log(ex.Message, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
                        }
                        conn.Close();
                    }
                    if (success)
                    {
                        if (OnEffortUpdated != null)
                        {
                            EffortEventArg e = new EffortEventArg(sampling.SamplingDateTime.Date, sampling.GearVariationGuid, sampling.LandingSiteGuid);
                            e.CatchWeight = sampling.CatchWeight;
                            e.SampleWeight = sampling.SampleWeight;
                            OnEffortUpdated(this, e);
                        }

                        //if (sampling.FishingGroundList.Count > 1)
                        //{
                        SaveAdditionalFishingGroundsEx(sampling.FishingGroundList, sampling.SamplingGUID);
                        //}
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex.Message, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
                }
            }
            if (success)
            {
                if (sampling.FishingVessel?.Engine.Length > 0 && !_engines.Contains(sampling.FishingVessel?.Engine))
                {
                    _engines.Add(engine);
                }

                if (sampling.IsNew)
                {
                    //FishCatchMonitoringSamplings.Add(sampling.SamplingGUID, sampling);
                    SamplingsForMonth.Add(sampling.SamplingGUID, sampling);
                }
                else
                {
                    //if (FishCatchMonitoringSamplings.ContainsKey(sampling.SamplingGUID))
                    //{
                    //    FishCatchMonitoringSamplings[sampling.SamplingGUID] = sampling;
                    //}
                }
            }
            return success;
        }

        private void SaveAdditionalFishingGroundsEx(List<FishingGround> fishingGrounds, string SamplingGUID)
        {
            using (OleDbConnection conn = new OleDbConnection(global.ConnectionString))
            {
                conn.Open();
                var sql = $"Delete * from tblGrid where SamplingGuid = {{{SamplingGUID}}}";
                using (OleDbCommand update = new OleDbCommand(sql, conn))
                {
                    update.ExecuteNonQuery();
                }

                for (int n = 1; n < fishingGrounds.Count; n++)
                {
                    var fg = fishingGrounds[n].GridName;
                    var subGrid = fishingGrounds[n].SubGrid.ToString();
                    if (subGrid.Length == 0)
                    {
                        subGrid = "null";
                    }
                    sql = $@"Insert into tblGrid (SamplingGuid, GridName,RowGUID,SubGrid) values
                            (
                              {{{SamplingGUID}}},
                              '{fg}',
                              {{{Guid.NewGuid()}}},
                              {subGrid}
                            )";
                    using (OleDbCommand update = new OleDbCommand(sql, conn))
                    {
                        try
                        {
                            update.ExecuteNonQuery();
                        }
                        catch (OleDbException)
                        {
                        }
                        catch (Exception ex)
                        {
                            Logger.LogError(ex);
                        }
                    }
                }
            }
        }

        private void SaveAdditionalFishingGrounds(List<string> FishingGrounds, string SamplingGUID)
        {
            using (OleDbConnection conn = new OleDbConnection(global.ConnectionString))
            {
                conn.Open();
                var sql = $"Delete * from tblGrid where SamplingGuid = {{{SamplingGUID}}}";
                using (OleDbCommand update = new OleDbCommand(sql, conn))
                {
                    update.ExecuteNonQuery();
                }

                for (int n = 1; n < FishingGrounds.Count; n++)
                {
                    var fgParts = FishingGrounds[n].Split('-');
                    var fg = $"{fgParts[0]}-{fgParts[1]}";
                    var subGrid = "";
                    if (fgParts.Length == 3)
                    {
                        subGrid = fgParts[2];
                    }
                    else
                    {
                        subGrid = "Null";
                    }
                    sql = $@"Insert into tblGrid (SamplingGuid, GridName,RowGUID,SubGrid) values
                            (
                              {{{SamplingGUID}}},
                              '{fg}',
                              {{{Guid.NewGuid()}}},
                              {subGrid}
                            )";
                    using (OleDbCommand update = new OleDbCommand(sql, conn))
                    {
                        update.ExecuteNonQuery();
                    }
                }
                conn.Close();
            }
        }

        /// <summary>
        /// represents the data structure of effort data and will be
        /// used to generate the user interface of the
        /// effort data edit form
        /// </summary>
        public struct UserInterfaceStructure
        {
            public UIControlType _control;

            private string _ButtonText;

            private string _DataType;

            private int _Height;

            private string _Key;

            private string _Label;

            private bool _ReadOnly;

            private bool _Required;

            private string _ToolTip;

            //CONSTRUCTOR
            public UserInterfaceStructure(UIControlType c, string Label, string Key, int Height,
                                            string DataType, bool ReadOnly, string ToolTip,
                                            bool Required, string ButtonText = "")
            {
                _control = c;
                _Label = Label;
                _Key = Key;
                _ButtonText = ButtonText;
                _Height = Height;
                _DataType = DataType;
                _ReadOnly = ReadOnly;
                _ToolTip = ToolTip;
                _Required = Required;
            }

            public string ButtonText
            {
                get { return _ButtonText; }
                set { _ButtonText = value; }
            }

            public UIControlType Control
            {
                get { return _control; }
                set { _control = value; }
            }

            public string DataType
            {
                get { return _DataType; }
                set { _DataType = value; }
            }

            public int Height
            {
                get { return _Height; }
                set { _Height = value; }
            }

            public string Key
            {
                get { return _Key; }
                set { _Key = value; }
            }

            public string Label
            {
                get { return _Label; }
                set { _Label = value; }
            }

            public bool ReadOnly
            {
                get { return _ReadOnly; }
                set { _ReadOnly = value; }
            }

            public bool Required
            {
                get { return _Required; }
                set { _Required = value; }
            }

            public string ToolTip
            {
                get { return _ToolTip; }
                set { _ToolTip = value; }
            }
        }
    }
}