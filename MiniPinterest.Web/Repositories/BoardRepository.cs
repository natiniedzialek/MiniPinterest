using Microsoft.EntityFrameworkCore;
using MiniPinterest.Web.Data;
using MiniPinterest.Web.Models.Domain;
using MiniPinterest.Web.Models.ViewModels;
using System.Linq;

namespace MiniPinterest.Web.Repositories
{
    public class BoardRepository : IBoardRepository
    {
        private readonly MiniPinterestDbContext miniPinterestDbContext;

        public BoardRepository(MiniPinterestDbContext miniPinterestDbContext)
        {
            this.miniPinterestDbContext = miniPinterestDbContext;
        }

        public async Task<Board> AddAsync(Board board)
        {
            await miniPinterestDbContext.Boards.AddAsync(board);
            await miniPinterestDbContext.SaveChangesAsync();
            return board;
        }

        public async Task<IEnumerable<Board>> GetAllAsync()
        {
            return await miniPinterestDbContext
                .Boards
                .Include(x => x.Pins)
                .ToListAsync();
        }

        public async Task<Board?> GetByIdAsync(Guid id)
        {
            return await miniPinterestDbContext
                .Boards
                .Include(x => x.Pins)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Board>> GetByAuthorIdAsync(Guid authorId)
        {
            return await miniPinterestDbContext
                .Boards
                .Include(x => x.Pins)
                .Where(x => x.AuthorId == authorId)
                .ToListAsync();
        }

        public async Task<Board?> UpdateAsync(Board board)
        {
            Board? existingBoard = await miniPinterestDbContext
                                            .Boards
                                            .Include(x => x.Pins)
                                            .FirstOrDefaultAsync(x => x.Id == board.Id);

            if (existingBoard != null) 
            {
                existingBoard.Name = board.Name;
                existingBoard.Description = board.Description;
                existingBoard.IsPublic = board.IsPublic;
                existingBoard.Pins = board.Pins;

                await miniPinterestDbContext.SaveChangesAsync();

                return existingBoard;
            }
            return null;
        }

        public async Task<Pin?> AddPinAsync(Guid boardId, Guid pinId)
        {
            // check if board and pin exist in db
            Board? boardFound = await miniPinterestDbContext
                                        .Boards
                                        .Include(x => x.Pins)
                                        .FirstOrDefaultAsync(x => x.Id == boardId);

            Pin? pinFound = await miniPinterestDbContext
                                    .Pins
                                    .FirstOrDefaultAsync(x => x.Id == pinId);

            if (pinFound != null && boardFound != null && !boardFound.Pins.Contains(pinFound))
            {
                boardFound.Pins.Add(pinFound);
                await miniPinterestDbContext.SaveChangesAsync();
                // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!1
                // czy board trzeba dodawać do pina ???
                return pinFound;
            }

            return null;
        }

        public async Task<Board?> DeleteAsync(Guid id)
        {
            Board ?existingBoard = await miniPinterestDbContext.Boards.FindAsync(id);

            if (existingBoard != null)
            {
                miniPinterestDbContext.Boards.Remove(existingBoard);
                await miniPinterestDbContext.SaveChangesAsync();
            }

            return existingBoard;
        }
    }
}
