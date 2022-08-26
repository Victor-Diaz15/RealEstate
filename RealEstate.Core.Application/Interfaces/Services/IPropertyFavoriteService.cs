using RealEstate.Core.Application.Dtos.Account;
using RealEstate.Core.Application.ViewModels.PropertyFavorite;
using RealEstate.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Application.Interfaces.Services
{
    public interface IPropertyFavoriteService :
        IGenericService<PropertyFavoriteSaveViewModel, PropertyFavoriteViewModel, PropertyFavorite>
    {
        Task ChangeFavouritePropStatus(string clientId, int propId);
    }
}
