using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Source.Classes.Entities
{
    public class EntityIdentifier
    {
        [Key]
        public int EntityIdentifierKey { get; set; }

        public int EntityKey { get; set; }
        public int IdentifierTypeKey { get; set; }
        [Required]
        [Column(TypeName = "NVARCHAR")]
        [StringLength(30)]
        public string IdentifierValue { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime UpdatedDt { get; set; }
        public bool IsEntityIdentifierValuePrivate { get; set; }


        public virtual Entity Entity { get; set; }
        public virtual IdentifierType IdentifierType { get; set; }
    }
}