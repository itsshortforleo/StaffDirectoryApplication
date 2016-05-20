using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Source.Classes.Entities
{
    public class Floor
    {
        [Key]
        public int FloorKey { get; set; }

        public int BuildingKey { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR")]
        [StringLength(100)]
        public string FloorValue { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR")]
        [StringLength(40)]
        public string DescShort { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR")]
        [StringLength(256)]
        public string DescLong { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime CreatedDt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime UpdatedDt { get; set; }
        public bool IsFloorValuePrivate { get; set; }

        public virtual Building Building { get; set; }
        public virtual ICollection<GridLocation> GridLocations { get; set; }
    }
}