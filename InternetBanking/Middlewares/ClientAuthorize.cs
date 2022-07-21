using InternetBanking.Controllers;
using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace WebApp.InternetBanking.Middlewares
{
    public class ClientAuthorize : IAsyncActionFilter
    {
        private readonly IHttpContextAccessor _httpContextAccesor;

        public ClientAuthorize(IHttpContextAccessor httpContextAccesor)
        {
            _httpContextAccesor = httpContextAccesor;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var user = _httpContextAccesor.HttpContext.Session.Get<AuthenticationResponse>("user");
            var isAdmin = user.Roles.Contains(Roles.Admin.ToString());
            if (isAdmin)
            {
                var controller = (HomeController)context.Controller;
                context.Result = controller.RedirectToAction("AccessDenied", "User");
            }
            else await next();
        }
    }
}
