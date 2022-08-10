using Microsoft.AspNetCore.Mvc;

namespace WebApp.RealEstate.Controllers
{
    public class AgentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
