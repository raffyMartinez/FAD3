using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAD3.Database.Classes.merge
{
    public class AdditionalFishingGroundsMerged
    {
        public string MergedFishingGrounds { get; private set; }
        public AdditionalFishingGroundsMerged(string id, FADEntities fadEntities)
        {
            foreach(var item in fadEntities.AdditionalFishingGroundViewModel.AdditionalFishingGroundCollection.Where(t => t.Sampling.RowID == id).ToList())
            {
                MergedFishingGrounds += $"{item.GridCell.ToString()}, ";
            }
            if (MergedFishingGrounds != null)
            {
                MergedFishingGrounds = MergedFishingGrounds.Trim(new char[] { ' ', ',' });
            }
        }
    }
}
