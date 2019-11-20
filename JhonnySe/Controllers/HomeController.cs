using Microsoft.AspNetCore.Mvc;

namespace JhonnySe.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Jhonny.se";
            return View();
        }
    }
}