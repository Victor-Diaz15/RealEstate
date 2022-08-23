using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Application.Dtos.UserAccounts
{
    public class AgentDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PropQty { get; set; }
        public string Mail { get; set; }
        public string PhoneNumber { get; set; }
    }
}
