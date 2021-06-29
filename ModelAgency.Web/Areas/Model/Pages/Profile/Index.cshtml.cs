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

namespace ModelAgency.Web.Areas.Model.Pages.Profile
{
    [Authorize(Policy = "ApprovedOrOwner")]
    public class IndexModel : PageModel
    {
        private readonly ModelRepository models;

        public ModelUser Model { get; set; }
        public List<Event> Events { get; set; }

        public IndexModel(ModelRepository models) {
            this.models = models;
        }
        public IActionResult OnGet(string id)
        {
            Model = models.GetById(id, models => models
                .Include(model => model.Photos)
                .Include(model => model.Invites)
                .ThenInclude(invite => invite.Event));

            if (Model == null)
                return NotFound();

            Events = Model.Invites
                .Where(invite => invite.InviteeAccepted == InviteState.Accepted && invite.OrganizerAccepted == InviteState.Accepted)
                .Select(invite => invite.Event).ToList();

            return Page();
        }
    }
}
