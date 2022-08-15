using AutoMapper;
using RealEstate.Core.Application.Helpers;
using RealEstate.Core.Application.Interfaces.Repositories;
using RealEstate.Core.Application.Interfaces.Services;
using RealEstate.Core.Application.ViewModels.Property;
using RealEstate.Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Core.Application.Services
{
    public class PropertyService : 
        GenericService<PropertySaveViewModel, PropertyViewModel, Property>,
        IPropertyService
    {
        private readonly PropertyCodeGenerator _codeGenerator = new();
        private readonly IPropertyRepository _repo;
        private readonly IMapper _mapper;
        public PropertyService(IPropertyRepository repo, IMapper mapper) : base(repo, mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        //Overrating the method add
        public async override Task<PropertySaveViewModel> AddAsync(PropertySaveViewModel vm)
        {
            vm.Code = _codeGenerator.PropertyCodeGen();
            if (await ExistCodeNumber(vm.Code))
            {
                var newCodeNumber = _codeGenerator.PropertyCodeGen();
                vm.Code = newCodeNumber;
            }
            return await base.AddAsync(vm);
        }

        private async Task<bool> ExistCodeNumber(string code)
        {
            List<Property> properties = await _repo.GetAllAsync();
            bool exist = properties.Any(e => e.Code == code);
            return exist;
        }
    }
}
