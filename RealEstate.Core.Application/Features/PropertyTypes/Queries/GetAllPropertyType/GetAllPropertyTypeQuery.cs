using AutoMapper;
using MediatR;
using RealEstate.Core.Application.Dtos.PropertyTypeDtos;
using RealEstate.Core.Application.Interfaces.Repositories;
using RealEstate.Core.Application.ViewModels.PropertyType;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Core.Application.Features.PropertyTypes.Queries.GetAllPropertyType
{
    public class GetAllPropertyTypeQuery : IRequest<IList<PropTypeDto>>
    {
        public int? id { get; set; }
    }

    public class GetAllPropertyTypeQueryHandler : IRequestHandler<GetAllPropertyTypeQuery, IList<PropTypeDto>>
    {
        private readonly IPropertyTypeRepository _repo;
        private readonly IMapper _mapper;

        public GetAllPropertyTypeQueryHandler(IPropertyTypeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IList<PropTypeDto>> Handle(GetAllPropertyTypeQuery request, CancellationToken cancellationToken)
        {
            var propertyTypeList =  await _repo.GetAllAsync();
            var propertyTypeListDto = _mapper.Map<List<PropTypeDto>>(propertyTypeList);
            return propertyTypeListDto;
        }
    }
}
