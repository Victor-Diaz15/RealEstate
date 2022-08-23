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
                PropertyTypeId = Convert.ToString(GetPropertyTypeName(p.Id)),
                SaleTypeId = Convert.ToString(GetSaleTypeName(p.Id)),
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

        private async Task<string> GetPropertyTypeName(int id)
        {
            var propType = await _propTypeRepository.GetByIdAsync(id);
            return propType.Name;
        }
        private async Task<string> GetSaleTypeName(int id)
        {
            var saleType = await _saleTypeRepository.GetByIdAsync(id);
            return saleType.Name;
        }
    }
}
