using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModelAgency.Web.Data;
using ModelAgency.Web.Data.Entities;

namespace ModelAgency.Web.Areas.Customer.Pages.Events
{
    [Authorize(Policy = "PageOwner")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;

        public string Id { get; set; }
        [BindProperty]
        public EventModel Event { get; set; }

        public class EventModel {
            [Required]
            public string Name { get; set; }
            public EventType EventType { get; set; }
            public bool Private { get; set; }
        }

        public CreateModel(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }
        public void OnGet(string id)
        {
            Id = id;
        }

        public IActionResult OnPost(string id) {
            var customer = dbContext.Customers.FirstOrDefault(customer => customer.Id == id);
            if(customer != null) {
                var ev = new Event() {
                    Name = Event.Name,
                    EventType = Event.EventType,
                    Private = Event.Private,
                    Organizer = customer
                };
                if (customer.Events == null)
                    customer.Events = new();
                customer.Events.Add(ev);
                dbContext.SaveChanges();

                return LocalRedirect($"/Customer/{id}/Events/Details/{ev.Id}");
            }

            return Page();
        }
    }
}
