using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModelAgency.Web.Data.Entities;
using ModelAgency.Web.Data.Repositories;

namespace ModelAgency.Web.Areas.Model.Pages.Profile {
    [Authorize(Policy = "PageOwner")]
    public class EditModel : PageModel
    {
        private readonly ModelRepository models;
        private readonly IWebHostEnvironment webHost;

        public ModelUser Model { get; set; }

        public EditModel(ModelRepository models,
            IWebHostEnvironment webHost) {
            this.models = models;
            this.webHost = webHost;
        }

        public IActionResult OnGet(string id)
        {
            Model = models.Get(model => model.Id == id, photos: true);
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
            var dbModel = models.Get(model => model.Id == id);

            if (dbModel == null)
                return NotFound();

            dbModel.Name = model.Name;
            dbModel.DOB = model.DOB;

            models.Update(dbModel);
            return LocalRedirect($"/Model/{id}/Profile/Edit");
        }

        public IActionResult OnPostAddPhotos(string id, List<IFormFile> photos) {
            var dbModel = models.Get(model => model.Id == id, photos: true);

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
            models.Update(dbModel);
            return LocalRedirect($"/Model/{id}/Profile/Edit");
        }

        public IActionResult OnPostDelete(string id, int photoId) {
            var dbModel = models.Get(model => model.Id == id, photos: true);

            if (dbModel == null)
                return NotFound();

            var photo = dbModel.Photos.FirstOrDefault(photo => photo.Id == photoId);

            if (photo == null)
                return NotFound();

            System.IO.File.Delete(Path.Combine(webHost.WebRootPath, photo.Path));
            dbModel.Photos.Remove(photo);
            models.Update(dbModel);

            return LocalRedirect($"/Model/{id}/Profile/Edit");
        }
    }
}
