using RealEstate.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Application.Interfaces.Repositories
{
    public interface IPropertyRepository: IGenericRepository<Property>
    {
        Task<Property> GetByCodeAsync(string Code);
    }
}
