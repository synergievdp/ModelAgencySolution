using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ModelAgency.Web.Areas.Identity {
    public class ApprovedOrOwnerHandler : AuthorizationHandler<ApprovedOrOwnerRequirement> {
        private readonly IHttpContextAccessor httpContext;

        public ApprovedOrOwnerHandler(IHttpContextAccessor httpContext) {
            this.httpContext = httpContext;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ApprovedOrOwnerRequirement requirement) {
            if (context.User.IsInRole("Admin") || context.User.HasClaim("AccountState", "Approved")) {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            var id = context.User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier);
            if (id == null)
                return Task.CompletedTask;
            if (httpContext.HttpContext.Request.Query["id"] == id.Value)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
