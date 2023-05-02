using Microsoft.AspNetCore.Mvc;
using MiniPinterest.Web.Models;
using MiniPinterest.Web.Repositories;
using System.Diagnostics;

namespace MiniPinterest.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPinRepository pinRepository;

        public HomeController(ILogger<HomeController> logger, IPinRepository pinRepository)
        {
            _logger = logger;
            this.pinRepository = pinRepository;
        }

        public async Task<IActionResult> Index()
        {
            var pins = await pinRepository.GetAllAsync();
            var public_pins = pins.Where(x => x.IsPublic).ToList();

            return View(public_pins);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}