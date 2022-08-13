using Microsoft.AspNetCore.Identity;
using RealEstate.Core.Application.Enums;
using RealEstate.Infrastructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Infrastructure.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Agent.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Client.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Developer.ToString()));
        }
    }
}
