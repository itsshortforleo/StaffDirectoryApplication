using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Source.Classes.Entities
{
    public class Picture
    {
        [Key]
        public int PictureKey { get; set; }

        public int EntityKey { get; set; }
        public string FileName { get; set; }
        public byte PictureData { get; set; }
        public string DescShort { get; set; }
        public string DescLong { get; set; }
        public bool ActiveFlag { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime UpdatedDt { get; set; }
        public bool IsPictureValuePrivate { get; set; }

        public virtual ICollection<Entity> Entities { get; set; }
    }
}