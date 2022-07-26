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

        public async Task AddAmountSavingAccount(string idUser, double amount)
        {
            List<Product> savingAccounts = await GetAllProductByUser(idUser, (int)AccountTypes.SavingAccount);
            Product sAPrincipal = savingAccounts.Where(sav => sav.IsPrincipal == true).SingleOrDefault();

            sAPrincipal.Charge += amount;

            await _repo.UpdateAsync(sAPrincipal, sAPrincipal.Id);
        }
        public async Task<List<Product>> GetAllProductByUser(string idUser, int typeAccountId)
        {
            List<Product> products = await _repo.GetAllAsync();
            products = products.Where(p => p.ClientId == idUser && p.TypeAccountId == typeAccountId).ToList();

            return products;
        }
        public async Task<bool> ExistProduct(int IdProduct)
        {
            List<Product> products = await _repo.GetAllAsync();
            bool exist = products.Any(e => e.Id == IdProduct);
            return true;
        }
        private async Task<bool> ExistSavingAccountByUser(string idUser)
        {
            var productList = await _repo.GetAllAsync();

            var listViewModels = productList.Where(e => e.ClientId == idUser && e.TypeAccountId == 1).Select(product => new ProductViewModel
            {
                Id = product.Id,

                IdClient = product.ClientId,
                TypeAccountId = product.TypeAccountId,
                Charge = product.Charge,

            }).ToList();

            if (idUser != null)
            {
                listViewModels = listViewModels.Where(product => product.IdClient == idUser).ToList();
            }
            if (listViewModels.Count() == 0)
            {
                return false;
            }

            return true;
        }
        private async Task<bool> ExistCodeNumber(string accountNumber)
        {
            List<Product> products = await _repo.GetAllAsync();
            bool exist =  products.Any(e => e.AccountNumber == accountNumber);
            return exist;
        }
    }
}
