using InternetBanking.Core.Application.ViewModels.Account;
using InternetBanking.Infrastructure.Identity.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface ITypeAccountService : IGenericService<TypeAccountSaveViewModel, TypeAccountViewModel, TypeAccount>
    {



    }
}
