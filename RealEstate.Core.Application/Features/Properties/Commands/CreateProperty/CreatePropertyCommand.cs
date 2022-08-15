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

namespace RealEstate.Core.Application.Features.Properties.Commands.CreateProperty
{
    public class CreatePropertyCommand : IRequest<int>
    {
        public string Code { get; set; }

        public int PropertyTypeId { get; set; }

        public int SaleTypeId { get; set; }

        public double Price { get; set; }
        public double ParcelSize { get; set; }
        public int RoomQty { get; set; }
        public int RestRoomQty { get; set; }
        public string Description { get; set; }

        public string AgentName { get; set; }
        public int IdAgent { get; set; }
    }

    public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, int>
    {
        private readonly IPropertyRepository _repo;
        private readonly IMapper _mapper;
        public CreatePropertyCommandHandler(IPropertyRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreatePropertyCommand command, CancellationToken cancellationToken)
        {
            var property = _mapper.Map<Property>(command);
            property = await _repo.AddAsync(property);
            return property.Id;
        }
    }

}
