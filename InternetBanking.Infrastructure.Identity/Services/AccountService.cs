using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Dtos.Email;
using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Infrastructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;

        public AccountService(
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, 
            IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
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
            if (!user.EmailConfirmed)
            {
                res.HasError = true;
                res.Error = $"Account not confirmed for {req.Email}.";
                return res;
            }

            res.Id = user.Id;
            res.Email = user.Email;
            res.UserName = user.UserName;
            var listRoles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            res.Roles = listRoles.ToList();
            res.IsVerified = user.EmailConfirmed;

            return res;
        }

        //method for signout
        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        //method for create a new basic user
        public async Task<RegisterResponse> RegisterBasicUserAsync(RegisterRequest req, string origin)
        {
            RegisterResponse res = new();
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
                Email = req.Email,
                FirstName = req.FirstName,
                LastName = req.LastName,
                UserName = req.UserName
            };

            var result = await _userManager.CreateAsync(user, req.Password);
            if (!result.Succeeded)
            {
                res.HasError = true;
                res.Error = $"An error occurred when trying to register the user.";
                return res;
            }

            await _userManager.AddToRoleAsync(user, Roles.Basic.ToString());

            var verificationUri = await SendVerificationEmailUri(user, origin);
            await _emailService.SendAsync(new EmailRequest()
            { 
                To = user.Email,
                Body = $"Please confirm your account visiting this URL {verificationUri}",
                Subject = "Confirm Registration"
            });

            return res;
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

            return $"Account confirmed for {user.Email}. You can now use the app";
        }

        //method to send an email to the user for reset their password
        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest req, string origin)
        {
            ForgotPasswordResponse res = new(); 
            res.HasError = false;

            var user = await _userManager.FindByEmailAsync(req.Email);
            if (user == null)
            {
                res.HasError = true;
                res.Error = $"No user registered with {req.Email}.";
                return res;
            }

            var forgotPassUri = await SendForgotPasswordUri(user, origin);
            await _emailService.SendAsync(new EmailRequest()
            {
                To = user.Email,
                Body = $"Please reset your password account visiting this URL {forgotPassUri}",
                Subject = "Reset Password"
            });

            return res;
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

        //Method private to form the url for the ForgotPassword page
        private async Task<string> SendForgotPasswordUri(ApplicationUser user, string origin)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            var route = "User/ResetPassword";
            var uri = new Uri(string.Concat($"{origin}/", route));
            var forgotPassUri = QueryHelpers.AddQueryString(uri.ToString(), "token", token);

            return forgotPassUri;
        }
    }
}
