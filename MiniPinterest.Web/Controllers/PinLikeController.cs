using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniPinterest.Web.Models.Domain;
using MiniPinterest.Web.Models.ViewModels;
using MiniPinterest.Web.Repositories;
using System.Security.Claims;

namespace MiniPinterest.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PinLikeController : ControllerBase
    {
        private readonly IPinLikeRepository pinLikeRepository;
        private readonly IPinRepository pinRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        public PinLikeController(IPinLikeRepository pinLikeRepository, IPinRepository pinRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.pinLikeRepository = pinLikeRepository;
            this.pinRepository = pinRepository;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddLike([FromBody] AddLikeRequest addLikeRequest)
        {
            PinLike pinLike = new()
            {
                UserId = addLikeRequest.UserId,
                PinId = addLikeRequest.PinId
            };

            await pinLikeRepository.AddLikeAsync(pinLike);

            return RedirectToAction("Index", "PinDetails", new { urlHandle = addLikeRequest.PinId.ToString() });
        }
    }
}

