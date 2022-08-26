using AutoMapper;
using RealEstate.Core.Application.Helpers;
using RealEstate.Core.Application.Interfaces.Repositories;
using RealEstate.Core.Application.Interfaces.Services;
using RealEstate.Core.Application.ViewModels.Filters;
using RealEstate.Core.Application.ViewModels.Improvement;
using RealEstate.Core.Application.ViewModels.PropertyImprovement;
using RealEstate.Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Core.Application.Services
{
    public class PropertyImprovementService : 
        GenericService<PropertyImprovementSaveViewModel, PropertyImprovementViewModel, PropertyImprovement>,
        IPropertyImprovementService
    {
        private readonly IPropertyImprovementRepository _repo;
        private readonly IMapper _mapper;
        public PropertyImprovementService(IPropertyImprovementRepository repo, IMapper mapper) : base(repo, mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

    }
}
