using Microsoft.AspNetCore.Mvc;
using MiniPinterest.Web.Helpers.Abstract;
using MiniPinterest.Web.Models;
using MiniPinterest.Web.Repositories;
using System.Diagnostics;

namespace MiniPinterest.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPinRepository pinRepository;
        private readonly IPinHelper pinHelper;

        public HomeController(ILogger<HomeController> logger, IPinRepository pinRepository, IPinHelper pinHelper)
        {
            _logger = logger;
            this.pinRepository = pinRepository;
            this.pinHelper = pinHelper;
        }

        public async Task<IActionResult> Index()
        {
            var pins = await pinRepository.GetAllAsync();
            pins = pinHelper.GetPublicPins(pins);
            var pinsColumns = pinHelper.DivideIntoColumns(pins, 5);

            return View(pinsColumns);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}