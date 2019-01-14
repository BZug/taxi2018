using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using TaxiService2018.Models;

namespace TaxiService2018.Database
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext() : base(/*"name = defaultConnection"*/)
        {
            System.Data.Entity.Database.SetInitializer(new ApplicationDBInitializer());
        }

        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public virtual DbSet<Ride> Rides { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
    }
}