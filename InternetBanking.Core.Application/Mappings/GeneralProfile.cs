using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region mappings

            #region entityexamplemapping

            //CreateMap<User, UserVM>()
            //   .ReverseMap()
            //   .ForMember(dest => dest.Created, opt => opt.Ignore())
            //   .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            //   .ForMember(dest => dest.LastModified, opt => opt.Ignore())
            //   .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore());
            //CreateMap<User, UserSaveVM>()
            //    .ForMember(dest => dest.ConfirmPassword, opt => opt.Ignore())
            //    .ReverseMap()
            //    .ForMember(dest => dest.Created, opt => opt.Ignore())
            //    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            //    .ForMember(dest => dest.LastModified, opt => opt.Ignore())
            //    .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore());

            #endregion

            #endregion
        }
    }
}
