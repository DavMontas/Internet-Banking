using InternetBanking.Core.Application.ViewModels.Products;
using InternetBanking.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    interface  IProductService : IGenericService<ProductSaveViewModel, ProductViewModel, Product>
    {
        //Task<List<ProductViewModel>> GetAllViewModelWithInclude();
    }
}
