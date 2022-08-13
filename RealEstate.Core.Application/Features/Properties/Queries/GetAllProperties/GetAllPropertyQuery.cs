using AutoMapper;
using MediatR;
using RealEstate.Core.Application.Interfaces.Repositories;
using RealEstate.Core.Application.ViewModels.Property;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Core.Application.Features.Properties.Queries.GetAllProperties
{

    public class GetAllPropertyQuery : IRequest<IList<PropertyViewModel>>
    {
        public int? Id { get; set; }
    }

    //Handler Class
    public class GetAllPropertyQueryHandler : IRequestHandler<GetAllPropertyQuery, IList<PropertyViewModel>> 
    {
        private readonly IPropertyRepository _repo;
        private readonly IMapper _mapper;
        public GetAllPropertyQueryHandler(IPropertyRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IList<PropertyViewModel>> Handle(GetAllPropertyQuery request, CancellationToken cancellationToken)
        {
            return null;
        }
    }
}
