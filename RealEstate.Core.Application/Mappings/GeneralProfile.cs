using AutoMapper;
using RealEstate.Core.Application.Dtos.Account;
using RealEstate.Core.Application.Dtos.Improvements;
using RealEstate.Core.Application.Dtos.PropertyTypeDtos;
using RealEstate.Core.Application.Dtos.SalesType;
using RealEstate.Core.Application.Features.Improvements.Commands.CreateImprovement;
using RealEstate.Core.Application.Features.Improvements.Commands.UpdateImprovement;
using RealEstate.Core.Application.Features.Properties.Commands.CreateProperty;
using RealEstate.Core.Application.Features.Properties.Commands.UpdateProperty;
using RealEstate.Core.Application.Features.Properties.Queries.GetAllProperties;
using RealEstate.Core.Application.Features.PropertyTypes.Commands.CreatePropertyType;
using RealEstate.Core.Application.Features.PropertyTypes.Commands.UpdatePropertyType;
using RealEstate.Core.Application.Features.SalesType.Commands.CreateSaleType;
using RealEstate.Core.Application.Features.SalesType.Commands.UpdateSaleType;
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

            CreateMap<ManagerSaveViewModel, UserSaveViewModel>()
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

            CreateMap<Improvement, ImprovementDto>()
                .ReverseMap()
                .ForMember(x => x.Properties, opt => opt.Ignore());

            CreateMap<UpdateImprovementCommand, ImprovementDto>()
               //.ForMember(x => x., opt => opt.Ignore())
               .ReverseMap();

            CreateMap<Improvement, CreateImprovementCommand>()
                .ReverseMap()
                .ForMember(x => x.Properties, opt => opt.Ignore());

            CreateMap<Improvement, UpdateImprovementCommand>() //
               .ReverseMap()
               .ForMember(x => x.Properties, opt => opt.Ignore());

            CreateMap<Improvement, ImprovementUpdateResponse>()
               .ReverseMap()
               .ForMember(x => x.Properties, opt => opt.Ignore());
            #endregion

            #region SaleType
            CreateMap<SaleType, SaleTypeViewModel>()
                .ReverseMap();

            CreateMap<SaleType, SaleTypeSaveViewModel>()
                .ReverseMap();

            CreateMap<SaleType, SaleTypeDto>()
              //.ForMember(x => x., opt => opt.Ignore())
              .ReverseMap();

            CreateMap<UpdateSaleTypeCommand, SaleTypeDto>()
                //.ForMember(x => x., opt => opt.Ignore())
                .ReverseMap();

            CreateMap<SaleType, CreateSaleTypeCommand>()
                .ReverseMap()
                .ForMember(x => x.Properties, opt => opt.Ignore());

            CreateMap<SaleType, UpdateSaleTypeCommand>() //
               .ReverseMap()
               .ForMember(x => x.Properties, opt => opt.Ignore());

            CreateMap<SaleType, SaleTypeUpdateResponse>()
               .ReverseMap()
               .ForMember(x => x.Properties, opt => opt.Ignore());

            #endregion

            #region PropertyType
            CreateMap<PropertyType, PropertyTypeViewModel>()
                .ReverseMap();

            CreateMap<PropertyType, PropertyTypeSaveViewModel>()
                .ReverseMap();

            CreateMap<PropertyType, PropTypeDto>()
                //.ForMember(x => x., opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UpdatePropertyTypeCommand, PropTypeDto>()
                //.ForMember(x => x., opt => opt.Ignore())
                .ReverseMap();

            CreateMap<PropertyType, CreatePropertyTypeCommand>()
                .ReverseMap()
                .ForMember(x => x.Properties, opt => opt.Ignore()); 

            CreateMap<PropertyType, UpdatePropertyTypeCommand>() //
               .ReverseMap()
               .ForMember(x => x.Properties, opt => opt.Ignore());

            CreateMap<PropertyType, PropertyTypeUpdateResponse>()
               .ReverseMap()
               .ForMember(x => x.Properties, opt => opt.Ignore());


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