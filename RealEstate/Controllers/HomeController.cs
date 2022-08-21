using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RealEstate.Core.Application.Interfaces.Services;
using RealEstate.Core.Application.Services;
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

        public HomeController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        public async Task<IActionResult> Index()
        {
            List<PropertyViewModel> properties = await _propertyService.GetAllWithInclude();
            return View(properties);
        }

        public IActionResult Details()
        {
            return View();
        }
    }
}
