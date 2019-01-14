using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaxiService2018.ViewModels;

namespace TaxiService2018.Models
{
    public class Location
    {
        public Location()
        {

        }

        public int Id { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public string Street { get; set; }

        public int StreetNumber { get; set; }

        public string City { get; set; }

        public int PostalCode { get; set; }

        public override string ToString()
        {
            return $"{Street} {StreetNumber}, {City} {PostalCode}, {Longitude} - {Latitude}";
        }

        public Location(LocationEditForm f)
        {
            Latitude = f.Latitude;
            Longitude = f.Longitude;
            Street = f.Street;
            StreetNumber = f.StreetNumber;
            City = f.City;
            PostalCode = f.PostalCode;
        }

        public Location(RideCreateForm r)
        {
            Latitude = r.Latitude;
            Longitude = r.Longitude;
            Street = r.Street;
            StreetNumber = r.StreetNumber;
            City = r.City;
            PostalCode = r.PostalCode;
        }

        public Location(SuccesfulRideForm s)
        {
            Latitude = s.Latitude;
            Longitude = s.Longitude;
            Street = s.Street;
            StreetNumber = s.StreetNumber;
            City = s.City;
            PostalCode = s.PostalCode;
        }
    }
}