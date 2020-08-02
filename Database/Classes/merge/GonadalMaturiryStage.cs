using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAD3.Database.Classes.merge
{
   public class GonadalMaturiryStage
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
        public double? Weight { get; set; }
        public Sex Sex { get; set; }
        public FishCrabGMS GMS { get; set; }
        public string RowGUID { get; set; }
        public double? GonadWeight { get; set; }

        public double? Length { get; set; }

        public override string ToString()
        {
            string nameGUID = CatchComposition.NameGUID;
            string catchName = FADEntities.SpeciesViewModel.GetSpecies(nameGUID).ToString();
            if (CatchComposition.NameType == Identification.LocalName)
            {
                catchName = FADEntities.CatchLocalNameViewModel.GetCatchLocalName(nameGUID).ToString();
            }

            return $"GMS: {catchName} - {Sex.ToString()} {GMS.ToString()}";
        }
    }
}
