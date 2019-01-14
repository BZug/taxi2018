using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using TaxiService2018.Models;
using static TaxiService2018.Models.Enums;

namespace TaxiService2018.Database
{
    public class ApplicationDBInitializer : DropCreateDatabaseAlways<ApplicationDBContext>//CreateDatabaseIfNotExists<ApplicationDBContext>
    {
        protected override void Seed(ApplicationDBContext context)
        {
            context.ApplicationUsers.AddOrUpdate(new ApplicationUser()
            {
                Id = 1,
                Username = "admin",
                Password = "admin",
                FirstName = "Adam",
                LastName = "Adminović",
                Gender = Gender.Male,
                UMCN = "1231231231231",
                Phone = "065 656 656",
                Email = "admin@taxiservice.com",
                Role = UserRole.Dispatcher
            });

            context.SaveChanges();

            base.Seed(context);

        }
    }
}