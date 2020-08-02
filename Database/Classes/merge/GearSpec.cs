using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAD3.Database.Classes.merge
{
    public class GearSpec
    {
        public string Property { get; set; }
        public string Type { get; set; }
        public string Notes { get; set; }
        public int? Sequence { get; set; }

        public int Version { get; set; }

        public string RowGUID { get; set; }

        public Gear Gear { get; set; }

        public override string ToString()
        {
            return $"{Gear.ToString()} - {Property}";
        }
    }
}
