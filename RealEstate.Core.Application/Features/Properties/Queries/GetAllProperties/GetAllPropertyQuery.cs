using AutoMapper;
using MediatR;
using RealEstate.Core.Application.Interfaces.Repositories;
using RealEstate.Core.Application.ViewModels.Property;
using System;
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

            var propList = await GetAllPropertyVM();
            if (propList == null || propList.Count == 0) throw new Exception("Record Not Found");
            return propList;
        }

        public async Task<List<PropertyViewModel>> GetAllPropertyVM()
        {
            var propList =await  _repo.GetAllAsync();
            var propVmList = _mapper.Map<List<PropertyViewModel>>(propList);
            return propVmList;
        }
    }
}
