using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaxiService2018.ViewModels;
using static TaxiService2018.Models.Enums;

namespace TaxiService2018.Models
{
    public class Ride
    {
        public Ride()
        {

        }

        public int Id { get; set; }

        public DateTime OrderDateTime { get; set; }

        public RideVehicleType VehicleType { get; set; }

        public Location Source { get; set; }

        public Location Destination { get; set; }

        public ApplicationUser Dispatcher { get; set; }

        public ApplicationUser Driver { get; set; }

        public int? Price { get; set; }

        public Comment Comment { get; set; }

        public RideStatus Status { get; set; }

        public Ride(Location source, ApplicationUser disp, RideVehicleType vehicle, ApplicationUser driver)
        {
            OrderDateTime = DateTime.Now;
            Source = source;
            VehicleType = vehicle;
            Dispatcher = disp;
            Driver = driver;
            Status = RideStatus.Formed;
        }

        public void Update(SuccesfulRideForm f)
        {
            Destination = new Location(f);
            Price = f.Price;
            Status = RideStatus.Successful;
        }
    }
}