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

namespace ModelAgency.Web.Areas.Model.Pages.Events
{
    [Authorize(Policy = "PageOwner")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;

        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }
        public List<Event> Events { get; set; }
        public List<Invite> Invites { get; set; }

        public IndexModel(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }
        public void OnGet(string id)
        {
            var model = dbContext.Models.Include(model => model.Invites).ThenInclude(invite => invite.Event).FirstOrDefault(model => model.Id == id);
            if (model != null) {
                Events = dbContext.Events.Include(ev => ev.Invites).Where(ev => ev.Private == false).ToList();
                Invites = model.Invites.Where(invite => invite.InviteeAccepted == InviteState.Pending).ToList();
            }
        }

        public IActionResult OnPostSignUp(string id, int eventid) {
            var ev = dbContext.Events.Include(ev => ev.Invites).FirstOrDefault(ev => ev.Id == eventid);
            var model = dbContext.Models.Include(model => model.Invites).FirstOrDefault(model => model.Id == id);

            if (model != null && ev != null) {
                Invite invite = new() {
                    Model = model,
                    InviteeAccepted = InviteState.Accepted
                };
                if (ev.Invites == null)
                    ev.Invites = new();
                ev.Invites.Add(invite);
                dbContext.SaveChanges();
            }

            return RedirectToPage($"/Model/{id}/Events/Index");
        }
    }
}
