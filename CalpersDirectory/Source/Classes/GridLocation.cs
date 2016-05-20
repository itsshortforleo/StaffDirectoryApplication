using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Source.Classes
{
    public class GridLocation
    {
        public int GridLocationKey { get; set; }
        public string GridLocationValue { get; set; }
        public string DescShort { get; set; }
        public string DescLong { get; set; }
        public System.DateTime CreatedDt { get; set; }
        public System.DateTime UpdatedDt { get; set; }
        public int FloorKey { get; set; }

        public virtual ICollection<Entity> Entities { get; set; }
        public virtual Floor Floor { get; set; }
    }
}