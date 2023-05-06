using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MiniPinterest.Web.Models.Domain;
using MiniPinterest.Web.Models.ViewModels;
using MiniPinterest.Web.Repositories;
using System.Security.Claims;

namespace MiniPinterest.Web.Controllers
{
    [Authorize]
    public class PinsController : Controller
    {
        private readonly IPinRepository pinRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IAuthorizationService authorizationService;

        public PinsController(IPinRepository pinRepository, IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService)
        {
            this.pinRepository = pinRepository;
            this.httpContextAccessor = httpContextAccessor;
            this.authorizationService = authorizationService;
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
            bool authorIdSuccesfullyParsed = Guid.TryParse(httpContextAccessor
                    .HttpContext?
                    .User
                    .FindFirstValue(ClaimTypes.NameIdentifier),
                    out Guid authorGuid);

            if(!authorIdSuccesfullyParsed)
            {
                // error
                return View();
            }

            Pin pin = new()
            {
                AuthorId = authorGuid,
                Title = addPinRequest.Title,
                Description = addPinRequest.Description,
                ImageUrl = addPinRequest.ImageUrl,
                CreatedAt = DateTime.Now,
                IsPublic = addPinRequest.IsPublic,
                Boards = new List<Board>()
            };

            await pinRepository.AddAsync(pin);

            // success
            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            bool authorIdSuccesfullyParsed = Guid.TryParse(httpContextAccessor
                    .HttpContext?
                    .User
                    .FindFirstValue(ClaimTypes.NameIdentifier),
                    out Guid authorGuid);

            if (!authorIdSuccesfullyParsed)
            {
                // error
                return View(null);
            }

            IEnumerable<Pin>? pins = await pinRepository.GetByAuthorIdAsync(authorGuid);

            if (!pins.IsNullOrEmpty())
            {
                return View(pins);
            }

            return View(null);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            Pin ?pin = await pinRepository.GetByIdAsync(id);

            if (pin != null)
            {
                var authorizationResult = await authorizationService.AuthorizeAsync(httpContextAccessor
                    .HttpContext?
                    .User, pin, "UserIsPinAuthorPolicy");

                if (authorizationResult != null && authorizationResult.Succeeded)
                {
                    EditPinRequest editPinRequest = new()
                    {
                        Id = pin.Id,
                        AuthorId = pin.AuthorId,
                        Title = pin.Title,
                        Description = pin.Description,
                        ImageUrl = pin.ImageUrl,
                        CreatedAt = pin.CreatedAt,
                        IsPublic = pin.IsPublic,
                        Boards = pin.Boards
                    };

                    return View(editPinRequest);
                }
                else 
                {
                    return View("AccessDenied");
                }
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditPinRequest editPinRequest)
        {
            Pin pin = new()
            {
                Id = editPinRequest.Id,
                AuthorId = editPinRequest.AuthorId,
                Title = editPinRequest.Title,
                Description = editPinRequest.Description,
                ImageUrl = editPinRequest.ImageUrl,
                CreatedAt = editPinRequest.CreatedAt,
                IsPublic = editPinRequest.IsPublic,
                Boards = editPinRequest.Boards
            };

            Pin ?updatedPin = await pinRepository.UpdateAsync(pin);

            if(updatedPin != null)
            {
                // success
            }
            else
            {
                // error
            }

            return RedirectToAction("Edit", new { id = editPinRequest.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditPinRequest editPinRequest)
        {
            Pin? deletedPin = await pinRepository.DeleteAsync(editPinRequest.Id);

            if (deletedPin != null)
            {
                //success
                return RedirectToAction("List");
            }
            // error
            return RedirectToAction("Edit", new { id = editPinRequest.Id });
        }
    }
}
