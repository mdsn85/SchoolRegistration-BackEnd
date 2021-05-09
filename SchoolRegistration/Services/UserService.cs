using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SchoolRegistration.Constants;
using SchoolRegistration.Contexts;
using SchoolRegistration.Models;
using SchoolRegistration.Settings;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegistration.Services
{
    public class UserService:IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly JWT _jwt;

        public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWT> jwt,
            ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
            _jwt = jwt.Value;
        }


        public async Task<List<ApplicationUser>> GetUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            return users;

        }


        public async Task<ApplicationUser> GetUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return user;
            }
            return user;
        }
        public async Task<ApplicationUser> AcceptUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if(user == null)
            {
                return user;
            }
            user.UserStatus = ApplicationUser.UserStatuses.Accepted;
            await _userManager.UpdateAsync(user);

            return user;
        }

        public async Task<ApplicationUser> RejectUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return user;
            }
            user.UserStatus = ApplicationUser.UserStatuses.Rejected;
            await _userManager.UpdateAsync(user);

            return user;
        }

        public async Task<string> RegisterAsync(RegisterModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.email,
                FirstName = model.firstName,
                LastName = model.lastName
            };
            var userWithSameEmail = await _userManager.FindByEmailAsync(model.email);
            if (userWithSameEmail == null)
            {
                var result = await _userManager.CreateAsync(user, model.password);
               
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Authorization.Roles.Student.ToString());
                }
                else 
                { 
                    return string.Join(",\r\n", result.Errors.Select(a => a.Description).ToArray());
                }
                return  $"User registered successfully";
            }
            else
            {
                return $"Email {user.Email } is already registered.";
            }
        }
        public async Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model)
        {
            var authenticationModel = new AuthenticationModel();
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = $"No Accounts Registered with {model.Email}.";
                
                return authenticationModel;
            }
            if (await _userManager.CheckPasswordAsync(user, model.Password))
            {
                if(user.UserStatus == ApplicationUser.UserStatuses.Rejected)
                {
                    authenticationModel.IsAuthenticated = false;
                    authenticationModel.Message = $"your registration is rejected by HR.";
                    return authenticationModel;
                }
                if (user.UserStatus == ApplicationUser.UserStatuses.Pending)
                {
                    authenticationModel.IsAuthenticated = false;
                    authenticationModel.Message = $"your registration is pendding, please contact HR.";
                    return authenticationModel;
                }
                authenticationModel.IsAuthenticated = true;

                authenticationModel.Email = user.Email;
                authenticationModel.UserName = user.UserName;
                //authenticationModel.Roles = (List<string>)await _userManager.GetRolesAsync(user);
                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                authenticationModel.Roles = rolesList.ToList();
                JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
                authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);


                return authenticationModel;
            }
            authenticationModel.IsAuthenticated = false;
            authenticationModel.Message = $"Incorrect Credentials for user {user.Email}.";
            return authenticationModel;
        }
        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }


    }
}
