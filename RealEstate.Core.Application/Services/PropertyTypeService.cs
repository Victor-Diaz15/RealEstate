using AutoMapper;
using RealEstate.Core.Application.Interfaces.Repositories;
using RealEstate.Core.Application.Interfaces.Services;
using RealEstate.Core.Application.ViewModels.PropertyType;
using RealEstate.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Application.Services
{
    public class PropertyTypeService : 
        GenericService<PropertyTypeSaveViewModel, PropertyTypeViewModel, PropertyType>, IPropertyTypeService
    {
        private readonly IPropertyTypeRepository _propertyTypeRepo;
        private readonly IMapper _mapper;
        public PropertyTypeService(IPropertyTypeRepository propertyTypeRepo, IMapper mapper) : base(propertyTypeRepo, mapper)
        {
            _propertyTypeRepo = propertyTypeRepo;
            _mapper = mapper;
        }

        public async Task<List<PropertyTypeViewModel>> GetAllWithInclude()
        {
            List<PropertyType> propertyTypes = await _propertyTypeRepo.GetAllWithIncludeAsync(new List<string> { "Properties" });
            List<PropertyTypeViewModel> propertyTypeViewModels = new();

            propertyTypeViewModels = propertyTypes.Select(prop => new PropertyTypeViewModel()
            {
                Id = prop.Id,
                Name = prop.Name,
                Description = prop.Description,
                PropertiesQty = prop.Properties.Count()

            }).ToList();

            return propertyTypeViewModels;
        }

    }
}
