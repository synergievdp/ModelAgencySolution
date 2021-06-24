using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ModelAgency.Web.Data;
using ModelAgency.Web.Data.Entities;

namespace ModelAgency.Web.Pages
{
    [Authorize(Policy = "PageOwner")]
    public class EditProfileModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IWebHostEnvironment webHost;

        public ModelUser Model { get; set; }
        public string ReturnUrl { get; set; }

        public EditProfileModel(ApplicationDbContext dbContext,
            IWebHostEnvironment webHost) {
            this.dbContext = dbContext;
            this.webHost = webHost;
        }

        public IActionResult OnGet(string id)
        {
            if (!User.IsInRole("Admin") && !User.HasClaim(claim => claim.Value == id))
                return LocalRedirect($"/Profile?id={id}");
            Model = dbContext.Models.Include(model => model.Photos).FirstOrDefault(model => model.Id == id);
            if(Model == null) {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost(string id, string name, DateTime dob) {
            Model = dbContext.Models.Include(model => model.Photos).FirstOrDefault(model => model.Id == id);

            Model.Name = name;
            Model.DOB = dob;
            Model.AccountState = AccountState.Pending;

            dbContext.SaveChanges();
            return Page();
        }

        public IActionResult OnPostAddPhotos(string id, List<IFormFile> photos) {
            Model = dbContext.Models.Include(model => model.Photos).FirstOrDefault(model => model.Id == id);
            foreach (var photo in photos) {
                var relativedir = Path.Combine("img", "models", Model.Name);
                var dir = Path.Combine(webHost.WebRootPath, relativedir);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                var relative = Path.Combine(relativedir, photo.FileName);
                var path = Path.Combine(webHost.WebRootPath, relative);
                Model.Photos.Add(new Photo() { Path = relative });
                using (var file = System.IO.File.Create(path)) {
                    photo.CopyTo(file);
                }
            }

            Model.AccountState = AccountState.Pending;
            dbContext.SaveChanges();

            return Page();
        }

        public IActionResult OnPostDelete(string id, int photoId) {
            Model = dbContext.Models.Include(model => model.Photos).FirstOrDefault(model => model.Id == id);
            var photo = Model.Photos.FirstOrDefault(photo => photo.Id == photoId);
            if (photo == null)
                return NotFound();
            System.IO.File.Delete(Path.Combine(webHost.WebRootPath, photo.Path));
            Model.Photos.Remove(photo);
            dbContext.SaveChanges();
            Model.AccountState = AccountState.Pending;

            return Page();
        }
    }
}
