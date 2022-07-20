using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Helpers;
using InternetBanking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApp.InternetBanking.Middlewares;

namespace InternetBanking.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public HomeController()
        {

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

        public IActionResult DashboardClient()
        {
            return View();
        }
    }
}
