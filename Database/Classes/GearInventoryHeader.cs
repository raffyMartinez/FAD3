using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAD3.Database.Classes
{
    public class GearInventoryHeader
    {
        public string ProjectName { get; set; }
        public string Province { get; set; }
        public string LGU { get; set; }
        public string Barangay { get; set; }
        public string Sitio { get; set; }
        public string Enumerator { get; set; }
        public DateTime SurveyDate { get; set; }
        public string GearClass { get; set; }
        public string GearVariation { get; set; }
        public string LocalNames { get; set; }
    }
}
