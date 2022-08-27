using AutoMapper;
using RealEstate.Core.Application.Dtos.Account;
using RealEstate.Core.Application.Dtos.Improvements;
using RealEstate.Core.Application.Dtos.Properties;
using RealEstate.Core.Application.Dtos.PropertyTypeDtos;
using RealEstate.Core.Application.Dtos.SalesType;
using RealEstate.Core.Application.Dtos.UserAccounts;
using RealEstate.Core.Application.Features.ChangeStatus.Commands.ChangeStatus;
using RealEstate.Core.Application.Features.Improvements.Commands.CreateImprovement;
using RealEstate.Core.Application.Features.Improvements.Commands.UpdateImprovement;
using RealEstate.Core.Application.Features.PropertyTypes.Commands.CreatePropertyType;
using RealEstate.Core.Application.Features.PropertyTypes.Commands.UpdatePropertyType;
using RealEstate.Core.Application.Features.SalesType.Commands.CreateSaleType;
using RealEstate.Core.Application.Features.SalesType.Commands.UpdateSaleType;
using RealEstate.Core.Application.ViewModels.Improvement;
using RealEstate.Core.Application.ViewModels.Property;
using RealEstate.Core.Application.ViewModels.PropertyFavorite;
using RealEstate.Core.Application.ViewModels.PropertyImprovement;
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

            CreateMap<UpdateAgentViewModel, UserSaveViewModel>()
                .ReverseMap();

            CreateMap<RegisterBasicRequest, UserSaveViewModel>()
                //.ForMember(x => x.HasError, opt => opt.Ignore())
                //.ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ManagerSaveViewModel, UserSaveViewModel>()
                //.ForMember(x => x.HasError, opt => opt.Ignore())
                //.ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            
            CreateMap<RegisterManagerRequest, RegisterManagerDevRequest>()
                .ReverseMap()
                .ForMember(x => x.IsVerified, opt => opt.Ignore());

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

            CreateMap<AgentDto, RegisterManagerRequest>()
                .ForMember(x => x.ConfirmPassword, opt => opt.Ignore())
                .ForMember(x => x.Password, opt => opt.Ignore())
                .ForMember(x => x.IsVerified, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<AgentDtoActive, AgentDto>()
                .ReverseMap()
                .ForMember(x => x.IsActive, opt => opt.Ignore());

            CreateMap<AgentDto, AuthenticationResponse>()
                .ForMember(x => x.RefreshToken, opt => opt.Ignore())
                .ForMember(x => x.JWToken, opt => opt.Ignore())
                .ForMember(x => x.TypeUser, opt => opt.Ignore())
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ForMember(x => x.Password, opt => opt.Ignore())
                .ForMember(x => x.ProfilePicture, opt => opt.Ignore())
                .ForMember(x => x.IsVerified, opt => opt.Ignore())
                .ForMember(x => x.Roles, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<AgentDtoActive, AuthenticationResponse>()
                .ForMember(x => x.RefreshToken, opt => opt.Ignore())
                .ForMember(x => x.JWToken, opt => opt.Ignore())
                .ForMember(x => x.TypeUser, opt => opt.Ignore())
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ForMember(x => x.Password, opt => opt.Ignore())
                .ForMember(x => x.ProfilePicture, opt => opt.Ignore())
                .ForMember(x => x.IsVerified, opt => opt.Ignore())
                .ForMember(x => x.Roles, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.Properties, opt => opt.Ignore());

            CreateMap<ChangeAgentStatusCommand, ChangeAgentStatusResponse>()
              .ReverseMap();

            CreateMap<UpdateResponse, ChangeAgentStatusResponse>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Status, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ForMember(x => x.HasError, opt => opt.Ignore());

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
              .ForMember(x => x.IsFavourite, opt => opt.Ignore())
              .ReverseMap()
              .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
              .ForMember(dest => dest.LastModified, opt => opt.Ignore())
              .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore());

            CreateMap<Property, PropertySaveViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.LastModified, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore());

            CreateMap<Property, PropertyDto>()
                .ForMember(x => x.ImprovementList, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.PropertyType, opt => opt.Ignore())
                .ForMember(x => x.Improvements, opt => opt.Ignore())
                .ForMember(x => x.PropertyImgUrl1, opt => opt.Ignore())
                .ForMember(x => x.PropertyImgUrl2, opt => opt.Ignore())
                .ForMember(x => x.PropertyImgUrl3, opt => opt.Ignore())
                .ForMember(x => x.PropertyImgUrl4, opt => opt.Ignore());

            #endregion

            #region propertyFavorite

            CreateMap<PropertyFavorite, PropertyFavoriteViewModel>()
              .ReverseMap();

            CreateMap<PropertyFavorite, PropertyFavoriteSaveViewModel>()
                .ReverseMap();

            #endregion

            #region PropertyImproment

            CreateMap<PropertyImprovement, PropertyImprovementViewModel>()
             .ReverseMap()
             .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<PropertyImprovement, PropertyImprovementSaveViewModel>()
             .ReverseMap()
             .ForMember(x => x.Id, opt => opt.Ignore());

            #endregion
            #endregion
        }
    }
}