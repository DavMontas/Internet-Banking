using InternetBanking.Core.Application.ViewModels.Account;
using InternetBanking.Infrastructure.Identity.Entities;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface ITypeAccountService : IGenericService<TypeAccountSaveViewModel, TypeAccountViewModel, TypeAccount>
    {
    }
}
