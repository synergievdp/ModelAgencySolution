using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelAgency.Web.Data.Entities {
    public class ApplicationUser : IdentityUser {
        public AccountState AccountState { get; set; }
    }
}
