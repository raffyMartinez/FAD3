using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAD3.Database.Classes.merge
{
    public class Municipality
    {
        public int MunicipalityID { get; set; }
        public string MunicipalityName { get; set; }
        public Province Province { get; set; }

        public ISO_Classes.Coordinate Coordinate {get;set;}
        public bool IsCoastal { get; set; }

        public override string ToString()
        {
            return $"{MunicipalityName}, {Province.ProvinceName}";
        }

    }
}
