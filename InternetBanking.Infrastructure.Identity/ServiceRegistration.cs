using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Infrastructure.Identity.Context;
using InternetBanking.Infrastructure.Identity.Entities;
using InternetBanking.Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace InternetBanking.Infrastructure.Identity
{
    //Main reason for creating this class is to follow the Single responsability
    public static class ServiceRegistration
    {
        // Extension methods | "Decorator"
        // This allows us to extend and create new functionallity following "Open-Closed Principle"
        public static void AddIdentityInfrastructure(this IServiceCollection service, IConfiguration config)
        {
            if (config.GetValue<bool>("UseInMemoryDatabase"))
            {
                service.AddDbContext<IdentityContext>(options => options.UseInMemoryDatabase("IdentityDb"));
            }
            else
            {
                service.AddDbContext<IdentityContext>(options =>
                    options.UseSqlServer(config.GetConnectionString("IdentityConnection"),
                    m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName)));
            }
            
            #region 'Identity'
            service.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            service.ConfigureApplicationCookie(opts => 
            {
                opts.LoginPath = "/User";
                opts.AccessDeniedPath = "/User/AccessDenied";
            });

            service.AddAuthentication();
            #endregion

            #region 'Services'
            service.AddTransient<IAccountService, AccountService>();
            #endregion
        }
    }
}
