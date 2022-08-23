using AutoMapper;
using MediatR;
using RealEstate.Core.Application.Dtos.Properties;
using RealEstate.Core.Application.Interfaces.Repositories;
using RealEstate.Core.Application.ViewModels.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Core.Application.Features.Propertys.Queries.GetPropertyById
{
    public class GetPropertyByCodeQuery : IRequest<PropertyDto>
    {
        public string Code { get; set; } 
    }
    public class GetPropertyByCodeQueryHanler : IRequestHandler<GetPropertyByCodeQuery, PropertyDto>
    {
        private readonly IPropertyRepository _repo;
        private readonly IMapper _mapper;
        public GetPropertyByCodeQueryHanler(IPropertyRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PropertyDto> Handle(GetPropertyByCodeQuery request, CancellationToken cancellationToken)
        {
            var property = await GetByCodeVm(request.Code);
            if (property == null) throw new Exception("record not found");
            return property;


        }
        public async Task<PropertyDto> GetByCodeVm(string code)
        {
            var property = await _repo.GetByCodeAsync(code);
            var propertyDto = _mapper.Map<PropertyDto>(property);

            return propertyDto;
        }

        
    }

}
