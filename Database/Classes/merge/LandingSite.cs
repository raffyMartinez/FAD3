using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISO_Classes;

namespace FAD3.Database.Classes.merge
{
    public class LandingSite
    {
        public override bool Equals(object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                LandingSite ls = (LandingSite)obj;
                
                return (LandingSiteName == ls.LandingSiteName) && (LandingSiteGuid == ls.LandingSiteGuid) && (Municipality.MunicipalityID==ls.Municipality.MunicipalityID);
            }
        }
        public string LandingSiteGuid { get; set; }
        public string LandingSiteName { get; set; }

        public Coordinate Coordinate { get; set; }

        public Municipality Municipality { get; set; }

        public AOI AOI { get; set; }

        public override string ToString()
        {
            return $"{LandingSiteName}, {Municipality}";
        }

    }
}
