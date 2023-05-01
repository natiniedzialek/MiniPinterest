using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniPinterest.Web.Repositories;
using System.Net;

namespace MiniPinterest.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;            
        }

        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
             string imageURL = await imageRepository.UploadAsync(file);

            if (imageURL == null)
            {
                return Problem("Failed to upload the image", null, (int)HttpStatusCode.InternalServerError);
            }

            return new JsonResult(new { imageURL = imageURL });
        }
    }
}
