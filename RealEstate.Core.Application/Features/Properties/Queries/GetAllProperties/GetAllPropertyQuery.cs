using MediatR;
using RealEstate.Core.Application.ViewModels.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Application.Features.Properties.Queries.GetAllProperties
{
    public class GetAllPropertyQuery : IRequest<IList<PropertyViewModel>>
    {
        public int? Id { get; set; }
    }
    public class GetAllPropertyQueryHandler : IRequestHandler<GetAllPropertyQuery, IList<PropertyViewModel>> 
    {
        private readonly IPropertyRepository repo;
    }
}
