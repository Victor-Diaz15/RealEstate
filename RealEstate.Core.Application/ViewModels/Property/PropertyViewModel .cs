using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Application.ViewModels.Property
{
    public class PropertyViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }

        public int PropertyTypeId { get; set; }
        public int SaleTypeId { get; set; }
        public double Price { get; set; }
        public double ParcelSize { get; set; }
        public int RoomQty { get; set; }
        public int RestRoomQty { get; set; }
        public string Description { get; set; }

        public string AgentName { get; set; }
        public int IdAgent { get; set; }

        public string PropertyImgUrl1 { get; set; }
        public string PropertyImgUrl2 { get; set; }
        public string PropertyImgUrl3 { get; set; }
        public string PropertyImgUrl4 { get; set; }
    }
}
