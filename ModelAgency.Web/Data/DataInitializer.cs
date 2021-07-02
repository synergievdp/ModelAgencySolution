using Microsoft.AspNetCore.Identity;
using ModelAgency.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ModelAgency.Web.Data {
    public static class DataInitializer {
        public static void SeedData(UserManager<ApplicationUser> userManager) {
            SeedUsers(userManager);
        }
        public static void SeedUsers(
            UserManager<ApplicationUser> userManager) {
            string email = "admin@admin.com";
            if(userManager.FindByEmailAsync(email).Result == null) {
                var user = new ApplicationUser() {
                    Email = email,
                    UserName = email,
                    AccountState = AccountState.Approved,
                    EmailConfirmed = true
                };
                var result = userManager.CreateAsync(user, "password").Result;
                if(result.Succeeded) {
                    userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Admin")).Wait();
                }
            }
        }
    }
}
