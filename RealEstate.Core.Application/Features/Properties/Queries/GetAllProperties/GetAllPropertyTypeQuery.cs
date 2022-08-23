using AutoMapper;
using MediatR;
using RealEstate.Core.Application.Dtos.Improvements;
using RealEstate.Core.Application.Dtos.Properties;
using RealEstate.Core.Application.Interfaces.Repositories;
using RealEstate.Core.Application.ViewModels.Property;
using RealEstate.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Core.Application.Features.Propertys.Queries.GetAllProperty
{
    public class GetAllPropertyQuery : IRequest<IList<PropertyDto>>
    {
        public int? id { get; set; }
    }

    public class GetAllPropertyQueryHandler : IRequestHandler<GetAllPropertyQuery, IList<PropertyDto>>
    {
        private readonly IPropertyRepository _repo;
        private readonly IImprovementRepository _repoImrpvement;
        private readonly IPropertyTypeRepository _propTypeRepository;
        private readonly ISaleTypeRepository _saleTypeRepository;
        private readonly IMapper _mapper;


        public GetAllPropertyQueryHandler(IPropertyRepository repo, IMapper mapper, IPropertyTypeRepository propertyTypeRepository, ISaleTypeRepository saleTypeRepostory)
        {
            _repo = repo;
            _mapper = mapper;
            _propTypeRepository = propertyTypeRepository;
            _saleTypeRepository = saleTypeRepostory;
        }

        public async Task<IList<PropertyDto>> Handle(GetAllPropertyQuery request, CancellationToken cancellationToken)
        {
            var propertyList = await GetAllDtos();
            if (propertyList == null || propertyList.Count == 0) throw new Exception("No Record Found");
            return propertyList;
        }
        private async Task<List<PropertyDto>> GetAllDtos()
        {
            var propertyList = await _repo.GetAllWithIncludeAsync( new List<string> { "Improvements" });
            var propListDto = propertyList.Select(p => new PropertyDto
            {
                Id = p.Id,
                Code = p.Code,
                PropertyTypeId =  GetPropertyTypeName(p.PropertyTypeId),
                SaleTypeId = GetSaleTypeName(p.SaleTypeId),
                Price = p.Price,
                ParcelSize = p.ParcelSize,
                RoomQty = p.RoomQty,
                RestRoomQty = p.RestRoomQty,
                Description = p.Description,
                AgentName = p.AgentName,
                IdAgent = p.IdAgent
            }).ToList();

            return propListDto;
        }

        private string GetPropertyTypeName(int id)
        {
            var propType =  _propTypeRepository.GetById(id);
            return propType.Name;
        }
        private  string GetSaleTypeName(int id)
        {
            var saleType =  _saleTypeRepository.GetById(id);
            return saleType.Name;
        }
    }
}
