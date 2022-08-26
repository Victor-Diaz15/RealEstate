using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RealEstate.Core.Application.Interfaces.Services;
using RealEstate.Core.Application.Services;
using RealEstate.Core.Application.ViewModels.Filters;
using RealEstate.Core.Application.ViewModels.Improvement;
using RealEstate.Core.Application.ViewModels.Property;
using RealEstate.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPropertyService _propertyService;
        private readonly IUserService _userService;
        private readonly IPropertyImprovementService _propertyImprovementService;
        private readonly IImprovementService _improvementService;
        private readonly IPropertyTypeService _propertyTypeService;


        public HomeController(IPropertyService propertyService, IPropertyTypeService propertyTypeService, IPropertyImprovementService propertyImprovementService, IImprovementService improvementService,IUserService userService)
        {
            _propertyService = propertyService;
            _propertyTypeService = propertyTypeService;
            _propertyImprovementService = propertyImprovementService;
            _improvementService = improvementService;
            _userService = userService;
        }

        public async Task<IActionResult> Index(FiltersViewModel filters)
        {
            ViewBag.PropertyTypes = await _propertyTypeService.GetAllVmAsync();
            return View(await _propertyService.GetAllWithFilters(filters));
        }

        public async Task<IActionResult> Details(int id)
        {
            var props = await _propertyService.GetAllWithInclude();
            var prop = props.Where(p => p.Id == id).FirstOrDefault();

            var agent = await _userService.GetAllVmAsync();
            ViewBag.Agent = agent.Where(a => a.Id == prop.IdAgent);

            var imps = await _propertyImprovementService.GetAllVmAsync();
            var impsIds = imps.Where(i => i.PropId == prop.Id).ToList();

            List<ImprovementSaveViewModel> improvements = new();

            foreach (var item in impsIds)
            {
                improvements.Add(await _improvementService.GetByIdVmAsync(item.ImprovementId));
            }

            ViewBag.Imps = improvements;
            
            return View(prop);
        }
    }
}
