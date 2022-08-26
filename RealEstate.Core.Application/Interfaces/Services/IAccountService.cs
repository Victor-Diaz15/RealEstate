using RealEstate.Core.Application.Dtos.Account;
using RealEstate.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticationAsync(AuthenticationRequest req);
        Task SignOutAsync();
        Task DeleteUserAsync(string id);
        Task<AuthenticationResponse> RegisterBasicUserAsync(RegisterBasicRequest req, string origin);
        Task<RegisterManagerResponse> RegisterAdminUserAsync(RegisterManagerRequest req);
        Task<RegisterManagerResponse> RegisterDevUserAsync(RegisterManagerDevRequest req);
        Task<UpdateResponse> UpdateUserAsync(UpdateRequest req, string id);
        Task<UpdateResponse> UpdateAgentAsync(UpdateAgentViewModel vm);
        Task<UpdateResponse> ActivedUserAsync(string id);
        Task<List<AuthenticationResponse>> GetAllUsers();
        Task<AuthenticationResponse> GetUserById(string id);
        Task<string> ConfirmAccountAsync(string userId, string token);
        //Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest req, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest req);
        Task<UpdateResponse> ActivedAgentAsync(string id, bool status);
    }
}
