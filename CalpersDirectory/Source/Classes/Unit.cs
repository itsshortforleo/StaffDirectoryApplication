using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Source.Classes
{
    public class Unit
    {
        public int UnitKey { get; set; }
        public int DivisionKey { get; set; }
        public string UnitValue { get; set; }
        public string DescShort { get; set; }
        public string DescLong { get; set; }
        public System.DateTime CreatedDt { get; set; }
        public System.DateTime UpdatedDt { get; set; }

        public virtual Division Division { get; set; }
        public virtual ICollection<Entity> Entities { get; set; }
    }
}