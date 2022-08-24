using RealEstate.Core.Application.ViewModels.Filters;
using RealEstate.Core.Application.ViewModels.Property;
using RealEstate.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstate.Core.Application.Interfaces.Services
{
    public interface IPropertyService : 
        IGenericService<PropertySaveViewModel, PropertyViewModel, Property>
    {
        Task<List<PropertyViewModel>> GetAllWithInclude();
        Task<List<PropertyViewModel>> GetAllWithFilters(FiltersViewModel filters);
    }
}
