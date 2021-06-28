using Microsoft.AspNetCore.Identity;
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
        public DbSet<Event> Events { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Invite>()
                .HasKey(invite => new { invite.ModelId, invite.EventId });
        }
    }
}
