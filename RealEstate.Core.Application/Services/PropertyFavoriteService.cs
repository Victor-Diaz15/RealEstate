using AutoMapper;
using RealEstate.Core.Application.Interfaces.Repositories;
using RealEstate.Core.Application.Interfaces.Services;
using RealEstate.Core.Application.ViewModels.PropertyFavorite;
using RealEstate.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Application.Services
{
    public class PropertyFavoriteService : 
        GenericService<PropertyFavoriteSaveViewModel, PropertyFavoriteViewModel, PropertyFavorite>, IPropertyFavoriteService
    {
        private readonly IPropertyFavoriteRepository _propertyFavoriteRepo;
        private readonly IMapper _mapper;
        public PropertyFavoriteService(IPropertyFavoriteRepository propertyFavoriteRepo, IMapper mapper) : base(propertyFavoriteRepo, mapper)
        {
            _propertyFavoriteRepo = propertyFavoriteRepo;
            _mapper = mapper;
        }


    }
}
