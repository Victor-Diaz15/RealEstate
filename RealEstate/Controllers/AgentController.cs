using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Core.Application.Helpers;
using RealEstate.Core.Application.Interfaces.Services;
using RealEstate.Core.Application.Services;
using RealEstate.Core.Application.ViewModels.Property;
using RealEstate.Core.Application.ViewModels.User;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.RealEstate.Controllers
{
    public class AgentController : Controller
    {
        private readonly IPropertyService _propertyService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AgentController(IPropertyService propertyService, IHttpContextAccessor httpContextAccessor)
        {
            _propertyService = propertyService;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<IActionResult> Index()
        {
            UserViewModel user = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");

            List<PropertyViewModel> properties = await _propertyService.GetAllWithInclude();
            properties = properties.Where(prop => prop.IdAgent == user.Id).ToList();

            return View(properties);
        }
    }
}
