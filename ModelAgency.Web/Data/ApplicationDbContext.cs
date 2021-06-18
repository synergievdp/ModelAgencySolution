using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ModelAgency.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelAgency.Web.Data {
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {

        public DbSet<CustomerUser> Customers { get; set; }
        public DbSet<ModelUser> Models { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {
        }
    }
}
