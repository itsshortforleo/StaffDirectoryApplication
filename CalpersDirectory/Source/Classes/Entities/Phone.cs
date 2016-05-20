using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Source.Classes.Entities
{
    public class Phone
    {
        [Key]
        public int PhoneKey { get; set; }

        public int PhoneTypeKey { get; set; }
        public int EntityKey { get; set; }
        public string AreaCode { get; set; }
        public string PhoneNumber { get; set; }
        public string ForeignPhoneNumber { get; set; }
        public bool ForeignFlag { get; set; }
        public string PhoneExtension { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime UpdatedDt { get; set; }
        public bool IsPhoneValuePrivate { get; set; }

        public virtual Entity Entity { get; set; }
        public virtual PhoneType PhoneType { get; set; }
    }
}