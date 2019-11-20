using Microsoft.AspNetCore.Mvc;

namespace JhonnySe.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}