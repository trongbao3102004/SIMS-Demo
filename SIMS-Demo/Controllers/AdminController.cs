using Microsoft.AspNetCore.Mvc;

namespace SIMS_Demo.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
