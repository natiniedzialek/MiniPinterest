using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiniPinterest.Web.Models.Domain;
using MiniPinterest.Web.Models.ViewModels;
using MiniPinterest.Web.Repositories;

namespace MiniPinterest.Web.Controllers
{
    public class PinDetailsController : Controller
    {
        private readonly IPinRepository pinRepository;
        private readonly IPinLikeRepository pinLikeRepository;
        private readonly IPinCommentRepository pinCommentRepository;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public PinDetailsController(IPinRepository pinRepository, IPinLikeRepository pinLikeRepository, IPinCommentRepository pinCommentRepository, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.pinRepository = pinRepository;
            this.pinLikeRepository = pinLikeRepository;
            this.pinCommentRepository = pinCommentRepository;
            this.userManager = userManager;
            this.signInManager = signInManager;
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
                        TotalLikes = totalLikes,
                        Comments = pin.Comments
                    };

                    return View(pinDetailsViewModel);
                }
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Index(PinDetailsViewModel pinDetailsViewModel)
        {
            if (signInManager.IsSignedIn(User))
            {
                var comment = new PinComment
                {
                    PinId = pinDetailsViewModel.Id,
                    UserId = Guid.Parse(userManager.GetUserId(User)),
                    CreatedAt = DateTime.Now,
                    Content = pinDetailsViewModel.NewComment
                };

                await pinCommentRepository.AddAsync(comment);

                return RedirectToAction("Index", new { urlHandle = pinDetailsViewModel.Id.ToString() });
            }

            return View();
        }
    }
}
