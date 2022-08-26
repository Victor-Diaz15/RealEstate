using AutoMapper;
using RealEstate.Core.Application.Helpers;
using RealEstate.Core.Application.Interfaces.Repositories;
using RealEstate.Core.Application.Interfaces.Services;
using RealEstate.Core.Application.ViewModels.Filters;
using RealEstate.Core.Application.ViewModels.Improvement;
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

        public async Task<List<PropertyViewModel>> GetAllWithInclude()
        {
            List<Property> properties = await _repo.GetAllWithIncludeAsync(new List<string> { "Improvements", "PropertyType", "SaleType" });

            //List<Improvement> result = new();

            //foreach (var prop in properties)
            //{
            //    foreach (var imp in prop.Improvements)
            //    {
            //        result.Add(imp);
            //    }
            //}

            return properties.Select(prop => new PropertyViewModel()
            {
                Id = prop.Id,
                AgentName = prop.AgentName,
                Ubication = prop.Ubication,
                Code = prop.Code,
                IdAgent = prop.IdAgent,
                ParcelSize = prop.ParcelSize,
                PropertyType = prop.PropertyType.Name,
                //Improvements = (List<ImprovementViewModel>)prop.Improvements,
                SaleType = prop.SaleType.Name,
                Price = prop.Price,
                Description = prop.Description,
                RestRoomQty = prop.RestRoomQty,
                RoomQty = prop.RoomQty,
                PropertyImgUrl1 = prop.PropertyImgUrl1,
                PropertyImgUrl2 = prop.PropertyImgUrl2,
                PropertyImgUrl3 = prop.PropertyImgUrl3,
                PropertyImgUrl4 = prop.PropertyImgUrl4,
               // IsFavourite = prop.IsFavourite

            }).ToList();
        }

        public async Task<List<PropertyViewModel>> GetAllWithFilters(FiltersViewModel filters)
        {
            List<Property> properties = await _repo.GetAllWithIncludeAsync(new List<string> { "Improvements", "PropertyType", "SaleType" });

            var listVm = properties.Select(prop => new PropertyViewModel()
            {
                Id = prop.Id,
                AgentName = prop.AgentName,
                Ubication = prop.Ubication,
                Code = prop.Code,
                IdAgent = prop.IdAgent,
                ParcelSize = prop.ParcelSize,
                PropertyTypeId = prop.PropertyType.Id,
                PropertyType = prop.PropertyType.Name,
                SaleTypeId = prop.SaleType.Id,
                SaleType = prop.SaleType.Name,
                //Improvements = (List<ImprovementViewModel>)prop.Improvements,
                Price = prop.Price,
                Description = prop.Description,
                RestRoomQty = prop.RestRoomQty,
                RoomQty = prop.RoomQty,
                PropertyImgUrl1 = prop.PropertyImgUrl1,
                PropertyImgUrl2 = prop.PropertyImgUrl2,
                PropertyImgUrl3 = prop.PropertyImgUrl3,
                PropertyImgUrl4 = prop.PropertyImgUrl4,
                //IsFavourite = prop.IsFavourite

            }).ToList();

            if (!string.IsNullOrWhiteSpace(filters.code))
            {
                listVm = listVm.Where(prop => prop.Code == filters.code).ToList();
                return listVm;
            }

            if (filters.propertyTypeId != 0 && filters.roomQty == 0 && filters.restRoomQty == 0)
            {
                listVm = listVm
                    .Where(prop => prop.PropertyTypeId == filters.propertyTypeId)
                    .ToList();
                return listVm;
            }
            if (filters.propertyTypeId == 0 && filters.roomQty != 0 && filters.restRoomQty == 0)
            {
                listVm = listVm
                    .Where(prop => prop.RoomQty == filters.roomQty)
                    .ToList();
                return listVm;
            }
            if (filters.propertyTypeId == 0 && filters.roomQty == 0 && filters.restRoomQty != 0)
            {
                listVm = listVm
                    .Where(prop => prop.RestRoomQty == filters.restRoomQty)
                    .ToList();
                return listVm;
            }
            if (filters.propertyTypeId != 0 && filters.roomQty != 0 && filters.restRoomQty == 0)
            {
                listVm = listVm
                    .Where(prop => prop.PropertyTypeId == filters.propertyTypeId
                    && prop.RoomQty == filters.roomQty)
                    .ToList();
                return listVm;
            }
            if (filters.propertyTypeId != 0 && filters.roomQty == 0 && filters.restRoomQty != 0)
            {
                listVm = listVm
                    .Where(prop => prop.PropertyTypeId == filters.propertyTypeId
                    && prop.RestRoomQty == filters.restRoomQty)
                    .ToList();
                return listVm;
            }
            if (filters.propertyTypeId == 0 && filters.roomQty != 0 && filters.restRoomQty != 0)
            {
                listVm = listVm
                    .Where(prop => prop.RoomQty == filters.roomQty
                    && prop.RestRoomQty == filters.restRoomQty)
                    .ToList();
                return listVm;
            }
            if (filters.propertyTypeId != 0 && filters.roomQty != 0 && filters.restRoomQty != 0)
            {
                listVm = listVm
                    .Where(prop => prop.PropertyTypeId == filters.propertyTypeId 
                    && prop.RoomQty == filters.roomQty && prop.RestRoomQty == filters.restRoomQty)
                    .ToList();
                return listVm;
            }
            return listVm;
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
