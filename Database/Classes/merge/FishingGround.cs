using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAD3.Database.Classes.merge
{
    public class FishingGround
    {
        private string _fishingGroundText;
        
        public Sampling Sampling { get; set; }

        public string FishingGroundText
        {
            get 
            {
                if (IsGrid25)
                {
                    //if (SubGrid != null)
                    //if(Grid25FishingGrounds[0].SubGrids != null && Grid25FishingGrounds[0].SubGrids.Count>0)
                    if(Grid25FishingGrounds[0].SubGrids.Count>0)
                    {
                        //return $"{Grid25FishingGrounds[0].ToString()}-{SubGrid.ToString()}";
                        return $"{Grid25FishingGrounds[0].ToString()}-{Grid25FishingGrounds[0].SubGrids[0].SubGrid}";
                    }
                    else
                    {
                        return Grid25FishingGrounds[0].ToString();
                    }
                }
                else
                {
                    return _fishingGroundText;
                }
            } 

        }
        public byte? SubGrid { get; set; }
        public bool IsGrid25 { get; set; }
        public List<Grid25GridCell> Grid25FishingGrounds { get; set; }
        public FishingGround(Grid25GridCell fg, Sampling s)
        {
            Sampling = s;
            IsGrid25 = true;
            if (Grid25FishingGrounds == null)
            {
                Grid25FishingGrounds = new List<Grid25GridCell>();
            }
            Grid25FishingGrounds.Add(fg);
        }

        public Grid25GridCell GetGridCell(Grid25GridCell gc)
        {
            return Grid25FishingGrounds
                .Where(t => t.GridNumber == gc.GridNumber)
                .Where(t => t.Row == gc.Row)
                .Where(t => t.Column == gc.Column)
                .FirstOrDefault();
        }

        public FishingGround(string fg, Sampling s)
        {
            Sampling = s;
            if(Sampling.AOI.IsGrid25)
            {
                IsGrid25 = true;
                Grid25GridCell g25 = new Grid25GridCell(s.AOI.UTMZone, fg);
                if(Grid25FishingGrounds==null)
                {
                    Grid25FishingGrounds = new List<Grid25GridCell>();
                }
                Grid25FishingGrounds.Add(g25);

            }
            else
            {
                IsGrid25 = false;
                _fishingGroundText = fg;
            }
        }

        public override string ToString()
        {
            return FishingGroundText;

        }


    }
}
