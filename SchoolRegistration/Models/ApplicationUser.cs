using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRegistration.Models
{
    public class ApplicationUser: IdentityUser
    {
        public enum UserStatuses { Pending /*(defult)*/, Accepted, Rejected }
        [StringLength(30)]
        public string FirstName { get; set; }
        [StringLength(30)]
        public string LastName { get; set; }

        public UserStatuses? UserStatus { get; set; }

        public ApplicationUser()
        {
            UserStatus = UserStatuses.Pending;
        }
    }
}
