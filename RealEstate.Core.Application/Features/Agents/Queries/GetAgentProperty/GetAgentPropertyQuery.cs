using AutoMapper;
using MediatR;
using RealEstate.Core.Application.Dtos.Properties;
using RealEstate.Core.Application.Dtos.UserAccounts;
using RealEstate.Core.Application.Interfaces.Repositories;
using RealEstate.Core.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Core.Application.Features.Agents.Queries.GetAgentById
{
    public class GetAgentPropertyQuery : IRequest<List<PropertyDto>>
    {
        public string id { get; set; }
    }
    public class GetAgentPropertyQueryHanler : IRequestHandler<GetAgentPropertyQuery, List<PropertyDto>>
    {
        private readonly IAccountService _svc;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;
        public GetAgentPropertyQueryHanler(IAccountService svc, IMapper mapper, IPropertyRepository property)
        {
            _svc = svc;
            _mapper = mapper;
            _propertyRepository = property;
        }

        public async Task<List<PropertyDto>> Handle(GetAgentPropertyQuery request, CancellationToken cancellationToken)
        {
            var listProp = await GetAgentProperty(request.id);
            if (listProp == null || listProp.Count == 0) throw new Exception("record not found");
            return listProp;
        }

        public async Task<List<PropertyDto>> GetAgentProperty(string id)
        {
            var agent = await _svc.GetUserById(id);
            var listProperties = await _propertyRepository.GetAllAsync();
            listProperties = listProperties.Where(x => x.IdAgent == agent.Id).ToList();
           
            var agentListDto = _mapper.Map<List<PropertyDto>>(listProperties);

            return agentListDto;
        }
    }

}
