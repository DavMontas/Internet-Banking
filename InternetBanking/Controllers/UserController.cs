using Microsoft.AspNetCore.Mvc;

namespace WebApp.InternetBanking.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
