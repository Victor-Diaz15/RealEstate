using RealEstate.Core.Application.ViewModels.PropertyType;
using RealEstate.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstate.Core.Application.Interfaces.Services
{
    public interface IPropertyTypeService : 
        IGenericService<PropertyTypeSaveViewModel, PropertyTypeViewModel, PropertyType>
    {
        Task<List<PropertyTypeViewModel>> GetAllWithInclude();
    }
}
