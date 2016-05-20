using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Source.Classes.Entities
{
    public class GridLocation
    {
        [Key]
        public int GridLocationKey { get; set; }

        public int FloorKey { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR")]
        [StringLength(100)]
        public string GridLocationValue { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime CreatedDt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime UpdatedDt { get; set; }
        public bool IsGridLocationValuePrivate { get; set; }

        public virtual ICollection<Entity> Entities { get; set; }
        public virtual Floor Floor { get; set; }
    }
}