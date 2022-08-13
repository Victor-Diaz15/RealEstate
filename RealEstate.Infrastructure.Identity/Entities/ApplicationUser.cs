using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Infrastructure.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string CardId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsVerified { get; set; } = false;
        public int TypeUser { get; set; }
        public string ProfilePicture { get; set; }

    }
}