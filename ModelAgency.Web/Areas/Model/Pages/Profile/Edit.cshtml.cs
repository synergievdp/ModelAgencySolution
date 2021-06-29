using System;
using System.Collections.Generic;
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

namespace ModelAgency.Web.Areas.Model.Pages.Profile
{
    [Authorize(Policy = "PageOwner")]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IWebHostEnvironment webHost;

        public ModelUser Model { get; set; }

        public EditModel(ApplicationDbContext dbContext,
            IWebHostEnvironment webHost) {
            this.dbContext = dbContext;
            this.webHost = webHost;
        }

        public IActionResult OnGet(string id)
        {
            Model = dbContext.Models
                .Include(model => model.Photos)
                .FirstOrDefault(model => model.Id == id);
            if(Model == null) {
                return NotFound();
            }
            return Page();
        }
        
        public class ModelModel {
            public string Name { get; set; }
            public DateTime DOB { get; set; }
        }

        public IActionResult OnPost(string id, ModelModel model) {
            var dbModel = dbContext.Models.FirstOrDefault(dbm => dbm.Id == id);

            if (dbModel == null)
                return NotFound();

            dbModel.Name = model.Name;
            dbModel.DOB = model.DOB;

            dbContext.SaveChanges();
            return LocalRedirect($"/Model/{id}/Profile/Edit");
        }

        public IActionResult OnPostAddPhotos(string id, List<IFormFile> photos) {
            var dbModel = dbContext.Models
                .Include(model => model.Photos)
                .FirstOrDefault(model => model.Id == id);

            if (dbModel == null)
                return NotFound();

            foreach(var photo in photos) {
                var relativedir = Path.Combine("img", "models", id);
                var dir = Path.Combine(webHost.WebRootPath, relativedir);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                var relativepath = Path.Combine(relativedir, photo.FileName);
                var path = Path.Combine(webHost.WebRootPath, relativepath);
                dbModel.Photos.Add(new Photo() { Path = relativepath });
                using(var file = System.IO.File.Create(path)) {
                    photo.CopyTo(file);
                }
            }
            dbContext.SaveChanges();
            return LocalRedirect($"/Model/{id}/Profile/Edit");
        }

        public IActionResult OnPostDelete(string id, int photoId) {
            var dbModel = dbContext.Models
                .Include(model => model.Photos)
                .FirstOrDefault(model => model.Id == id);

            if (dbModel == null)
                return NotFound();

            var photo = dbModel.Photos.FirstOrDefault(photo => photo.Id == photoId);

            if (photo == null)
                return NotFound();

            System.IO.File.Delete(Path.Combine(webHost.WebRootPath, photo.Path));
            dbModel.Photos.Remove(photo);
            dbContext.SaveChanges();

            return LocalRedirect($"/Model/{id}/Profile/Edit");
        }
    }
}
