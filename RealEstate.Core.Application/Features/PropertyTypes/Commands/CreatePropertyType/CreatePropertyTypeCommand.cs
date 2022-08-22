using AutoMapper;
using MediatR;
using RealEstate.Core.Application.Interfaces.Repositories;
using RealEstate.Core.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Core.Application.Features.PropertyTypes.Commands.CreatePropertyType
{
    public class CreatePropertyTypeCommand : IRequest<int>
    {

        public string Name { get; set; }
        public string Description { get; set; }

    }

    public class CreatePropertyTypeCommandHandler : IRequestHandler<CreatePropertyTypeCommand, int>
    {
        private readonly IPropertyTypeRepository _repo;
        private readonly IMapper _mapper;
        public CreatePropertyTypeCommandHandler(IPropertyTypeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreatePropertyTypeCommand command, CancellationToken cancellationToken)
        {
            var propType = _mapper.Map<PropertyType>(command);
            propType = await _repo.AddAsync(propType);
            return propType.Id;
        }
    }
}
