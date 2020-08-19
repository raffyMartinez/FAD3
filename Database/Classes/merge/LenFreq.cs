using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAD3.Database.Classes.merge
{
    public class LenFreqFlattened
    {
        public LenFreqFlattened() { }
        public string RefNo { get; set; }
        public DateTime DateSampled { get; set; }
        public string LandingSite { get; set; }
        public string Gear { get; set; }
        
        public string CatchName { get; set; }
        public double Length { get; set; }
        public int Frequency { get; set; } 
    }
    public class LenFreq
    {
        private CatchComposition _catchComposition;
        public FADEntities FADEntities { get; set; }

        public CatchComposition CatchComposition
        {
            get
            {
                if (_catchComposition == null)
                {
                    _catchComposition = FADEntities.CatchCompositionViewModel.GetCatchComposition(CatchCompositionID);
                }
                return _catchComposition;
            }

            set
            {
                _catchComposition = value;
            }
        }
        public string CatchCompositionID { get; set; }
        public double LenClass { get; set; }
        public int Freq { get; set; }

        public int? Sequence { get; set; }
        public string RowGUID { get; set; }

        public override string ToString()
        {
            string nameGUID = CatchComposition.NameGUID;
            string catchName = "";
            
            if (CatchComposition.NameType==Identification.LocalName)
            {
                catchName = FADEntities.CatchLocalNameViewModel.GetCatchLocalName(nameGUID).ToString();
            }
            else
            {
                catchName = FADEntities.SpeciesViewModel.GetSpecies(nameGUID).ToString();
            }

            return $"LF: {catchName} - {LenClass} {Freq}";
        }
    }
}
