using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniPinterest.Web.Data;
using MiniPinterest.Web.Models.Domain;
using MiniPinterest.Web.Models.ViewModels;

namespace MiniPinterest.Web.Controllers
{
    public class BoardsController : Controller
    {
        private readonly MiniPinterestDbContext miniPinterestDbContext;
        public BoardsController(MiniPinterestDbContext miniPinterestDbContext)
        {
            this.miniPinterestDbContext = miniPinterestDbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public IActionResult Add(AddBoardRequest addBoardRequest)
        {
            Board board = new
            (
                addBoardRequest.Name,
                addBoardRequest.Description,
                addBoardRequest.IsPublic
            );

            miniPinterestDbContext.Boards.Add(board);
            miniPinterestDbContext.SaveChanges();

            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public IActionResult List()
        {
            List<Board> boards = miniPinterestDbContext.Boards.ToList();

            return View(boards);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            Board board = miniPinterestDbContext.Boards.Find(id);

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
        public IActionResult Edit(EditBoardRequest editBoardRequest)
        {
            var board = miniPinterestDbContext.Boards.Find(editBoardRequest.Id);

            if(board != null)
            {
                board.Name = editBoardRequest.Name;
                board.Description = editBoardRequest.Description;
                board.IsPublic = editBoardRequest.IsPublic;
                board.Pins = editBoardRequest.Pins;

                miniPinterestDbContext.SaveChanges();
                // success
                return RedirectToAction("Edit", new { id = editBoardRequest.Id });
            }

            // error
            return RedirectToAction("Edit", new { id = editBoardRequest.Id });
        }

        [HttpPost]
        public IActionResult Delete(EditBoardRequest editBoardRequest)
        {
            Board board = miniPinterestDbContext.Boards.Find(editBoardRequest.Id);

            if(board != null)
            {
                miniPinterestDbContext.Boards.Remove(board);
                miniPinterestDbContext.SaveChanges();
                // success
                return RedirectToAction("List");
            }
            // error
            return RedirectToAction("Edit", new { id = editBoardRequest.Id });
        }
    }
}
