using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRegistration.Constants
{
    public  class Authorization
    {
        public enum Roles
        {
            Administrator,
            Staff,
            HR,
            Student
        }
        public const string default_username = "admin";
        public const string default_email = "admin@school.com";
        public const string default_password = "Abc123*0";
        public const Roles default_role = Roles.Administrator;

        public const string HR_username = "hr";
        public const string HR_email = "hr@school.com";
        public const string Hr_password = "Abc@123*";
        public const Roles Hr_role = Roles.HR;
        public const string HR_FirstName = "Jhon";
        public const string HR_LastName = "Red";

        public const string staf_username = "staf";
        public const string staf_email = "staf@school.com";
        public const string staf_password = "Abc@123*";
        public const Roles staf_role = Roles.Staff;
        public const string staf_FirstName = "ahmad";
        public const string staf_LastName = "ahmad";

    }
}
