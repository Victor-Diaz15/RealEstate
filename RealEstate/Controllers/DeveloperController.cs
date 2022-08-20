using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Core.Application.Dtos.Account;
using RealEstate.Core.Application.Enums;
using RealEstate.Core.Application.Helpers;
using RealEstate.Core.Application.Interfaces.Services;
using RealEstate.Core.Application.ViewModels.User;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.RealEstate.Controllers
{
    public class DeveloperController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DeveloperController(IUserService userService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            List<UserViewModel> users = await _userService.GetAllVmAsync();
            users = users.Where(user => user.TypeUser == (int)Roles.Developer).ToList();

            return View(users);
        }

        public IActionResult Add()
        {
            return View("Save", new ManagerSaveViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(ManagerSaveViewModel vm)
        {
            if (!ModelState.IsValid) return View("Save", vm);

            RegisterManagerResponse response = await _userService.RegisterDevAsync(vm);
            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View("Save", vm);
            }

            return RedirectToRoute(new { controller = "Developer", action = "Index" });

        }

        public async Task<IActionResult> Update(string id)
        {
            var user = HttpContext.Session.Get<AuthenticationResponse>("user");

            if (id == user.Id)
            {
                return RedirectToRoute(new { controller = "Developer", action = "Index" });
            }
            UserSaveViewModel vm = await _userService.GetUserByIdAsync(id);
            ManagerSaveViewModel managerSaveVm = _mapper.Map<ManagerSaveViewModel>(vm);
            return View("Save", managerSaveVm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ManagerSaveViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Save", vm);
            }
            UserSaveViewModel userSaveVm = _mapper.Map<UserSaveViewModel>(vm);
            await _userService.UpdateUserAsync(userSaveVm, userSaveVm.Id);

            return RedirectToRoute(new { controller = "Developer", action = "Index" });
        }

        public async Task<IActionResult> ActiveUser(string id)
        {
            return View("ActiveUser", await _userService.GetUserByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> ActiveUser(UserSaveViewModel vm)
        {
            await _userService.ActivedUserAsync(vm.Id);
            return RedirectToRoute(new { controller = "Developer", action = "Index" });
        }
    }
}
