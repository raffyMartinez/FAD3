using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;

namespace FAD3.Database.Classes.gearinventory
{
    class InventoryRepository
    {
        public string InventoryProjectGUID { get; internal set; }
        public List<Inventory> Inventories{ get; set; }

        public InventoryProject InventoryProject { get; set; }

        public  InventoryRepository(string inventoryProjectGuid, InventoryReadHelper irh)
        {
            InventoryProjectGUID = inventoryProjectGuid;
            InventoryProject = GetProject(inventoryProjectGuid);
            Inventories = getInventories(irh);
        }

        private InventoryProject GetProject(string inventoryProjectGuid)
        {
            var dt = new DataTable();
            using (var conection = new OleDbConnection(global.ConnectionString))
            {
                try
                {
                    conection.Open();
                    string query = $@"SELECT InventoryName, InventoryGuid, DateConducted, TargetArea, AOIName
                                    FROM tblAOI INNER JOIN tblGearInventories ON tblAOI.AOIGuid = tblGearInventories.TargetArea
                                    Where InventoryGuid = {{{inventoryProjectGuid}}}";
                    var adapter = new OleDbDataAdapter(query, conection);
                    adapter.Fill(dt);
                    if(dt.Rows.Count>0)
                    {
                        DataRow dr = dt.Rows[0];
                        InventoryProject = new InventoryProject
                        {
                            Name = dr["InventoryName"].ToString(),
                            ProjectGUID = InventoryProjectGUID,
                            DateStarted = (DateTime)dr["DateConducted"],
                            AOI = dr["AOIName"].ToString()
                        };
                    }

                }
                catch (Exception ex)
                {

                }
            }
            return InventoryProject;
        }
        private  List<BarangayGearInventory> getGearInventories(string barangayInventoryGUID)
        {
            int counter = 0;
            List<BarangayGearInventory> thisList = new List<BarangayGearInventory>();
            var dt = new DataTable();
            using (var conection = new OleDbConnection(global.ConnectionString))
            {
                try
                {
                    conection.Open();
                    //string query = $"Select * from tblGearInventoryBarangayData where BarangayInventoryGUID={{{barangayInventoryGUID}}}";
                    string query = $@"SELECT tblGearInventoryBarangayData.*
                                      FROM (tblGearClass INNER JOIN 
                                          tblGearVariations ON 
                                          tblGearClass.GearClass = tblGearVariations.GearClass) INNER JOIN 
                                          tblGearInventoryBarangayData ON 
                                          tblGearVariations.GearVarGUID = tblGearInventoryBarangayData.GearVariation
                                      WHERE BarangayInventoryGUID={{{barangayInventoryGUID}}}
                                      ORDER BY tblGearClass.GearClassName, 
                                          tblGearVariations.Variation";

                    var adapter = new OleDbDataAdapter(query, conection);
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        thisList.Clear();
                        foreach (DataRow dr in dt.Rows)
                        {
                            Gear g = InventoryEntities.GearViewModel.GetGear(dr["GearVariation"].ToString());
                            BarangayGearInventory bgi = new BarangayGearInventory
                            {
                                BrgyGearInventoryGuid = dr["DataGuid"].ToString(),
                                GearName = g.Name,
                                GearClass = g.ClassName,
                                CountCommercial = Convert.ToInt32(dr["CountCommercial"]),
                                CountMunicipalMotorized = Convert.ToInt32(dr["CountMunicipalMotorized"]),
                                CountMunicipalNonMotorized = Convert.ToInt32(dr["CountMunicipalNonMotorized"]),
                                CountNoBoat = Convert.ToInt32(dr["CountNoBoat"]),
                                NumberDaysUsedPerMonth = Convert.ToInt32(dr["NumberDaysPerMonth"]),
                                MaxCPUE = Convert.ToInt32(dr["MaxCPUE"]),
                                MinCPUE = Convert.ToInt32(dr["MinCPUE"]),
                                AverageCPUE = string.IsNullOrEmpty(dr["AverageCPUE"].ToString()) ? null : (double?)Convert.ToDouble(dr["AverageCPUE"]),
                                Mode=string.IsNullOrEmpty(dr["Mode"].ToString())? null: (double?)Convert.ToDouble(dr["Mode"]),
                                ModeLower=string.IsNullOrEmpty(dr["ModeLower"].ToString())? null: (double?)Convert.ToDouble(dr["ModeLower"]),
                                ModeUpper=string.IsNullOrEmpty(dr["ModeUpper"].ToString())? null: (double?)Convert.ToDouble(dr["ModeUpper"]),
                                CPUEUnit=dr["CPUEUnit"].ToString(),
                                EquivalentKg = string.IsNullOrEmpty(dr["EquivalentKg"].ToString())?null: (double?)Convert.ToDouble(dr["EquivalentKg"]),
                                DominantCatchPercent = string.IsNullOrEmpty(dr["DominantCatchPercent"].ToString())?null:(int?)Convert.ToInt32(dr["DominantCatchPercent"]),
                                Notes = dr["Notes"].ToString()
                            };
                            //bgi.FishingMonths = GetMonthsFishing(bgi.BrgyGearInventoryGuid);
                            //bgi.PeakFishingMonths = GetMonthsFishing(bgi.BrgyGearInventoryGuid, true);
                            bgi.ArrFishingMonths = GetMonthsFishingArr(bgi.BrgyGearInventoryGuid);
                            bgi.ArrFishingMonthsPeak = GetMonthsFishingArr(bgi.BrgyGearInventoryGuid,true);
                            bgi.LocalNames = GetGearLocalNames(bgi.BrgyGearInventoryGuid);
                            bgi.Accessories = GetAccessories(bgi.BrgyGearInventoryGuid);
                            bgi.CatchNames=getCatchNames(bgi.BrgyGearInventoryGuid);
                            bgi.DominantCatchNames=getCatchNames(bgi.BrgyGearInventoryGuid, true);
                            bgi.CPUEHistories = getCPUEHistory(bgi.BrgyGearInventoryGuid);
                            bgi.Expenses = getExpenses(bgi.BrgyGearInventoryGuid);
                            counter++;
                            thisList.Add(bgi);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(counter);
                    Logger.Log(ex);
                }
            }
                return thisList;
        }
        
        public List<InventoryExpense>getExpenses(string dataGuid  )
        {
            List<InventoryExpense> thisList = new List<InventoryExpense>();
            string sql = $"Select * from tblGearInventoryExpense where InventoryDataGuid = {{{dataGuid}}}";
            var dt = new DataTable();
            using (var conection = new OleDbConnection(global.ConnectionString))
            {
                try
                {
                    conection.Open();
                    var adapter = new OleDbDataAdapter(sql, conection);
                    adapter.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        InventoryExpense ex = new InventoryExpense
                        {
                            ExpenseItem = dr["ExpenseItem"].ToString(),
                            Cost = Convert.ToDouble(dr["Cost"]),
                            Source = dr["Source"].ToString(),
                            Notes = dr["Notes"].ToString()
                        };
                        thisList.Add(ex);
                    }
                }
                catch
                {
                }
            }
            return thisList;
        }
        public List<CPUEHistory>getCPUEHistory(string dataGuid)
        {
            List<CPUEHistory> thisList = new List<CPUEHistory>();
            string sql = $"Select * from tblGearInventoryCPUEHistorical where InventoryDataGuid = {{{dataGuid}}}";
            var dt = new DataTable();
            using (var conection = new OleDbConnection(global.ConnectionString))
            {
                try
                {
                    conection.Open();
                    var adapter = new OleDbDataAdapter(sql, conection);
                    adapter.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        CPUEHistory ch = new CPUEHistory
                        {
                            Decade = dr["Decade"].ToString(),
                            HistoryYear = string.IsNullOrEmpty(dr["HistoryYear"].ToString()) ? null : (int?)Convert.ToInt32(dr["HistoryYear"]),
                            CPUE = Convert.ToDouble(dr["CPUE"]),
                            CPUEUnit = dr["CPUEUnit"].ToString(),
                            Notes = dr["Notes"].ToString()
                        };
                        thisList.Add(ch);
                    }
                }
                catch
                {
                }
            }
            return thisList;
        }
        public List<string>getCatchNames(string dataGuid, bool isDominant = false)
        {
            List<string> thisList = new List<string>();
            string sql = $"Select * from tblGearInventoryCatchComposition where InventoryDataGuid = {{{dataGuid}}} and IsDominant = {isDominant}";
            var dt = new DataTable();
            using (var conection = new OleDbConnection(global.ConnectionString))
            {
                try
                {
                    conection.Open();
                    var adapter = new OleDbDataAdapter(sql, conection);
                    adapter.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        thisList.Add(InventoryEntities.CatchLocalNameViemModel.GetCatchLocalName(   dr["NameOfCatch"].ToString()).LocalName);
                    }
                }
                catch
                {
                }
            }
            return thisList;
        }
        public List<string>GetAccessories(string dataGuid)
        {
            List<string> thisList = new List<string>();
            string sql = $"Select * from tblGearInventoryAccesories where InventoryDataGuid = {{{dataGuid}}}";
            var dt = new DataTable();
            using (var conection = new OleDbConnection(global.ConnectionString))
            {
                try
                {
                    conection.Open();
                    var adapter = new OleDbDataAdapter(sql, conection);
                    adapter.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        thisList.Add(dr["Accessory"].ToString());
                    }
                }
                catch
                {
                }
            }
            return thisList;
        }
        public List<string>GetGearLocalNames(string dataGuid)
        {
            List<string> thisList = new List<string>();
            string sql = $"Select * from tblGearInventoryGearLocalNames where InventoryDataGuid = {{{dataGuid}}}";
            var dt = new DataTable();
            using (var conection = new OleDbConnection(global.ConnectionString))
            {
                try
                {
                    conection.Open();
                    var adapter = new OleDbDataAdapter(sql, conection);
                    adapter.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        thisList.Add(InventoryEntities.GearLocalNameViewModel.GetGearLocalName(dr["LocalNameGuid"].ToString()).LocalName);
                    }
                }
                catch
                {
                }
            }
            return thisList;
        }

        public string[] GetMonthsFishingArr(string dataGuid, bool getPeakMonths = false)
        {
            string[] arr = { " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " " };
            string sql;
            if (getPeakMonths)
            {
                sql = $@"SELECT PeakSeasonMonthNumber
                        FROM tblGearInventoryPeakMonths
                        WHERE InventoryDataGuid = {{{dataGuid}}}
                        ORDER BY PeakSeasonMonthNumber";
            }
            else
            {
                sql = $@"SELECT MonthNumber
                        FROM tblGearInventoryMonthsUsed
                        WHERE InventoryDataGuid={{{dataGuid}}}
                        ORDER BY MonthNumber";
            }

            var dt = new DataTable();
            using (var conection = new OleDbConnection(global.ConnectionString))
            {
                try
                {
                    conection.Open();
                    var adapter = new OleDbDataAdapter(sql, conection);
                    adapter.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        if (getPeakMonths)
                        {
                            arr[(int)dr["PeakSeasonMonthNumber"] - 1] = "x";
                        }
                        else
                        {
                            arr[(int)dr["MonthNumber"] - 1] = "x";
                        }
                    }
                }
                catch
                {
                }
            }
            return arr;
        }

        public List<int> GetMonthsFishing(string dataGuid, bool getPeakMonths = false)
        {
            List<int> months = new List<int>();
            var sql = "";
            if (getPeakMonths)
            {
                sql = $@"SELECT PeakSeasonMonthNumber
                        FROM tblGearInventoryPeakMonths
                        WHERE InventoryDataGuid = {{{dataGuid}}}
                        ORDER BY PeakSeasonMonthNumber";
            }
            else
            {
                sql = $@"SELECT MonthNumber
                        FROM tblGearInventoryMonthsUsed
                        WHERE InventoryDataGuid={{{dataGuid}}}
                        ORDER BY MonthNumber";
            }

            var dt = new DataTable();
            using (var conection = new OleDbConnection(global.ConnectionString))
            {
                try
                {
                    conection.Open();
                    var adapter = new OleDbDataAdapter(sql, conection);
                    adapter.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        if (getPeakMonths)
                        {
                            months.Add((int)dr["PeakSeasonMonthNumber"]);
                        }
                        else
                        {
                            months.Add((int)dr["MonthNumber"]);
                        }
                    }
                }
                catch
                {
                }
            }
            return months;
        }

        private List<string>getRespondents(string inventoryGUID)
        {
            List<string> thisList = new List<string>();
            var dt = new DataTable();
            using (var conection = new OleDbConnection(global.ConnectionString))
            {
                try
                {
                    conection.Open();
                    string query = $@"SELECT RespondentName from tblGearInventoryRespondents where BarangayInventoryGUID = {{{inventoryGUID}}}";

                    var adapter = new OleDbDataAdapter(query, conection);
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {

                        foreach (DataRow dr in dt.Rows)
                        {
                            thisList.Add(dr["RespondentName"].ToString());
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
        private List<Inventory> getInventories(InventoryReadHelper irh)
        {
            int counter = 0;
            List<Inventory> thisList = new List<Inventory>();
            var dt = new DataTable();
            using (var conection = new OleDbConnection(global.ConnectionString))
            {
                try
                {
                    conection.Open();
                    //string query = $@"SELECT * from tblGearInventoryBarangay where inventoryGuid = {{{InventoryProjectGUID}}}";
                    string query = $@"SELECT tblGearInventoryBarangay.*
                                      FROM Provinces INNER JOIN 
                                        (Municipalities INNER JOIN 
                                        tblGearInventoryBarangay ON 
                                        (Municipalities.MunNo = tblGearInventoryBarangay.Municipality) AND 
                                        (Municipalities.MunNo = tblGearInventoryBarangay.Municipality)) ON 
                                        Provinces.ProvNo = Municipalities.ProvNo
                                    WHERE inventoryGuid = {{{InventoryProjectGUID}}}
                                    ORDER BY Provinces.ProvinceName, 
                                        Municipalities.Municipality, 
                                        tblGearInventoryBarangay.Barangay, 
                                        tblGearInventoryBarangay.Sitio;";
                    var adapter = new OleDbDataAdapter(query, conection);
                    adapter.Fill(dt);
                    int recordCount = dt.Rows.Count;
                    if (recordCount>0)
                    {
                        
                        thisList.Clear();
                        foreach (DataRow dr in dt.Rows)
                        {
                            var mun = InventoryEntities.MunicipalityViewModel.GetMunicipality(Convert.ToInt32(dr["Municipality"]));
                            Location loc=  new Location(mun.Province.ProvinceName,mun.MunicipalityName,dr["Barangay"].ToString(), dr["Sitio"].ToString());
                            Inventory inv = new Inventory
                            {
                                Location = loc,
                                Enumerator = InventoryEntities.EnumeratorViewModel.GetEnumerator(dr["Enumerator"].ToString()).Name,
                                DateEnumerated = Convert.ToDateTime(dr["InventoryDate"]),
                                NumberOfFishers = Convert.ToInt32(dr["CountFishers"]),
                                NumberCommercial=Convert.ToInt32(dr["CountCommercial"]),
                                NumberMunicipalMotorized=Convert.ToInt32(dr["CountMunicipalMotorized"]),
                                NumberMunicipalNonMotorized=Convert.ToInt32(dr["CountMunicipalNonMotorized"]),
                                InventoryGuid = dr["BarangayInventoryGuid"].ToString()
                            };

                            inv.Respondents = getRespondents(inv.InventoryGuid);
                            inv.GearInventories = getGearInventories(inv.InventoryGuid);
                            thisList.Add(inv);
                            irh.RecordReading(recordCount, ++counter, loc.ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    //await Logger.LogAsync (ex);
                    Logger.Log(ex);

                }
            }

            return thisList;
        }

    }
}
