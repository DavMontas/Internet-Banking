using InternetBanking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace InternetBanking.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public HomeController()
        {

        }

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
