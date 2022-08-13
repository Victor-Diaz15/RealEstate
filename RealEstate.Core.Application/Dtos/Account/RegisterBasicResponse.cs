using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Application.Dtos.Account
{
    public class RegisterBasicResponse
    {
        public bool HasError { get; set; }
        public string Error { get; set; }

        public string Id { get; set; }
        public string ProfilePicture { get; set; }
    }
}
