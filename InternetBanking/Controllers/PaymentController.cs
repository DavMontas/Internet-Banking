using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Helpers;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Payment;
using InternetBanking.Core.Application.ViewModels.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.InternetBanking.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentSvc;
        private readonly IProductService _productService;
        private readonly IUserService _userService;

        private readonly IHttpContextAccessor _httpContextAccessor;
        AuthenticationResponse userSesion;

        public PaymentController(IHttpContextAccessor httPContextAccesor , IPaymentService paymentService, IProductService productService, IUserService userService)
        {
            _httpContextAccessor = httPContextAccesor;
            userSesion = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _paymentSvc = paymentService;
            _productService = productService;
            _userService = userService;
        }
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ExpressPayment()
        {
            return View(new SavePaymentViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> ExpressPayment(SavePaymentViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            ProductViewModel accountToPay = await
                _productService.GetProductByNumberAccountForPayment
                (vm.PaymentAccount, vm.AmountToPay);

            if (accountToPay.HasError)
            {
                vm.HasError = accountToPay.HasError;
                vm.Error = accountToPay.Error;
                return View(vm);
            }
            var owner = await _userService.GetUserById(accountToPay.ClientId);
            vm.Owner = $"{owner.FirstName} {owner.LastName}";
            return RedirectToAction("ConfirmExpressPayment", vm);
        }

        public ActionResult ConfirmExpressPayment(SavePaymentViewModel vm)
        {
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmPayment(SavePaymentViewModel vm)
        {
            await _paymentSvc.ExpressPayment(vm);
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }   
    }
}
