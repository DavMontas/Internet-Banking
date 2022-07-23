using AutoMapper;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Products;
using InternetBanking.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.Services
{
    public class ProductService : GenericService<ProductSaveViewModel, ProductViewModel, Product>, IProductService
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository repo, IMapper mapper): base(repo, mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


    }
}
