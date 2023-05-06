using MiniPinterest.Web.Models.Domain;

namespace MiniPinterest.Web.Repositories
{
    public interface IPinRepository
    {
        Task<IEnumerable<Pin>> GetAllAsync();
        Task<Pin?> GetByIdAsync(Guid id);
        Task<IEnumerable<Pin>> GetByAuthorIdAsync(Guid authorId);
        Task<Pin> AddAsync(Pin pin);
        Task<Pin?> UpdateAsync(Pin pin);
        Task<Pin?> DeleteAsync(Guid id);
    }
}
