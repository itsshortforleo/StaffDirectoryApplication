using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Source.Classes
{
    public class Email
    {
        public int EmailKey { get; set; }
        public int EntityKey { get; set; }
        public string EmailAddress { get; set; }
        public Nullable<bool> Valid { get; set; }
        public System.DateTime CreatedDt { get; set; }
        public System.DateTime UpdatedDt { get; set; }
        public int EmailTypeKey { get; set; }

        public virtual Entity Entity { get; set; }
        public virtual EmailType EmailType { get; set; }
    }
}