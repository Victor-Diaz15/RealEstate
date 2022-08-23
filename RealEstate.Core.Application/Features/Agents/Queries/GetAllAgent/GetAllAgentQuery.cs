using AutoMapper;
using MediatR;
using RealEstate.Core.Application.Dtos.UserAccounts;
using RealEstate.Core.Application.Enums;
using RealEstate.Core.Application.Interfaces.Repositories;
using RealEstate.Core.Application.Interfaces.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Core.Application.Features.Agents.Queries.GetAllAgent
{
    public class GetAllAgentQuery : IRequest<IList<AgentDto>>
    {
        public int? id { get; set; }
    }

    public class GetAllAgentQueryHandler : IRequestHandler<GetAllAgentQuery, IList<AgentDto>>
    {
        private readonly IAccountService _svc;
        private readonly IMapper _mapper;

        public GetAllAgentQueryHandler(IAccountService svc, IMapper mapper)
        {
            _svc = svc;
            _mapper = mapper;
        }

        public async Task<IList<AgentDto>> Handle(GetAllAgentQuery request, CancellationToken cancellationToken)
        {
            var agentList =  await _svc.GetAllUsers();

            agentList = agentList.Where(x => x.TypeUser == (int)Roles.Agent).ToList();

            var agentListDto = _mapper.Map<List<AgentDto>>(agentList);
            return agentListDto;
        }
    }
}
