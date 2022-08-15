using MediatR;
using RealEstate.Core.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Core.Application.Features.Properties.Commands.DeletePropertyById
{
    public class DeletePropertyByIdCommand : IRequest<int>
    {
        public int Id { get; set; }

    }
    public class DeletePropertyByIdCommandHandler : IRequestHandler<DeletePropertyByIdCommand, int>
    {
        private readonly IPropertyRepository _repo;
        public DeletePropertyByIdCommandHandler(IPropertyRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> Handle(DeletePropertyByIdCommand command, CancellationToken cancellationToken)
        {
            var property = await _repo.GetByIdAsync(command.Id);
            if (property == null) throw new Exception("Property not found");

            await _repo.DeleteAsync(property);
            return command.Id;

        }
    }

}
