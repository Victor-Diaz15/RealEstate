using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Application.ViewModels.Property
{
    public class PropertySaveViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        [Required(ErrorMessage = "Debe especificar la ubicacion de la propiedad")]
        public string Ubication { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe especificar el tipo de propiedad")]
        public int PropertyTypeId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe especificar el tipo de venta")]
        public int SaleTypeId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe especificar al menos una mejora")]
        public int ImprovementId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe especificar el precio de venta")]
        public double Price { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe especificar el tamaño de la propiedad en metros")]
        public double ParcelSize { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe especificar la cantidad de habitaciones")]
        public int RoomQty { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe especificar la cantidad de baños")]
        public int RestRoomQty { get; set; }

        [Required(ErrorMessage ="Debe especificar la descripcion de la propiedad")]
        public string Description { get; set; }

        public string AgentName { get; set; }
        public string IdAgent { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile PropertyImg1 { get; set; }
        public string PropertyImgUrl1 { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile PropertyImg2 { get; set; }
        public string PropertyImgUrl2 { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile PropertyImg3 { get; set; }
        public string PropertyImgUrl3 { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile PropertyImg4 { get; set; }
        public string PropertyImgUrl4 { get; set; }
    }
}
