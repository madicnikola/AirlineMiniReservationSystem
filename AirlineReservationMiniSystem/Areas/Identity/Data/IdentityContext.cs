using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirlineReservationMiniSystem.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AirlineReservationMiniSystem.Data
{
    public class IdentityContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            IdentityRole f1 = new IdentityRole { Id = 1.ToString(), Name = "Administrator", NormalizedName = "ADMINISTRATOR" };
            IdentityRole f2 = new IdentityRole { Id = 2.ToString(),  Name = "Agent", NormalizedName = "AGENT" };
            IdentityRole f3 = new IdentityRole { Id = 3.ToString(), Name = "Client", NormalizedName = "CLIENT" };

			
			
            builder.Entity<IdentityRole>().HasData(f1);
            builder.Entity<IdentityRole>().HasData(f2);
            builder.Entity<IdentityRole>().HasData(f3);
            
        }

	}
}
