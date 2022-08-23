using RealEstate.Core.Application.Dtos.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Application.Dtos.UserAccounts
{
    public class AgentDtoActive
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<PropertyDto> Properties { get; set; }
        public string EMail { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }

    }
}
