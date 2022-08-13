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
            return View(await _saleTypeService.GetAllVmAsync());
        }

        public IActionResult AddSaleType()
        {
            return View(new SaleTypeSaveViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddSaleType(SaleTypeSaveViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await _saleTypeService.AddAsync(vm);
            return RedirectToRoute(new { controller = "SaleType", action = "Index" });

        }

        public async Task<IActionResult> UpdateSaleType(int id)
        {
            return View("AddSaleType", await _saleTypeService.GetByIdVmAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSaleType(SaleTypeSaveViewModel vm)
        {
            await _saleTypeService.UpdateAsync(vm, vm.Id);
            return RedirectToRoute(new { controller = "SaleType", action = "Index" });
        }

        public async Task<IActionResult> DeleteSaleType(int id)
        {
            return View(await _saleTypeService.GetByIdVmAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSaleType(SaleTypeSaveViewModel vm)
        {
            await _saleTypeService.DeleteAsync(vm.Id);
            return RedirectToRoute(new { controller = "SaleType", action = "Index" });
        }

    }
}
