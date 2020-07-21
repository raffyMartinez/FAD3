using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAD3.Database.Classes
{
    public static class MergeDataBases
    {
        private static string _otherDatabaseFileName;
        public static string OtherDatabaseFileName 
        { 
            get 
            {
                return _otherDatabaseFileName; 
            }
            set 
            {
                if (DBCheck.CheckDB(value))
                {
                    _otherDatabaseFileName = value;
                    if(Merge())
                    {
                        MergeResultMessage = "Database successfully merged";
                    }
                }
                else
                {
                    MergeResultMessage = "Selected database could not be merged";
                }
            } 
        }

        public static string MergeResultMessage { get; private set; }
        private static bool Merge()
        {
            return true;
        }


    }
}
