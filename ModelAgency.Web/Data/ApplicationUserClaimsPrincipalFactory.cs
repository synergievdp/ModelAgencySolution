using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using ModelAgency.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ModelAgency.Web.Data {
    public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser> {

        public ApplicationUserClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager,
            IOptions<IdentityOptions> options)
            : base(userManager, options) {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user) {
            var identity = await base.GenerateClaimsAsync(user);

            identity.AddClaim(new Claim(nameof(AccountState), Enum.GetName(typeof(AccountState), user.AccountState)));

            return identity;
        }
    }
}
