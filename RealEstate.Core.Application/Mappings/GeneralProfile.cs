using AutoMapper;
using RealEstate.Core.Application.Dtos.Account;
using RealEstate.Core.Application.Features.Properties.Commands.CreateProperty;
using RealEstate.Core.Application.Features.Properties.Commands.UpdateProperty;
using RealEstate.Core.Application.Features.Properties.Queries.GetAllProperties;
using RealEstate.Core.Application.ViewModels.Improvement;
using RealEstate.Core.Application.ViewModels.Property;
using RealEstate.Core.Application.ViewModels.PropertyType;
using RealEstate.Core.Application.ViewModels.SaleType;
using RealEstate.Core.Application.ViewModels.User;
using RealEstate.Core.Domain.Entities;

namespace RealEstate.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region mappings

            #region User

            CreateMap<AuthenticationRequest, LoginViewModel>()
                //.ForMember(x => x.HasError, opt => opt.Ignore())
                //.ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<AuthenticationResponse, UserSaveViewModel>()
                .ReverseMap();

            CreateMap<AuthenticationResponse, UserViewModel>()
                .ReverseMap();

            CreateMap<RegisterBasicRequest, UserSaveViewModel>()
                //.ForMember(x => x.HasError, opt => opt.Ignore())
                //.ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<RegisterManagerRequest, ManagerSaveViewModel>()
                //.ForMember(x => x.HasError, opt => opt.Ignore())
                //.ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UpdateRequest, UserSaveViewModel>()
                //.ForMember(x => x.HasError, opt => opt.Ignore())
                //.ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UpdateRequest, ManagerSaveViewModel>()
                //.ForMember(x => x.HasError, opt => opt.Ignore())
                //.ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ResetPasswordRequest, ResetPasswordViewModel>()
                //.ForMember(x => x.HasError, opt => opt.Ignore())
                //.ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            #endregion

            #region Improvement
            CreateMap<Improvement, ImprovementViewModel>()
                .ReverseMap();

            CreateMap<Improvement, ImprovementSaveViewModel>()
                .ReverseMap();
            #endregion

            #region SaleType
            CreateMap<SaleType, SaleTypeViewModel>()
                .ReverseMap();

            CreateMap<SaleType, SaleTypeSaveViewModel>()
                .ReverseMap();
            #endregion

            #region PropertyType
            CreateMap<PropertyType, PropertyTypeViewModel>()
                .ReverseMap();

            CreateMap<PropertyType, PropertyTypeSaveViewModel>()
                .ReverseMap();
            #endregion

            #region property

            CreateMap<Property, PropertyViewModel>()
              .ReverseMap();

            CreateMap<PropertyType, PropertySaveViewModel>()
                .ReverseMap();

            CreateMap<GetAllPropertyQuery, GetAllPropertyParameter>()
                .ReverseMap();

            CreateMap<CreatePropertyCommand, Property>()
                .ForMember(x => x.PropertyType, opt => opt.Ignore())
                .ForMember(x => x.SaleType, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UpdatePropertyCommand, Property>()
                .ForMember(x => x.PropertyType, opt => opt.Ignore())
                .ForMember(x => x.SaleType, opt => opt.Ignore())
                .ReverseMap();

            #endregion

            #endregion
        }
    }
}