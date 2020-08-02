using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAD3.Database.Classes.merge
{
    public enum SubGridStyle
    {
        SubgridStyleNone,
        SubgridStyle_2=2,
        SubgridStyle_3,
        SubgridStyle_4
    }

    public class AOI
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
                AOI aoi = (AOI)obj;
                return (AOIName == aoi.AOIName) && (AOIGuid == aoi.AOIGuid);
            }
        }
        public bool IsGrid25 { get; set; }
        public string AOIName { get; set; }
        public string Code { get; set; }
        public SubGridStyle SubGridStyle {get;set;}

        public UTMZone UTMZone { get; set; }

        public bool AddMBR(MBR mbr)
        {
            bool isValid = mbr.IsValid;
            if(isValid)
            {
                if(MBRs==null)
                {
                    MBRs = new List<MBR>();
                    UpperLeftGrid = mbr.UpperLeftGrid;
                    LowerRightGrid = mbr.LowerRightGrid;
                }
                MBRs.Add(mbr);
            }
            return isValid;
        }

        public void SetSubGridStyleFromString(string subGridStyle)
        {
            if (subGridStyle.Length > 0)
            {
                SubGridStyle = (SubGridStyle)Enum.Parse(typeof(SubGridStyle), subGridStyle);
            }
            else
            {
                SubGridStyle = SubGridStyle.SubgridStyleNone;
            }
        }

        public List<MBR> MBRs { get; private set; }

        public Grid25GridCell UpperLeftGrid { get; private set; }

        public Grid25GridCell LowerRightGrid { get; private set; }

        public string AOIGuid { get; set; }

        public override string ToString()
        {
            return AOIName;
        }

    }
}
