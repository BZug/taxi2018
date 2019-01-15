using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaxiService2018.ViewModels;
using static TaxiService2018.Models.Enums;

namespace TaxiService2018.Models
{
    public class ApplicationUser
    {
        public ApplicationUser()
        {
            Rides = new HashSet<Ride>();
        }

        public ApplicationUser(CreateDriverForm form)
        {
            Username = form.Username;
            Password = form.Password;
            FirstName = form.FirstName;
            LastName = form.LastName;
            Gender = form.Gender;
            UMCN = form.UMCN;
            Phone = form.Phone;
            Email = form.Email;
            Role = UserRole.Driver;
            IsDriverBusy = false;
            Location = new Location();
            Vehicle = new Vehicle();
            
        }

        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; } 

        public string UMCN { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public UserRole Role { get; set; }

        public bool? IsDriverBusy { get; set; }

        public virtual ICollection<Ride> Rides { get; set; }

        public virtual Location Location { get; set; }

        public virtual Vehicle Vehicle { get; set; }

        public void Update(UserProfileEditForm form)
        {
            Password = form.Password;
            FirstName = form.FirstName;
            LastName = form.LastName;
            Gender = form.Gender;
            UMCN = form.UMCN;
            Phone = form.Phone;
            Email = form.Email;
        }

        public void EditLocation(Location l)
        {
            Location = l;
        }

        public ApplicationUser(string txtData)
        {
            string[] data = txtData.Split(';');
            Username = data[0];
            Password = data[1];
            FirstName = data[2];
            LastName = data[3];
            if (data[4] == "m")
                Gender = Gender.Male;
            else
                Gender = Gender.Female;
            UMCN = data[5];
            Phone = data[6];
            Email = data[7];
            if(data[8] == "Dispatcher")
            {
                Role = UserRole.Dispatcher;
            }
            else
            {
                Role = UserRole.Driver;
            }
        }
    }
}