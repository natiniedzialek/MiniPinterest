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

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            Pin ?pin = await pinRepository.GetByIdAsync(id);

            if (pin != null)
            {
                EditPinRequest editPinRequest = new()
                {
                    Id = pin.Id,
                    AuthorId = pin.AuthorId,
                    Title = pin.Title,
                    Description = pin.Description,
                    ImageUrl =pin.ImageUrl,
                    UrlHandle = pin.UrlHandle,
                    CreatedAt = pin.CreatedAt,
                    IsPublic = pin.IsPublic,
                    Boards = pin.Boards
                };

                return View(editPinRequest);
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
                UrlHandle = editPinRequest.UrlHandle,
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
