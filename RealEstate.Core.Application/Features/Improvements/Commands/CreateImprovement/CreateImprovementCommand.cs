using AutoMapper;
using MediatR;
using RealEstate.Core.Application.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;
using RealEstate.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstate.Core.Application.Features.Improvements.Commands.CreateImprovement
{
    public class CreateImprovementCommand : IRequest<int>
    {
        [SwaggerParameter(Description = "Improvement name")]
        public string Name { get; set; }
        [SwaggerParameter(Description = "Improvement Description")]
        public string Description { get; set; }
    }

    public class CreateImprovementCommandHandler : IRequestHandler<CreateImprovementCommand, int>
    {
        private readonly IImprovementRepository _repo;
        private readonly IMapper _mapper;
        public CreateImprovementCommandHandler(IImprovementRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateImprovementCommand command, CancellationToken cancellationToken)
        {
            var improvement = _mapper.Map<Improvement>(command);
            improvement = await _repo.AddAsync(improvement);
            return improvement.Id;
        }
    }
}
