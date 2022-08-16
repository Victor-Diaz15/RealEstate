using RealEstate.Core.Application.ViewModels.SaleType;
using RealEstate.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstate.Core.Application.Interfaces.Services
{
    public interface ISaleTypeService : 
        IGenericService<SaleTypeSaveViewModel, SaleTypeViewModel, SaleType>
    {
        Task<List<SaleTypeViewModel>> GetAllWithInclude();
    }
}
