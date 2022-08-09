using RealEstate.Core.Application.Dtos.Account;
using RealEstate.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<List<UserViewModel>> GetAllVmAsync();
        Task<List<AuthenticationResponse>> GetAllUsersAsync();
        Task<UserSaveViewModel> GetUserByIdAsync(string id);
        Task<AuthenticationResponse> LoginAsync(LoginViewModel vm);
        Task<RegisterResponse> RegisterAsync(UserSaveViewModel vm, string origin);
        Task<UpdateResponse> UpdateUserAsync(UserSaveViewModel vm, string id);
        Task<UpdateResponse> ActivedUserAsync(string id);
        Task<string> ConfirmEmailAsync(string userId, string token);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel vm);
        //Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPassViewModel vm, string origin);
        Task SignOutAsync();

    }
}
