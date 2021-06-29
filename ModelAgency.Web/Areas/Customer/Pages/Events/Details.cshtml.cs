using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ModelAgency.Web.Data;
using ModelAgency.Web.Data.Entities;

namespace ModelAgency.Web.Areas.Customer.Pages.Events
{
    [Authorize(Policy = "PageOwner")]
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;

        public List<ModelUser> Models { get; set; }
        public Event Event { get; set; }

        public DetailsModel(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }
        public void OnGet(string id, int eventid)
        {
            Event = dbContext.Events.Include(ev => ev.Invites).FirstOrDefault(ev => ev.Id == eventid);
            Models = dbContext.Models.Include(model => model.Invites).Where(model => model.AccountState == AccountState.Approved).ToList();
        }

        public IActionResult OnPostAccept(string id, int eventid, string modelid) {
            var ev = dbContext.Events.Include(ev => ev.Invites).FirstOrDefault(ev => ev.Id == eventid);
            if (ev != null) {
                var invite = ev.Invites.FirstOrDefault(invite => invite.ModelId == modelid);
                if (invite != null) {
                    invite.OrganizerAccepted = InviteState.Accepted;
                    dbContext.SaveChanges();
                }
            }
            return LocalRedirect($"/Customer/{id}/Events/Details/{eventid}");
        }

        public IActionResult OnPostInvite(string id, int eventid, string modelid) {
            var model = dbContext.Models.FirstOrDefault(model => model.Id == modelid);
            var ev = dbContext.Events.FirstOrDefault(ev => ev.Id == eventid);
            if(model != null && ev != null) {
                Invite invite = new() {
                    Model = model,
                    Event = ev,
                    OrganizerAccepted = InviteState.Accepted
                };
                if (ev.Invites == null)
                    ev.Invites = new();
                ev.Invites.Add(invite);
                dbContext.SaveChanges();
            }
            return LocalRedirect($"/Customer/{id}/Events/Details/{eventid}");
        }
    }
}
