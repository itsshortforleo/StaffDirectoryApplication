using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Source.Classes
{
    public class Floor
    {
        public int FloorKey { get; set; }
        public string FloorValue { get; set; }
        public string DescShort { get; set; }
        public string DescLong { get; set; }
        public System.DateTime CreatedDt { get; set; }
        public System.DateTime UpdatedDt { get; set; }
        public int BuildingKey { get; set; }

        public virtual Building Building { get; set; }
        public virtual ICollection<GridLocation> GridLocations { get; set; }

    }
}