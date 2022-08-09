using AutoMapper;
using RealEstate.Core.Application.Dtos.Account;
using RealEstate.Core.Application.ViewModels.User;

namespace RealEstate.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region mappings

            #region user

            CreateMap<AuthenticationRequest, LoginViewModel>()
                //.ForMember(x => x.HasError, opt => opt.Ignore())
                //.ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<AuthenticationResponse, UserSaveViewModel>()
                .ReverseMap();

            CreateMap<AuthenticationResponse, UserViewModel>()
                .ReverseMap();

            CreateMap<RegisterRequest, UserSaveViewModel>()
                //.ForMember(x => x.HasError, opt => opt.Ignore())
                //.ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UpdateRequest, UserSaveViewModel>()
                //.ForMember(x => x.HasError, opt => opt.Ignore())
                //.ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ResetPasswordRequest, ResetPasswordViewModel>()
                //.ForMember(x => x.HasError, opt => opt.Ignore())
                //.ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            #endregion

            #endregion
        }
    }
}