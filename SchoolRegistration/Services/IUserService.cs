using SchoolRegistration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolRegistration.Services
{
    public interface IUserService
    {
        Task<string> RegisterAsync(RegisterModel model);
        Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model);
        
        Task<List<ApplicationUser>> GetUsersAsync();
        Task<ApplicationUser> GetUserAsync(string userId);
        Task<ApplicationUser> AcceptUser(string userId);
        Task<ApplicationUser> RejectUser(string userId);


    }
}
