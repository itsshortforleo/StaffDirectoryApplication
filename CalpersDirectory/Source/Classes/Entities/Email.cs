using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Source.Classes.Entities
{
    public class Email
    {
        [Key]
        public int EmailKey { get; set; }

        public int EntityKey { get; set; }

        public int EmailTypeKey { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR")]
        [StringLength(200)]
        public string EmailAddress { get; set; }

        public bool Valid { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime UpdatedDt { get; set; }
        public bool IsEmailValuePrivate { get; set; }


        public virtual Entity Entity { get; set; }
        public virtual EmailType EmailType { get; set; }
    }
}