using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAD3.Database.Classes.merge
{
    public class CatchName
    {
        private Species _species;

        private CatchLocalName _catchLocalName;
    
        public FADEntities FADEntities { get; set; }
        public Species Species
        {
            get
            {
                if(_species==null)
                {
                    _species = FADEntities.SpeciesViewModel.GetSpecies(CatchNameGuid);
                }
                return _species;
            }
            set
            {
                _species = value;
            }
        }

        public CatchLocalName CatchLocalName
        {
            get
            {
                if(_catchLocalName==null)
                {
                    _catchLocalName = FADEntities.CatchLocalNameViewModel.GetCatchLocalName(CatchNameGuid);
                }
                return _catchLocalName;
            }

            set
            {
                _catchLocalName = value;
            }
        }

        public string CatchNameGuid { get; set; }

        public Identification CatchNameType { get; set; }
        public override string ToString()
        {
            string name = "";
            if(CatchNameType==Identification.LocalName)
            {
                name =  CatchLocalName.LocalName;
            }
            else if(CatchNameType==Identification.Scientific)
            {
                name = $"{Species.Generic} {Species.Specific}";
            }
            return name;
        }
    }
}
