using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Source.Classes
{
    public class EmailType
    {
        public int EmailTypeKey { get; set; }
        public string Code { get; set; }
        public string DescShort { get; set; }
        public string DescLong { get; set; }
        public bool ActiveFlag { get; set; }
        public System.DateTime CreatedDt { get; set; }
        public System.DateTime UpdatedDt { get; set; }

        public virtual ICollection<Email> Emails { get; set; }
    }
}