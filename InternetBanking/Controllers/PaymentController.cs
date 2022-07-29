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
        private readonly IRecipientService _recipientService;
        private readonly IProductService _productService;
        private readonly IUserService _userService;

        private readonly IHttpContextAccessor _httpContextAccessor;
        AuthenticationResponse userSesion;

        public PaymentController(IHttpContextAccessor httPContextAccesor , IPaymentService paymentService, IProductService productService, IUserService userService, IRecipientService recipientService)
        {
            _httpContextAccessor = httPContextAccesor;
            userSesion = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _paymentSvc = paymentService;
            _productService = productService;
            _userService = userService;
            _recipientService = recipientService;
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
            return RedirectToAction("ConfirmPayment", vm);
        }

        public ActionResult CreditPayment()
        {
            return View(new SavePaymentViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreditPayment(SavePaymentViewModel vm)
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
            return RedirectToAction("ConfirmPayment", vm);
        }

        public ActionResult LoamPayment()
        {
            return View(new SavePaymentViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> LoamPayment(SavePaymentViewModel vm)
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
            return RedirectToAction("ConfirmPayment", vm);
        }

        public async Task<ActionResult> BeneficiaryPayment()
        {
            var payments = await _recipientService.GetAllVm();
            ViewBag.Recipients = payments.Where(b => b.UserId == userSesion.Id);

            return View(new SavePaymentViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> BeneficiaryPayment(SavePaymentViewModel vm)
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
            return RedirectToAction("ConfirmPayment", vm);
        }

        public ActionResult ConfirmPayment(SavePaymentViewModel vm)
        {
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmPaymentPost(SavePaymentViewModel vm)
        {
            await _paymentSvc.Payment(vm);
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }   
    }
}
