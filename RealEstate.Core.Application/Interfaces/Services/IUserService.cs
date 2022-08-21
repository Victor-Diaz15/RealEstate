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
        Task<AuthenticationResponse> RegisterAsync(UserSaveViewModel vm, string origin);
        Task<RegisterManagerResponse> RegisterAdminAsync(ManagerSaveViewModel vm);
        Task<RegisterManagerResponse> RegisterDevAsync(ManagerSaveViewModel vm);
        Task<UpdateResponse> UpdateUserAsync(UserSaveViewModel vm, string id);
        Task<UpdateResponse> UpdateManagerUserAsync(ManagerSaveViewModel vm, string id);
        Task<UpdateResponse> ActivedUserAsync(string id);
        Task<string> ConfirmEmailAsync(string userId, string token);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel vm);
        Task SignOutAsync();

        Task DeleteUserAsync(string id);
    }
}
