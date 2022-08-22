using AutoMapper;
using MediatR;
using RealEstate.Core.Application.Dtos.Improvements;
using RealEstate.Core.Application.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Core.Application.Features.Improvements.Queries.GetAllImprovement
{
    public class GetAllImprovementQuery : IRequest<IList<ImprovementDto>>
    {
        public int? id { get; set; }
    }

    public class GetAllImprovementQueryHandler : IRequestHandler<GetAllImprovementQuery, IList<ImprovementDto>>
    {
        private readonly IImprovementRepository _repo;
        private readonly IMapper _mapper;

        public GetAllImprovementQueryHandler(IImprovementRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IList<ImprovementDto>> Handle(GetAllImprovementQuery request, CancellationToken cancellationToken)
        {
            var improvementList =  await _repo.GetAllAsync();
            var improvementListDto = _mapper.Map<List<ImprovementDto>>(improvementList);
            return improvementListDto;
        }
    }
}
