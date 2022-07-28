using AutoMapper;
using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Account;
using InternetBanking.Core.Application.ViewModels.Products;
using InternetBanking.Core.Application.ViewModels.Recipient;
using InternetBanking.Core.Application.ViewModels.User;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infrastructure.Identity.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.Services
{
    public class RecipientService : GenericService<RecipientSaveViewModel, RecipientViewModel, Recipient>, IRecipientService
    {

        private readonly IRecipientRepository _repo;
        private readonly IProductService _productSvc;
        private readonly IUserService _userSvc;
        private readonly IMapper _mapper;
        public RecipientService(IRecipientRepository repo, IMapper mapper, IProductService productSvc, IUserService userSvc) : base(repo, mapper)
        {
            _repo = repo;
            _mapper = mapper;
            _productSvc = productSvc;
            _userSvc = userSvc;
        }
        public override async Task<List<RecipientViewModel>> GetAllVm()
        {
            var recipientList = _mapper.Map<List<RecipientViewModel>>(await _repo.GetAllAsync());
            var productList = await _productSvc.GetAllVm();
            var userList = await _userSvc.GetAllVm();

            foreach (var recipient in recipientList)
            {
                recipient.Recipient = productList.Where(e => e.AccountNumber == recipient.RecipientCode).FirstOrDefault();

                recipient.User = userList.Where(e => e.Id == recipient.UserId).FirstOrDefault();
            }

            return recipientList;
        }

        public async Task<RecipientSaveViewModel> GetByIdAsync(int id)
        {
            Recipient recipient = await _repo.GetByIdAsync(id);
            RecipientSaveViewModel recipientVm = _mapper.Map<RecipientSaveViewModel>(recipient);
            return recipientVm;
        }
    }

}

