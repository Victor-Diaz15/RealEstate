using RealEstate.Core.Application.Dtos.Improvements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Application.Dtos.Properties
{
    public class PropertyDto
    {
        public int Id { get; set; }
        public string Code { get; set; }

        public string PropertyTypeId { get; set; }
        public string SaleTypeId { get; set; }
        public double Price { get; set; }
        public double ParcelSize { get; set; }
        public int RoomQty { get; set; }
        public int RestRoomQty { get; set; }
        public string Description { get; set; }
        public List<ImprovementDto> ImprovementList { get; set; }

        public string AgentName { get; set; }
        public string IdAgent { get; set; }
    }
}
