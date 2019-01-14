using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace TaxiService2018.Database
{
    internal sealed class ApplicationDBConfig : DbMigrationsConfiguration<ApplicationDBContext>
    {
        public ApplicationDBConfig()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
    }
}