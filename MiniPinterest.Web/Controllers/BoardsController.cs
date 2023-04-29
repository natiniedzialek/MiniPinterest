using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniPinterest.Web.Data;
using MiniPinterest.Web.Models.Domain;
using MiniPinterest.Web.Models.ViewModels;
using MiniPinterest.Web.Repositories;

namespace MiniPinterest.Web.Controllers
{
    public class BoardsController : Controller
    {
        private readonly IBoardRepository boardRepository;
        public BoardsController(IBoardRepository boardRepository)
        {
            this.boardRepository = boardRepository;
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
            Board board = new
            (
                addBoardRequest.Name,
                addBoardRequest.Description,
                addBoardRequest.IsPublic
            );

            await boardRepository.AddAsync(board);

            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            IEnumerable<Board> boards = await boardRepository.GetAllAsync();

            return View(boards);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            Board ?board = await boardRepository.GetByIdAsync(id);

            if(board != null)
            {
                EditBoardRequest editBoardRequest = new()
                {
                    Id = board.Id,
                    UserId = board.UserId,
                    Name = board.Name,
                    Description = board.Description,
                    CreatedAt = board.CreatedAt,
                    IsPublic = board.IsPublic,
                    Pins = board.Pins
                };

                return View(editBoardRequest);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditBoardRequest editBoardRequest)
        {
            Board board = new
                (
                    editBoardRequest.Id,
                    editBoardRequest.UserId,
                    editBoardRequest.Name,
                    editBoardRequest.Description,
                    editBoardRequest.CreatedAt,
                    editBoardRequest.IsPublic,
                    editBoardRequest.Pins
                );

            Board updatedBoard = await boardRepository.UpdateAsync(board);

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
