using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static TaxiService2018.Models.Enums;

namespace TaxiService2018.ViewModels
{
    public class FailedRideForm
    {
        public FailedRideForm()
        {

        }

        [Required]
        [Display(Name = "Ride")]
        public int IdR { get; set; }

        [Required]
        [StringLength(2000)]
        public string Description { get; set; }

        [Required]
        [EnumDataType(typeof(Rating))]
        public Rating Rating { get; set; }

        public FailedRideForm(int r)
        {
            IdR = r;
        }
    }
}