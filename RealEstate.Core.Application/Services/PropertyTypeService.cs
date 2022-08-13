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

    }
}
