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

namespace ModelAgency.Web.Areas.Model.Pages.Profile
{
    [Authorize(Policy = "ApprovedOrOwner")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;

        public ModelUser Model { get; set; }
        public List<Event> Events { get; set; }

        public IndexModel(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }
        public IActionResult OnGet(string id)
        {
            Model = dbContext.Models
                .Include(model => model.Photos)
                .Include(model => model.Invites)
                .FirstOrDefault(model => model.Id == id);

            if (Model == null)
                return NotFound();

            Events = Model.Invites
                .Where(invite => invite.InviteeAccepted == InviteState.Accepted && invite.OrganizerAccepted == InviteState.Accepted)
                .Select(invite => invite.Event).ToList();

            return Page();
        }
    }
}
