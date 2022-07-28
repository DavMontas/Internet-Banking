using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Helpers;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Products;
using InternetBanking.Core.Application.ViewModels.Recipient;
using InternetBanking.Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.InternetBanking.Controllers
{
    public class RecipientController : Controller
    {
        private readonly IRecipientService _recipientSvc;
        private readonly IProductService _productSvc;
        private readonly IUserService _userService;

        private readonly IHttpContextAccessor _httpContextAccessor;
        AuthenticationResponse response;

        public RecipientController(IHttpContextAccessor httPContextAccesor , IRecipientService recipientService, IProductService productSvc, IUserService userService)
        {
            _httpContextAccessor = httPContextAccesor;
            response = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _recipientSvc = recipientService;
            _productSvc = productSvc;
            _userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            var recipients = await _recipientSvc.GetAllVm();
            recipients = recipients.Where(r => r.UserId == response.Id).ToList();

            ViewBag.Recipients = recipients;

            //List<UserSaveViewModel> owners = new();

            //foreach (var item in recipients)
            //{
            //    // To get owner of beneficiary account
            //    var product = await _productSvc.GetProductByNumberAccountForPayment(item.RecipientCode);
            //    var owner = await _userService.GetUserById(product.ClientId);
            //    owners.Add(owner);
            //}

            //ViewBag.Owners = owners;

            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _recipientSvc.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRecipient(RecipientSaveViewModel vm)
        {
            await _recipientSvc.Delete(vm.Id);
            return RedirectToRoute(new { controller = "Recipient", action = "Index" });
        }

        [HttpPost]
        public async Task<IActionResult> Index(RecipientSaveViewModel vm)
        {
            vm.UserId = response.Id;
            
            var recipient = await _recipientSvc.GetAllVm();
            var products = await _productSvc.GetAllVm();

            var SavingAccounts = products.Where(e => e.AccountNumber == vm.RecipientCode).SingleOrDefault();

            recipient = recipient.Where(e => e.UserId == response.Id).ToList();
            ViewBag.Recipients = recipient;


            //if (!ModelState.IsValid)
            //{
            //    return RedirectToRoute(new { controller = "Recipient", action = "Index" });
            //}

            if (!await _productSvc.ExistCodeNumber(vm.RecipientCode))
            {
                ModelState.AddModelError("", "El numero de cuenta ingresado es invalido");
                return View("Index", vm);
            }

            if (SavingAccounts.ClientId == AccountTypes.LoanAccount.ToString() || SavingAccounts.ClientId == AccountTypes.CreditAccount.ToString())
            {
                ModelState.AddModelError("", "El numero de cuenta ingresado no es de una cuenta de ahorro.");
                return View("Index", vm);
            }

            var anyRecipient = recipient.Any(e => e.RecipientCode == vm.RecipientCode);

            if (anyRecipient)
            {
                ModelState.AddModelError("","Ya existe un beneficiario con este numero de cuenta.");
                return View("Index", vm);
            }


            RecipientSaveViewModel recipientVm = await _recipientSvc.Add(vm);
            return RedirectToRoute(new { controller = "Recipient", action = "Index" }) ;
        }
    }
}
