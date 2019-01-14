using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static TaxiService2018.Models.Enums;

namespace TaxiService2018.ViewModels
{
    public class RideCreateForm
    {

        [Required]
        [Range(-90, 90)]
        public double Latitude { get; set; }

        [Required]
        [Range(-180, 180)]
        public double Longitude { get; set; }


        [Required]
        [StringLength(120)]
        public string Street { get; set; }

        [Required]
        [Range(1, 999)]
        [Display(Name = "Street Number")]
        public int StreetNumber { get; set; }

        [Required]
        [StringLength(120)]
        public string City { get; set; }

        [Required]
        [Range(10000, 99999)]
        [Display(Name = "Postal Code")]
        public int PostalCode { get; set; }

        [Required]
        [EnumDataType(typeof(RideVehicleType))]
        [Display(Name = "Vehicle Type")]
        public RideVehicleType VehicleType { get; set; }

        [Required]
        [Range(1, 120)]
        [Display(Name = "Driver")]
        public int DriverId { get; set; }

    }
}