using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRegistration.Models
{
    public class RegisterModel
    {
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
    }
}
