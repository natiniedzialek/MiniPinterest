using Microsoft.AspNetCore.Mvc;
using MiniPinterest.Web.Repositories;

namespace MiniPinterest.Web.Controllers
{
    public class BoardDetailsController : Controller
    {
        private readonly IBoardRepository boardRepository;

        public BoardDetailsController(IBoardRepository boardRepository)
        {
            this.boardRepository = boardRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            bool guidSuccesfullyParsed = Guid.TryParse(urlHandle, out var boardId);

            if (guidSuccesfullyParsed)
            {
                var board = await boardRepository.GetByIdAsync(boardId);
                // show the pins saved on board
                return View(board);
            }

            return View(null);
        }
    }
}
