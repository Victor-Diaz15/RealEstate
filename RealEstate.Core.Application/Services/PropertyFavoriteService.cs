using AutoMapper;
using RealEstate.Core.Application.Dtos.Account;
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
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;
        public PropertyFavoriteService(IPropertyFavoriteRepository propertyFavoriteRepo, IPropertyRepository propertyRepository, IMapper mapper) : base(propertyFavoriteRepo, mapper)
        {
            _propertyFavoriteRepo = propertyFavoriteRepo;
            _propertyRepository = propertyRepository;
            _mapper = mapper;
        }

        public async Task ChangeFavouritePropStatus(string clientId, int propId)
        {
            var favList = await _propertyFavoriteRepo.GetAllAsync();

            var favProp = favList.Where(x => x.PropertyId == propId && x.ClientId == clientId).FirstOrDefault();
            
            if (favProp != null) 
            {
                await _propertyFavoriteRepo.DeleteAsync(favProp);
                return;
            }

            PropertyFavorite item = new PropertyFavorite
            {
                PropertyId = propId,
                ClientId = clientId,
            };

            await _propertyFavoriteRepo.AddAsync(item);
        }
    }
}
