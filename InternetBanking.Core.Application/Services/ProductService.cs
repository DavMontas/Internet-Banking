using AutoMapper;
using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Helpers;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Products;
using InternetBanking.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.Services
{
    public class ProductService : GenericService<ProductSaveViewModel, ProductViewModel, Product>, IProductService
    {
        private readonly AccountNumberGenerator _numberGenerator = new();
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repo, IMapper mapper) : base(repo, mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task AddSavingAccountAsync(string idUser, double amount)
        {
            var products = await ExistSavingAccountByUser(idUser);

            if (products != false)
            {
                Product saveAccount = new();
                saveAccount.ClientId = idUser;
                saveAccount.Charge = amount;
                saveAccount.AccountNumber = _numberGenerator.NumberGenerator();
                saveAccount.TypeAccountId = (int)AccountTypes.SavingAccount;

                if (await ExistCodeNumber(saveAccount.AccountNumber))
                {
                   var newAccountNumber =  _numberGenerator.NumberGenerator();
                   saveAccount.AccountNumber = newAccountNumber;
                }

                await _repo.AddAsync(saveAccount);
            }
            else
            {
                Product saveAccount = new();
                saveAccount.AccountNumber = _numberGenerator.NumberGenerator();
                saveAccount.ClientId = idUser;
                saveAccount.Charge = amount;
                saveAccount.TypeAccountId = (int)AccountTypes.SavingAccount;
                saveAccount.IsPrincipal = true;

                if (await ExistCodeNumber(saveAccount.AccountNumber))
                {
                    var newAccountNumber = _numberGenerator.NumberGenerator();
                    saveAccount.AccountNumber = newAccountNumber;
                }

                await _repo.AddAsync(saveAccount);
            }
        }
        public async Task CreateAccountAsync(string idUser, double amount, int typeAccount)
        {
            if (typeAccount == (int)AccountTypes.CreditAccount)
            {
                Product saveAccount = new();
                saveAccount.ClientId = idUser;
                saveAccount.Charge = amount;
                saveAccount.AccountNumber = _numberGenerator.NumberGenerator();
                saveAccount.TypeAccountId = (int)AccountTypes.CreditAccount;

                if (await ExistCodeNumber(saveAccount.AccountNumber))
                {
                    var newAccountNumber = _numberGenerator.NumberGenerator();
                    saveAccount.AccountNumber = newAccountNumber;
                }

                await _repo.AddAsync(saveAccount);
            }
            else if (typeAccount == (int)AccountTypes.LoanAccount)
            {
                Product saveAccount = new();
                saveAccount.ClientId = idUser;
                saveAccount.Charge = amount;
                saveAccount.AccountNumber = _numberGenerator.NumberGenerator();
                saveAccount.TypeAccountId = (int)AccountTypes.LoanAccount;

                if (await ExistCodeNumber(saveAccount.AccountNumber))
                {
                    var newAccountNumber = _numberGenerator.NumberGenerator();
                    saveAccount.AccountNumber = newAccountNumber;
                }
                //sumandole lo del prestamo a la cuenta principal
                await AddAmountSavingAccount(idUser, amount);
                await _repo.AddAsync(saveAccount);
            }
        }
        public async Task AddAmountSavingAccount(string idUser, double amount)
        {
            List<Product> savingAccounts = await GetAllProductByUser(idUser, (int)AccountTypes.SavingAccount);
            Product sAPrincipal = savingAccounts.Where(sav => sav.IsPrincipal == true).SingleOrDefault();

            sAPrincipal.Charge += amount;

            await _repo.UpdateAsync(sAPrincipal, sAPrincipal.Id);
        }
        public async Task<List<ProductViewModel>> GetAllProductWithInclude(string idUser)
        {
            List<Product> products = await _repo.GetAllWithIncludeAsync(new List<string> { "TypeAccount" });
            products = products.Where(p => p.ClientId == idUser).ToList();
            List<ProductViewModel> productsVm = _mapper.Map<List<ProductViewModel>>(products);

            return productsVm;
        }
        public async Task<List<Product>> GetAllProductByUser(string idUser, int typeAccountId)
        {
            List<Product> products = await _repo.GetAllAsync();
            products = products.Where(p => p.ClientId == idUser && p.TypeAccountId == typeAccountId).ToList();

            return products;
        }
        public async Task<ProductViewModel> GetProductByNumberAccountForPayment(string numberAccount, double amountToPay = -1.0)
        {
            ProductViewModel response = new();
            response.HasError = false;

            List<Product> products = await _repo.GetAllAsync();
            var product = products.Where(ac => ac.AccountNumber == numberAccount)
                    .SingleOrDefault();

            if (product != null)
            {
                response = _mapper.Map<ProductViewModel>(product);
                
                if (response.Charge > amountToPay)
                {
                    return response;
                }
                else
                {
                    response.HasError = true;
                    response.Error = "La cuenta no tiene el monto suficiente para realizar ese pago";
                }
            }
            else
            {
                response.HasError = true;
                response.Error = "No existe ese numero de cuenta en el sistema";
            }

            return response;
        }
        public async Task<ProductViewModel> DeleteProductAsync(int IdProduct)
        {
            Product product = await _repo.GetByIdAsync(IdProduct);

            ProductViewModel responseVm = _mapper.Map<ProductViewModel>(product);


            if (product.TypeAccountId == (int)AccountTypes.SavingAccount)
            {
                await AddAmountSavingAccount(product.ClientId, product.Charge);
            }
            if (product.TypeAccountId == (int)AccountTypes.CreditAccount)
            {
                if (product.Charge != 0)
                {
                    responseVm.HasError = true;
                    responseVm.Error = "No se puede eliminar esta cuenta de credito hasta que salde lo que debe.!";
                    return responseVm;
                }
            }
            if (product.TypeAccountId == (int)AccountTypes.LoanAccount)
            {
                if (product.Charge != 0)
                {
                    responseVm.HasError = true;
                    responseVm.Error = "No se puede eliminar este prestamo hasta que salde lo que debe.!";
                    return responseVm;
                }
            }

            await _repo.DeleteAsync(product);
            return responseVm;
        }
        public async Task<bool> ExistProduct(int IdProduct)
        {
            List<Product> products = await _repo.GetAllAsync();
            bool exist = products.Any(e => e.Id == IdProduct);
            return exist;
        }
        private async Task<bool> ExistSavingAccountByUser(string idUser)
        {
            var productList = await _repo.GetAllAsync();

            var listViewModels = productList.Where(e => e.ClientId == idUser && e.TypeAccountId == (int)AccountTypes.SavingAccount).Select(product => new ProductViewModel
            {
                Id = product.Id,

                ClientId = product.ClientId,
                TypeAccountId = product.TypeAccountId,
                Charge = product.Charge,

            }).ToList();

            if (idUser != null)
            {
                listViewModels = listViewModels.Where(product => product.ClientId == idUser).ToList();
            }
            if (listViewModels.Count() == 0)
            {
                return false;
            }

            return true;
        }
        public async Task<bool> ExistCodeNumber(string accountNumber)
        {
            List<Product> products = await _repo.GetAllAsync();
            bool exist =  products.Any(e => e.AccountNumber == accountNumber);
            return exist;
        }
    }
}
