using Microsoft.AspNetCore.Mvc;
using MiniPinterest.Web.Repositories;

namespace MiniPinterest.Web.Controllers
{
    public class PinDetailsController : Controller
    {
        private readonly IPinRepository pinRepository;

        public PinDetailsController(IPinRepository pinRepository)
        {
            this.pinRepository = pinRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            bool guidSuccesfullyParsed = Guid.TryParse(urlHandle, out var pinId);

            if(guidSuccesfullyParsed)
            {
                var pin = await pinRepository.GetByIdAsync(pinId);
                return View(pin);
            }

            return View(null);
        }
    }
}
