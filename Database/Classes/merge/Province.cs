using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAD3.Database.Classes.merge
{
    public class Province
    {
        public int ProvinceID { get; set; }
        public string ProvinceName { get; set; }

        public override string ToString()
        {
            return ProvinceName;
        }
    }
}
