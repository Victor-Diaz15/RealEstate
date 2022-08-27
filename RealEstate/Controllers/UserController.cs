using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstate.Core.Application.Dtos.Account;
using RealEstate.Core.Application.Enums;
using RealEstate.Core.Application.Helpers;
using RealEstate.Core.Application.Interfaces.Services;
using RealEstate.Core.Application.ViewModels.User;
using System.Threading.Tasks;
using WebApp.RealEstate.Middlewares;

namespace WebApp.RealEstate.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUploadFileService _uploadFileService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, RoleManager<IdentityRole> roleManager, IUploadFileService uploadFileService, IMapper mapper)
        {
            _userService = userService;
            _roleManager = roleManager;
            _uploadFileService = uploadFileService;
            _mapper = mapper;
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            AuthenticationResponse response = await _userService.LoginAsync(vm);

            if (response != null && !response.HasError)
            {
                if (response.TypeUser == (int)Roles.Developer)
                {
                    vm.HasError = true;
                    vm.Error = "Desarrollador no tiene acceso permitido a la web app.";
                    return View(vm);
                }

                HttpContext.Session.Set<AuthenticationResponse>("user", response);

                if (response.Roles.Contains(Roles.Admin.ToString()))
                {
                    return RedirectToRoute(new { controller = "Admin", action = "Index" });
                }
                else if (response.Roles.Contains(Roles.Agent.ToString())) 
                {
                    return RedirectToRoute(new { controller = "Agent", action = "Index" });
                }

                return RedirectToRoute(new { controller = "Client", action = "Index" });
            }
            else
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await _userService.SignOutAsync();
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            string response = await _userService.ConfirmEmailAsync(userId, token);
            return View("ConfirmEmail", response);
        }


        // Register section

        // [ServiceFilter(typeof(AdminAuthorize))]
        public async Task<IActionResult> Register()
        {
            ViewBag.Roles = await _roleManager.Roles.ToListAsync();

            return View(new UserSaveViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserSaveViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = await _roleManager.Roles.ToListAsync();
                return View(vm);
            }

            var origin = Request.Headers["origin"];

            AuthenticationResponse response = await _userService.RegisterAsync(vm, origin);
            
            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                ViewBag.Roles = await _roleManager.Roles.ToListAsync();
                return View(vm);
            }

            if (response != null && response.Id != null)
            {
                string basePath = $"/Images/Users/{response.Id}";
                response.ProfilePicture = _uploadFileService.UploadFile(vm.ProfilePictureFile, basePath);

                UserSaveViewModel userSvm = _mapper.Map<UserSaveViewModel>(response);
                await _userService.UpdateUserAsync(userSvm, response.Id);
            }

            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

    }
}
