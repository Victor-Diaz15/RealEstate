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
            return View(await _propertyTypeService.GetAllWithInclude());
        }

        public IActionResult Add()
        {
            return View("Save", new PropertyTypeSaveViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(PropertyTypeSaveViewModel vm)
        {
            if (!ModelState.IsValid) return View("Save", vm);

            await _propertyTypeService.AddAsync(vm);
            return RedirectToRoute(new { controller = "PropertyType", action = "Index" });

        }

        public async Task<IActionResult> Update(int id)
        {
            return View("Save", await _propertyTypeService.GetByIdVmAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(PropertyTypeSaveViewModel vm)
        {
            await _propertyTypeService.UpdateAsync(vm, vm.Id);
            return RedirectToRoute(new { controller = "PropertyType", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _propertyTypeService.GetByIdVmAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(PropertyTypeSaveViewModel vm)
        {
            await _propertyTypeService.DeleteAsync(vm.Id);
            return RedirectToRoute(new { controller = "PropertyType", action = "Index" });
        }

    }
}
