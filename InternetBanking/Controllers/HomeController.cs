using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Helpers;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.User;
using InternetBanking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApp.InternetBanking.Middlewares;

namespace InternetBanking.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUserService _svc;
        private readonly RoleManager<IdentityRole> _roleManager;

        public HomeController(IUserService svc, RoleManager<IdentityRole> roleManager)
        {
            _svc = svc;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var user = HttpContext.Session.Get<AuthenticationResponse>("user");
            var isAdmin = user.Roles.Contains(Roles.Admin.ToString());
            if (isAdmin)
            {
                return View("DashboardAdmin");
            }
            return View("DashboardClient");
        }

        [ServiceFilter(typeof(AdminAuthorize))]
        public IActionResult DashboardAdmin()
        {
            return View();
        }

        [ServiceFilter(typeof(ClientAuthorize))]
        public IActionResult DashboardClient()
        {
            return View();
        }

        public async Task<IActionResult> UserManagement()
        {
            ViewBag.Users = await _svc.GetAllUsers();
            return View();
        }

        [ServiceFilter(typeof(AdminAuthorize))]
        public  async Task<IActionResult> Register()
        {
            ViewBag.Roles = new SelectList(await _roleManager.Roles.ToListAsync());

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
            RegisterResponse response = await _svc.RegisterAsync(vm, origin);
            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);
            }

            return RedirectToRoute(new { controller = "Home", action = "UserManagement" });
        }

        public async Task<IActionResult> UpdateUser(string id)
        {
            var user = HttpContext.Session.Get<AuthenticationResponse>("user");

            if (id == user.Id)
            {
                return RedirectToRoute(new {controller ="Home", action = "UserManagement" });
            }
            UserSaveViewModel vm = await _svc.GetUserById(id);
            return View("Register", vm);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserSaveViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Register", vm);
            }
            await _svc.UpdateUserAsync(vm, vm.Id);
            return RedirectToRoute(new { controller = "Home", action = "UserManagement" });
        }

        public async Task<IActionResult> ActiveUser(string id)
        {
            return View("ActiveUser", await _svc.GetUserById(id));
        }
        [HttpPost]
        public async Task<IActionResult> ActiveUser(UserSaveViewModel vm)
        {
            await _svc.ActivedUserAsync(vm.Id);
            return RedirectToRoute(new { controller = "Home", action = "UserManagement" });
        }
    }
}
