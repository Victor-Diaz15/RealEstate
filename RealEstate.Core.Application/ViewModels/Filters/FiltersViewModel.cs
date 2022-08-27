using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Application.ViewModels.Filters
{
    public class FiltersViewModel
    {
        #region filter Properties

        public string code { get; set; }
        public int propertyTypeId { get; set; }
        public int roomQty { get; set; }
        public int restRoomQty { get; set; }

        #endregion

        #region filter Agents

        public string name { get; set; }

        #endregion

        #region filter price
        public double MaxPrice { get; set; }
        public double MinPrice { get; set; }
        #endregion


    }
}
