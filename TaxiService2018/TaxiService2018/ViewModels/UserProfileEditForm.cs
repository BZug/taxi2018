using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TaxiService2018.Models;
using static TaxiService2018.Models.Enums;

namespace TaxiService2018.ViewModels
{
    public class UserProfileEditForm
    {
        public UserProfileEditForm()
        {

        }

        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(120)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(120)]
        public string Password { get; set; }

        [Required]
        [StringLength(120)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(120)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }

        [Required]
        [StringLength(13, MinimumLength = 13)]
        [Display(Name = "Unique Master Citizen Number")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "Invalid Unique Master Citizen Number format.")]
        public string UMCN { get; set; }

        [Required]
        [Phone]
        [StringLength(120)]
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(120)]
        public string Email { get; set; }

        public UserProfileEditForm (ApplicationUser applicationUser)
        {
            Id = applicationUser.Id;
            Username = applicationUser.Username;
            Password = applicationUser.Password;
            FirstName = applicationUser.FirstName;
            LastName = applicationUser.LastName;
            Gender = applicationUser.Gender;
            UMCN = applicationUser.UMCN;
        }

    }
}