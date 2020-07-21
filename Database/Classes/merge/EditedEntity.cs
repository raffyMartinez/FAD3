using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAD3.Database.Classes.merge
{
    public enum EditAction
    {
        Add,
        Update,
        Delete
    }

    public class EditedEntity
    {
        public EditAction EditAction { get; set; }

        public object FAD4Entity { get; set; }
        public EditedEntity(EditAction editAction, object fad4Entity)
        {
            EditAction = editAction;
            string typeName = fad4Entity.GetType().Name;
            if (typeName == "Gear"
                || typeName=="AOI"
                || typeName == "LandingSite"
                || typeName == "Sampling")
            {
                FAD4Entity = fad4Entity;
            }
            else
            {
                throw new Exception("Error: BSCEntity is of the wrong type");
            }
        }
    }

}

