using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelAgency.Web.Data.Entities {
    public class CustomerUser : ApplicationUser {
        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string KvK { get; set; }
        public string BTW { get; set; }
        public string LogoPath { get; set; }
        public List<Event> Events { get; set; }
    }
}
