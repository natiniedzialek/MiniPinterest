using Microsoft.AspNetCore.Mvc;
using MiniPinterest.Web.Models.Domain;
using MiniPinterest.Web.Models.ViewModels;
using MiniPinterest.Web.Repositories;

namespace MiniPinterest.Web.Controllers
{
    public class PinsController : Controller
    {
        private readonly IPinRepository pinRepository;

        public PinsController(IPinRepository pinRepository)
        {
            this.pinRepository = pinRepository;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddPinRequest addPinRequest)
        {
            Pin pin = new()
            {
                Id = Guid.NewGuid(),
                AuthorId = Guid.NewGuid(),
                Title = addPinRequest.Title,
                Description = addPinRequest.Description,
                ImageUrl = addPinRequest.ImageUrl,
                UrlHandle = addPinRequest.UrlHandle,
                CreatedAt = DateTime.Now,
                IsPublic = addPinRequest.IsPublic,
                Boards = new List<Board>()
            };

            await pinRepository.AddAsync(pin);

            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            IEnumerable<Pin>? pins = await pinRepository.GetAllAsync();

            return View(pins);
        }
    }
}
