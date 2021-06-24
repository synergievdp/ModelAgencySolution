using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ModelAgency.Web.Areas.Identity {
    public class PageOwnerHandler : AuthorizationHandler<PageOwnerRequirement> {
        private readonly IHttpContextAccessor httpContext;

        public PageOwnerHandler(IHttpContextAccessor httpContext) {
            this.httpContext = httpContext;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PageOwnerRequirement requirement) {
            if(context.User.IsInRole("Admin")) {
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
