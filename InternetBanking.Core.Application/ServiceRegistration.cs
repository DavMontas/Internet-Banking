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
            #endregion
        }
    }
}
