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
using ModelAgency.Web.Data.Repositories;

namespace ModelAgency.Web.Areas.Model.Pages.Events
{
    [Authorize(Policy = "PageOwner")]
    public class IndexModel : PageModel
    {
        private readonly IModelRepository models;
        private readonly IEventRepository events;

        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }
        public List<Event> Events { get; set; }
        public List<Invite> Invites { get; set; }

        public IndexModel(IModelRepository models, IEventRepository events) {
            this.models = models;
            this.events = events;
        }
        public void OnGet(string id) { 
            var model = models.Get(model => model.Id == id, invites: true);
            if(model != null) {
                Events = events.GetAll(ev => ev.Private == false, invites: true).ToList();
                Invites = model.Invites.Where(invite => invite.InviteeAccepted == InviteState.Pending).ToList();
            }
        }

        public IActionResult OnPostSignUp(string id, int eventid) {
            var ev = events.Get(ev => ev.Id == eventid, invites: true);
            var model = models.Get(model => model.Id == id);

            if (model != null && ev != null) {
                Invite invite = new() {
                    Model = model,
                    InviteeAccepted = InviteState.Accepted
                };
                if (ev.Invites == null)
                    ev.Invites = new();
                ev.Invites.Add(invite);
                events.Update(ev);
            }

            return RedirectToPage($"/Model/{id}/Events/Index");
        }
    }
}
