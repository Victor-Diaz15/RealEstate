using RealEstate.Core.Application.ViewModels.SaleType;
using RealEstate.Core.Domain.Entities;

namespace RealEstate.Core.Application.Interfaces.Services
{
    public interface ISaleTypeService : 
        IGenericService<SaleTypeSaveViewModel, SaleTypeViewModel, SaleType>
    {
    }
}
