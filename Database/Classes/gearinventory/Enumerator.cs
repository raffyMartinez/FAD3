using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace FAD3.Database.Classes.gearinventory
{
    public class Enumerator
    {
        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

        private string _name;
        public string GUID { get; set; }
        public string Name
        {
            get
            {
                return textInfo.ToTitleCase(_name);
            }
            set
            {
                _name = value;
            }
        }
    }
}
