using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Domain.Entities
{
    public class Property
    {
        public int Id { get; set; }
        public int Name { get; set; }

        //Navigation Property
        public int SaleTypeId { get; set; }
        public SaleType SaleType { get; set; }

        //Navigation Property
        public int PropertyTypeId { get; set; }
        public PropertyType PropertyType { get; set; }
    }
}
