using RealEstate.Core.Application.ViewModels.PropertyType;
using RealEstate.Core.Domain.Entities;

namespace RealEstate.Core.Application.Interfaces.Services
{
    public interface IPropertyTypeService : 
        IGenericService<PropertyTypeSaveViewModel, PropertyTypeViewModel, PropertyType>
    {
    }
}
