using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Source.Classes.Entities
{
    public class Name
    {
        [Key]
        public int NameKey { get; set; }

        public int EntityKey { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [StringLength(256)]
        public string NameValue { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime UpdatedDt { get; set; }
        public bool IsNameValuePrivate { get; set; }

        public virtual Entity Entity { get; set; }
    }
}