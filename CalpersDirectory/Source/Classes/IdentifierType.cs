using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Source.Classes
{
    public class IdentifierType
    {
        public int IdentifierTypeKey { get; set; }
        public string Code { get; set; }
        public string DescShort { get; set; }
        public string DescLong { get; set; }
        public bool ActiveFlag { get; set; }
        public System.DateTime CreatedDt { get; set; }
        public System.DateTime UpdatedDt { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EntityIdentifier> EntityIdentifiers { get; set; }

    }
}