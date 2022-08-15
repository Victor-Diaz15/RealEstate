using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Core.Application.Interfaces.Services;
using RealEstate.Core.Application.ViewModels.Property;
using System.Threading.Tasks;

namespace WebApp.RealEstate.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IPropertyService _propertyService;
        private readonly IMapper _mapper;
        public PropertyController(IPropertyService propertyService, IMapper mapper)
        {
            _propertyService = propertyService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _propertyService.GetAllVmAsync());
        }

        public IActionResult AddProperty()
        {
            return View(new PropertySaveViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddProperty(PropertySaveViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await _propertyService.AddAsync(vm);
            return RedirectToRoute(new { controller = "Property", action = "Index" });

        }

        public async Task<IActionResult> UpdateProperty(int id)
        {
            return View("AddProperty", await _propertyService.GetByIdVmAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProperty(PropertySaveViewModel vm)
        {
            await _propertyService.UpdateAsync(vm, vm.Id);
            return RedirectToRoute(new { controller = "Property", action = "Index" });
        }

        public async Task<IActionResult> DeleteProperty(int id)
        {
            return View(await _propertyService.GetByIdVmAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProperty(PropertySaveViewModel vm)
        {
            await _propertyService.DeleteAsync(vm.Id);
            return RedirectToRoute(new { controller = "Property", action = "Index" });
        }

    }
}
