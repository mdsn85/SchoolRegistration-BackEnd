using Microsoft.AspNetCore.Identity;
using SchoolRegistration.Constants;
using SchoolRegistration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRegistration.Contexts
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedEssentialsAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Administrator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Staff.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.HR.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Student.ToString()));

            //Seed Default User
           
            var HRUser = new ApplicationUser { UserName = Authorization.HR_username, Email = Authorization.HR_email, EmailConfirmed = true, PhoneNumberConfirmed = true
                ,FirstName = Authorization.HR_FirstName, LastName = Authorization.HR_LastName, UserStatus = ApplicationUser.UserStatuses.Accepted};
            if (userManager.Users.All(u => u.Id != HRUser.Id))
            {
                try
                {
                    await userManager.CreateAsync(HRUser, Authorization.Hr_password);
                }
                catch (Exception ex) { Console.Write(ex.Message); }
                await userManager.AddToRoleAsync(HRUser, Authorization.Hr_role.ToString());
            }
            var defaultUser = new ApplicationUser { UserName = Authorization.default_username, Email = Authorization.default_email, EmailConfirmed = true, PhoneNumberConfirmed = true
                , UserStatus = ApplicationUser.UserStatuses.Accepted };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                await userManager.CreateAsync(defaultUser, Authorization.default_password);
                await userManager.AddToRoleAsync(defaultUser, Authorization.default_role.ToString());
            }

            var stafUser = new ApplicationUser
            {
                UserName = Authorization.staf_username,
                Email = Authorization.staf_email,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
    ,
                UserStatus = ApplicationUser.UserStatuses.Accepted
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                await userManager.CreateAsync(defaultUser, Authorization.staf_password);
                await userManager.AddToRoleAsync(defaultUser, Authorization.staf_role.ToString());
            }

        }
    }
}
