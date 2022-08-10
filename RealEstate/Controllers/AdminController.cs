using Microsoft.AspNetCore.Mvc;

namespace WebApp.RealEstate.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
