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

namespace ModelAgency.Web.Pages
{
    [Authorize(Policy = "ApprovedUsers")]
    public class ProfileModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;

        public ModelUser model { get; set; }

        public ProfileModel(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }
        public IActionResult OnGet(string id)
        {
            model = dbContext.Models.Include(model => model.Photos).FirstOrDefault(model => model.Id == id);
            if(model == null) {
                return NotFound();
            }
            return Page();
        }
    }
}
