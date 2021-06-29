using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ModelAgency.Web.Data;
using ModelAgency.Web.Data.Entities;
using ModelAgency.Web.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelAgency.Web.Pages {
    public class IndexModel : PageModel {
        private readonly ILogger<IndexModel> _logger;
        private readonly IModelRepository models;

        public List<ModelUser> Models { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IModelRepository models) {
            _logger = logger;
            this.models = models;
        }

        public void OnGet() {
            Models = models.Get(model => model.AccountState == AccountState.Approved, models => models.Include(model => model.Photos)).ToList();
        }
    }
}
