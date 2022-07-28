using InternetBanking.Core.Application.ViewModels.Products;
using InternetBanking.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface  IProductService : IGenericService<ProductSaveViewModel, ProductViewModel, Product>
    {
        Task AddSavingAccountAsync(string idUser, double amount);

        Task CreateAccountAsync(string idUser, double amount, int typeAccount);

        Task AddAmountSavingAccount(string idUser, double amount);

        Task<ProductViewModel> DeleteProductAsync(int IdProduct);

        Task<List<ProductViewModel>> GetAllProductWithInclude(string idUser);

        //Task<List<ProductViewModel>> GetAllProductWithList(string idUser);

        Task<List<Product>> GetAllProductByUser(string idUser, int typeAccountId);

        Task<ProductViewModel> GetProductByNumberAccountForPayment(string numberAccount, double amountToPay = -1.0);

        Task<bool> ExistProduct(int IdProduct);

        Task<bool> ExistCodeNumber(string accountNumber);
    }
}
