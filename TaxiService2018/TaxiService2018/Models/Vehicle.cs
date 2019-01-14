using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TaxiService2018.ViewModels;
using static TaxiService2018.Models.Enums;

namespace TaxiService2018.Models
{
    public class Vehicle
    {
        public Vehicle()
        {

        }

        public int Id { get; set; }

        [Required]
        public ApplicationUser Driver { get; set; }

        public int Year { get; set; }

        public string Registration { get; set; }

        public VehicleType Type { get; set; }

        public Vehicle(ApplicationUser driver, EditVehicleForm f)
        {
            Driver = driver;
            Year = f.Year;
            Registration = f.Registration;
            Type = f.Type;
        }

        public void Update(EditVehicleForm f)
        {
            Year = f.Year;
            Registration = f.Registration;
            Type = f.Type;
        }
    }
}