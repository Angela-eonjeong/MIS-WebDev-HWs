using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Kim_Angela_Eonjeong_HW1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        public HomeController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Passions()
        {
            return View();
        }

        public IActionResult Resume()
        {

            string path = _environment.WebRootPath + "/files/Resume_Angela(Eonjeong)_Kim .pdf";
            var stream = new FileStream(path, FileMode.Open);
            return File(stream, "application/pdf", "Resume_Angela(Eonjeong)_Kim .pdf");
        }
    }


}