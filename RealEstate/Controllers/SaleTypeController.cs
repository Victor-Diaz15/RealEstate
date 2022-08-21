using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Core.Application.Interfaces.Services;
using RealEstate.Core.Application.ViewModels.SaleType;
using System.Threading.Tasks;

namespace WebApp.RealEstate.Controllers
{
    public class SaleTypeController : Controller
    {
        private readonly ISaleTypeService _saleTypeService;
        private readonly IMapper _mapper;
        public SaleTypeController(ISaleTypeService saleTypeService, IMapper mapper)
        {
            _saleTypeService = saleTypeService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _saleTypeService.GetAllWithInclude());
        }

        public IActionResult Add()
        {
            return View("Save", new SaleTypeSaveViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(SaleTypeSaveViewModel vm)
        {
            if (!ModelState.IsValid) return View("Save", vm);

            await _saleTypeService.AddAsync(vm);
            return RedirectToRoute(new { controller = "SaleType", action = "Index" });

        }

        public async Task<IActionResult> Update(int id)
        {
            return View("Save", await _saleTypeService.GetByIdVmAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(SaleTypeSaveViewModel vm)
        {
            await _saleTypeService.UpdateAsync(vm, vm.Id);
            return RedirectToRoute(new { controller = "SaleType", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _saleTypeService.GetByIdVmAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(SaleTypeSaveViewModel vm)
        {
            await _saleTypeService.DeleteAsync(vm.Id);
            return RedirectToRoute(new { controller = "SaleType", action = "Index" });
        }

    }
}
