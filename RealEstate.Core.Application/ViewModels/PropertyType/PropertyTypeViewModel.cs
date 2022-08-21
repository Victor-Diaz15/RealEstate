using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Application.ViewModels.PropertyType
{
    public class PropertyTypeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int PropertiesQty { get; set; }
    }
}
