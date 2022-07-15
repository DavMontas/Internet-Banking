using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Helpers;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApp.InternetBanking.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _svc;
        public UserController(IUserService svc)
        {
            _svc = svc;
        }

        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            AuthenticationResponse response = await _svc.LoginAsync(vm);
            if (response != null && !response.HasError)
            {
                HttpContext.Session.Set<AuthenticationResponse>("user", response);
                return RedirectToRoute(new {controller = "Home", action = "Index"});
            }
            else
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);
            }
        }
        
        public async Task<IActionResult> LogOut()
        {
            await _svc.SignOutAsync();
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User" , action = "Index" });   
        }

        public IActionResult Register()
        {
            return View(new UserSaveViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserSaveViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var origin = Request.Headers["origin"];
            RegisterResponse response = await _svc.RegisterAsync(vm,origin);
            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);
            }

            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            string response = await _svc.ConfirmEmailAsync(userId, token);
            return View(response);
        }

        public IActionResult ForgotPassword()
        {
            return View(new ForgotPassViewModel());
        }

        [HttpPost]
        public async Task<IActionResult>ForgotPassword(ForgotPassViewModel vm)
        {
            if (ModelState.IsValid)
            {
                return View(vm);
            }

            var origin = Request.Headers["origin"];
            ForgotPasswordResponse response = await _svc.ForgotPasswordAsync(vm, origin);
            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);
            }
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        public IActionResult ResetPassword(string token)
        {
            return View(new ResetPassViewModel { Token = token });
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPassViewModel vm)
        {
            if (ModelState.IsValid)
            {
                return View(vm);
            }

            ResetPasswordResponse response = await _svc.ResetPasswordAsync(vm);
            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);
            }
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

    }
}
