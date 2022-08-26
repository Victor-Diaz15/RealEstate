using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Domain.Entities
{
    public class PropertyImprovement
    {
        public int Id { get; set; }
        public int PropId { get; set; }
        public int ImprovementId { get; set; }
    }
}
