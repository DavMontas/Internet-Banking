using InternetBanking.Core.Application.ViewModels.Recipient;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface IRecipientService : IGenericService<RecipientSaveViewModel, RecipientViewModel, Recipient>
    {
    }
}
