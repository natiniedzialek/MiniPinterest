using MiniPinterest.Web.Models.Domain;

namespace MiniPinterest.Web.Repositories
{
    public interface IBoardRepository
    {
        Task<IEnumerable<Board>> GetAllAsync();
        Task<Board?> GetByIdAsync(Guid id);
        Task<IEnumerable<Board>> GetByAuthorIdAsync(Guid authorGuid);
        Task<Board> AddAsync(Board board);
        Task<Board?> UpdateAsync(Board board);
        Task<Pin?> AddPinAsync(Guid boardId, Guid pinId);
        Task<Pin?> RemovePinAsync(Guid boardId, Guid pinId);
        Task<Board?> DeleteAsync(Guid id);
    }
}
