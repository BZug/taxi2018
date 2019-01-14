using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TaxiService2018.Models;
using static TaxiService2018.Models.Enums;

namespace TaxiService2018.ViewModels
{
    public class EditVehicleForm
    {
        public EditVehicleForm()
        {

        }

        [Required]
        public int Id { get; set; }

        [Required]
        [Range(1900, 2019)]
        public int Year { get; set; }

        [Required]
        [StringLength(120)]
        public string Registration { get; set; }

        [Required]
        [EnumDataType(typeof(VehicleType))]
        [Display(Name = "Type of Vehicle")]
        public VehicleType Type { get; set; }

        public EditVehicleForm(Vehicle v)
        {
            Id = v.Id;
            Registration = v.Registration;
            Year = v.Year;
            Type = v.Type;
        }
    }
}