using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TaxiService2018.Models;

namespace TaxiService2018.ViewModels
{
    public class LocationEditForm
    {
        public LocationEditForm()
        {

        }

        [Required]
        [Range(-90,90)]
        public double Latitude { get; set; }

        [Required]
        [Range(-180,180)]
        public double Longitude { get; set; }

        [Required]
        [StringLength(120)]
        public string Street { get; set; }

        [Required]
        [Range(1,999)]
        [Display(Name = "Street No.")]
        public int StreetNumber { get; set; }

        [Required]
        [StringLength(120)]
        public string City { get; set; }

        [Required]
        [Range(10000,99999)]
        [Display(Name = "Postal Code")]
        public int PostalCode { get; set; }

        public LocationEditForm(Location l)
        {
            Latitude = l.Latitude;
            Longitude = l.Longitude;
            Street = l.Street;
            StreetNumber = l.StreetNumber;
            City = l.City;
            PostalCode = l.PostalCode;
        }
    }
}