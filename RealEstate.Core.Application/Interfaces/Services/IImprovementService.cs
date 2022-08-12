using RealEstate.Core.Application.ViewModels.Improvement;
using RealEstate.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Application.Interfaces.Services
{
    public interface IImprovementService : 
        IGenericService<ImprovementSaveViewModel, ImprovementViewModel, Improvement>
    {
    }
}
