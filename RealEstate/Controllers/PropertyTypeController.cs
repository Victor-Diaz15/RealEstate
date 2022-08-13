using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Core.Application.Interfaces.Services;
using RealEstate.Core.Application.ViewModels.PropertyType;
using System.Threading.Tasks;

namespace WebApp.RealEstate.Controllers
{
    public class PropertyTypeController : Controller
    {
        private readonly IPropertyTypeService _propertyTypeService;
        private readonly IMapper _mapper;
        public PropertyTypeController(IPropertyTypeService propertyTypeService, IMapper mapper)
        {
            _propertyTypeService = propertyTypeService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _propertyTypeService.GetAllVmAsync());
        }

        public IActionResult AddPropertyType()
        {
            return View(new PropertyTypeSaveViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddPropertyType(PropertyTypeSaveViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await _propertyTypeService.AddAsync(vm);
            return RedirectToRoute(new { controller = "PropertyType", action = "Index" });

        }

        public async Task<IActionResult> UpdatePropertyType(int id)
        {
            return View("AddPropertyType", await _propertyTypeService.GetByIdVmAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePropertyType(PropertyTypeSaveViewModel vm)
        {
            await _propertyTypeService.UpdateAsync(vm, vm.Id);
            return RedirectToRoute(new { controller = "PropertyType", action = "Index" });
        }

        public async Task<IActionResult> DeletePropertyType(int id)
        {
            return View(await _propertyTypeService.GetByIdVmAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePropertyType(PropertyTypeSaveViewModel vm)
        {
            await _propertyTypeService.DeleteAsync(vm.Id);
            return RedirectToRoute(new { controller = "PropertyType", action = "Index" });
        }

    }
}
