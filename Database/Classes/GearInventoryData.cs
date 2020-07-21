using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAD3.Database.Classes
{
    public class GearInventoryData
    {
        public int CountCommercial { get; set; }

        public int CountMotorized { get; set; }
        public int CountNonMotorized { get; set; }
        public int CountNoBoat { get; set; }
        public int? MaxCPUE { get; set; }
        public int? MinCPUE { get; set; }
        public int? UpperMode { get; set; }
        public int? LowerMode { get; set; }
        public int NumberDaysUsed { get; set; }
        public string CPUEUnit { get; set; }
        public string Notes { get; set; }
        public int? DominantPercent { get; set; }
        public int? AverageCPUE { get; set; }
        public int? CpueMode { get; set; }
        public double? EquivalentKg { get; set; }
    }
}
