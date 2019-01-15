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
           
            base.Seed(context);

        }
    }
}