using RealEstate.Core.Application.Interfaces.Repositories;
using RealEstate.Core.Domain.Entities;
using RealEstate.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Infrastructure.Persistence.Repositories
{
    public class PropertyFavoriteRepository : GenericRepository<PropertyFavorite>, IPropertyFavoriteRepository
    {
        private readonly AppDbContext _db;
        public PropertyFavoriteRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

    }
}
