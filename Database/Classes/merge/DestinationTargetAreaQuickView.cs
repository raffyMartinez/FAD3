using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAD3.Database.Classes.merge
{
    public class DestinationTargetAreaQuickView
    {
        public string Name { get; set; }
        public int NumberLandingSites { get; set; }

        public int NumberSamplings { get; set; }
        public string FirstRefNo { get; set; }
        public string LastRefNo { get; set; }
        public DateTime FirstSamplingDate { get; set; }
        public DateTime LastSamplingDate { get; set; }
        public int SerialNumberMin { get; set; }
        public int SerialNumerMax { get; set; }
    }
}
