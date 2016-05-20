using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Source.Classes
{
    public class Name
    {
        public int NameKey { get; set; }
        public int EntityKey { get; set; }
        public string NameValue { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public System.DateTime CreatedDt { get; set; }
        public System.DateTime UpdatedDt { get; set; }

        public virtual Entity Entity { get; set; }
    }
}