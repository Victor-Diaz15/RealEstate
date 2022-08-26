using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RealEstate.Core.Domain.Commons;


namespace RealEstate.Core.Domain.Entities
{
    public class Property : AuditableBaseEntity
    {
        public string Code { get; set; }
        public string Ubication { get; set; }

        public int PropertyTypeId { get; set; }
        public PropertyType PropertyType { get; set; }

        public int SaleTypeId { get; set; }
        public SaleType SaleType { get; set; }

        public ICollection<Improvement> Improvements { get; set; }

        public double Price { get; set; }
        public double ParcelSize { get; set; }
        public int RoomQty { get; set; }
        public int RestRoomQty { get; set; }
        public string Description { get; set; }

        public string AgentName { get; set; }
        public string IdAgent { get; set; }

        public string PropertyImgUrl1 { get; set; }
        public string PropertyImgUrl2 { get; set; }
        public string PropertyImgUrl3 { get; set; }
        public string PropertyImgUrl4 { get; set; }


    }
}
