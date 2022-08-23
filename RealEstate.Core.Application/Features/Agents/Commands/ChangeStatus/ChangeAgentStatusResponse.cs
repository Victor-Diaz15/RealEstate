using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Application.Features.ChangeStatus.Commands.ChangeStatus
{
    public class ChangeAgentStatusResponse
    {
        public string Id { get; set; }
        public bool Status { get; set; }

    }
}