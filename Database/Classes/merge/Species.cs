using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAD3.Database.Classes.merge
{
    public class Species
    {
        public string Generic { get; set; }
        public string Specific { get; set; }
        public Taxa Taxa { get; set; }
        public string SpeciesID { get; set; }
        public bool ListedInFishbase { get; set; }
        public int? FishbaseSpeciesID { get; set;}
        public override string ToString()
        {
            return $"{Generic} {Specific}";
        }
        public Species()
        {

        }

     }
}
