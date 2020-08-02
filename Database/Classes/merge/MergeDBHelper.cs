using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAD3.Database.Classes.merge
{
    public class MergeDBHelper
    {
        public EventHandler<MergeDBEventArgs> OnMergeDBTable;
        public EventHandler<MergeDBEventArgs> OnMergeDBTableDone;
        private int _runningCount;
        public MergeDBHelper(int totalTables)
        {
            TotalTables = totalTables;
            _runningCount = 0;
        }

        public void IsDone()
        {
            OnMergeDBTableDone?.Invoke(null, new MergeDBEventArgs { IsDone = true });
        }

        public int TotalTables { get; set; }

        public string TableLocation { get; set; }
        public void MergingTable (string currentTableName)
        {

            OnMergeDBTable?.Invoke(null, new MergeDBEventArgs { TableCount = TotalTables, RunningCount=++_runningCount , CurrentTableRead = currentTableName, Location = TableLocation });
        }
    }
}
