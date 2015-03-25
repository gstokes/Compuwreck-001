using Compuwreck_001.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Compuwreck_001.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Compuwreck_001.Models.CompuwreckEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Compuwreck_001.Models.Models.ApplicationDbContext";
        }

        protected override void Seed(Compuwreck_001.Models.CompuwreckEntities context)
        {
            var manager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(
                    new ApplicationDbContext()));

            for (int i = 0; i < 4; i++) {
                var user = new ApplicationUser() {
                    UserName = string.Format("User{0}", i.ToString())
                };
                manager.Create(user, string.Format("Password{0}", i.ToString()));
            }
        }
    }
}
