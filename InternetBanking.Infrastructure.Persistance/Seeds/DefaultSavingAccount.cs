using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Infrastructure.Identity.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace InternetBanking.Infrastructure.Persistence.Seeds
{
    public static class DefaultSavingAccount
    {
        public static async Task SeedAsync(ITypeAccountRepository _repo)
        {

            TypeAccount account = new();
            account.NameAccount = "Cuenta de Ahorro";

            var accounts = await _repo.GetAllAsync();

            if (accounts.All(e => e.NameAccount != account.NameAccount))
            {
                var newAccount = await _repo.AddAsync(account);
            }
        }

    }
}
