using MediatR;
using RealEstate.Core.Application.Interfaces.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Core.Application.Features.PropertyTypes.Commands.DeletePropertyType
{
    public class DeletePropertyTypeByIdCommand : IRequest<int>
    {
        [SwaggerParameter(Description = "Property Id")]
        public int Id { get; set; }
    }

    public class DeletePropertyTypeByIdCommandHandler :IRequestHandler<DeletePropertyTypeByIdCommand, int>
    {
        private readonly IPropertyTypeRepository _repo;
        public DeletePropertyTypeByIdCommandHandler(IPropertyTypeRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> Handle(DeletePropertyTypeByIdCommand command, CancellationToken cancellationToken)
        {
            var propType = await _repo.GetByIdAsync(command.Id);

            if (propType == null) throw new Exception("Record Not Found");

            await _repo.DeleteAsync(propType);

            return command.Id;

        }
    }
}
