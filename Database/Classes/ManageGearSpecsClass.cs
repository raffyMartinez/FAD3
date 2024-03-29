﻿
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace FAD3.Database.Classes
{
    public static class ManageGearSpecsClass
    {
        private static string _gearVarGuid;
        private static string _gearVarName;
        private static string _samplingGuid = "";
        private static bool _HasSampledGearSpecs;
        private static bool _HasUnsavedSampledGearSpecEdits;

        //this field contains the template for gear specs of a given gear variation
        private static List<GearSpecification> _GearSpecifications = new List<GearSpecification>();

        //this field contains the spec data of the sampled gear
        private static Dictionary<string, SampledGearSpecData> _sampledGearSpecs = new Dictionary<string, SampledGearSpecData>();

        /// <summary>
        /// Assigns the SamplingGuid.
        /// A new GUID sets the _HasUnsavedSampledGearSpecEdits to false
        /// </summary>
        public static string SamplingGuid
        {
            get { return _samplingGuid; }
            set
            {
                //set flag to false if sampling guid changes
                if (_samplingGuid != value)
                    _HasUnsavedSampledGearSpecEdits = false;

                _samplingGuid = value;

                //get the specs of the sampled gear from the database
                //if (!_HasUnsavedSampledGearSpecEdits && _GearSpecifications.Count > 0)
                //if a gear has a specs template then we get the sampled gear specs
                //GetSampledGearSpecs will fill the _SampledGearSpecs Dictionary
                GetSampledGearSpecs();
            }
        }

        /// <summary>
        /// Boolean. Returns if there are unsaved edits in the sampled gear's specifications
        /// </summary>
        public static bool HasUnsavedSampledGearSpecEdits
        {
            get { return _HasUnsavedSampledGearSpecEdits; }
        }

        /// <summary>
        /// Clears the sampled gear spec dictionary so that it will
        /// receive the edited or new data and then sets the
        /// _HasUnsavedSampledGearSpecEdits flag to true
        /// </summary>
        public static void SetSampledGearSpecsForPreSave()
        {
            _sampledGearSpecs.Clear();
            _HasUnsavedSampledGearSpecEdits = true;
        }

        /// <summary>
        /// Dictionary. Returns the specifications of the sampled gear
        /// </summary>
        public static Dictionary<string, SampledGearSpecData> SampledGearSpecs
        {
            get { return _sampledGearSpecs; }
        }


        /// <summary>
        /// structure that holds the data of sampled gear's specs
        /// </summary>
        public struct SampledGearSpecData
        {
            private static fad3DataStatus _DataStatus;
            public fad3DataStatus DataStatus { get; set; }
            public string SpecificationGuid { get; set; }
            public string SpecificationName { get; set; }
            public string SamplingGuid { get; set; }
            public string RowID { get; set; }
            public string SpecificationValue { get; set; }
        }

        /// <summary>
        /// Boolean. If there is specs data for the sampled gear in the database
        /// </summary>
        public static bool HasSampledGearSpecs
        {
            get { return _HasSampledGearSpecs; }
        }

        /// <summary>
        /// Set gear variation GUID and then read the specs template of the gear
        /// </summary>
        /// <param name="GearVarGuid"></param>
        public static void GearVarGuid(string GearVarGuid)
        {
            _gearVarGuid = GearVarGuid;
            GetGearSpecs();  //get spec template of the gear variation
        }

        /// <summary>
        /// Set gear variation GUID and variation name and then read the specs template of the gear
        /// </summary>
        /// <param name="GearVarGuid"></param>
        /// <param name="GearVarName"></param>
        public static void GearVariation(string GearVarGuid, string GearVarName)
        {
            _gearVarGuid = GearVarGuid;
            _gearVarName = GearVarName;
            GetGearSpecs();  //get spec template of the gear variation
        }

        /// <summary>
        /// List. Returns the specifications template for the gear variation
        /// </summary>
        public static List<GearSpecification> GearSpecifications
        {
            get { return _GearSpecifications; }
        }


        public static int DeleteSampledGearSpec(string samplingGUID)
        {
            var deleteCount = 0;
            using (var con = new OleDbConnection(global.ConnectionString))
            {
                con.Open();
                string sql = $"Delete * from tblSampledGearSpec where SamplingGUID = {{{samplingGUID}}}";

                using (OleDbCommand update = new OleDbCommand(sql, con))
                {
                    deleteCount = update.ExecuteNonQuery();
                }

            }
            return deleteCount;
        }

        private static bool DeleteSavedSpec(string rowGuid)
        {
            bool success = false;
            using (var con = new OleDbConnection(global.ConnectionString))
            {
                con.Open();
                var sql = $"Delete * from tblSamplesGearSpec where RowID={{{rowGuid}}}";
                using (OleDbCommand update = new OleDbCommand(sql, con))
                {
                    try
                    {
                        if (update.ExecuteNonQuery() > 0)
                        {
                            success = true;
                        }
                    }
                    catch (OleDbException oex)
                    {
                        Logger.LogError(oex.Message, oex.StackTrace);
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError(ex.Message, ex.StackTrace);
                    }

                }
            }
            return success;
        }
        private static bool SaveNewGearSpec(KeyValuePair<string, SampledGearSpecData> kv)
        {
            bool success = false;
            using (var con = new OleDbConnection(global.ConnectionString))
            {
                con.Open();
                var sql = $@"Insert into tblSampledGearSpec (RowID, SamplingGUID, SpecID, [Value]) values (
                                {{{kv.Value.RowID}}},
                                {{{kv.Value.SamplingGuid}}},
                                {{{kv.Value.SpecificationGuid}}},
                                '{kv.Value.SpecificationValue}')";

                using (OleDbCommand update = new OleDbCommand(sql, con))
                {
                    try
                    {
                        if (update.ExecuteNonQuery() > 0)
                        {
                            success = true;
                        }
                    }
                    catch (OleDbException oex)
                    {
                        Logger.LogError(oex.Message, oex.StackTrace);
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError(ex.Message, ex.StackTrace);
                    }
                }
            }
            return success;
        }

        /// <summary>
        /// Find out if a specific gear spec from a sampled landing is saved
        /// </summary>
        /// <param name="samplingGuid"></param>
        /// <returns></returns>
        private static bool SampledGearSpecRecordExist(string samplingGuid, string specGUID)
        {
            string s = "";
            using (var con = new OleDbConnection(global.ConnectionString))
            {
                con.Open();
                
                string sql = $@"Select Top 1 [Value] from tblSampledGearSpec where 
                            SamplingGUID={{{samplingGuid}}} and SpecID={{{specGUID}}}";

                OleDbCommand c = new OleDbCommand(sql, con);
                s = (string)c.ExecuteScalar();
            }
            return s != null && s.Length > 0;
        }

        /// <summary>
        /// Find out if a sampled landing has its gear specifications already saved
        /// </summary>
        /// <param name="samplingGuid"></param>
        /// <returns></returns>
        private static bool SampledGearSpecsRecordExist(string samplingGuid)
        {
            string s = "";
            using (var con = new OleDbConnection(global.ConnectionString))
            {
                con.Open();
                string sql = $"Select Top 1 [Value] from tblSampledGearSpec where SamplingGUID={{{samplingGuid}}}";
                OleDbCommand c = new OleDbCommand(sql, con);
                s = (string)c.ExecuteScalar();
            }
            return s != null && s.Length > 0;
        }

        public static bool SaveSampledGearSpecs(string samplingGuid)
        {
            int saveSuccessCount = 0;
            int deletedCount = 0;
            string sql = "";
            using (var con = new OleDbConnection(global.ConnectionString))
            {
                con.Open();

                if (SampledGearSpecsRecordExist(samplingGuid))
                {
                    foreach (KeyValuePair<string, SampledGearSpecData> kv in _sampledGearSpecs)
                    {

                        if (kv.Value.SpecificationValue.Length > 0)
                        {

                            if (SampledGearSpecRecordExist(kv.Value.SamplingGuid,kv.Value.SpecificationGuid))
                            {
                                sql = $@"UPDATE tblSampledGearSpec set [Value] = '{kv.Value.SpecificationValue.ToString()}'
                                Where SamplingGUID={{{kv.Value.SamplingGuid}}} AND SpecID = {{{kv.Value.SpecificationGuid}}}";

                                using (OleDbCommand update = new OleDbCommand(sql, con))
                                {
                                    try
                                    {
                                        if (update.ExecuteNonQuery() > 0)
                                        {
                                            saveSuccessCount++;
                                        }
                                    }
                                    catch (OleDbException oex)
                                    {
                                        Logger.LogError(oex.Message, oex.StackTrace);
                                    }
                                    catch (Exception ex)
                                    {
                                        Logger.LogError(ex.Message, ex.StackTrace);
                                    }
                                }
                            }
                            else
                            {
                                if(SaveNewGearSpec(kv))
                                {
                                    saveSuccessCount++;
                                }
                            }
                        }
                        else
                        {
                            sql = $@"Delete * from tblSampledGearSpec where SamplingGUID={{{samplingGuid}}}
                                        and SpecID={{{kv.Value.SpecificationGuid}}}";
                            using (OleDbCommand update = new OleDbCommand(sql, con))
                            {
                                try
                                {
                                    if (update.ExecuteNonQuery() > 0)
                                    {
                                        deletedCount++;
                                    }
                                }
                                catch (OleDbException oex)
                                {
                                    Logger.LogError(oex.Message, oex.StackTrace);
                                }
                                catch (Exception ex)
                                {
                                    Logger.LogError(ex.Message, ex.StackTrace);
                                }
                            }

                        }
                    }
                }
                else
                {
                    foreach (KeyValuePair<string, SampledGearSpecData> kv in _sampledGearSpecs)
                    {
                        if (SaveNewGearSpec(kv))
                        {
                            saveSuccessCount++;
                        }
                    }
                }
            }

            return saveSuccessCount > 0 || SampledGearSpecs.Count == 0 || deletedCount > 0;
        }



        /// <summary>
        /// Retrieve the specs of the gear that was sampled.
        /// A Dictionary (_SampledGearSpecs) is filled.
        /// </summary>
        private static void GetSampledGearSpecs()
        {
            using (var con = new OleDbConnection(global.ConnectionString))
            {
                _HasSampledGearSpecs = false;
                _sampledGearSpecs.Clear();
                var sql = $@"SELECT tblGearSpecs.RowID, ElementName, SpecID, Value
                    FROM tblGearSpecs INNER JOIN tblSampledGearSpec ON tblGearSpecs.RowID = tblSampledGearSpec.SpecID
                    WHERE tblGearSpecs.Version = '2' AND tblSampledGearSpec.SamplingGUID = {{{_samplingGuid}}}";

                using (var dt = new DataTable())
                {
                    con.Open();
                    var adapter = new OleDbDataAdapter(sql, con);
                    adapter.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {
                        _HasSampledGearSpecs = true;
                        var s = new SampledGearSpecData();

                        s.RowID = dr["RowID"].ToString();
                        s.SpecificationValue = dr["Value"].ToString();
                        s.SamplingGuid = _samplingGuid;
                        s.SpecificationGuid = dr["SpecID"].ToString();
                        s.SpecificationName = dr["ElementName"].ToString();
                        s.DataStatus = fad3DataStatus.statusFromDB;

                        _sampledGearSpecs.Add(s.SpecificationGuid, s);
                    }
                    con.Close();
                }
            }
        }

        /// <summary>
        /// returns a List<> of gear specifications
        /// </summary>
        /// <param name="variationGuid">gear variation guid</param>
        /// <returns></returns>
        public static List<GearSpecification> GearVariationSpecs(string variationGuid)
        {
            List<GearSpecification> gearSpecs = new List<GearSpecification>();
            var dt = new DataTable();
            using (var conection = new OleDbConnection(global.ConnectionString))
            {
                conection.Open();
                string query = $"Select RowID, ElementName,ElementType,Sequence,Description from tblGearSpecs where Version = '2' AND GearVarGuid={{{variationGuid}}}";
                using (var adapter = new OleDbDataAdapter(query, conection))
                {
                    adapter.Fill(dt);

                    for (int n = 0; n < dt.Rows.Count; n++)
                    {
                        DataRow dr = dt.Rows[n];
                        GearSpecification gs = new GearSpecification(dr["ElementName"].ToString(), dr["ElementType"].ToString(), dr["RowID"].ToString(), (int)dr["Sequence"]);
                        gs.Notes = dr["Description"].ToString();
                        gs.DataStatus = fad3DataStatus.statusFromDB;
                        gearSpecs.Add(gs);
                    }
                }
            }
            return gearSpecs;
        }

        /// <summary>
        /// Gets the template for the gear specifications of a given gear variation
        /// A List (_GearSpecifications) is filled
        /// </summary>
        private static void GetGearSpecs()
        {
            _GearSpecifications.Clear();
            using (var con = new OleDbConnection(global.ConnectionString))
            {
                con.Open();
                string query = $@"Select RowID, ElementName, Description, ElementType, Sequence
                               from tblGearSpecs where Version = '2' and GearVarGuid ={{{_gearVarGuid}}}
                               order by sequence";
                using (var dt = new DataTable())
                {
                    var adapter = new OleDbDataAdapter(query, con);
                    adapter.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        var spec = new GearSpecification();
                        spec.Property = row["ElementName"].ToString();
                        spec.Type = row["ElementType"].ToString();
                        spec.Notes = row["Description"].ToString();
                        spec.RowGuid = row["RowID"].ToString();
                        spec.DataStatus = fad3DataStatus.statusFromDB;
                        var seq = 0;
                        if (int.TryParse(row["Sequence"].ToString(), out seq)) spec.Sequence = seq;
                        _GearSpecifications.Add(spec);
                    }
                }
                con.Close();
            }
        }

        public static bool SaveGearSpec(string gearVariationGuid, GearSpecification spec)
        {
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(global.ConnectionString))
            {
                conn.Open();
                int version = 2;
                string sql = $@"Insert into tblGearSpecs (ElementName, ElementType, Description, Sequence, Version, RowId, GearVarGuid)
                              values (
                              '{spec.Property}',
                              '{spec.Type}',
                              '{spec.Notes}',
                              {spec.Sequence},
                              '{version}',
                              {{{spec.RowGuid}}},
                              {{{gearVariationGuid}}})";
                using (OleDbCommand update = new OleDbCommand(sql, conn))
                {
                    try
                    {
                        success = (update.ExecuteNonQuery() > 0);
                    }
                    catch (OleDbException)
                    {
                        success = false;
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError(ex.Message, ex.StackTrace);
                        success = false;
                    }
                }
            }
            return success;
        }

        /// <summary>
        /// Save a gear spec template of a gear variation
        /// </summary>
        /// <param name="specifications"></param>
        /// <returns></returns>
        public static bool SaveGearSpecs(List<GearSpecification> specifications)
        {
            var sql = "";
            int version = 2;
            bool success;
            using (OleDbConnection conn = new OleDbConnection(global.ConnectionString))
            {
                conn.Open();
                //foreach (ManageGearSpecsClass.GearSpecification spec in specifications)
                foreach (GearSpecification spec in specifications)
                {
                    if (spec.DataStatus == fad3DataStatus.statusEdited)
                    {
                        sql = $@"Update tblGearSpecs set
                              ElementName ='{spec.Property}',
                              ElementType = '{spec.Type}',
                              Description = '{spec.Notes}',
                              Sequence =  {spec.Sequence},
                              Version = '2' where RowID = {{{spec.RowGuid}}}";
                    }
                    else if (spec.DataStatus == fad3DataStatus.statusNew)
                    {
                        sql = $@"Insert into tblGearSpecs (ElementName, ElementType, Description, Sequence, Version, RowId, GearVarGuid)
                              values (
                              '{spec.Property}',
                              '{spec.Type}',
                              '{spec.Notes}',
                              {spec.Sequence},
                              '{version}',
                              {{{Guid.NewGuid().ToString()}}},
                              {{{_gearVarGuid}}})";
                    }
                    else if (spec.DataStatus == fad3DataStatus.statusForDeletion)
                    {
                        sql = $"Delete * from tblGearSpecs where RowID = {{{spec.RowGuid}}}";
                    }

                    if (sql.Length > 0)
                        using (OleDbCommand update = new OleDbCommand(sql, conn))
                        {
                            success = (update.ExecuteNonQuery() > 0);
                            //TODO: what to do if Success=false?
                            sql = "";
                        }
                }
                conn.Close();
            }
            return true;
        }

        /// <summary>
        /// Returns the Property/Specification name given a specification guid
        /// </summary>
        /// <param name="SpecGuid"></param>
        /// <returns></returns>
        public static string SpecNameFromSpecGUID(string SpecGuid)
        {
            var SpecName = "";
            foreach (GearSpecification item in _GearSpecifications)
            {
                if (item.RowGuid == SpecGuid)
                {
                    SpecName = item.Property;
                    break;
                }
            }
            return SpecName;
        }

        /// <summary>
        /// returns a string of the presaved specifications of the sampled gear
        /// </summary>
        /// <returns></returns>
        public static string PreSavedSampledGearSpec()
        {
            var s = "";
            foreach (KeyValuePair<string, SampledGearSpecData> kv in _sampledGearSpecs)
            {
                s += kv.Value.SpecificationName + ": " + kv.Value.SpecificationValue + "\r\n";
            }
            return s;
        }

        /// <summary>
        /// returns the saved sampled gear specs in string format
        /// </summary>
        /// <param name="SamplingGuid"></param>
        /// <param name="Truncated"></param>
        /// <param name="TruncateLength"></param>
        /// <returns></returns>
        public static string GetSampledSpecsEx(string SamplingGuid, bool Truncated = false, int TruncateLength = 0)
        {
            var s = "";
            var isDone = false;
            var FirstRow = "";
            using (var con = new OleDbConnection(global.ConnectionString))
            {
                con.Open();
                var sql = $@"SELECT ElementName, Value FROM tblGearSpecs INNER JOIN
                      tblSampledGearSpec ON tblGearSpecs.RowID = tblSampledGearSpec.SpecID
                      WHERE SamplingGUID = {{{SamplingGuid}}} AND Version = '2'
                      ORDER BY sequence";
                using (var dt = new DataTable())
                {
                    var adapter = new OleDbDataAdapter(sql, con);
                    adapter.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        s += row["ElementName"] + ": " + row["Value"] + "\r\n";
                        if (!isDone)
                        {
                            FirstRow = row["ElementName"] + ": " + row["Value"];
                            isDone = true;
                        }
                    }
                }
            }

            if (Truncated && s.Length > 0)
            {
                if (TruncateLength == 0)
                    return FirstRow + " ...";
                else
                {
                    if (s.Length > 0)
                        return s.Substring(0, TruncateLength) + " ...";
                    else
                        return s;
                }
            }
            else
                return s;
        }
    }
}