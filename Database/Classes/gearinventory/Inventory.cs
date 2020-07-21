using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
namespace FAD3.Database.Classes.gearinventory
{

    public class CPUEHistory
    {
        public string Decade { get; set; }
        public int? HistoryYear { get; set; }
        public double CPUE { get; set; }
        public string CPUEUnit { get; set; }
        public string Notes { get; set; }
    }
    public class InventoryExpense
    {
        public string ExpenseItem { get; set; }
        public double Cost { get; set; }
        public string Source { get; set; }
        public string Notes { get; set; }
    }
    public class BarangayGearInventory
    {
        public string[] ArrFishingMonths { get; set; }
        public string[] ArrFishingMonthsPeak { get; set; }
        public string BrgyGearInventoryGuid { get; set; }
        public int CountCommercial { get; set; }
        public int CountMunicipalMotorized { get; set; }
        public int CountMunicipalNonMotorized { get; set; }
        public int CountNoBoat { get; set; }

        public int CountTotal
        {
            get
            {
                return CountCommercial + CountMunicipalMotorized + CountMunicipalNonMotorized + CountNoBoat;
            }
        }
        public string GearName { get; set; }

        public string GearClass { get; set; }

        public int NumberDaysUsedPerMonth { get; set; }
        public double MaxCPUE { get; set; }
        public double MinCPUE { get; set; }
        public double? AverageCPUE { get; set; }
        public double? ModeUpper { get; set; }
        public double? ModeLower { get; set; }
        public double? Mode { get; set; }
        public string CPUEUnit { get; set; }
        public double? EquivalentKg { get; set; }
        public int? DominantCatchPercent { get; set; }
        public string Notes { get; set; }
        public List<string> LocalNames { get; set; }

        public string CatchComposition
        {
            get
            {
                string names = "";
                foreach (var item in CatchNames)
                {
                    names += $"{item}, ";
                }
                return names.Trim(new char[] { ' ', ',' });
            }
        }

        public string GearAccessories
        {
            get
            {
                string blings = "";
                foreach (var item in Accessories)
                {
                    blings += $"{item}, ";
                }
                return blings.Trim(new char[] { ' ', ',' });
            }
        }

        public string CatchCompositionDominant
        {
            get
            {
                string names = "";
                foreach (var item in DominantCatchNames)
                {
                    names += $"{item}, ";
                }
                return names.Trim(new char[] { ' ', ',' });
            }
        }
        public string GearLocalNames
        {
            get 
            {
                string localNames="";
                foreach(var item in LocalNames )
                {
                    localNames += $"{item}, ";
                }
                return localNames.Trim(new char[] {' ',','} );
            }
        }

        public string[] PeakFishingMonthsArr()
        {
            string[] arr = { " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " " };
            foreach (var item in PeakFishingMonths)
            {
                arr[item - 1] = "x";
            }
            return arr;
        }

        public string[] FishingMonthsArr()
        {
            string[] arr = { " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " " };
            foreach (var item in FishingMonths)
            {
                arr[item - 1] = "x";
            }
            return arr;
        }
        public List<string> PeakFishingMonthsString()
        {
            List<string> months = new List<string>();
            for (int n = 0; n < 12; n++)
            {
                months.Add(" ");
            }
            foreach (var item in PeakFishingMonths)
            {
                months[item - 1] = "x";
            }
            return months;
        }
        public List<string> FishingMonthsString()
        {
            List<string> months = new List<string>();
            for(int n=0;n<12;n++)
            {
                months.Add(" ");
            }
            foreach(var item in FishingMonths)
            {
                months[item - 1] = "x";
            }
            return months;
        }
        public List<int> PeakFishingMonths { get; set; }
        public List<int> FishingMonths { get; set; }

        public List<string> Accessories { get; set; }

        public List<string> DominantCatchNames { get; set; }

        public List<string> CatchNames { get; set; }

        public List<InventoryExpense> Expenses {get;set;}

        public List<CPUEHistory> CPUEHistories { get; set; }

        public override string ToString()
        {
            return $"{GearName}, {GearClass} inventory";
        }
    }
    public class Location
    {
        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
        public Location(string province, string municipality, string barangay, string sitio)
        {
            Province = province;
            Municipality = municipality;
            Barangay = barangay;
            Sitio = sitio;
        }
        public string Province { get; set; }
        public string Municipality { get; set; }
        public string Barangay { get; set; }
        public string Sitio { get; set; }

        public override string ToString()
        {
            if (Sitio.Length == 0)
            {
                return $"Brgy {textInfo.ToTitleCase(Barangay)}, {Municipality}, {Province}";
            }
            else
            {
                
                return $"{textInfo.ToTitleCase(Sitio)}, Brgy {textInfo.ToTitleCase(Barangay)}, {Municipality}, {Province}";
            }
        }
    }
    public class Inventory
    {

        public Inventory()
        {

            
        }
        public string InventoryGuid { get; set; }
        public Location Location { get; set; }
        public string Enumerator { get; set; }
        public DateTime DateEnumerated { get; set; }
        public int NumberOfFishers { get; set; }
        public int NumberCommercial { get; set; }
        public int NumberMunicipalMotorized { get; set; }
        public int NumberMunicipalNonMotorized { get; set; }
        public List<BarangayGearInventory> GearInventories { get; set; }

        public List<string> Respondents { get; set; }
        public override string ToString()
        {
            return $"Inventory for {Location}";
        }

    }

    public class InventoryProject
    {
        public string Name { get; set; }
        public string ProjectGUID { get; set; }
        public DateTime DateStarted { get; set; }
        public string AOI { get; set; }
        public List<Inventory> Inventories { get; set; }
    }
}
