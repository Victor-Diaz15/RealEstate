using AutoMapper;
using MediatR;
using RealEstate.Core.Application.Dtos.SalesType;
using RealEstate.Core.Application.Interfaces.Repositories;
using RealEstate.Core.Application.ViewModels.SaleType;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Core.Application.Features.SalesType.Queries.GetAllSaleType
{
    public class GetAllSaleTypeQuery : IRequest<IList<SaleTypeDto>>
    {
        public int? id { get; set; }
    }

    public class GetAllSaleTypeQueryHandler : IRequestHandler<GetAllSaleTypeQuery, IList<SaleTypeDto>>
    {
        private readonly ISaleTypeRepository _repo;
        private readonly IMapper _mapper;

        public GetAllSaleTypeQueryHandler(ISaleTypeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IList<SaleTypeDto>> Handle(GetAllSaleTypeQuery request, CancellationToken cancellationToken)
        {
            var saleTypeList =  await _repo.GetAllAsync();
            var saleTypeListDto = _mapper.Map<List<SaleTypeDto>>(saleTypeList);
            return saleTypeListDto;
        }
    }
}
