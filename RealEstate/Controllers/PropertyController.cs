using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Core.Application.Dtos.Account;
using RealEstate.Core.Application.Helpers;
using RealEstate.Core.Application.Interfaces.Services;
using RealEstate.Core.Application.ViewModels.Property;
using RealEstate.Core.Application.ViewModels.User;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.RealEstate.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IPropertyService _propertyService;
        private readonly IPropertyTypeService _propertyTypeService;
        private readonly ISaleTypeService _saleTypeService;
        private readonly IImprovementService _improvementService;
        private readonly IMapper _mapper;
        private readonly IUploadFileService _uploadFileService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PropertyController(
            IPropertyService propertyService,
            IPropertyTypeService propertyTypeService,
            ISaleTypeService saleTypeService,
            IImprovementService improvementService,
            IUserService userService,
            IUploadFileService uploadFileService,
            IMapper mapper, 
            IHttpContextAccessor httpContextAccessor)
        {
            _propertyService = propertyService;
            _propertyTypeService = propertyTypeService;
            _saleTypeService = saleTypeService;
            _improvementService = improvementService;
            _httpContextAccessor = httpContextAccessor;
            _uploadFileService = uploadFileService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _propertyService.GetAllVmAsync());
        }

        public async Task<IActionResult> AgentProperties()
        {
            UserViewModel user = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            var props = await _propertyService.GetAllWithInclude();
            List<PropertyViewModel> agentProps = props.Where(prop => prop.IdAgent == user.Id).ToList();

            return View(agentProps);
        }

        public async Task<IActionResult> AddProperty()
        {
            ViewBag.PropTypes = await _propertyTypeService.GetAllVmAsync();
            ViewBag.SaleTypes = await _saleTypeService.GetAllVmAsync();
            ViewBag.Improvements = await _improvementService.GetAllVmAsync();

            return View("Save", new PropertySaveViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddProperty(PropertySaveViewModel vm)
        {
            if (!ModelState.IsValid) return View("Save", vm);

            UserViewModel user = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            vm.IdAgent = user.Id;
            vm.AgentName = $"{user.FirstName} {user.LastName}";

            PropertySaveViewModel prop = await _propertyService.AddAsync(vm);

            if (prop != null && prop.Id != 0) 
            {
                string basePath = $"/Images/Property/{prop.Id}";
                prop.PropertyImgUrl1 = _uploadFileService.UploadFile(vm.PropertyImg1, basePath);
                prop.PropertyImgUrl2 = _uploadFileService.UploadFile(vm.PropertyImg2, basePath);
                prop.PropertyImgUrl3 = _uploadFileService.UploadFile(vm.PropertyImg3, basePath);
                prop.PropertyImgUrl4 = _uploadFileService.UploadFile(vm.PropertyImg4, basePath);

                await _propertyService.UpdateAsync(prop, prop.Id);
            }

            return RedirectToRoute(new { controller = "Property", action = "AgentProperties" });
        }

        public async Task<IActionResult> UpdateProperty(int id)
        {
            ViewBag.PropTypes = await _propertyTypeService.GetAllVmAsync();
            ViewBag.SaleTypes = await _saleTypeService.GetAllVmAsync();
            ViewBag.Improvements = await _improvementService.GetAllVmAsync();

            return View("Save", await _propertyService.GetByIdVmAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProperty(PropertySaveViewModel vm)
        {
            await _propertyService.UpdateAsync(vm, vm.Id);
            return RedirectToRoute(new { controller = "Property", action = "AgentProperties" });
        }

        public async Task<IActionResult> DeleteProperty(int id)
        {
            return View(await _propertyService.GetByIdVmAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProperty(PropertySaveViewModel vm)
        {
            await _propertyService.DeleteAsync(vm.Id);
            return RedirectToRoute(new { controller = "Property", action = "AgentProperties" });
        }

    }
}
