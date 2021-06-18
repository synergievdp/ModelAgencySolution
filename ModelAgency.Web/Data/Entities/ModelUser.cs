using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ModelAgency.Web.Data.Entities {
    public class ModelUser : ApplicationUser {
        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public DateTime DOB { get; set; }
        public List<Photo> Photos { get; set; }
    }
}
