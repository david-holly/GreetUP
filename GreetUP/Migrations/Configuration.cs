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

        bool AddUserAndRole(GreetUP.Models.ApplicationDbContext context)
        {
            IdentityResult ir;
            var rm = new RoleManager<IdentityRole>
                (new RoleStore<IdentityRole>(context));
            ir = rm.Create(new IdentityRole("canEdit"));
            var um = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser()
            {
                UserName = "user1@contoso.com",
            };
            ir = um.Create(user, "P_assw0rd1");
            if (ir.Succeeded == false)
                return ir.Succeeded;
            ir = um.AddToRole(user.Id, "canEdit");
            return ir.Succeeded;
        }



        protected override void Seed(GreetUP.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            AddUserAndRole(context);
            context.Users.AddOrUpdate(
              p => p.UserName,

              new ApplicationUser
              { UserName = "AndrewPeters",
                  Email = "andrew@example.com",
                  PhoneNumber = "555-5655",

              },
              new ApplicationUser
              { UserName = "BriceLambson",
                Email = "brice@example.com",
                PhoneNumber = "555-5755",
              },
              new ApplicationUser
              { UserName = "RowanMiller",
              Email = "rowan@example.com",
              PhoneNumber = "555-5855",
              }
            );

            context.Events.AddOrUpdate(
                p => p.EventName,
                new Event
                {
                    
                    EventName = "Comicon",
                    Time = "Noon",
                    Address = "SanDiego",
                    Date = "January 12th, 2016",
                    Description = "Come get your comic on!",
                }
                );

            var Comicon = context.Events.Single(e => e.EventName == "Comicon");
            context.RSVPs.AddOrUpdate(
                p => p.RSVPID,
                new RSVP
                {
                    Event = Comicon,
                    ApplicationUser = context.Users.Single(u => u.UserName == "BriceLambson")
                }
                );
        }
    }
}
