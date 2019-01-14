using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaxiService2018.ViewModels
{
    public class SignInForm
    {
        [Required]
        [StringLength(120)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(120)]
        public string Password { get; set; }

    }
}