using MiniPinterest.Web.Models.Domain;

namespace MiniPinterest.Web.Repositories
{
    public interface IBoardRepository
    {
        Task<IEnumerable<Board>> GetAllAsync();
        Task<Board?> GetByIdAsync(Guid id);
        Task<Board> AddAsync(Board board);
        Task<Board?> UpdateAsync(Board board);
        Task<Board?> DeleteAsync(Guid id);
        Task<IEnumerable<Board>> GetByAuthorIdAsync(Guid authorGuid);
    }
}
