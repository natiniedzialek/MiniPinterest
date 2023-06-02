using MiniPinterest.Web.Models.Domain;

namespace MiniPinterest.Web.Repositories
{
    public interface IPinCommentRepository
    {
        public Task<PinComment> AddAsync(PinComment pinComment);
    }
}
