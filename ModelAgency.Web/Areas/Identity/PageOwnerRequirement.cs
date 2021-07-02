using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelAgency.Web.Areas.Identity {
    public class PageOwnerRequirement : IAuthorizationRequirement {
    }
}
