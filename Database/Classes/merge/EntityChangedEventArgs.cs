using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAD3.Database.Classes.merge
{
    public class EntityChangedEventArgs : EventArgs
    {
        public string EntityType { get; private set; }
        public object FAD44Entity { get; private set; }

        public EntityChangedEventArgs(string entityType, object entity)
        {
            EntityType = entityType;
            FAD44Entity = entity;
        }
    }
}
