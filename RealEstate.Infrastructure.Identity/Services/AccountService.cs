using RealEstate.Core.Application.Dtos.Account;
using RealEstate.Core.Application.Dtos.Email;
using RealEstate.Core.Application.Enums;
using RealEstate.Core.Application.Interfaces.Services;
using RealEstate.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Core.Domain.Settings;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace RealEstate.Infrastructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly JWTSettings _jwtSettings;

        public AccountService(
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            IEmailService emailService, 
            IOptions<JWTSettings> jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _jwtSettings = jwtSettings.Value;
        }

        //Methods

        //Method for login
        public async Task<AuthenticationResponse> AuthenticationAsync(AuthenticationRequest req)
        {
            AuthenticationResponse res = new();

            var user = await _userManager.FindByEmailAsync(req.Email);
            if (user == null)
            {
                res.HasError = true;
                res.Error = $"No Accounts registered with {req.Email}.";
                return res;
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, req.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                res.HasError = true;
                res.Error = $"Invalid Credentials for {req.Email}.";
                return res;
            }
            if (!user.IsVerified)
            {
                res.HasError = true;
                res.Error = $"Cuenta inactiva asegurese de que su cuenta este activa.";
                return res;
            }

            JwtSecurityToken jwtSecurityToken =await GenerateJWToken(user);

            res.Id = user.Id;
            res.FirstName = user.FirstName;
            res.LastName = user.LastName;
            res.Email = user.Email;
            res.UserName = user.UserName;
            var listRoles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            res.Roles = listRoles.ToList();
            res.IsVerified = user.IsVerified;
            
            res.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            var refreshToken = GenerateRefreshToken();
            res.RefreshToken = refreshToken.Token;

            return res;
        }

        //method for signout
        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        //method for delete a user
        public async Task DeleteUserAsync(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);
        }

        //method for create a new basic user
        public async Task<AuthenticationResponse> RegisterBasicUserAsync(RegisterBasicRequest req, string origin)
        {
            AuthenticationResponse res = new();
            res.HasError = false;

            var userNameExist = await _userManager.FindByNameAsync(req.UserName);
            if (userNameExist != null)
            {
                res.HasError = true;
                res.Error = $"User '{req.UserName}' already exist.";
                return res;
            }

            var emailExist = await _userManager.FindByEmailAsync(req.Email);
            if (emailExist != null)
            {
                res.HasError = true;
                res.Error = $"Email '{req.Email}' already registered.";
                return res;
            }

            var user = new ApplicationUser
            {
                FirstName = req.FirstName,
                LastName = req.LastName,
                UserName = req.UserName,
                Email = req.Email,
                PhoneNumber = req.PhoneNumber,
                IsVerified = req.IsVerified,
                TypeUser = req.TypeUser,
                ProfilePicture = req.ProfilePicture
            };

            var result = await _userManager.CreateAsync(user, req.Password);
            if (!result.Succeeded)
            {
                res.HasError = true;
                res.Error = $"An error occurred when trying to register the user.";
                return res;
            }

            if (user.TypeUser == (int)Roles.Agent)
            {
                await _userManager.AddToRoleAsync(user, Roles.Agent.ToString());
            }
            else if (user.TypeUser == (int)Roles.Client)
            {
                await _userManager.AddToRoleAsync(user, Roles.Client.ToString());
                var verificacionUri = await SendVerificationEmailUri(user, origin);
                await _emailService.SendAsync(new EmailRequest()
                {
                    To = user.Email,
                    Body = $"Se ha creado su cuenta con exito, ahora solo debe acceder a este link para que active su usuario!!. {verificacionUri}",
                    Subject = "Bienevenido a Real Estate App"
                });
            }

            res.Id = user.Id;
            res.FirstName = user.FirstName;
            res.LastName = user.LastName;
            res.Email = user.Email;
            res.UserName = user.UserName;
            res.TypeUser = user.TypeUser;
            res.Password = req.Password;
            res.PhoneNumber = user.PhoneNumber;
            var listRoles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            res.Roles = listRoles.ToList();
            res.IsVerified = user.IsVerified;
            res.ProfilePicture = user.ProfilePicture;

            return res;
        }

        //method for create a new admin user
        public async Task<RegisterManagerResponse> RegisterAdminUserAsync(RegisterManagerRequest req)
        {
            RegisterManagerResponse res = new();
            res.HasError = false;

            var userNameExist = await _userManager.FindByNameAsync(req.UserName);
            if (userNameExist != null)
            {
                res.HasError = true;
                res.Error = $"User '{req.UserName}' already exist.";
                return res;
            }

            var emailExist = await _userManager.FindByEmailAsync(req.Email);
            if (emailExist != null)
            {
                res.HasError = true;
                res.Error = $"Email '{req.Email}' already registered.";
                return res;
            }

            List<ApplicationUser> userList = await _userManager.Users.ToListAsync();
            //Checking if vm.Cedula is already exists int the Db
            bool cedulaRepetion = userList.Any(user => user.CardId == req.CardId);
            if (cedulaRepetion)
            {
                res.HasError = true;
                res.Error = $"Este numero de cedula {req.CardId} ya existe.";
                return res;
            }

            var userAdmin = new ApplicationUser
            {
                CardId = req.CardId,
                FirstName = req.FirstName,
                LastName = req.LastName,
                UserName = req.UserName,
                Email = req.Email,
                IsVerified = req.IsVerified,
                TypeUser = (int)Roles.Admin
            };

            var result = await _userManager.CreateAsync(userAdmin, req.Password);
            if (!result.Succeeded)
            {
                res.HasError = true;
                res.Error = $"An error occurred when trying to register the user.";
                return res;
            }
            await _userManager.AddToRoleAsync(userAdmin, Roles.Admin.ToString());
            return res;
        }

        //method for create a new Dev user
        public async Task<RegisterManagerResponse> RegisterDevUserAsync(RegisterManagerRequest req)
        {
            RegisterManagerResponse res = new();
            res.HasError = false;

            var userNameExist = await _userManager.FindByNameAsync(req.UserName);
            if (userNameExist != null)
            {
                res.HasError = true;
                res.Error = $"User '{req.UserName}' already exist.";
                return res;
            }

            var emailExist = await _userManager.FindByEmailAsync(req.Email);
            if (emailExist != null)
            {
                res.HasError = true;
                res.Error = $"Email '{req.Email}' already registered.";
                return res;
            }

            var userDev = new ApplicationUser
            {
                CardId = req.CardId,
                FirstName = req.FirstName,
                LastName = req.LastName,
                UserName = req.UserName,
                Email = req.Email,
                IsVerified = req.IsVerified,
                TypeUser = (int)Roles.Developer
            };

            var result = await _userManager.CreateAsync(userDev, req.Password);
            if (!result.Succeeded)
            {
                res.HasError = true;
                res.Error = $"An error occurred when trying to register the user.";
                return res;
            }
            await _userManager.AddToRoleAsync(userDev, Roles.Developer.ToString());
            return res;
        }
        //method for update an user
        public async Task<UpdateResponse> UpdateUserAsync(UpdateRequest req, string id)
        {
            UpdateResponse res = new();
            res.HasError = false;
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.CardId = req.CardId;
                user.FirstName = req.FirstName;
                user.LastName = req.LastName;
                user.UserName = req.UserName;
                user.Email = req.Email;
                user.PhoneNumber = req.PhoneNumber;
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, req.Password);
                user.ProfilePicture = req.ProfilePicture;

                var userUpdated = await _userManager.UpdateAsync(user);
                if (!userUpdated.Succeeded)
                {
                    res.HasError = true;
                    res.Error = "Error when trying update the user";
                    return res;

                }
                return res;
            }
            else
            {
                res.HasError = true;
                res.Error = $"No accounts exists with this id: {id}";
                return res;
            }
        }

        //method to activate an user
        public async Task<UpdateResponse> ActivedUserAsync(string id)
        {
            UpdateResponse res = new();
            res.HasError = false;
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                if (user.IsVerified)
                {
                    user.IsVerified = false;
                    var userUpdated = await _userManager.UpdateAsync(user);
                    if (!userUpdated.Succeeded)
                    {
                        res.HasError = true;
                        res.Error = "Error when trying desactive the user";
                        return res;

                    }
                    return res;
                }
                else
                {
                    user.IsVerified = true;
                    var userUpdated = await _userManager.UpdateAsync(user);
                    if (!userUpdated.Succeeded)
                    {
                        res.HasError = true;
                        res.Error = "Error when trying active the user";
                        return res;

                    }
                    return res;
                }
            }
            else
            {
                res.HasError = true;
                res.Error = $"No accounts exists with this id: {id}";
                return res;
            }
        }
        
        //method to confirm the account of the user
        public async Task<string> ConfirmAccountAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return "No Accounts registered with this user";
            }
            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return $"An error occurred while confirming {user.Email}.";
            }
            var isActive = await ActivedUserAsync(user.Id);
            if (isActive.HasError)
            {
                return isActive.Error;
            }
            return $"Account confirmed for {user.Email}. You can now use the app";
        }

        //method to reset the user password
        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest req)
        {
            ResetPasswordResponse res = new();
            res.HasError = false;

            var user = await _userManager.FindByEmailAsync(req.Email);
            if (user == null)
            {
                res.HasError = true;
                res.Error = $"No user registered with {req.Email}.";
                return res;
            }

            req.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(req.Token));
            var result = await _userManager.ResetPasswordAsync(user, req.Token, req.Password);
            if (!result.Succeeded)
            {
                res.HasError = true;
                res.Error = $"An error occurred while reset the password.";
                return res;
            }

            return res;
        }

        //Method to get all users
        public async Task<List<AuthenticationResponse>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();

            List<AuthenticationResponse> res = new();

            foreach (var user in users)
            {
                var rol = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

                AuthenticationResponse user_res = new()
                {
                    Id = user.Id,
                    CardId = user.CardId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Roles = rol.ToList(),
                    IsVerified = user.IsVerified,
                    TypeUser = user.TypeUser,
                    ProfilePicture = user.ProfilePicture
                };

                res.Add(user_res);
            };

            return res;
        }

        //Method to get all users
        public async Task<AuthenticationResponse> GetUserById(string id)
        {
            AuthenticationResponse res = new();
            ApplicationUser user = new();
            user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                res.Id = user.Id;
                res.CardId = user.CardId;
                res.Email = user.Email;
                res.FirstName = user.FirstName;
                res.LastName = user.LastName;
                res.UserName = user.UserName;
                res.PhoneNumber = user.PhoneNumber;
                res.IsVerified = user.IsVerified;
                res.TypeUser = user.TypeUser;

                return res;
            }

            res.HasError = true;
            res.Error = $"Not user exists with this id: {id}";
            return res;
        }

        #region private methods

        private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid",user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires:DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        private  RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expire = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow
            };
        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);


            return BitConverter.ToString(randomBytes).Replace("-","");
        }


        //Method private to form the url for the emailVerificationUser page
        private async Task<string> SendVerificationEmailUri(ApplicationUser user, string origin)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            var route = "User/ConfirmEmail";
            var uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(uri.ToString(), "userId", user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "token", token);

            return verificationUri;
        }

        #endregion
    }
}