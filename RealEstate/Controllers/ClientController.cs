using Microsoft.AspNetCore.Mvc;
using RealEstate.Core.Application.Interfaces.Services;
using RealEstate.Core.Application.ViewModels.Filters;
using System.Threading.Tasks;

namespace WebApp.RealEstate.Controllers
{
    public class ClientController : Controller
    {
        private readonly IPropertyService _propertyService;
        private readonly IPropertyTypeService _propertyTypeService;

        public ClientController(IPropertyService propertyService, IPropertyTypeService propertyTypeService)
        {
            _propertyService = propertyService;
            _propertyTypeService = propertyTypeService;
        }

        public async Task<IActionResult> Index(FiltersViewModel filters)
        {
            ViewBag.PropertyTypes = await _propertyTypeService.GetAllVmAsync();
            return View(await _propertyService.GetAllWithFilters(filters));
        }

        public async Task<IActionResult> MyProperties(FiltersViewModel filters)
        {
            ViewBag.PropertyTypes = await _propertyTypeService.GetAllVmAsync();
            // Retornará el filtro de las propiedades favoritas
            return View(await _propertyService.GetAllWithFilters(filters));
        }
    }
}
