using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Helpers;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.User;
using InternetBanking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private readonly IProductService _productSvc;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public HomeController(IUserService svc, RoleManager<IdentityRole> roleManager, IProductService productSvc, IHttpContextAccessor http)
        {
            _svc = svc;
            _roleManager = roleManager;
            _productSvc = productSvc;
            _httpContextAccessor = http;
        }

        public IActionResult Index()
        {
            var user = HttpContext.Session.Get<AuthenticationResponse>("user");
            var isAdmin = user.Roles.Contains(Roles.Admin.ToString());
            if (isAdmin)
            {
                return RedirectToAction("DashboardAdmin");
            }
            return RedirectToAction("DashboardClient");
        }

        [ServiceFilter(typeof(AdminAuthorize))]
        public IActionResult DashboardAdmin()
        {
            return View();
        }

        [ServiceFilter(typeof(ClientAuthorize))]
        public async Task<IActionResult> DashboardClient()
        {
            var user = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");

            ViewBag.SavingAccount = await _productSvc.GetAllProductByUser(user.Id, (int)AccountTypes.SavingAccount);
            ViewBag.CreditCard = await _productSvc.GetAllProductByUser(user.Id, (int)AccountTypes.CreditAccount);
            ViewBag.Loan = await _productSvc.GetAllProductByUser(user.Id, (int)AccountTypes.LoanAccount);

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
            ViewBag.Roles = await _roleManager.Roles.ToListAsync();

            return View(new UserSaveViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserSaveViewModel vm)
        {
            
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = await _roleManager.Roles.ToListAsync();
                return View(vm);
            }
            var origin = Request.Headers["origin"];
            RegisterResponse response = await _svc.RegisterAsync(vm, origin);
            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                ViewBag.Roles = await _roleManager.Roles.ToListAsync();
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
            ViewBag.Roles = await _roleManager.Roles.ToListAsync();
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
