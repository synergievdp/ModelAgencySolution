using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModelAgency.Web.Data;
using ModelAgency.Web.Data.Entities;

namespace ModelAgency.Web.Areas.Admin.Pages
{
    [Authorize(Roles = "Admin")]
    public class ApprovalsModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;

        public List<ApplicationUser> Users { get; set; }

        public ApprovalsModel(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }
        public void OnGet()
        {
            Users = dbContext.Users.Where(user => user.AccountState == AccountState.Pending).ToList();
        }

        public IActionResult OnPostApprove(string userid) {
            var user = dbContext.Users.First(user => user.Id == userid);
            if (user != null) {
                user.AccountState = AccountState.Approved;
                dbContext.SaveChanges();
            }

            return LocalRedirect("/Admin/Approvals");
        }
        public IActionResult OnPostReject(string userid) {
            var user = dbContext.Users.First(user => user.Id == userid);
            if (user != null) {
                user.AccountState = AccountState.Rejected;
                dbContext.SaveChanges();
            }

            return LocalRedirect("/Admin/Approvals");
        }
    }
}
