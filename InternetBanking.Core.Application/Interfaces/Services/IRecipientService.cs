using InternetBanking.Core.Application.ViewModels.Products;
using InternetBanking.Core.Application.ViewModels.Recipient;
using InternetBanking.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface IRecipientService : IGenericService<RecipientSaveViewModel, RecipientViewModel, Recipient>
    {
        Task<RecipientSaveViewModel> GetByIdAsync(int id);
    }
}
