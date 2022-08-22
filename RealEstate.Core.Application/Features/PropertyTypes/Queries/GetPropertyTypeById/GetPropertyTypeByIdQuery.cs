using AutoMapper;
using MediatR;
using RealEstate.Core.Application.Dtos.PropertyTypeDtos;
using RealEstate.Core.Application.Interfaces.Repositories;
using RealEstate.Core.Application.ViewModels.PropertyType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Core.Application.Features.PropertyTypes.Queries.GetPropertyTypeById
{
    public class GetPropertyTypeByIdQuery : IRequest<PropTypeDto>
    {
        public int Id { get; set; } 
    }
    public class GetPropertyTypeByIdQueryHanler: IRequestHandler<GetPropertyTypeByIdQuery, PropTypeDto>
    {
        private readonly IPropertyTypeRepository _repo;
        private readonly IMapper _mapper;
        public GetPropertyTypeByIdQueryHanler(IPropertyTypeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PropTypeDto> Handle(GetPropertyTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var propType = await GetByIdVm(request.Id);
            if (propType == null) throw new Exception("record not found");
            return propType;
        }

        public async Task<PropTypeDto> GetByIdVm(int id)
        {
            var propType = await _repo.GetByIdAsync(id);
            var propTypeDto = _mapper.Map<PropTypeDto>(propType);

            return propTypeDto;
        }
    }

}
