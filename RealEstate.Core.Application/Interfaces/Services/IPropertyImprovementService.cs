using RealEstate.Core.Application.ViewModels.PropertyImprovement;
using RealEstate.Core.Domain.Entities;

namespace RealEstate.Core.Application.Interfaces.Services
{
    public interface IPropertyImprovementService : 
        IGenericService<PropertyImprovementSaveViewModel, PropertyImprovementViewModel, PropertyImprovement>
    {
       
    }
}
