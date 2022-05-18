using Microsoft.AspNetCore.Mvc;

//TODO: Upddate this namespace to match your project name
namespace sp22IdentityTemplate.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
