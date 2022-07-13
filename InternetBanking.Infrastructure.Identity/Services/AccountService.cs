using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Infrastructure.Identity.Services
{
    public class AccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _mailManager;

        public AccountService(
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, 
            IEmailService mailManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mailManager = mailManager;
        }
    }
}
