using Microsoft.AspNetCore.Mvc;
using Cu_ServicePattern_Movies_01.Models;
using System.Diagnostics;

namespace Cu_ServicePattern_Movies_01.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if(HttpContext.Request.Cookies.ContainsKey("CartItems") && !HttpContext.Session.Keys.Contains("CartRestored"))
            {
                HttpContext.Session.SetInt32("CartRestored", 1);
                return RedirectToAction("ConfirmRestore", "Cart");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}