using AutoMapper;
using RealEstate.Core.Application.Interfaces.Repositories;
using RealEstate.Core.Application.Interfaces.Services;
using RealEstate.Core.Application.ViewModels.Property;
using RealEstate.Core.Domain.Entities;

namespace RealEstate.Core.Application.Services
{
    public class PropertyService : GenericService<PropertySaveViewModel, PropertyViewModel, Property>, IPropertyService
    {
        private readonly IPropertyRepository _repo;
        private readonly IMapper _mapper;
        public PropertyService(IPropertyRepository repo, IMapper mapper) : base(repo, mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
    }
}
