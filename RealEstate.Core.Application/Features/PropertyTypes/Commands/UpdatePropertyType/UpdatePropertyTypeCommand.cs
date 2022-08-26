using AutoMapper;
using MediatR;
using RealEstate.Core.Application.Interfaces.Repositories;
using RealEstate.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;
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
        [SwaggerParameter(Description = "Property Type id")]
        public int Id { get; set; }
        public string Name { get; set; }
        [SwaggerParameter(Description = "Property Type name")]
        public string Description { get; set; }
        [SwaggerParameter(Description = "Property Type description")]

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
