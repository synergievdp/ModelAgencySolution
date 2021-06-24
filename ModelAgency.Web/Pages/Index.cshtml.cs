using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ModelAgency.Web.Data;
using ModelAgency.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelAgency.Web.Pages {
    public class IndexModel : PageModel {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext dbContext;

        public List<ModelUser> Models { get; set; }

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext dbContext) {
            _logger = logger;
            this.dbContext = dbContext;
        }

        public void OnGet() {
            Models = dbContext.Models.Include(model => model.Photos).Where(model => model.AccountState == AccountState.Approved).ToList();
        }
    }
}
