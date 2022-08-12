using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Application.ViewModels.SaleType
{
    public class SaleTypeSaveViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "File required")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [Required(ErrorMessage = "File required")]
        [DataType(DataType.Text)]
        public string Description { get; set; }

    }
}
