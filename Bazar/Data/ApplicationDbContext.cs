using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Bazar.Models;

namespace Bazar.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<Products>()
                .Property(b => b.DateCreated)
                .HasDefaultValueSql("strftime('%Y-%m-%d %H:%M:%S')");
            builder.Entity<Orders>()
                .Property(b => b.DateCreated)
                .HasDefaultValueSql("strftime('%Y-%m-%d %H:%M:%S')");

        }
        public DbSet<Bazar.Models.Products> Products { get; set; }

        public DbSet<Bazar.Models.Orders> Orders { get; set; }

        public DbSet<Bazar.Models.OrderProduct> OrderProduct { get; set; }


        public DbSet<Bazar.Models.PaymentTypes> PaymentTypes { get; set; }

        public DbSet<Bazar.Models.ProductTypes> ProductTypes { get; set; }

        public DbSet<Bazar.Models.Address> Address { get; set; }
    }
}
