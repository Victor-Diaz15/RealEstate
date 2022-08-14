﻿using AutoMapper;
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
    public class GetPropertyByIdQuery : IRequest<PropertyViewModel>
    {
        public int Id { get; set; }
    }


    public class GetPropertyByIdQueryHandler : IRequestHandler<GetPropertyByIdQuery, PropertyViewModel>
    {
        private readonly IPropertyRepository _repo;
        private readonly IMapper _mapper;
        public GetPropertyByIdQueryHandler(IPropertyRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        public async Task<PropertyViewModel> Handle(GetPropertyByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private async Task<PropertyViewModel> GetAllVM(int id)
        {
            var property = _repo.GetByIdAsync(id);
            var result = _mapper.Map<PropertyViewModel>(property);

            return result;
        }
    } 

}
