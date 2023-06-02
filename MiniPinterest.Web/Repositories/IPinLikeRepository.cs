using MiniPinterest.Web.Models.Domain;

namespace MiniPinterest.Web.Repositories
{
    public interface IPinLikeRepository
    {
        Task<int> GetTotalLikes(Guid PinId);
        Task<PinLike> AddLikeAsync(PinLike pinLike);
    }
}
