﻿using RealEstate.Core.Application.Dtos.Account;
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

            #endregion

            #region primary keys

            mb.Entity<Improvement>()
                .HasKey(e => e.Id);

            #endregion

            #region relations

            //mb.Entity<TypeAccount>()
            //    .HasMany<Product>(t => t.Products)
            //    .WithOne(p => p.TypeAccount)
            //    .HasForeignKey(p => p.TypeAccountId)
            //    .OnDelete(DeleteBehavior.Cascade);


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

            #endregion
        }
    }
}