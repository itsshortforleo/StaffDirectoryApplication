using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Source.Classes
{
    public class Division
    {
        public int DivisionKey { get; set; }
        public int EntityKey { get; set; }
        public string DivisionValue { get; set; }
        public string DescShort { get; set; }
        public string DescLong { get; set; }
        public System.DateTime CreatedDt { get; set; }
        public System.DateTime UpdatedDt { get; set; }

        public virtual ICollection<Unit> Units { get; set; }
    }
}