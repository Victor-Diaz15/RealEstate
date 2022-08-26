using AutoMapper;
using MediatR;
using RealEstate.Core.Application.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;
using RealEstate.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstate.Core.Application.Features.SalesType.Commands.CreateSaleType
{
    public class CreateSaleTypeCommand : IRequest<int>
    {
        [SwaggerParameter(Description = "Sale Type name")]
        public string Name { get; set; }
        [SwaggerParameter(Description = "Sale Type name")]
        public string Description { get; set; }
    }

    public class CreateSaleTypeCommandHandler : IRequestHandler<CreateSaleTypeCommand, int>
    {
        private readonly ISaleTypeRepository _repo;
        private readonly IMapper _mapper;
        public CreateSaleTypeCommandHandler(ISaleTypeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateSaleTypeCommand command, CancellationToken cancellationToken)
        {
            var saleType = _mapper.Map<SaleType>(command);
            saleType = await _repo.AddAsync(saleType);
            return saleType.Id;
        }
    }
}
