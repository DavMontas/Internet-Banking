using AutoMapper;
using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Account;
using InternetBanking.Core.Application.ViewModels.Recipient;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infrastructure.Identity.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.Services
{
    public class RecipientService : GenericService<RecipientSaveViewModel, RecipientViewModel, Recipient>, IRecipientService
    {
        private readonly IRecipientRepository _repo;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public RecipientService(IRecipientRepository repo, IMapper mapper, IProductRepository productRepository) : base(repo, mapper)
        {
            _repo = repo;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        private async Task<List<Recipient>> GetAllRecipients()
        {
            var recipients = await _repo.GetAllAsync();


            return null;
        }

    }
}
