using Microsoft.AspNetCore.Mvc;

namespace WebApp.RealEstate.Controllers
{
    public class ClientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
