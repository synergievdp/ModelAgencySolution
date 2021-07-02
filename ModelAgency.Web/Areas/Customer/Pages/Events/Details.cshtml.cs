using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ModelAgency.Web.Data.Entities;
using ModelAgency.Web.Data.Repositories;

namespace ModelAgency.Web.Areas.Customer.Pages.Events {
    [Authorize(Policy = "PageOwner")]
    public class DetailsModel : PageModel
    {
        private readonly IModelRepository models;
        private readonly IEventRepository events;

        public List<ModelUser> Models { get; set; }
        public Event Event { get; set; }

        public DetailsModel(IModelRepository models, IEventRepository events) {
            this.models = models;
            this.events = events;
        }
        public void OnGet(string id, int eventid)
        {
            Event = events.Get(ev => ev.Id == eventid, invites: true);
            Models = models.GetAll(model => model.AccountState == AccountState.Approved, invites: true).ToList();
        }

        public IActionResult OnPostAccept(string id, int eventid, string modelid) {
            var ev = events.Get(ev => ev.Id == eventid, invites: true);
            if (ev != null) {
                var invite = ev.Invites.FirstOrDefault(invite => invite.ModelId == modelid);
                if (invite != null) {
                    invite.OrganizerAccepted = InviteState.Accepted;
                    events.Update(ev);
                }
            }
            return LocalRedirect($"/Customer/{id}/Events/Details/{eventid}");
        }

        public IActionResult OnPostInvite(string id, int eventid, string modelid) {
            var model = models.Get(model => model.Id == id);
            var ev = events.Get(ev => ev.Id == eventid, invites: true);
            if (model != null && ev != null) {
                Invite invite = new() {
                    Model = model,
                    Event = ev,
                    OrganizerAccepted = InviteState.Accepted
                };
                if (ev.Invites == null)
                    ev.Invites = new();
                ev.Invites.Add(invite);
                events.Update(ev);
            }
            return LocalRedirect($"/Customer/{id}/Events/Details/{eventid}");
        }
    }
}
