using RealEstate.Core.Application.ViewModels.Property;
using RealEstate.Core.Domain.Entities;

namespace RealEstate.Core.Application.Interfaces.Services
{
    public interface IPropertyService : 
        IGenericService<PropertySaveViewModel, PropertyViewModel, Property>
    {
    }
}
