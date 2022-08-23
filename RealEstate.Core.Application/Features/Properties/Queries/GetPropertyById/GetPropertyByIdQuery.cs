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
    public class GetPropertyByIdQuery : IRequest<PropertyDto>
    {
        public int Id { get; set; } 
    }
    public class GetPropertyByIdQueryHanler: IRequestHandler<GetPropertyByIdQuery, PropertyDto>
    {
        private readonly IPropertyRepository _repo;
        private readonly IMapper _mapper;
        public GetPropertyByIdQueryHanler(IPropertyRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PropertyDto> Handle(GetPropertyByIdQuery request, CancellationToken cancellationToken)
        {
            var property = await GetByIdVm(request.Id);
            if (property == null) throw new Exception("record not found");
            return property;
        }

        public async Task<PropertyDto> GetByIdVm(int id)
        {
            var property = await _repo.GetByIdAsync(id);
            var propertyDto = _mapper.Map<PropertyDto>(property);

            return propertyDto;
        }
    }

}
