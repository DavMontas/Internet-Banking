using AutoMapper;
using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.ViewModels.Account;
using InternetBanking.Core.Application.ViewModels.Payment;
using InternetBanking.Core.Application.ViewModels.Products;
using InternetBanking.Core.Application.ViewModels.Recipient;
using InternetBanking.Core.Application.ViewModels.User;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infrastructure.Identity.Entities;

namespace InternetBanking.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region mappings

            #region user

            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<AuthenticationResponse, UserSaveViewModel>()
                .ReverseMap()
                .ForMember(x => x.Roles, opt => opt.Ignore());

            CreateMap<AuthenticationResponse, UserViewModel>()
                .ReverseMap()
                .ForMember(x => x.Roles, opt => opt.Ignore());

            CreateMap<RegisterRequest, UserSaveViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UpdateRequest, UserSaveViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ForgotPasswordRequest, ForgotPassViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ResetPasswordRequest, ResetPassViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            #endregion

            #region product

            CreateMap<Product, ProductViewModel>()
                .ReverseMap();

            CreateMap<Product, ProductSaveViewModel>()
                .ReverseMap();

            CreateMap<ProductViewModel, ProductSaveViewModel>()
                .ReverseMap();

            #endregion

            #region typeaccount

            CreateMap<TypeAccount, TypeAccountViewModel>()
                .ReverseMap();

            CreateMap<TypeAccount, TypeAccountSaveViewModel>()
                .ReverseMap();

            #endregion

            #region recipient

            CreateMap<Recipient, RecipientViewModel>()
                .ReverseMap();

            CreateMap<Recipient, RecipientSaveViewModel>()
                .ReverseMap();

            #endregion

            #region payment

            CreateMap<Payment, PaymentViewModel>()
                .ReverseMap();

            CreateMap<Payment, SavePaymentViewModel>()
                .ReverseMap();

            #endregion

            #endregion
        }
    }
}
