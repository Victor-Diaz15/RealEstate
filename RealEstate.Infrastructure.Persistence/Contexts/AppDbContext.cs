using RealEstate.Core.Application.Dtos.Account;
using RealEstate.Core.Application.Helpers;
using RealEstate.Core.Application.ViewModels.User;
using RealEstate.Core.Domain.Commons;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RealEstate.Core.Domain.Entities;

namespace RealEstate.Infrastructure.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private UserViewModel user = new();

        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor http) : base(options)
        {
            _httpContextAccessor = http;
        }

        #region dbSets -->
        public DbSet<Improvement> Improvements { get; set; }
        public DbSet<SaleType> SaleTypes { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<Property> Properties { get; set; }

        #endregion

        public override Task<int> SaveChangesAsync(CancellationToken ct = new())
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                user = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            }

            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = user.UserName;

                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.ModifiedBy = user.UserName;
                        break;
                }
            }

            return base.SaveChangesAsync(ct);
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            #region tables

            mb.Entity<Improvement>()
                .ToTable("Improvements");

            mb.Entity<SaleType>()
                .ToTable("SaleTypes");

            mb.Entity<PropertyType>()
                .ToTable("PropertyTypes");

            mb.Entity<Property>()
                .ToTable("Properties");

            #endregion

            #region primary keys

            mb.Entity<Improvement>()
                .HasKey(e => e.Id);

            mb.Entity<SaleType>()
                .HasKey(e => e.Id);

            mb.Entity<PropertyType>()
                .HasKey(e => e.Id);

            mb.Entity<Property>()
                .HasKey(e => e.Id);

            #endregion

            #region relations

            mb.Entity<SaleType>()
                .HasMany<Property>(t => t.Properties)
                .WithOne(p => p.SaleType)
                .HasForeignKey(p => p.SaleTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            mb.Entity<PropertyType>()
                .HasMany<Property>(t => t.Properties)
                .WithOne(p => p.PropertyType)
                .HasForeignKey(p => p.PropertyTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            mb.Entity<Property>()
                .HasMany<Improvement>(p => p.Improvements)
                .WithMany(i => i.Properties);

            #endregion

            #region property configurations

            #region Improvement
            mb.Entity<Improvement>()
                .Property(p => p.Name)
                .IsRequired();

            mb.Entity<Improvement>()
                .Property(p => p.Description)
                .IsRequired();
            #endregion

            #region SaleType
            mb.Entity<SaleType>()
                .Property(p => p.Name)
                .IsRequired();

            mb.Entity<SaleType>()
                .Property(p => p.Description)
                .IsRequired();
            #endregion

            #region PropertyType
            mb.Entity<PropertyType>()
                .Property(p => p.Name)
                .IsRequired();

            mb.Entity<PropertyType>()
                .Property(p => p.Description)
                .IsRequired();
            #endregion

            #region Property
            mb.Entity<Property>()
                .Property(p => p.Code)
                .IsRequired();

            mb.Entity<Property>()
                .Property(p => p.PropertyTypeId)
                .IsRequired();

            mb.Entity<Property>()
                .Property(p => p.SaleTypeId)
                .IsRequired();

            mb.Entity<Property>()
                .Property(p => p.RoomQty)
                .IsRequired();

            mb.Entity<Property>()
                .Property(p => p.RestRoomQty)
                .IsRequired();

            mb.Entity<Property>()
                .Property(p => p.PropertyImgUrl1)
                .IsRequired();

            mb.Entity<Property>()
                .Property(p => p.Price)
                .IsRequired();

            mb.Entity<Property>()
                .Property(p => p.ParcelSize)
                .IsRequired();

            mb.Entity<Property>()
                .Property(p => p.IdAgent)
                .IsRequired();

            mb.Entity<Property>()
                .Property(p => p.AgentName)
                .IsRequired();

            mb.Entity<Property>()
                .Property(p => p.Description)
                .IsRequired();
            #endregion

            #endregion
        }
    }
}