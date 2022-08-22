using AutoMapper;
using MediatR;
using RealEstate.Core.Application.Interfaces.Repositories;
using RealEstate.Core.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Core.Application.Features.Improvements.Commands.UpdateImprovement
{
    public class UpdateImprovementCommand : IRequest<ImprovementUpdateResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class UpdateImprovementCommandHandler : IRequestHandler<UpdateImprovementCommand, ImprovementUpdateResponse>
    {
        private readonly IMapper _mapper;
        private readonly IImprovementRepository _repo;
        public UpdateImprovementCommandHandler(IMapper mapper, IImprovementRepository repo)
        {
            _mapper = mapper;
            _repo = repo;   
        }

        public async Task<ImprovementUpdateResponse> Handle(UpdateImprovementCommand command, CancellationToken cancellationToken)
        {
            var improvement = await _repo.GetByIdAsync(command.Id);
            
            if (improvement == null) throw new Exception("Record Not Found");

            improvement = _mapper.Map<Improvement>(command);

            await _repo.UpdateAsync(improvement, improvement.Id);

            var improvementResponse = _mapper.Map<ImprovementUpdateResponse>(improvement);

            return improvementResponse;
        }
    }
}
