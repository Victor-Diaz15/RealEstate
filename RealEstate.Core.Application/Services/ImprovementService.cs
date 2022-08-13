using AutoMapper;
using RealEstate.Core.Application.Interfaces.Repositories;
using RealEstate.Core.Application.Interfaces.Services;
using RealEstate.Core.Application.ViewModels.Improvement;
using RealEstate.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Application.Services
{
    public class ImprovementService : 
        GenericService<ImprovementSaveViewModel, ImprovementViewModel, Improvement>, IImprovementService
    {
        private readonly IImprovementRepository _improvementRepo;
        private readonly IMapper _mapper;
        public ImprovementService(IImprovementRepository improvementRepo, IMapper mapper) : base(improvementRepo, mapper)
        {
            _improvementRepo = improvementRepo;
            _mapper = mapper;
        }

    }
}
