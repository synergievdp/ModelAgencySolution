using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ModelAgency.Web.Data;
using ModelAgency.Web.Data.Entities;

namespace ModelAgency.Web.Areas.Customer.Pages.Events
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;
        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }
        public List<Event> Events { get; set; }
        public IndexModel(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }
        public void OnGet(string id)
        {
            var customer = dbContext.Customers.Include(customer => customer.Events).ThenInclude(ev => ev.Invites).FirstOrDefault(customer => customer.Id == id);
            if(customer != null) {
                Events = customer.Events;
            }
        }
    }
}
