using AutoMapper;
using MediatR;
using RealEstate.Core.Application.Features.Properties.Commands.UpdateProperty;
using RealEstate.Core.Application.Interfaces.Repositories;
using RealEstate.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Core.Application.Features.PropertyTypes.Commands.UpdatePropertyType
{
    public class UpdatePropertyTypeCommand : IRequest<PropertyTypeUpdateResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class UpdatePropertyTypeCommandHandler : IRequestHandler<UpdatePropertyTypeCommand, PropertyTypeUpdateResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPropertyTypeRepository _repo;
        public UpdatePropertyTypeCommandHandler(IMapper mapper, IPropertyTypeRepository repo)
        {
            _mapper = mapper;
            _repo = repo;   
        }

        public async Task<PropertyTypeUpdateResponse> Handle(UpdatePropertyTypeCommand command, CancellationToken cancellationToken)
        {
            var propType = await _repo.GetByIdAsync(command.Id);
            
            if (propType == null) throw new Exception("Record Not Found");

            propType = _mapper.Map<PropertyType>(command);

            await _repo.UpdateAsync(propType, propType.Id);

            var propTypeResponse = _mapper.Map<PropertyTypeUpdateResponse>(propType);

            return propTypeResponse;
        }
    }
}
