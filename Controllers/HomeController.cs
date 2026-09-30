using Microsoft.AspNetCore.Mvc;

namespace trabalhoav1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}