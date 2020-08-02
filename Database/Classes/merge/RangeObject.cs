using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAD3.Database.Classes.merge
{
    public class RangeObject
    {
        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; }

        public int MinNumber { get; set; }

        public int MaxNumber { get; set; }

        public bool IsOverlapping(RangeObject otherRange, bool compareDate)
        {
            if(compareDate)
            {
                return (this.MinDate <= otherRange.MinDate && this.MaxDate >= otherRange.MinDate)
                    || (this.MinDate <= otherRange.MaxDate && this.MaxDate >= otherRange.MaxDate)
                    || (this.MinDate <= otherRange.MinDate && this.MaxDate >= otherRange.MaxDate)
                    || (this.MinDate >= otherRange.MinDate && this.MaxDate <= otherRange.MaxDate);

            }
            else
            {
                return (this.MinNumber <= otherRange.MinNumber && this.MaxNumber >= otherRange.MinNumber)
                    || (this.MinNumber <= otherRange.MaxNumber && this.MaxNumber >= otherRange.MaxNumber)
                    || (this.MinNumber <= otherRange.MinNumber && this.MaxNumber >= otherRange.MaxNumber)
                    || (this.MinNumber >= otherRange.MinNumber && this.MaxNumber <= otherRange.MaxNumber);
            }

        }
    }
}
