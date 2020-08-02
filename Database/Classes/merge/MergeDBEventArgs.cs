using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAD3.Database.Classes.merge
{
   public class MergeDBEventArgs:EventArgs
    {
        public string CurrentTableRead { get; set; }
        public int TableCount { get; set; }
        public int RunningCount { get; set; }

        public bool IsDone { get; set; }
        public string Location { get; set; }


    }
}
