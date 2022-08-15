using AutoMapper;
using MediatR;
using RealEstate.Core.Application.Interfaces.Repositories;
using RealEstate.Core.Application.ViewModels.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Core.Application.Features.Properties.Queries.GetPropertyById
{
    public class GetPropertyByCodeQuery : IRequest<PropertyViewModel>
    {
        public string Code { get; set; }
    }


    public class GetPropertyByCodeQueryHandler : IRequestHandler<GetPropertyByCodeQuery, PropertyViewModel>
    {
        private readonly IPropertyRepository _repo;
        private readonly IMapper _mapper;
        public GetPropertyByCodeQueryHandler(IPropertyRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        public async Task<PropertyViewModel> Handle(GetPropertyByCodeQuery request, CancellationToken cancellationToken)
        {
            var property = await GetByIdVM(request.Code);
            return property;
        }

        private async Task<PropertyViewModel> GetByIdVM(string code)
        {
            var property = await _repo.GetByCodeAsync(code);
            var result = _mapper.Map<PropertyViewModel>(property);
            return result;
        }
    } 

}
