using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Core.Application.Enums;
using RealEstate.Core.Application.Helpers;
using RealEstate.Core.Application.Interfaces.Services;
using RealEstate.Core.Application.Services;
using RealEstate.Core.Application.ViewModels.Filters;
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
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IUploadFileService _uploadFileService;

        public AgentController(IPropertyService propertyService, IUserService userService, IHttpContextAccessor httpContextAccessor, IMapper mapper,
            IUploadFileService uploadFileService)
        {
            _propertyService = propertyService;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _mapper = mapper;
            _uploadFileService = uploadFileService;
        }

        public async Task<IActionResult> Index()
        {
            UserViewModel user = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");

            List<PropertyViewModel> properties = await _propertyService.GetAllWithInclude();
            properties = properties.Where(prop => prop.IdAgent == user.Id).ToList();

            return View(properties);
        }

        public async Task<IActionResult> AgentList()
        {
            List<UserViewModel> users = await _userService.GetAllVmAsync();
            users = users.Where(user => user.TypeUser == (int)Roles.Agent).ToList();


            var listUsers = users.Select(user => new UserViewModel() {
                Id = user.Id,
                CardId = user.CardId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Password = user.Password,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                ProfilePicture = user.ProfilePicture,
                HasError = user.HasError,
                Error = user.Error,
                IsVerified = user.IsVerified,
                PropQty = PropertyQuantity(user.Id) //tu ta claro de que no
            }).ToList();

            ////searching all properties of an agent
            //List<PropertyViewModel> properties = await _propertyService.GetAllWithInclude();
            //foreach (var user in users)
            //{
            //    properties = properties.Where(prop => prop.IdAgent == user.Id).ToList();
            //}
            //ViewBag.PropertyQty = properties.Count;


            listUsers = listUsers.Where(user => user.IsVerified == true).OrderBy(user => user.FirstName).ToList();
            return View(listUsers);
        }

        private int PropertyQuantity(string agentId) 
        {
            var listAgent = _userService.GetAllUsers();
            var agent = listAgent.FirstOrDefault(x => x.Id == agentId);

            var listProps = _propertyService.GetAll();
            var propsFiltered = listProps.Where(x => x.IdAgent == agentId);
            int result = propsFiltered.Count();
            return result;
        }

    public async Task<IActionResult> Agents(FiltersViewModel filters) 
        {
            var agents = await _userService.GetAllAgentsWithFilters(filters);
            return View(agents);
        }

        public async Task<IActionResult> ActiveUser(string id)
        {
            return View("ActiveUser", await _userService.GetUserByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> ActiveUser(UserSaveViewModel vm)
        {
            await _userService.ActivedUserAsync(vm.Id);
            return RedirectToRoute(new { controller = "Agent", action = "AgentList" });
        }

        public async Task<IActionResult> MyProfile()
        {
            UserViewModel user = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            UserSaveViewModel userVm = await _userService.GetUserByIdAsync(user.Id);
            UpdateAgentViewModel agentVm = _mapper.Map<UpdateAgentViewModel>(userVm);
            return View("Update", agentVm);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAgent(UpdateAgentViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Update", vm);
            }
            UserSaveViewModel userVm = await _userService.GetUserByIdAsync(vm.Id);

            string basePath = $"/Images/Users/{userVm.Id}";
            vm.ProfilePicture = _uploadFileService.UploadFile(vm.ProfilePictureFile, basePath, true, userVm.ProfilePicture);


            var response = await _userService.UpdateAgentAsync(vm);

            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View("Update", vm);
            }

            return RedirectToRoute(new { controller = "Agent", action = "Index" });
        }

        public async Task<IActionResult> Delete(string id)
        {
            return View(await _userService.GetUserByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UserSaveViewModel vm)
        {
            await _userService.DeleteUserAsync(vm.Id);
            return RedirectToRoute(new { controller = "Agent", action = "AgentList" });
        }
    }
}
