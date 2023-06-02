using Microsoft.AspNetCore.Mvc;
using MiniPinterest.Web.Models.ViewModels;
using MiniPinterest.Web.Repositories;

namespace MiniPinterest.Web.Controllers
{
    public class PinDetailsController : Controller
    {
        private readonly IPinRepository pinRepository;
        private readonly IPinLikeRepository pinLikeRepository;

        public PinDetailsController(IPinRepository pinRepository, IPinLikeRepository pinLikeRepository)
        {
            this.pinRepository = pinRepository;
            this.pinLikeRepository = pinLikeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            bool guidSuccesfullyParsed = Guid.TryParse(urlHandle, out var pinId);

            if(guidSuccesfullyParsed)
            {
                var pin = await pinRepository.GetByIdAsync(pinId); 

                if(pin != null)
                {
                    int totalLikes = await pinLikeRepository.GetTotalLikes(pin.Id);

                    var pinDetailsViewModel = new PinDetailsViewModel
                    {
                        Id = pin.Id,
                        AuthorId = pin.AuthorId,
                        Title = pin.Title,
                        Description = pin.Description,
                        ImageUrl = pin.ImageUrl,
                        CreatedAt = pin.CreatedAt,
                        IsPublic = pin.IsPublic,
                        Boards = pin.Boards,
                        TotalLikes = totalLikes
                    };

                    return View(pinDetailsViewModel);
                }
            }

            return View(null);
        }
    }
}
