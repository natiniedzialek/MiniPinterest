using Microsoft.EntityFrameworkCore;
using MiniPinterest.Web.Data;
using MiniPinterest.Web.Models.Domain;
using MiniPinterest.Web.Models.ViewModels;

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
            return await miniPinterestDbContext.Boards.Include(x => x.Pins).ToListAsync();
        }

        public async Task<Board?> GetByIdAsync(Guid id)
        {
            return await miniPinterestDbContext.Boards.FindAsync(id);
        }

        public async Task<Board?> UpdateAsync(Board board)
        {
            Board? existingBoard = await miniPinterestDbContext.Boards.FindAsync(board.Id);

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
