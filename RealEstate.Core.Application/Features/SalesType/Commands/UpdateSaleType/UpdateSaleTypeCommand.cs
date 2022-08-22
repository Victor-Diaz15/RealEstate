using AutoMapper;
using MediatR;
using RealEstate.Core.Application.Interfaces.Repositories;
using RealEstate.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Core.Application.Features.SalesType.Commands.UpdateSaleType
{
    public class UpdateSaleTypeCommand : IRequest<SaleTypeUpdateResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class UpdateSaleTypeCommandHandler : IRequestHandler<UpdateSaleTypeCommand, SaleTypeUpdateResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISaleTypeRepository _repo;
        public UpdateSaleTypeCommandHandler(IMapper mapper, ISaleTypeRepository repo)
        {
            _mapper = mapper;
            _repo = repo;   
        }

        public async Task<SaleTypeUpdateResponse> Handle(UpdateSaleTypeCommand command, CancellationToken cancellationToken)
        {
            var saleType = await _repo.GetByIdAsync(command.Id);
            
            if (saleType == null) throw new Exception("Record Not Found");

            saleType = _mapper.Map<SaleType>(command);

            await _repo.UpdateAsync(saleType, saleType.Id);

            var saleTypeTypeResponse = _mapper.Map<SaleTypeUpdateResponse>(saleType);

            return saleTypeTypeResponse;
        }
    }
}
