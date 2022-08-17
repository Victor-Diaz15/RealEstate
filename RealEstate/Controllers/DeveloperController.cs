using Microsoft.AspNetCore.Mvc;
using RealEstate.Core.Application.Enums;
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
        public DeveloperController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            List<UserViewModel> users = await _userService.GetAllVmAsync();
            users = users.Where(user => user.TypeUser == (int)Roles.Developer).ToList();

            return View(users);
        }

        public IActionResult RegisterDev() 
        {
            return View(new ManagerSaveViewModel());
        }

        //[HttpPost]
        //public async Task<IActionResult> RegisterDev(ManagerSaveViewModel vm)
        //{

        //    return View();
        //}
    }
}
