using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace InternetBanking.Core.Application
{
    //Extension Methods - application of this design pattern Decorator
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection service)
        {
            service.AddAutoMapper(Assembly.GetExecutingAssembly());

            #region Services

            service.AddTransient<IUserService, UserService>();
            service.AddTransient<IProductService, ProductService>();
            service.AddTransient<ITypeAccountService, TypeAccountService>();
            service.AddTransient<IRecipientService, RecipientService>();
            service.AddTransient<IPaymentService, PaymentService>();

            #endregion
        }
    }
}
