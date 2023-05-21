using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniPinterest.Web.Migrations;
using MiniPinterest.Web.Models.Domain;
using MiniPinterest.Web.Models.ViewModels;
using MiniPinterest.Web.Repositories;
using System.Net.NetworkInformation;
using System.Security.Claims;

namespace MiniPinterest.Web.Controllers
{
    [Authorize]
    public class BoardDetailsController : Controller
    {
        private readonly IBoardRepository boardRepository;
        private readonly IPinRepository pinRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IAuthorizationService authorizationService;

        public BoardDetailsController(IBoardRepository boardRepository, IPinRepository pinRepository, IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService)
        {
            this.boardRepository = boardRepository;
            this.pinRepository = pinRepository;
            this.httpContextAccessor = httpContextAccessor;
            this.authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            bool guidSuccesfullyParsed = Guid.TryParse(urlHandle, out var boardId);

            if (guidSuccesfullyParsed)
            {
                var board = await boardRepository.GetByIdAsync(boardId);

                if (board != null && !board.IsPublic)
                {
                    var authorizationResult = await authorizationService.AuthorizeAsync(httpContextAccessor
                        .HttpContext?
                        .User, board, "UserIsBoardAuthorPolicy");
                    // show the pins saved on board
                    if (authorizationResult == null || !authorizationResult.Succeeded)
                    {
                        return View("AccessDenied");
                    }
                }

                return View(board);
            }
            return View(null);
        }

        [HttpGet]
        public async Task<IActionResult> AddPinToBoard(string urlHandle)
        {
            bool guidSuccesfullyParsed = Guid.TryParse(urlHandle, out var pinId);

            if (guidSuccesfullyParsed)
            {
                Pin? pin = await pinRepository.GetByIdAsync(pinId);
                
                if (pin != null)
                {
                    bool authorIdSuccesfullyParsed = Guid.TryParse(httpContextAccessor
                            .HttpContext?
                            .User
                            .FindFirstValue(ClaimTypes.NameIdentifier),
                            out Guid authorGuid);

                    if (authorIdSuccesfullyParsed)
                    {
                        var boards = await boardRepository.GetByAuthorIdAsync(authorGuid);

                        var model = new AddPinToBoardRequest
                        {
                            PinId = pinId.ToString(),
                            Boards = boards.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
                        };

                        // show author boards
                        return View(model);
                    }
                }
            }

            return RedirectToAction("Index", "PinDetails", new { id = pinId });
        }

        [HttpPost]
        public async Task<IActionResult> AddPinToBoard(AddPinToBoardRequest addPinToBoardRequest)
        {
            await boardRepository.AddPinAsync(Guid.Parse(addPinToBoardRequest.BoardId), Guid.Parse(addPinToBoardRequest.PinId));

            // success
            return RedirectToAction("Index", "PinDetails", new { urlHandle = addPinToBoardRequest.PinId });
        }
    }
}
