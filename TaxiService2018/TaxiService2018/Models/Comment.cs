using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static TaxiService2018.Models.Enums;

namespace TaxiService2018.Models
{
    public class Comment
    {
        public Comment()
        {

        }

        public int Id { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public ApplicationUser Commenter { get; set; }

        [Required]
        public Ride Ride { get; set; }

        public Rating Rating { get; set; }

    }
}