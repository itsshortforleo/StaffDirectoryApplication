using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Source.Classes
{
    public class EntityIdentifier
    {
        public int EntityIdentifierKey { get; set; }
        public string IdentifierValue { get; set; }
        public string DescShort { get; set; }
        public string DescLong { get; set; }
        public System.DateTime CreatedDt { get; set; }
        public System.DateTime UpdatedDt { get; set; }
        public int EntityKey { get; set; }
        public int IdentifierTypeKey { get; set; }

        public virtual Entity Entity { get; set; }
        public virtual IdentifierType IdentifierType { get; set; }

    }
}