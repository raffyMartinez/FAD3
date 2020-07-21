using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAD3.Database.Classes.merge
{
    public class SamplingEnumerator
    {
        public string Name { get; set; }
        public string EnumeratorID { get; set; }

        public DateTime HireDate { get; set; }
        public AOI AOI { get; set; }
        public bool IsActive { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
