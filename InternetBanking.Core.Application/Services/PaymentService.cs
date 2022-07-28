using AutoMapper;
using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Payment;
using InternetBanking.Core.Application.ViewModels.Products;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infrastructure.Identity.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.Services
{
    public class PaymentService : GenericService<SavePaymentViewModel, PaymentViewModel, Payment>, IPaymentService
    {
        private readonly IPaymentRepository _repo;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public PaymentService(IPaymentRepository repo, IProductService productService, IMapper mapper): base(repo, mapper)
        {
            _repo = repo;
            _productService = productService;
            _mapper = mapper;
        }

        public async Task Payment(SavePaymentViewModel vm)
        {
            try
            {
                ProductViewModel accountToPay = await
                _productService.GetProductByNumberAccountForPayment
                (vm.PaymentAccount, vm.AmountToPay);

                ProductViewModel accountDestination = await
                    _productService.GetProductByNumberAccountForPayment
                    (vm.PaymentDestinationAccount);

                if (accountToPay != null && accountDestination != null)
                {
                    if (accountDestination.TypeAccountId == (int)AccountTypes.SavingAccount)
                    {
                        var disCharge = accountToPay.Charge - vm.AmountToPay;
                        accountToPay.Charge = disCharge;
                        accountToPay.Discharge += vm.AmountToPay;
                        ProductSaveViewModel acToPayUpdated = _mapper.Map<ProductSaveViewModel>(accountToPay);
                        await _productService.Update(acToPayUpdated, acToPayUpdated.Id);

                        accountDestination.Charge += vm.AmountToPay;
                        ProductSaveViewModel acDsUpdated = _mapper.Map<ProductSaveViewModel>(accountDestination);
                        await _productService.Update(acDsUpdated, acDsUpdated.Id);
                    }
                    if 
                        (accountDestination.TypeAccountId == (int)AccountTypes.CreditAccount ||
                        accountDestination.TypeAccountId == (int)AccountTypes.LoanAccount)
                    {
                        var disCharge = accountToPay.Charge - vm.AmountToPay;
                        accountToPay.Charge = disCharge;
                        accountToPay.Discharge += vm.AmountToPay;
                        ProductSaveViewModel acToPayUpdated = _mapper.Map<ProductSaveViewModel>(accountToPay);
                        await _productService.Update(acToPayUpdated, acToPayUpdated.Id);

                        accountDestination.Charge -= vm.AmountToPay;
                        accountDestination.Discharge = vm.AmountToPay;
                        ProductSaveViewModel acDsUpdated = _mapper.Map<ProductSaveViewModel>(accountDestination);
                        await _productService.Update(acDsUpdated, acDsUpdated.Id);
                    }

                    Payment payment = _mapper.Map<Payment>(vm);
                    await _repo.AddAsync(payment);
                    
                }
            }
            catch (System.Exception)
            {

                throw;
            }
            
        }

    }
}
