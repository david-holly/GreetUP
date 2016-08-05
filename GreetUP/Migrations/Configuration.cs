namespace GreetUP.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using GreetUP.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    
    internal sealed class Configuration : DbMigrationsConfiguration<GreetUP.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "GreetUP.Models.ApplicationDbContext";
        }

        protected override void Seed(GreetUP.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.

            context.ApplicationUsers.AddOrUpdate(
              p => p.UserName,

              new ApplicationUser
              { UserName = "AndrewPeters",
                  Email = "andrew@example.com",
                  PhoneNumber = "555-5555",

              },
              new ApplicationUser
              { UserName = "BriceLambson",
                Email = "brice@example.com",
                PhoneNumber = "555-5555",
              },
              new ApplicationUser
              { UserName = "RowanMiller",
              Email = "brice@example.com",
              PhoneNumber = "555-5555",
              }

            );

        }
    }
}
