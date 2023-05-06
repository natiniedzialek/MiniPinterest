using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MiniPinterest.Web.Models.Domain;
using MiniPinterest.Web.Models.ViewModels;
using MiniPinterest.Web.Repositories;
using System.Net.NetworkInformation;
using System.Security.Claims;

namespace MiniPinterest.Web.Controllers
{
    [Authorize]
    public class BoardsController : Controller
    {
        private readonly IBoardRepository boardRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IAuthorizationService authorizationService;

        public BoardsController(IBoardRepository boardRepository, IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService)
        {
            this.boardRepository = boardRepository;
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
        public async Task<IActionResult> Add(AddBoardRequest addBoardRequest)
        {
            bool authorIdSuccesfullyParsed = Guid.TryParse(httpContextAccessor
                    .HttpContext?
                    .User
                    .FindFirstValue(ClaimTypes.NameIdentifier),
                    out Guid authorGuid);

            if (!authorIdSuccesfullyParsed)
            {
                // error
                return View();
            }

            Board board = new()
            {
                AuthorId = authorGuid,
                Name = addBoardRequest.Name,
                Description = addBoardRequest.Description,
                CreatedAt = DateTime.Now,
                IsPublic = addBoardRequest.IsPublic,
                Pins = new List<Pin>()
            };

            await boardRepository.AddAsync(board);

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

            IEnumerable<Board> boards = await boardRepository.GetByAuthorIdAsync(authorGuid);

            if (!boards.IsNullOrEmpty())
            {
                return View(boards);
            }

            return View(null);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            Board ?board = await boardRepository.GetByIdAsync(id);

            if(board != null)
            {
                var authorizationResult = await authorizationService.AuthorizeAsync(httpContextAccessor
                    .HttpContext?
                    .User, board, "UserIsPinAuthorPolicy");

                if (authorizationResult != null && authorizationResult.Succeeded)
                {
                    EditBoardRequest editBoardRequest = new()
                    {
                        Id = board.Id,
                        AuthorId = board.AuthorId,
                        Name = board.Name,
                        Description = board.Description,
                        CreatedAt = board.CreatedAt,
                        IsPublic = board.IsPublic,
                        Pins = board.Pins
                    };

                    return View(editBoardRequest);
                }
                else
                {
                    return View("AccessDenied");
                }
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditBoardRequest editBoardRequest)
        {
            Board board = new()
            {
                    Id = editBoardRequest.Id,
                    AuthorId = editBoardRequest.AuthorId,
                    Name = editBoardRequest.Name,
                    Description = editBoardRequest.Description,
                    CreatedAt = editBoardRequest.CreatedAt,
                    IsPublic = editBoardRequest.IsPublic,
                    Pins = editBoardRequest.Pins
            };

            Board ?updatedBoard = await boardRepository.UpdateAsync(board);

            if (updatedBoard != null)
            {
                // success
            }
            else
            {
                // error
            }

            return RedirectToAction("Edit", new { id = editBoardRequest.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditBoardRequest editBoardRequest)
        {
            Board ?deletedBoard = await boardRepository.DeleteAsync(editBoardRequest.Id);
            
            if (deletedBoard != null)
            {
                //success
                return RedirectToAction("List");
            }

            // error
            return RedirectToAction("Edit", new { id = editBoardRequest.Id });
        }
    }
}
