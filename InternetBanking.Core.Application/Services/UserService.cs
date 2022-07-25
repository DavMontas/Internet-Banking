using AutoMapper;
using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.User;
using InternetBanking.Infrastructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.Services
{
    public class UserService : IUserService
    {

        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public UserService(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        public async Task<AuthenticationResponse> LoginAsync(LoginViewModel vm)
        {
            AuthenticationRequest request = _mapper.Map<AuthenticationRequest>(vm);
            AuthenticationResponse response = await _accountService.AuthenticationAsync(request);
            return response;
        }

        public async Task SignOutAsync()
        {
            await _accountService.SignOutAsync();

        }

        public async Task<RegisterResponse> RegisterAsync(UserSaveViewModel vm, string origin)
        {
            RegisterRequest request = _mapper.Map<RegisterRequest>(vm);
            return await _accountService.RegisterBasicUserAsync(request, origin);
        }

        public async Task<UpdateResponse> UpdateUserAsync(UserSaveViewModel vm, string id)
        {
            UpdateRequest req = _mapper.Map<UpdateRequest>(vm);
            return await _accountService.UpdateUserAsync(req, id);
        }

        public async Task<UpdateResponse> ActivedUserAsync(string id)
        {
            return await _accountService.ActivedUserAsync(id);
        }

        public async Task<List<AuthenticationResponse>> GetAllUsers()
        {
            List<AuthenticationResponse> users = await _accountService.GetAllUsers();
            return users;
        }

        public async Task<UserSaveViewModel> GetUserById(string id)
        {
            AuthenticationResponse user = await _accountService.GetUserById(id);
            UserSaveViewModel userMap = _mapper.Map<UserSaveViewModel>(user);
            return userMap;
        }

        public async Task<string> ConfirmEmailAsync(string userId, string token)
        {
            return await _accountService.ConfirmAccountAsync(userId, token);
        }

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPassViewModel vm, string origin)
        {
            ForgotPasswordRequest request = _mapper.Map<ForgotPasswordRequest>(vm);
            return await _accountService.ForgotPasswordAsync(request, origin);
        }

        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPassViewModel vm)
        {
            ResetPasswordRequest request = _mapper.Map<ResetPasswordRequest>(vm);
            return await _accountService.ResetPasswordAsync(request);
        }

        public async Task<List<UserViewModel>> GetAllVm()
        {
            var users = await this.GetAllUsers();
            var usersVm = _mapper.Map<List<UserViewModel>>(users);

            return usersVm;
        }
    }
}
