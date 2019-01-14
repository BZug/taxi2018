using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TaxiService2018.Models;
using static TaxiService2018.Models.Enums;

namespace TaxiService2018.ViewModels
{
    public class ActiveRideForm
    {
        public ActiveRideForm()
        {

        }

        public int Id { get; set; }

        [Display(Name = "Order Date Time")]
        public DateTime OrderDateTime { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Street { get; set; }

        [Display(Name ="Street No.")]
        public int StreetNumber { get; set; }

        public string City { get; set; }

        [Display(Name ="Postal Code")]
        public int PostalCode { get; set; }

        [EnumDataType(typeof(RideVehicleType))]
        [Display(Name = "Type of Vehicle")]
        public RideVehicleType VehicleType { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public ActiveRideForm(Ride r)
        {
            Id = r.Id;
            OrderDateTime = r.OrderDateTime;
            Latitude = r.Source.Longitude;
            Longitude = r.Source.Longitude;
            Street = r.Source.Street;
            StreetNumber = r.Source.StreetNumber;
            City = r.Source.City;
            PostalCode = r.Source.PostalCode;
            VehicleType = r.VehicleType;
            FirstName = r.Dispatcher.FirstName;
            LastName = r.Dispatcher.LastName;
        }

    }
}