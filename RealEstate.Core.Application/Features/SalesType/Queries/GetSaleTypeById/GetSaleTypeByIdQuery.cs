using AutoMapper;
using MediatR;
using RealEstate.Core.Application.Dtos.SalesType;
using RealEstate.Core.Application.Interfaces.Repositories;
using RealEstate.Core.Application.ViewModels.SaleType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Core.Application.Features.SaleTypes.Queries.GetSaleTypeById
{
    public class GetSaleTypeByIdQuery : IRequest<SaleTypeDto>
    {
        public int Id { get; set; } 
    }
    public class GetSaleTypeByIdQueryHandler: IRequestHandler<GetSaleTypeByIdQuery, SaleTypeDto>
    {
        private readonly ISaleTypeRepository _repo;
        private readonly IMapper _mapper;
        public GetSaleTypeByIdQueryHandler(ISaleTypeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<SaleTypeDto> Handle(GetSaleTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var saleType = await GetByIdVm(request.Id);
            if (saleType == null) throw new Exception("record not found");
            return saleType;
        }

        public async Task<SaleTypeDto> GetByIdVm(int id)
        {
            var saleType = await _repo.GetByIdAsync(id);
            var saleTypeDto = _mapper.Map<SaleTypeDto>(saleType);

            return saleTypeDto;
        }
    }

}
