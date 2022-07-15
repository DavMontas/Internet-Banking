using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.ViewModels.User;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<string> ConfirmEmailAsync(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPassViewModel vm, string origin);
        Task<RegisterResponse> RegisterAsync(UserSaveViewModel vm, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPassViewModel vm);
        Task SignOutAsync();
        Task<AuthenticationResponse> LoginAsync(LoginViewModel vm);
    }
}