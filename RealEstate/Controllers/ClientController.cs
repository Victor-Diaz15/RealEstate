using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Core.Application.Helpers;
using RealEstate.Core.Application.Interfaces.Services;
using RealEstate.Core.Application.ViewModels.Filters;
using RealEstate.Core.Application.ViewModels.Property;
using RealEstate.Core.Application.ViewModels.User;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.RealEstate.Controllers
{
    public class ClientController : Controller
    {
        private readonly IPropertyService _propertyService;
        private readonly IPropertyFavoriteService _propertyFavoriteService;
        private readonly IPropertyTypeService _propertyTypeService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClientController(IPropertyService propertyService, IPropertyFavoriteService propertyFavoriteService, IPropertyTypeService propertyTypeService, IHttpContextAccessor httpContextAccessor)
        {
            _propertyService = propertyService;
            _propertyFavoriteService = propertyFavoriteService;
            _propertyTypeService = propertyTypeService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index(FiltersViewModel filters)
        {
            UserViewModel user = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");

            var filter = await _propertyService.GetAllWithFilters(filters);
            var favs = await _propertyFavoriteService.GetAllVmAsync();

            filter.ForEach(prop =>
                prop.IsFavourite = favs.Any(fav => fav.PropertyId == prop.Id && fav.ClientId == user.Id) ? true : false
            );

            ViewBag.PropertyTypes = await _propertyTypeService.GetAllVmAsync();
            return View(filter);
        }

        public async Task<IActionResult> MyProperties(FiltersViewModel filters)
        {
            UserViewModel user = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");

            var filter = await _propertyService.GetAllWithFilters(filters);
            var favs = await _propertyFavoriteService.GetAllVmAsync();

            filter.ForEach(prop =>
                prop.IsFavourite = favs.Any(fav => fav.PropertyId == prop.Id && fav.ClientId == user.Id) ? true : false
            );

            var favList = filter.Where(x => x.IsFavourite == true).ToList();

            ViewBag.PropertyTypes = await _propertyTypeService.GetAllVmAsync();

            return View(favList);
        }

        [HttpPost]
        public async Task<IActionResult> SetFavourite(int id)
        {
            UserViewModel user = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");

            await _propertyFavoriteService.ChangeFavouritePropStatus(user.Id, id);

            return RedirectToRoute(new { controller = "Client", action = "Index" });
        }
    }
}

            // vitol e intenso, pero lo amamo'