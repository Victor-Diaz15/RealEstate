using AutoMapper;
using MediatR;
using RealEstate.Core.Application.Dtos.UserAccounts;
using RealEstate.Core.Application.Features.Improvements.Commands.UpdateImprovement;
using RealEstate.Core.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Core.Application.Features.ChangeStatus.Commands.ChangeStatus
{
    public class ChangeAgentStatusCommand : IRequest<ChangeAgentStatusResponse>
    {
        public string Id { get; set; }
        public bool Status { get; set; }
    }
    public class ChangeAgentStatusCommandHandler : IRequestHandler<ChangeAgentStatusCommand, ChangeAgentStatusResponse>
    {
        private readonly IAccountService _svc;
        private readonly IMapper _mapper;
        public ChangeAgentStatusCommandHandler(IAccountService svc, IMapper mapper)
        {
            _svc = svc;
            _mapper = mapper;
        }
        public async Task<ChangeAgentStatusResponse> Handle(ChangeAgentStatusCommand command, CancellationToken cancellationToken)
        {
            var agent = await _svc.GetUserById(command.Id);
            if (agent == null) throw new Exception("Record Not Found");

            var changeStatus = await _svc.ActivedAgentAsync(agent.Id, command.Status);
            var returnChangeStatus = _mapper.Map<ChangeAgentStatusResponse>(changeStatus);
            return returnChangeStatus;

        }
    }

}
