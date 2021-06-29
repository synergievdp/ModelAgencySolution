using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ModelAgency.Web.Data;
using ModelAgency.Web.Data.Entities;
using ModelAgency.Web.Data.Repositories;

namespace ModelAgency.Web.Areas.Customer.Pages.Events
{
    public class IndexModel : PageModel
    {
        private readonly ICustomerRepository customers;

        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }
        public List<Event> Events { get; set; }
        public IndexModel(ICustomerRepository customers) {
            this.customers = customers;
        }
        public void OnGet(string id)
        {
            var customer = customers.Get(customer => customer.Id == id, events: true);
            if(customer != null) {
                Events = customer.Events;
            }
        }
    }
}
